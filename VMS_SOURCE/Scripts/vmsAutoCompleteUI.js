/***************************************************
'Copyright	     : VMS, MCC, KOLKATA
'JavaScript Name : vmsAutoCompleteUI.js
'Purpose         : Presentation only helper for the asp:AutoCompleteExtender
'                  suggestion list. It flips the list above the textbox when
'                  there is not enough room below it and keeps it inside the
'                  viewport horizontally.
'                  It never touches the service method, the selection logic or
'                  any of the OnClient* handlers - if anything here fails the
'                  extender simply keeps its default positioning.
'***************************************************/

(function () {
    'use strict';

    var LIST_SELECTOR = '.vmsAutoComplete, .autoCompleteFlyout, .raw-material-autocomplete';
    var ABOVE_CLASS = 'vmsAutoCompleteAbove';
    var GAP = 4;    /* space kept between the textbox and the list */
    var EDGE = 8;   /* space kept between the list and the viewport edge */

    var suppress = 0;
    var watched = [];

    function hasClass(el, name) {
        return (' ' + (el.className || '') + ' ').indexOf(' ' + name + ' ') > -1;
    }

    function addClass(el, name) {
        if (!hasClass(el, name)) {
            el.className = (el.className ? el.className + ' ' : '') + name;
        }
    }

    function removeClass(el, name) {
        if (hasClass(el, name)) {
            el.className = (' ' + el.className + ' ').split(' ' + name + ' ').join(' ')
                .replace(/^\s+|\s+$/g, '');
        }
    }

    function isVisible(el) {
        return !!el && el.offsetHeight > 0 && el.offsetWidth > 0;
    }

    /* While the list is open the caret is still in the textbox it belongs to. */
    function currentTextBox() {
        var el = document.activeElement;
        if (!el || !el.tagName) {
            return null;
        }
        var tag = el.tagName.toLowerCase();
        return (tag === 'input' || tag === 'textarea') ? el : null;
    }

    function styleValue(el, prop, fallback) {
        var value = parseFloat(el.style[prop]);
        return isNaN(value) ? fallback : value;
    }

    function place(list) {
        if (suppress > 0 || !isVisible(list)) {
            return;
        }

        var input = currentTextBox();
        if (!input) {
            return;
        }

        var box = input.getBoundingClientRect();
        var rect = list.getBoundingClientRect();
        var vh = window.innerHeight || document.documentElement.clientHeight;
        var vw = window.innerWidth || document.documentElement.clientWidth;
        var height = rect.height || list.offsetHeight;

        var spaceBelow = vh - box.bottom;
        var spaceAbove = box.top;
        var flip = (spaceBelow < height + GAP) && (spaceAbove > spaceBelow);

        /* Vertical - work in deltas so the offset parent does not matter. */
        var dy = 0;
        if (flip) {
            var wantedTop = box.top - height - GAP;
            if (wantedTop < EDGE) {
                wantedTop = EDGE;
            }
            dy = wantedTop - rect.top;
        }

        /* Horizontal - keep the whole list on screen. */
        var dx = 0;
        if (rect.right + dx > vw - EDGE) {
            dx = (vw - EDGE) - rect.right;
        }
        if (rect.left + dx < EDGE) {
            dx = EDGE - rect.left;
        }

        suppress++;
        try {
            if (flip) {
                if (Math.abs(dy) >= 1) {
                    list.style.top = Math.round(styleValue(list, 'top', list.offsetTop) + dy) + 'px';
                }
                addClass(list, ABOVE_CLASS);
            } else {
                removeClass(list, ABOVE_CLASS);
            }

            if (Math.abs(dx) >= 1) {
                list.style.left = Math.round(styleValue(list, 'left', list.offsetLeft) + dx) + 'px';
            }
        } catch (e) {
            /* positioning is cosmetic only - never break the page */
        }
        setTimeout(function () { suppress--; }, 0);
    }

    /* The list is measured twice: right away, and once more after the optional
       AjaxControlToolkit show animation (0.4s) has settled. */
    function schedule(list) {
        setTimeout(function () { place(list); }, 0);
        setTimeout(function () { place(list); }, 500);
    }

    function watch(list) {
        if (!list || list.vmsAcWatched) {
            return;
        }
        list.vmsAcWatched = true;
        watched.push(list);

        if (window.MutationObserver) {
            new MutationObserver(function () { schedule(list); })
                .observe(list, { attributes: true, attributeFilter: ['style'] });
        }
        schedule(list);
    }

    function scan(root) {
        if (!root || !root.querySelectorAll) {
            return;
        }
        var lists = root.querySelectorAll(LIST_SELECTOR);
        for (var i = 0; i < lists.length; i++) {
            watch(lists[i]);
        }
    }

    function matches(el) {
        if (!el || el.nodeType !== 1) {
            return false;
        }
        var fn = el.matches || el.msMatchesSelector || el.webkitMatchesSelector;
        try {
            return !!fn && fn.call(el, LIST_SELECTOR);
        } catch (e) {
            return false;
        }
    }

    function init() {
        if (!document.body) {
            return;
        }
        scan(document);

        /* The completion list is created on the first search, so watch for it. */
        if (window.MutationObserver) {
            new MutationObserver(function (records) {
                for (var i = 0; i < records.length; i++) {
                    var added = records[i].addedNodes;
                    for (var j = 0; j < added.length; j++) {
                        var node = added[j];
                        if (matches(node)) {
                            watch(node);
                        } else if (node.nodeType === 1) {
                            scan(node);
                        }
                    }
                }
            }).observe(document.body, { childList: true, subtree: true });
        }

        function reposition() {
            scan(document);
            for (var i = 0; i < watched.length; i++) {
                place(watched[i]);
            }
        }

        if (window.addEventListener) {
            window.addEventListener('resize', reposition, false);
            window.addEventListener('orientationchange', reposition, false);
        }
    }

    if (document.readyState === 'complete' || document.readyState === 'interactive') {
        setTimeout(init, 0);
    } else if (document.addEventListener) {
        document.addEventListener('DOMContentLoaded', init, false);
    } else {
        window.attachEvent('onload', init);
    }
})();
