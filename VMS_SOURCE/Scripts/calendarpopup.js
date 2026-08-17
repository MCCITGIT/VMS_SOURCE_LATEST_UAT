/*
    calendarpopup.js - VMS date picker

    Drop-in replacement for the legacy Matt Kruse CalendarPopup / PopupWindow
    calendar. The public API is unchanged, so existing markup keeps working:

        var cal1 = new CalendarPopup();
        <a href="javascript:cal1.select(document.forms[0].txtChallanDate,'ChallanDate','dd/MM/yyyy');">
            <img src="images/date_icon.gif" id="ChallanDate" alt="Calender" />
        </a>

    What the old version got wrong, and what changed here:

    1.  new CalendarPopup() built a PopupWindow of type "WINDOW", so the calendar
        was drawn into a real window.open() popup. Modern browsers block that.
        The calendar is now an absolutely positioned DIV appended to <body>.

    2.  document.forms[0].txtChallanDate is undefined on every page that uses
        MasterPage.master, because ASP.NET renders the control as
        name="ctl00$ContentPlaceHolder1$txtChallanDate". The old select() then
        threw on inputobj.type and nothing happened at all. Two fixes:
          - installFormAliases() aliases the short name back onto the <form>,
            so document.forms[0].txtChallanDate resolves again (this also fixes
            the other non-calendar scripts that use the same pattern);
          - select() falls back to locating the field from the anchor/icon when
            the passed object is still missing.

    3.  getAnchorPosition() returned offsetLeft/offsetTop (relative to the
        offset parent, not the page) and PopupWindow assigned style.left with no
        "px" unit, which standards mode ignores. Positioning now uses
        getBoundingClientRect() with px units, flips above the field when there
        is no room below, and is clamped to the viewport.

    4.  Anchors are auto-bound on load and after every UpdatePanel async
        postback, so the calendar survives partial page updates. Clicking the
        text box itself also opens the picker.

    Requires no other script. date.js is still used elsewhere for validation but
    is not needed by this file.
*/

(function (window, document) {
    'use strict';

    var MONTH_NAMES = ['January', 'February', 'March', 'April', 'May', 'June',
        'July', 'August', 'September', 'October', 'November', 'December'];
    var MONTH_ABBREVIATIONS = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var DAY_HEADERS = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
    var DEFAULT_FORMAT = 'dd/MM/yyyy';
    var YEARS_BACK = 100;
    var YEARS_FORWARD = 20;

    /* ------------------------------------------------------------------ */
    /* Small helpers                                                       */
    /* ------------------------------------------------------------------ */

    function lz(n) { return (n < 10 ? '0' : '') + n; }

    function trim(s) { return String(s == null ? '' : s).replace(/^\s+|\s+$/g, ''); }

    function daysInMonth(month, year) {
        return new Date(year, month, 0).getDate();
    }

    function sameDay(a, b) {
        return a && b && a.getDate() === b.getDate() &&
            a.getMonth() === b.getMonth() && a.getFullYear() === b.getFullYear();
    }

    function isTextField(el) {
        if (!el || el.nodeType !== 1 || !el.tagName) { return false; }
        var tag = el.tagName.toLowerCase();
        if (tag === 'textarea') { return true; }
        if (tag !== 'input') { return false; }
        var type = (el.getAttribute('type') || 'text').toLowerCase();
        return type === 'text' || type === 'hidden' || type === 'search' || type === 'date';
    }

    function addClass(el, name) {
        if (el && (' ' + el.className + ' ').indexOf(' ' + name + ' ') < 0) {
            el.className = trim(el.className + ' ' + name);
        }
    }

    function on(el, type, fn) {
        if (el.addEventListener) { el.addEventListener(type, fn, false); }
        else { el.attachEvent('on' + type, fn); }
    }

    function stop(e) {
        e = e || window.event;
        if (e.preventDefault) { e.preventDefault(); } else { e.returnValue = false; }
        if (e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }
    }

    function fireEvent(el, type) {
        var evt;
        try {
            if (typeof window.Event === 'function') {
                evt = new window.Event(type, { bubbles: true, cancelable: true });
            } else {
                evt = document.createEvent('HTMLEvents');
                evt.initEvent(type, true, true);
            }
            el.dispatchEvent(evt);
        } catch (err) {
            if (el.fireEvent) { try { el.fireEvent('on' + type); } catch (err2) { } }
        }
    }

    /* ------------------------------------------------------------------ */
    /* Date parsing / formatting                                           */
    /* ------------------------------------------------------------------ */

    // Splits a format string such as "dd/MM/yyyy" into its runs of like chars.
    function tokenize(format) {
        var tokens = [], i = 0;
        format = String(format || DEFAULT_FORMAT);
        while (i < format.length) {
            var c = format.charAt(i), token = '';
            while (i < format.length && format.charAt(i) === c) { token += format.charAt(i++); }
            tokens.push(token);
        }
        return tokens;
    }

    // Same token vocabulary as date.js: MM = month, mm = minutes.
    function formatDateValue(date, format) {
        var values = {
            yyyy: String(date.getFullYear()),
            yy: String(date.getFullYear()).substring(2),
            y: String(date.getFullYear()),
            MMM: MONTH_ABBREVIATIONS[date.getMonth()],
            MM: lz(date.getMonth() + 1),
            M: String(date.getMonth() + 1),
            dd: lz(date.getDate()),
            d: String(date.getDate()),
            HH: lz(date.getHours()),
            H: String(date.getHours()),
            mm: lz(date.getMinutes()),
            m: String(date.getMinutes()),
            ss: lz(date.getSeconds()),
            s: String(date.getSeconds())
        };
        var tokens = tokenize(format), out = '';
        for (var i = 0; i < tokens.length; i++) {
            out += (values[tokens[i]] != null) ? values[tokens[i]] : tokens[i];
        }
        return out;
    }

    function monthFromName(name) {
        name = trim(name).toLowerCase();
        for (var i = 0; i < 12; i++) {
            if (MONTH_NAMES[i].toLowerCase() === name ||
                MONTH_ABBREVIATIONS[i].toLowerCase() === name) {
                return i + 1;
            }
        }
        return 0;
    }

    // Lenient parser: reads the numeric/word groups of the value in the order
    // the day/month/year tokens appear in the format.
    function parseDateValue(value, format) {
        value = trim(value);
        if (!value) { return null; }
        var tokens = tokenize(format), fields = [];
        for (var i = 0; i < tokens.length; i++) {
            var head = tokens[i].charAt(0);
            if (head === 'd' || head === 'M' || head === 'y') { fields.push(tokens[i]); }
        }
        var parts = value.match(/\d+|[A-Za-z]+/g);
        if (!parts || !fields.length || parts.length < fields.length) { return null; }

        var day = 1, month = 1, year = null;
        for (var f = 0; f < fields.length; f++) {
            var token = fields[f], part = parts[f], num;
            if (token.charAt(0) === 'd') {
                if (!/^\d+$/.test(part)) { return null; }
                day = parseInt(part, 10);
            } else if (token.charAt(0) === 'M') {
                if (/^\d+$/.test(part)) { month = parseInt(part, 10); }
                else { month = monthFromName(part); if (!month) { return null; } }
            } else {
                if (!/^\d+$/.test(part)) { return null; }
                num = parseInt(part, 10);
                if (token.length <= 2 && num < 100) { num += (num < 50 ? 2000 : 1900); }
                year = num;
            }
        }
        if (year == null || isNaN(day) || isNaN(month)) { return null; }
        if (month < 1 || month > 12) { return null; }
        if (day < 1 || day > daysInMonth(month, year)) { return null; }
        return new Date(year, month - 1, day, 0, 0, 0, 0);
    }

    /* ------------------------------------------------------------------ */
    /* Stylesheet                                                          */
    /* ------------------------------------------------------------------ */

    var STYLE_ID = 'vmsCalendarPopupStyles';

    var CSS = [
        '.vmsCal{position:absolute;z-index:100000;display:none;width:250px;',
        'background:#fff;border:1px solid #c8ced3;border-radius:4px;',
        'box-shadow:0 4px 16px rgba(0,0,0,.18);',
        'font-family:"Segoe UI",Arial,Helvetica,sans-serif;font-size:13px;color:#23282c;',
        'padding:8px;box-sizing:border-box;-webkit-user-select:none;user-select:none;}',
        '.vmsCal *{box-sizing:border-box;}',
        '.vmsCal.vmsCalOpen{display:block;}',
        '.vmsCalHd{display:table;width:100%;table-layout:fixed;margin-bottom:6px;}',
        '.vmsCalHd>*{display:table-cell;vertical-align:middle;}',
        '.vmsCalNav{width:26px;height:26px;padding:0;line-height:1;cursor:pointer;',
        'border:1px solid #c8ced3;border-radius:3px;background:#f5f6f7;color:#23282c;',
        'font-size:15px;font-weight:bold;text-align:center;}',
        '.vmsCalNav:hover{background:#0198cf;border-color:#0198cf;color:#fff;}',
        '.vmsCalSel{width:100%;height:26px;padding:1px 2px;cursor:pointer;',
        'border:1px solid #c8ced3;border-radius:3px;background:#fff;color:#23282c;font-size:13px;}',
        '.vmsCalSelWrapM{padding:0 3px;}',
        '.vmsCalSelWrapY{padding:0 3px;width:74px;}',
        '.vmsCalGrid{width:100%;border-collapse:collapse;table-layout:fixed;}',
        '.vmsCalGrid th{padding:4px 0;font-size:11px;font-weight:600;color:#73818f;text-align:center;}',
        '.vmsCalGrid th.vmsCalSun{color:#c62828;}',
        '.vmsCalGrid td{padding:1px;text-align:center;}',
        '.vmsCalDay{display:block;width:100%;padding:5px 0;cursor:pointer;',
        'border:1px solid transparent;border-radius:3px;background:transparent;',
        'color:#23282c;font-size:13px;line-height:1.1;text-align:center;}',
        '.vmsCalDay:hover{background:#e6f4fa;border-color:#0198cf;}',
        '.vmsCalDay.vmsCalOther{color:#b0b6bb;}',
        '.vmsCalDay.vmsCalToday{border-color:#0198cf;font-weight:bold;}',
        '.vmsCalDay.vmsCalSelected{background:#0198cf;border-color:#0198cf;color:#fff;font-weight:bold;}',
        '.vmsCalDay.vmsCalDisabled,.vmsCalCell.vmsCalDisabled{color:#c8ced3;cursor:default;',
        'background:transparent;border-color:transparent;}',
        '.vmsCalCell{display:block;width:100%;padding:8px 0;cursor:pointer;',
        'border:1px solid transparent;border-radius:3px;background:transparent;color:#23282c;font-size:13px;}',
        '.vmsCalCell:hover{background:#e6f4fa;border-color:#0198cf;}',
        '.vmsCalCell.vmsCalSelected{background:#0198cf;border-color:#0198cf;color:#fff;font-weight:bold;}',
        '.vmsCalFt{margin-top:6px;padding-top:6px;border-top:1px solid #eceff1;text-align:center;}',
        '.vmsCalLink{display:inline-block;margin:0 4px;padding:3px 10px;cursor:pointer;',
        'border:1px solid #c8ced3;border-radius:3px;background:#f5f6f7;color:#23282c;font-size:12px;}',
        '.vmsCalLink:hover{background:#0198cf;border-color:#0198cf;color:#fff;}'
    ].join('');

    function injectStyles() {
        if (document.getElementById(STYLE_ID)) { return; }
        var head = document.getElementsByTagName('head')[0] || document.documentElement;
        var style = document.createElement('style');
        style.id = STYLE_ID;
        style.type = 'text/css';
        if (style.styleSheet) { style.styleSheet.cssText = CSS; }
        else { style.appendChild(document.createTextNode(CSS)); }
        head.appendChild(style);
    }

    /* ------------------------------------------------------------------ */
    /* Field lookup                                                        */
    /* ------------------------------------------------------------------ */

    // ASP.NET renders controls as ctl00$ContentPlaceHolder1$txtChallanDate.
    // Alias the trailing segment back onto the form so legacy expressions such
    // as document.forms[0].txtChallanDate keep resolving.
    function installFormAliases() {
        for (var f = 0; f < document.forms.length; f++) {
            var form = document.forms[f];
            var elements = form.elements;
            for (var i = 0; i < elements.length; i++) {
                var el = elements[i];
                var names = [];
                if (el.name && el.name.indexOf('$') > -1) {
                    names.push(el.name.substring(el.name.lastIndexOf('$') + 1));
                }
                if (el.id && el.id.indexOf('_') > -1) {
                    names.push(el.id.substring(el.id.lastIndexOf('_') + 1));
                }
                for (var n = 0; n < names.length; n++) {
                    var shortName = names[n];
                    // The `in` test keeps real form members (submit, action,
                    // elements, ...) and already-resolvable names untouched.
                    if (shortName && !(shortName in form)) {
                        try { form[shortName] = el; } catch (err) { }
                    }
                }
            }
        }
    }

    // Finds the text box that belongs to a calendar icon/anchor by walking up
    // from it and preferring the nearest field that precedes it.
    function findFieldNear(el) {
        var node = el, depth = 0;
        while (node && depth < 6) {
            var candidates = node.getElementsByTagName ? node.getElementsByTagName('input') : null;
            if (candidates && candidates.length) {
                var before = null, after = null;
                for (var i = 0; i < candidates.length; i++) {
                    var input = candidates[i];
                    if (!isTextField(input) || input.type === 'hidden') { continue; }
                    if (input.compareDocumentPosition) {
                        // 4 = DOCUMENT_POSITION_FOLLOWING (input comes after el)
                        if (el.compareDocumentPosition(input) & 4) {
                            if (!after) { after = input; }
                        } else {
                            before = input;
                        }
                    } else if (!before) {
                        before = input;
                    }
                }
                if (before) { return before; }
                if (after) { return after; }
            }
            node = node.parentNode;
            depth++;
        }
        return null;
    }

    // Last resort: match an ASP.NET mangled id/name by its trailing segment.
    function findFieldByShortName(shortName) {
        if (!shortName) { return null; }
        var el = document.getElementById(shortName);
        if (isTextField(el)) { return el; }
        var inputs = document.getElementsByTagName('input');
        for (var i = 0; i < inputs.length; i++) {
            var input = inputs[i];
            if (!isTextField(input)) { continue; }
            var id = input.id || '', name = input.name || '';
            if (id.substring(id.lastIndexOf('_') + 1) === shortName ||
                name.substring(name.lastIndexOf('$') + 1) === shortName) {
                return input;
            }
        }
        return null;
    }

    function resolveAnchor(anchorname) {
        if (!anchorname) { return null; }
        if (anchorname.nodeType === 1) { return anchorname; }
        var el = document.getElementById(anchorname);
        if (el) { return el; }
        var byName = document.getElementsByName(anchorname);
        return (byName && byName.length) ? byName[0] : null;
    }

    /* ------------------------------------------------------------------ */
    /* The shared popup                                                    */
    /* ------------------------------------------------------------------ */

    var popupEl = null;         // shared calendar DIV
    var activeCal = null;       // CalendarPopup instance currently shown
    var activeAnchor = null;    // element the popup is positioned against
    var viewYear = 0;           // year currently rendered
    var viewMonth = 1;          // month currently rendered (1-12)
    var yearPageStart = 0;      // first year of the "year" display page

    function getPopupEl() {
        if (popupEl && popupEl.parentNode) { return popupEl; }
        injectStyles();
        popupEl = document.createElement('div');
        popupEl.className = 'vmsCal';
        popupEl.setAttribute('role', 'dialog');
        // Keep focus in the field; mousedown inside must not close the popup.
        on(popupEl, 'mousedown', function (e) {
            e = e || window.event;
            if (e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }
        });
        (document.body || document.documentElement).appendChild(popupEl);
        return popupEl;
    }

    function button(className, text, handler) {
        var b = document.createElement('button');
        b.type = 'button';
        b.className = className;
        b.innerHTML = text;
        on(b, 'click', function (e) { stop(e); handler(); });
        return b;
    }

    function clear(el) {
        while (el.firstChild) { el.removeChild(el.firstChild); }
    }

    function shiftMonth(delta) {
        var m = viewMonth + delta, y = viewYear;
        while (m < 1) { m += 12; y--; }
        while (m > 12) { m -= 12; y++; }
        viewMonth = m; viewYear = y;
        render();
    }

    function commitDate(year, month, day) {
        var cal = activeCal;
        hide();
        if (!cal) { return; }
        cal.currentDate = new Date(year, month - 1, day, 0, 0, 0, 0);
        invokeReturn(cal.returnFunction, [year, month, day], function () {
            applyToInput(cal, cal.currentDate);
        });
    }

    function commitMonth(year, month) {
        var cal = activeCal;
        hide();
        if (!cal) { return; }
        invokeReturn(cal.returnMonthFunction, [year, month], null);
    }

    function commitQuarter(year, quarter) {
        var cal = activeCal;
        hide();
        if (!cal) { return; }
        invokeReturn(cal.returnQuarterFunction, [year, quarter], null);
    }

    function commitYear(year) {
        var cal = activeCal;
        hide();
        if (!cal) { return; }
        invokeReturn(cal.returnYearFunction, [year], null);
    }

    // The legacy API accepts either a function or a global function name.
    // A missing/default handler falls back to writing into the bound input.
    function invokeReturn(handler, args, fallback) {
        var fn = null;
        if (typeof handler === 'function') { fn = handler; }
        else if (typeof handler === 'string' && handler &&
            handler.indexOf('CalendarPopup_tmpReturn') !== 0 &&
            typeof window[handler] === 'function') {
            fn = window[handler];
        }
        if (fn) { fn.apply(window, args); }
        else if (fallback) { fallback(); }
    }

    function applyToInput(cal, date) {
        var input = cal.targetInput || window.CalendarPopup_targetInput;
        if (!input) { return; }
        var format = cal.dateFormat || window.CalendarPopup_dateFormat || DEFAULT_FORMAT;
        input.value = date ? formatDateValue(date, format) : '';
        fireEvent(input, 'input');
        fireEvent(input, 'change');
        try { input.focus(); } catch (err) { }
    }

    function renderHeader(cal, root) {
        var head = document.createElement('div');
        head.className = 'vmsCalHd';

        head.appendChild(button('vmsCalNav', '&lsaquo;', function () { shiftMonth(-1); }));

        var monthWrap = document.createElement('span');
        monthWrap.className = 'vmsCalSelWrapM';
        var monthSel = document.createElement('select');
        monthSel.className = 'vmsCalSel';
        for (var m = 0; m < 12; m++) {
            var mo = document.createElement('option');
            mo.value = String(m + 1);
            mo.appendChild(document.createTextNode(cal.monthNames[m]));
            if (m + 1 === viewMonth) { mo.selected = true; }
            monthSel.appendChild(mo);
        }
        on(monthSel, 'change', function () {
            viewMonth = parseInt(monthSel.value, 10);
            render();
        });
        monthWrap.appendChild(monthSel);
        head.appendChild(monthWrap);

        var yearWrap = document.createElement('span');
        yearWrap.className = 'vmsCalSelWrapY';
        var yearSel = document.createElement('select');
        yearSel.className = 'vmsCalSel';
        var thisYear = new Date().getFullYear();
        var first = Math.min(thisYear - YEARS_BACK, viewYear);
        var last = Math.max(thisYear + YEARS_FORWARD, viewYear);
        for (var y = first; y <= last; y++) {
            var yo = document.createElement('option');
            yo.value = String(y);
            yo.appendChild(document.createTextNode(String(y)));
            if (y === viewYear) { yo.selected = true; }
            yearSel.appendChild(yo);
        }
        on(yearSel, 'change', function () {
            viewYear = parseInt(yearSel.value, 10);
            render();
        });
        yearWrap.appendChild(yearSel);
        head.appendChild(yearWrap);

        head.appendChild(button('vmsCalNav', '&rsaquo;', function () { shiftMonth(1); }));
        root.appendChild(head);
    }

    function renderDateGrid(cal, root) {
        var today = new Date();
        var selected = cal.currentDate;
        var table = document.createElement('table');
        table.className = 'vmsCalGrid';

        var thead = document.createElement('thead');
        var hrow = document.createElement('tr');
        for (var j = 0; j < 7; j++) {
            var weekday = (cal.weekStartDay + j) % 7;
            var th = document.createElement('th');
            if (weekday === 0) { th.className = 'vmsCalSun'; }
            th.appendChild(document.createTextNode(cal.dayHeaders[weekday]));
            hrow.appendChild(th);
        }
        thead.appendChild(hrow);
        table.appendChild(thead);

        // First cell of the grid: back up to the start of the week that
        // contains the 1st of the displayed month.
        var firstOfMonth = new Date(viewYear, viewMonth - 1, 1);
        var offset = firstOfMonth.getDay() - cal.weekStartDay;
        if (offset < 0) { offset += 7; }
        var cursor = new Date(viewYear, viewMonth - 1, 1 - offset);

        var tbody = document.createElement('tbody');
        for (var row = 0; row < 6; row++) {
            var tr = document.createElement('tr');
            for (var col = 0; col < 7; col++) {
                var cellDate = new Date(cursor.getFullYear(), cursor.getMonth(), cursor.getDate());
                var td = document.createElement('td');
                var disabled = !!cal.disabledWeekDays[cellDate.getDay()];
                var label = String(cellDate.getDate());

                if (disabled) {
                    var span = document.createElement('span');
                    span.className = 'vmsCalDay vmsCalDisabled';
                    span.appendChild(document.createTextNode(label));
                    td.appendChild(span);
                } else {
                    var day = document.createElement('button');
                    day.type = 'button';
                    var cls = 'vmsCalDay';
                    if (cellDate.getMonth() !== viewMonth - 1) { cls += ' vmsCalOther'; }
                    if (sameDay(cellDate, today)) { cls += ' vmsCalToday'; }
                    if (sameDay(cellDate, selected)) { cls += ' vmsCalSelected'; }
                    day.className = cls;
                    day.appendChild(document.createTextNode(label));
                    (function (d) {
                        on(day, 'click', function (e) {
                            stop(e);
                            // "week-end" returns the last day of the clicked week.
                            if (cal.displayType === 'week-end') {
                                var end = new Date(d.getFullYear(), d.getMonth(), d.getDate());
                                end.setDate(end.getDate() + (6 - ((end.getDay() - cal.weekStartDay + 7) % 7)));
                                commitDate(end.getFullYear(), end.getMonth() + 1, end.getDate());
                            } else {
                                commitDate(d.getFullYear(), d.getMonth() + 1, d.getDate());
                            }
                        });
                    })(cellDate);
                    td.appendChild(day);
                }
                tr.appendChild(td);
                cursor.setDate(cursor.getDate() + 1);
            }
            tbody.appendChild(tr);
        }
        table.appendChild(tbody);
        root.appendChild(table);
    }

    function renderCellGrid(items, columns, root, isSelected, onPick) {
        var table = document.createElement('table');
        table.className = 'vmsCalGrid';
        var tbody = document.createElement('tbody');
        var tr = null;
        for (var i = 0; i < items.length; i++) {
            if (i % columns === 0) {
                tr = document.createElement('tr');
                tbody.appendChild(tr);
            }
            var td = document.createElement('td');
            var cell = document.createElement('button');
            cell.type = 'button';
            cell.className = 'vmsCalCell' + (isSelected(i) ? ' vmsCalSelected' : '');
            cell.appendChild(document.createTextNode(items[i]));
            (function (index) {
                on(cell, 'click', function (e) { stop(e); onPick(index); });
            })(i);
            td.appendChild(cell);
            tr.appendChild(td);
        }
        table.appendChild(tbody);
        root.appendChild(table);
    }

    function renderYearHeader(cal, root, label, onPrev, onNext) {
        var head = document.createElement('div');
        head.className = 'vmsCalHd';
        head.appendChild(button('vmsCalNav', '&lsaquo;', onPrev));
        var title = document.createElement('span');
        title.style.textAlign = 'center';
        title.style.fontWeight = 'bold';
        title.appendChild(document.createTextNode(label));
        head.appendChild(title);
        head.appendChild(button('vmsCalNav', '&rsaquo;', onNext));
        root.appendChild(head);
    }

    function renderFooter(cal, root) {
        var foot = document.createElement('div');
        foot.className = 'vmsCalFt';
        foot.appendChild(button('vmsCalLink', cal.todayText, function () {
            var now = new Date();
            commitDate(now.getFullYear(), now.getMonth() + 1, now.getDate());
        }));
        foot.appendChild(button('vmsCalLink', cal.clearText, function () {
            var c = activeCal;
            hide();
            if (c) { c.currentDate = null; applyToInput(c, null); }
        }));
        root.appendChild(foot);
    }

    function render() {
        var cal = activeCal;
        if (!cal) { return; }
        var root = cal.divName ? document.getElementById(cal.divName) : getPopupEl();
        if (!root) { return; }
        if (cal.divName) { injectStyles(); addClass(root, 'vmsCal'); }
        clear(root);

        var type = cal.displayType;
        if (type === 'month' || type === 'quarter') {
            renderYearHeader(cal, root, String(viewYear),
                function () { viewYear--; render(); },
                function () { viewYear++; render(); });
            if (type === 'month') {
                renderCellGrid(cal.monthAbbreviations, 3, root,
                    function () { return false; },
                    function (i) { commitMonth(viewYear, i + 1); });
            } else {
                renderCellGrid(['Q1', 'Q2', 'Q3', 'Q4'], 2, root,
                    function () { return false; },
                    function (i) { commitQuarter(viewYear, i + 1); });
            }
        } else if (type === 'year') {
            var years = [];
            for (var i = 0; i < 12; i++) { years.push(String(yearPageStart + i)); }
            renderYearHeader(cal, root, years[0] + ' - ' + years[years.length - 1],
                function () { yearPageStart -= 12; render(); },
                function () { yearPageStart += 12; render(); });
            renderCellGrid(years, 3, root,
                function () { return false; },
                function (i) { commitYear(yearPageStart + i); });
        } else {
            renderHeader(cal, root);
            renderDateGrid(cal, root);
            renderFooter(cal, root);
        }
    }

    function position() {
        if (!popupEl || !activeAnchor) { return; }
        var rect = activeAnchor.getBoundingClientRect();
        var scrollX = window.pageXOffset != null ? window.pageXOffset :
            (document.documentElement.scrollLeft || document.body.scrollLeft || 0);
        var scrollY = window.pageYOffset != null ? window.pageYOffset :
            (document.documentElement.scrollTop || document.body.scrollTop || 0);
        var viewW = document.documentElement.clientWidth || window.innerWidth;
        var viewH = document.documentElement.clientHeight || window.innerHeight;
        var width = popupEl.offsetWidth, height = popupEl.offsetHeight;
        var offsetX = activeCal ? (activeCal.offsetX || 0) : 0;
        var offsetY = activeCal ? (activeCal.offsetY || 0) : 0;

        var left = rect.left + scrollX + offsetX;
        var top = rect.bottom + scrollY + 2 + offsetY;
        // Flip above the field when there is no room below it.
        if (rect.bottom + height + 2 > viewH && rect.top - height - 2 > 0) {
            top = rect.top + scrollY - height - 2 + offsetY;
        }
        var maxLeft = scrollX + viewW - width - 8;
        if (left > maxLeft) { left = maxLeft; }
        if (left < scrollX + 4) { left = scrollX + 4; }
        if (top < scrollY + 4) { top = scrollY + 4; }

        popupEl.style.left = Math.round(left) + 'px';
        popupEl.style.top = Math.round(top) + 'px';
    }

    function show(cal, anchor) {
        activeCal = cal;
        activeAnchor = anchor;

        var base = cal.currentDate || new Date();
        viewYear = base.getFullYear();
        viewMonth = base.getMonth() + 1;
        yearPageStart = viewYear - cal.yearSelectStartOffset;

        if (cal.divName) {
            render();
            var host = document.getElementById(cal.divName);
            if (host) { host.style.display = 'block'; host.style.visibility = 'visible'; }
            cal.visible = true;
            return;
        }

        var root = getPopupEl();
        render();
        root.style.left = '-9999px';
        root.style.top = '-9999px';
        addClass(root, 'vmsCalOpen');
        position();
        cal.visible = true;
    }

    function hide() {
        if (activeCal && activeCal.divName) {
            var host = document.getElementById(activeCal.divName);
            if (host) { host.style.display = 'none'; }
        }
        if (popupEl) {
            popupEl.className = popupEl.className.replace(/\s*vmsCalOpen\s*/g, ' ');
            popupEl.className = trim(popupEl.className);
        }
        if (activeCal) { activeCal.visible = false; }
        activeCal = null;
        activeAnchor = null;
    }

    function isOpen() { return !!activeCal; }

    /* ------------------------------------------------------------------ */
    /* Global listeners                                                    */
    /* ------------------------------------------------------------------ */

    on(document, 'mousedown', function () { if (isOpen()) { hide(); } });

    on(document, 'keydown', function (e) {
        e = e || window.event;
        if (isOpen() && (e.keyCode === 27 || e.key === 'Escape')) { hide(); }
    });

    on(window, 'resize', function () { if (isOpen()) { position(); } });
    on(window, 'scroll', function () { if (isOpen()) { position(); } });

    /* ------------------------------------------------------------------ */
    /* CalendarPopup - public API (unchanged from the legacy version)      */
    /* ------------------------------------------------------------------ */

    function CalendarPopup(divName) {
        if (!(this instanceof CalendarPopup)) { return new CalendarPopup(divName); }

        if (!window.popupWindowIndex) { window.popupWindowIndex = 0; }
        if (!window.popupWindowObjects) { window.popupWindowObjects = []; }
        this.index = window.popupWindowIndex++;
        window.popupWindowObjects[this.index] = this;

        this.type = divName ? 'DIV' : 'DIV';   // never "WINDOW" any more
        this.divName = divName || null;
        this.offsetX = 0;
        this.offsetY = 0;
        this.width = 0;
        this.height = 0;
        this.visible = false;
        this.autoHideEnabled = true;

        this.monthNames = MONTH_NAMES.slice(0);
        this.monthAbbreviations = MONTH_ABBREVIATIONS.slice(0);
        this.dayHeaders = DAY_HEADERS.slice(0);
        this.returnFunction = 'CalendarPopup_tmpReturnFunction';
        this.returnMonthFunction = 'CalendarPopup_tmpReturnMonthFunction';
        this.returnQuarterFunction = 'CalendarPopup_tmpReturnQuarterFunction';
        this.returnYearFunction = 'CalendarPopup_tmpReturnYearFunction';
        this.weekStartDay = 0;
        this.isShowYearNavigation = true;
        this.displayType = 'date';
        this.disabledWeekDays = {};
        this.yearSelectStartOffset = 2;
        this.currentDate = null;
        this.todayText = 'Today';
        this.clearText = 'Clear';
        this.targetInput = null;
        this.dateFormat = DEFAULT_FORMAT;

        injectStyles();
    }

    CalendarPopup.prototype.setReturnFunction = function (fn) { this.returnFunction = fn; };
    CalendarPopup.prototype.setReturnMonthFunction = function (fn) { this.returnMonthFunction = fn; };
    CalendarPopup.prototype.setReturnQuarterFunction = function (fn) { this.returnQuarterFunction = fn; };
    CalendarPopup.prototype.setReturnYearFunction = function (fn) { this.returnYearFunction = fn; };

    CalendarPopup.prototype.setMonthNames = function () {
        for (var i = 0; i < arguments.length && i < 12; i++) { this.monthNames[i] = arguments[i]; }
    };
    CalendarPopup.prototype.setMonthAbbreviations = function () {
        for (var i = 0; i < arguments.length && i < 12; i++) { this.monthAbbreviations[i] = arguments[i]; }
    };
    CalendarPopup.prototype.setDayHeaders = function () {
        for (var i = 0; i < arguments.length && i < 7; i++) { this.dayHeaders[i] = arguments[i]; }
    };
    CalendarPopup.prototype.setWeekStartDay = function (day) { this.weekStartDay = day % 7; };
    CalendarPopup.prototype.showYearNavigation = function () { this.isShowYearNavigation = true; };
    CalendarPopup.prototype.setYearSelectStartOffset = function (n) { this.yearSelectStartOffset = n; };
    CalendarPopup.prototype.setTodayText = function (text) { this.todayText = text; };
    CalendarPopup.prototype.setSize = function (w, h) { this.width = w; this.height = h; };
    CalendarPopup.prototype.populate = function (contents) { this.contents = contents; };
    CalendarPopup.prototype.refresh = function () { if (activeCal === this) { render(); } };
    CalendarPopup.prototype.autoHide = function () { this.autoHideEnabled = true; };

    CalendarPopup.prototype.setDisplayType = function (type) {
        if (type !== 'date' && type !== 'week-end' && type !== 'month' &&
            type !== 'quarter' && type !== 'year') {
            alert('Invalid display type! Must be one of: date,week-end,month,quarter,year');
            return false;
        }
        this.displayType = type;
        return true;
    };

    CalendarPopup.prototype.setDisabledWeekDays = function () {
        this.disabledWeekDays = {};
        for (var i = 0; i < arguments.length; i++) { this.disabledWeekDays[arguments[i]] = true; }
    };

    CalendarPopup.prototype.showCalendar = function (anchorname) {
        var anchor = resolveAnchor(anchorname) ||
            (this.targetInput ? this.targetInput : null);
        if (!anchor) { return; }
        show(this, anchor);
    };

    CalendarPopup.prototype.hideCalendar = function () { hide(); };
    CalendarPopup.prototype.hidePopup = function () { hide(); };
    CalendarPopup.prototype.showPopup = function (anchorname) { this.showCalendar(anchorname); };

    // PopupWindow_hidePopupWindows() walks window.popupWindowObjects and calls
    // these on every mouseup. Autocomplete.js still creates PopupWindow objects,
    // which installs that handler, so the stubs must exist.
    CalendarPopup.prototype.isClicked = function () { return activeCal === this; };
    CalendarPopup.prototype.hideIfNotClicked = function () { };

    /*
        select(inputobj, anchorname, format)

        inputobj may legitimately arrive as undefined - every call site in this
        application passes document.forms[0].<shortName>, which ASP.NET breaks
        on master-page rendered controls. When that happens the field is
        located from the anchor/icon instead, so no markup change is required.
    */
    CalendarPopup.prototype.select = function (inputobj, anchorname, format) {
        var anchorEl = resolveAnchor(anchorname);
        var input = isTextField(inputobj) ? inputobj : null;

        if (!input && anchorEl) { input = findFieldNear(anchorEl); }
        if (!input && typeof anchorname === 'string') { input = findFieldByShortName(anchorname); }
        if (!input) { return; }

        format = format || DEFAULT_FORMAT;
        this.targetInput = input;
        this.dateFormat = format;
        window.CalendarPopup_targetInput = input;
        window.CalendarPopup_dateFormat = format;
        this.currentDate = parseDateValue(input.value, format);

        // Anchor to the field itself so the calendar drops under the control
        // rather than under the absolutely positioned icon.
        show(this, input || anchorEl);
    };

    /* ------------------------------------------------------------------ */
    /* Legacy globals kept so older generated markup still resolves        */
    /* ------------------------------------------------------------------ */

    window.CalendarPopup = CalendarPopup;

    window.CalendarPopup_tmpReturnFunction = function (y, m, d) {
        var cal = activeCal;
        var input = (cal && cal.targetInput) || window.CalendarPopup_targetInput;
        if (!input) { return; }
        var format = (cal && cal.dateFormat) || window.CalendarPopup_dateFormat || DEFAULT_FORMAT;
        input.value = formatDateValue(new Date(y, m - 1, d, 0, 0, 0, 0), format);
        fireEvent(input, 'input');
        fireEvent(input, 'change');
    };
    window.CalendarPopup_tmpReturnMonthFunction = function () { };
    window.CalendarPopup_tmpReturnQuarterFunction = function () { };
    window.CalendarPopup_tmpReturnYearFunction = function () { };

    window.CalendarPopup_hideCalendar = function () { hide(); };
    window.CalendarPopup_refreshCalendar = function (index, month, year) {
        if (month) { viewMonth = parseInt(month, 10); }
        if (year) { viewYear = parseInt(year, 10); }
        render();
    };

    /* ------------------------------------------------------------------ */
    /* Auto-binding                                                        */
    /* ------------------------------------------------------------------ */

    // Matches: cal1.select(document.forms[0].txtChallanDate,'ChallanDate','dd/MM/yyyy')
    // Group 1 is the calendar object name, so any per-page configuration on it
    // (disabled weekdays, month names, ...) is honoured instead of being lost.
    var SELECT_CALL = /([A-Za-z_$][\w$]*)\s*\.select\s*\(\s*[^,]*,\s*['"]([^'"]+)['"]\s*,\s*['"]([^'"]+)['"]/;
    var BOUND_FLAG = 'data-vms-cal-bound';
    var sharedCal = null;

    function getSharedCal() {
        if (!sharedCal) { sharedCal = new CalendarPopup(); }
        return sharedCal;
    }

    function calendarNamed(name) {
        var owner = name ? window[name] : null;
        return (owner instanceof CalendarPopup) ? owner : getSharedCal();
    }

    function openFor(input, anchorname, format, calName) {
        if (!input || input.disabled) { return; }
        var cal = calendarNamed(calName);
        cal.select(input, anchorname, format);
    }

    // Rewrites every "javascript:calX.select(...)" anchor into a real click
    // handler, so the broken document.forms[0].* argument is bypassed, and
    // makes the paired text box open the picker as well.
    function bindAnchors() {
        var anchors = document.getElementsByTagName('a');
        for (var i = 0; i < anchors.length; i++) {
            var a = anchors[i];
            if (a.getAttribute(BOUND_FLAG)) { continue; }
            var href = a.getAttribute('href') || '';
            if (href.indexOf('.select(') < 0) { continue; }
            var match = SELECT_CALL.exec(href);
            if (!match) { continue; }

            var calName = match[1];
            var anchorname = match[2];
            var format = match[3];
            var input = findFieldNear(a) || findFieldByShortName(anchorname);
            if (!input) { continue; }

            a.setAttribute(BOUND_FLAG, '1');
            a.setAttribute('href', 'javascript:void(0);');
            a.style.cursor = 'pointer';

            (function (anchor, field, name, fmt, owner) {
                on(anchor, 'click', function (e) {
                    stop(e);
                    // Some pages disable a date field by removing the href.
                    if (!anchor.getAttribute('href')) { return; }
                    openFor(field, name, fmt, owner);
                });
                on(anchor, 'mousedown', function (e) {
                    e = e || window.event;
                    if (e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }
                });

                if (!field.getAttribute(BOUND_FLAG)) {
                    field.setAttribute(BOUND_FLAG, '1');
                    field.style.cursor = 'pointer';
                    on(field, 'click', function (e) {
                        e = e || window.event;
                        if (e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }
                        openFor(field, name, fmt, owner);
                    });
                    on(field, 'mousedown', function (e) {
                        e = e || window.event;
                        if (e.stopPropagation) { e.stopPropagation(); } else { e.cancelBubble = true; }
                    });
                }
            })(a, input, anchorname, format, calName);
        }
    }

    function initialise() {
        injectStyles();
        installFormAliases();
        bindAnchors();
    }

    function ready(fn) {
        if (document.readyState === 'complete' || document.readyState === 'interactive') {
            window.setTimeout(fn, 0);
        } else {
            on(document, 'DOMContentLoaded', fn);
            on(window, 'load', fn);
        }
    }

    ready(initialise);

    // UpdatePanel partial postbacks replace the markup, so re-run afterwards.
    ready(function () {
        try {
            if (window.Sys && window.Sys.WebForms && window.Sys.WebForms.PageRequestManager) {
                var prm = window.Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(function () { hide(); });
                prm.add_endRequest(function () { initialise(); });
            }
        } catch (err) { }
    });

    // Exposed for pages that add date fields dynamically.
    window.VmsCalendar = {
        refresh: initialise,
        open: openFor,
        hide: hide,
        format: formatDateValue,
        parse: parseDateValue
    };

})(window, document);
