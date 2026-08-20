(function () {
    var pending = null;
    var ACTION_COPY = {
        add: { title: "Add record?", text: "Are you sure you want to add this record?" },
        submit: { title: "Submit?", text: "Are you sure you want to submit?" },
        update: { title: "Update record?", text: "Are you sure you want to update this record?" },
        "delete": { title: "Delete record?", text: "Are you sure you want to delete this record?" },
        approve: { title: "Approve?", text: "Are you sure you want to approve the selected requisition(s)?" },
        status: { title: "Change status?", text: "Are you sure you want to change the status?" }
    };

    function ensureModal() {
        if (document.getElementById("rmStatusConfirmOverlay")) {
            return;
        }

        var overlay = document.createElement("div");
        overlay.id = "rmStatusConfirmOverlay";
        overlay.className = "rm-confirm-overlay";
        overlay.innerHTML =
            '<div class="rm-confirm-dialog" role="dialog" aria-modal="true" aria-labelledby="rmStatusConfirmTitle">' +
                '<div class="rm-confirm-icon"><i class="fas fa-exclamation"></i></div>' +
                '<h5 id="rmStatusConfirmTitle" class="rm-confirm-title">Please confirm</h5>' +
                '<p class="rm-confirm-text">Are you sure?</p>' +
                '<div class="rm-confirm-actions">' +
                    '<button type="button" class="btn btn-secondary rm-confirm-cancel" id="rmStatusConfirmCancel">Cancel</button>' +
                    '<button type="button" class="btn btn-primary" id="rmStatusConfirmOk">Confirm</button>' +
                "</div>" +
            "</div>";

        var host = document.querySelector(".rm-module") || document.body;
        host.appendChild(overlay);

        document.getElementById("rmStatusConfirmOk").addEventListener("click", function () {
            var ok = pending && pending.ok;
            hideModal();
            if (ok) {
                ok();
            }
        });

        document.getElementById("rmStatusConfirmCancel").addEventListener("click", function () {
            var cancel = pending && pending.cancel;
            hideModal();
            if (cancel) {
                cancel();
            }
        });

        overlay.addEventListener("click", function (e) {
            if (e.target === overlay) {
                document.getElementById("rmStatusConfirmCancel").click();
            }
        });

        document.addEventListener("keydown", function (e) {
            if (e.key === "Escape" && overlay.classList.contains("is-open")) {
                document.getElementById("rmStatusConfirmCancel").click();
            }
        });
    }

    function toPlain(value) {
        var html = value == null ? "" : String(value);
        var holder = document.createElement("div");
        holder.innerHTML = html;
        return (holder.textContent || holder.innerText || "").replace(/\s+/g, " ").trim();
    }

    function setDialogMode(mode, title, message, okText) {
        ensureModal();
        var dialog = document.querySelector("#rmStatusConfirmOverlay .rm-confirm-dialog");
        var icon = document.querySelector("#rmStatusConfirmOverlay .rm-confirm-icon");
        var titleEl = document.getElementById("rmStatusConfirmTitle");
        var textEl = document.querySelector("#rmStatusConfirmOverlay .rm-confirm-text");
        var cancel = document.getElementById("rmStatusConfirmCancel");
        var ok = document.getElementById("rmStatusConfirmOk");

        if (dialog) {
            dialog.classList.remove("is-success", "is-error");
            if (mode === "success") {
                dialog.classList.add("is-success");
            } else if (mode === "error") {
                dialog.classList.add("is-error");
            }
        }
        if (icon) {
            if (mode === "success") {
                icon.innerHTML = '<i class="fas fa-check"></i>';
            } else if (mode === "error") {
                icon.innerHTML = '<i class="fas fa-times"></i>';
            } else {
                icon.innerHTML = '<i class="fas fa-exclamation"></i>';
            }
        }
        if (titleEl) {
            titleEl.textContent = title;
        }
        if (textEl) {
            textEl.textContent = message;
        }
        if (cancel) {
            cancel.style.display = mode === "confirm" ? "" : "none";
        }
        if (ok) {
            ok.textContent = okText;
        }
    }

    function showOverlay() {
        var overlay = document.getElementById("rmStatusConfirmOverlay");
        overlay.style.display = "flex";
        overlay.classList.remove("is-open");
        window.requestAnimationFrame(function () {
            window.requestAnimationFrame(function () {
                overlay.classList.add("is-open");
            });
        });
        var okBtn = document.getElementById("rmStatusConfirmOk");
        if (okBtn) {
            okBtn.focus();
        }
    }

    function hideModal() {
        var overlay = document.getElementById("rmStatusConfirmOverlay");
        if (overlay) {
            overlay.classList.remove("is-open");
        }
        pending = null;
    }

    function showConfirm(action, onOk, onCancel) {
        var copy = ACTION_COPY[action] || ACTION_COPY.submit;
        setDialogMode("confirm", copy.title, copy.text, "Confirm");
        pending = { ok: onOk, cancel: onCancel || null };
        showOverlay();
    }

    function continueControl(el) {
        el.setAttribute("data-rm-confirmed", "1");
        el.click();
    }

    function isAlreadyConfirmed(el) {
        if (el.getAttribute("data-rm-confirmed") === "1") {
            el.removeAttribute("data-rm-confirmed");
            return true;
        }
        return false;
    }

    function clearPageError() {
        var lbl = document.getElementById("lblErrorMessage");
        if (lbl) {
            lbl.innerHTML = "";
        }
    }

    window.rmShowResult = function (message, isSuccess, redirectUrl) {
        var ok = isSuccess !== false;
        setDialogMode(ok ? "success" : "error", ok ? "Success" : "Validation", toPlain(message) || (ok ? "Completed successfully." : "Please correct the highlighted fields."), "OK");
        pending = {
            ok: function () {
                if (ok && redirectUrl) {
                    window.location.href = redirectUrl;
                }
            },
            cancel: null
        };
        showOverlay();
    };

    window.rmShowStatusResult = function (message) {
        window.rmShowResult(message, true);
    };

    window.rmFailValidation = function (messageHtml) {
        clearPageError();
        window.rmShowResult(messageHtml, false);
        return false;
    };

    window.rmConfirmAction = function (el, action) {
        if (!el) {
            return true;
        }
        if (isAlreadyConfirmed(el)) {
            return true;
        }
        showConfirm(action || "submit", function () {
            continueControl(el);
        });
        return false;
    };

    window.rmConfirmPostback = function (buttonId, action) {
        var btn = document.getElementById(buttonId);
        if (!btn) {
            btn = document.querySelector("[id$='" + buttonId + "']");
        }
        if (!btn) {
            return false;
        }
        var resolvedAction = action || "submit";
        var buttonText = ((btn.value || btn.textContent || "") + "").toLowerCase();
        if (!action) {
            if (buttonText.indexOf("update") >= 0) {
                resolvedAction = "update";
            } else if (buttonText.indexOf("add") >= 0) {
                resolvedAction = "add";
            } else if (buttonText.indexOf("approve") >= 0) {
                resolvedAction = "approve";
            }
        } else if (resolvedAction === "submit" && buttonText.indexOf("update") >= 0) {
            resolvedAction = "update";
        }
        showConfirm(resolvedAction, function () {
            btn.disabled = true;
            __doPostBack(btn.name, "");
        });
        return false;
    };

    window.rmConfirmStatusUpdate = function (el) {
        if (!el) {
            return true;
        }
        if (isAlreadyConfirmed(el)) {
            return true;
        }

        var row = el.closest("tr");
        var ddl = row ? row.querySelector('select[id*="ddlactive"], select.rm-status-ddl') : null;
        var original = ddl ? (ddl.getAttribute("data-rm-original") || "") : "";
        var current = ddl ? ddl.value : "";
        var action = ddl && current !== original ? "status" : "update";

        showConfirm(action, function () {
            continueControl(el);
        });
        return false;
    };

    window.rmConfirmVendorStatusSubmit = function () {
        if (typeof validateRawMaterialVendorInputs === "function") {
            return validateRawMaterialVendorInputs();
        }
        var btn = document.getElementById("btnSubmit");
        return window.rmConfirmAction(btn, "submit");
    };

    function rememberOriginalStatus() {
        var dropdowns = document.querySelectorAll('select[id*="ddlactive"], select.rm-status-ddl');
        for (var i = 0; i < dropdowns.length; i++) {
            if (!dropdowns[i].getAttribute("data-rm-original")) {
                dropdowns[i].setAttribute("data-rm-original", dropdowns[i].value);
            }
        }

        var yesRadio = document.querySelector('input[type="radio"][id$="rbtnActiveY"]');
        if (yesRadio && !yesRadio.getAttribute("data-rm-original")) {
            yesRadio.setAttribute("data-rm-original", yesRadio.checked ? "Y" : "N");
        }
    }

    function showPendingResult() {
        if (window.__rmPendingActionResult && window.rmShowResult) {
            window.rmShowResult(window.__rmPendingActionResult.message, window.__rmPendingActionResult.success, window.__rmPendingActionResult.redirect || null);
            window.__rmPendingActionResult = null;
        } else if (window.__rmPendingStatusResult && window.rmShowResult) {
            window.rmShowResult(window.__rmPendingStatusResult, true);
            window.__rmPendingStatusResult = null;
        }
    }

    function start() {
        rememberOriginalStatus();
        showPendingResult();
        if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                rememberOriginalStatus();
                showPendingResult();
            });
        }
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start);
    } else {
        start();
    }
})();
