// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(function () {
    var $menuRoot = $("#side-menu[data-menu-root='true']");

    if (!$menuRoot.length) {
        return;
    }

    function setExpanded($group, expanded, animate) {
        var $toggle = $group.children("a[data-menu-toggle='true']");
        var $panel = $group.children("ul[data-menu-panel='true']");
        var shouldAnimate = animate === true;

        $group.toggleClass("menu-expanded", expanded);
        $group.toggleClass("menu-collapsed", !expanded);
        $toggle.attr("aria-expanded", expanded ? "true" : "false");

        if (!shouldAnimate) {
            $panel.stop(true, true);
            $panel.toggle(expanded);
            return;
        }

        if (expanded) {
            $panel.stop(true, true).slideDown(150);
        } else {
            $panel.stop(true, true).slideUp(150);
        }
    }

    $menuRoot.find("li.has-submenu").each(function () {
        var $group = $(this);
        var shouldExpand = $group.hasClass("active") || $group.find("ul[data-menu-panel='true'] li.active").length > 0;

        setExpanded($group, shouldExpand, false);
    });

    $menuRoot.on("click", "a[data-menu-toggle='true']", function (event) {
        event.preventDefault();

        var $toggle = $(this);
        var $group = $toggle.closest("li.has-submenu");
        var isExpanded = $group.hasClass("menu-expanded");

        setExpanded($group, !isExpanded, true);
    });
});

$(function () {
    var modalElement = document.getElementById("campaignAjaxModal");
    var loadingOverlayElement = document.getElementById("campaignAjaxLoadingOverlay");
    var minimumLoadingMs = 800;
    if (!modalElement || typeof bootstrap === "undefined") {
        return;
    }

    var modal = new bootstrap.Modal(modalElement);
    var $modalDialog = $("#campaignAjaxModalDialog");
    var $modalBody = $("#campaignAjaxModalBody");
    var $modalTitle = $("#campaignAjaxModalTitle");
    var isFinalizingSubmit = false;

    function setModalDismissEnabled(enabled) {
        var $dismissButtons = $(modalElement).find(".btn-close, [data-bs-dismiss='modal']");
        if (enabled) {
            $dismissButtons.removeClass("disabled").prop("disabled", false).attr("aria-disabled", "false");
            return;
        }

        $dismissButtons.addClass("disabled").prop("disabled", true).attr("aria-disabled", "true");
    }

    function showOverlayLoading() {
        if (!loadingOverlayElement) {
            return;
        }

        modalElement.addEventListener("hide.bs.modal", function (event) {
            if (isFinalizingSubmit) {
                event.preventDefault();
            }
        });

        modalElement.addEventListener("hidden.bs.modal", function () {
            isFinalizingSubmit = false;
            setModalDismissEnabled(true);
        });

        loadingOverlayElement.classList.add("active");
        document.body.classList.add("campaign-loading");
    }

    function hideOverlayLoading() {
        if (!loadingOverlayElement) {
            return;
        }

        loadingOverlayElement.classList.remove("active");
        document.body.classList.remove("campaign-loading");
    }

    function hideOverlayLoadingWithMinimum(startTime, onHidden) {
        var elapsed = Date.now() - startTime;
        var delay = Math.max(0, minimumLoadingMs - elapsed);

        window.setTimeout(function () {
            hideOverlayLoading();
            if (typeof onHidden === "function") {
                onHidden();
            }
        }, delay);
    }

    function applyModalSize(sizeClass) {
        $modalDialog.removeClass("modal-sm modal-md modal-lg modal-xl");
        if (sizeClass) {
            $modalDialog.addClass(sizeClass);
        }
    }

    function showSuccess(message) {
        var safeMessage = message || "Saved successfully.";
        $modalBody.html('<div class="alert alert-success mb-0">' + safeMessage + "</div>");
    }

    $(document).on("click", ".js-campaign-modal-link", function (event) {
        if ($(this).is("[disabled]") || $(this).hasClass("disabled")) {
            event.preventDefault();
            return;
        }

        event.preventDefault();

        var url = $(this).data("campaign-modal-url");
        var title = $(this).data("campaign-modal-title") || "Campaign";
        var sizeClass = $(this).data("campaign-modal-size") || "";

        if (!url) {
            return;
        }

        $modalTitle.text(title);
        applyModalSize(sizeClass);
        showOverlayLoading();
        var loadingStartTime = Date.now();

        $.get(url)
            .done(function (html) {
                hideOverlayLoadingWithMinimum(loadingStartTime, function () {
                    $modalBody.html(html);
                    modal.show();
                });
            })
            .fail(function () {
                hideOverlayLoadingWithMinimum(loadingStartTime, function () {
                    $modalBody.html('<div class="alert alert-danger mb-0">Unable to load the requested form.</div>');
                    modal.show();
                });
            });
    });

    $(document).on("submit", "form[data-campaign-modal-form='true']", function (event) {
        event.preventDefault();

        var form = this;
        var $form = $(form);
        var hasFileInput = $form.find("input[type='file']").length > 0;
        var requestData = hasFileInput ? new FormData(form) : $form.serialize();

        var $submitButton = $form.find("button[type='submit'], input[type='submit']").first();
        var $feedbackHost = $form.find(".campaign-modal-feedback").first();

        if (!$feedbackHost.length) {
            $feedbackHost = $('<div class="campaign-modal-feedback mt-3"></div>');
            $form.append($feedbackHost);
        }

        $submitButton.addClass("d-none");

        var savingMarkup = '<div class="campaign-submit-feedback campaign-submit-feedback-loading"><span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span><span>Saving...</span></div>';
        $feedbackHost.html(savingMarkup);

        $.ajax({
            url: $form.attr("action") || window.location.href,
            type: ($form.attr("method") || "POST").toUpperCase(),
            data: requestData,
            processData: !hasFileInput,
            contentType: hasFileInput ? false : "application/x-www-form-urlencoded; charset=UTF-8"
        })
            .done(function (response) {
                if (response && response.succeeded) {
                    isFinalizingSubmit = true;
                    setModalDismissEnabled(false);

                    var successMessage = response.message || "Saved successfully.";
                    $feedbackHost.html('<div class="campaign-submit-feedback campaign-submit-feedback-success"><i class="fa fa-check-circle" aria-hidden="true"></i><span>' + successMessage + "</span></div>");

                    window.setTimeout(function () {
                        if (response.redirectUrl) {
                            window.location.href = response.redirectUrl;
                        } else {
                            window.location.reload();
                        }
                    }, 600);

                    return;
                }

                if (typeof response === "string") {
                    $modalBody.html(response);
                    return;
                }

                var errorMessage = response && response.message
                    ? response.message
                    : "Unable to submit the form.";
                $feedbackHost.html('<div class="campaign-submit-feedback campaign-submit-feedback-error">' + errorMessage + "</div>");
            })
            .fail(function () {
                $feedbackHost.html('<div class="campaign-submit-feedback campaign-submit-feedback-error">Unable to submit the form.</div>');
            });
    });
});

$(document).on(
    'change',
    '[data-enable-button]',
    function () {
        const buttonSelector =
            $(this).data('enable-button');

        $(buttonSelector).prop(
            'disabled',
            !this.checked);
    });

$(function () {
    $('[data-enable-button]').each(function () {
        const buttonSelector =
            $(this).data('enable-button');

        $(buttonSelector).prop(
            'disabled',
            !this.checked);
    });
});