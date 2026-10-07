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
        $modalBody.html('<div class="alert alert-success mb-0">' + safeMessage + '</div>');
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

        console.log("Submitting form...", { action: $form.attr("action"), method: $form.attr("method"), hasFileInput: hasFileInput });

        $.ajax({
            url: $form.attr("action") || window.location.href,
            type: ($form.attr("method") || "POST").toUpperCase(),
            data: requestData,
            processData: !hasFileInput,
            contentType: hasFileInput ? false : "application/x-www-form-urlencoded; charset=UTF-8"
        })
            .done(function (response) {
                console.log("Form submission response:", response, typeof response);

                // Check if this is a successful JSON response
                if (response && typeof response === "object" && response.succeeded) {
                    isFinalizingSubmit = true;
                    setModalDismissEnabled(false);

                    var successMessage = response.message || "Saved successfully.";
                    $feedbackHost.html('<div class="campaign-submit-feedback campaign-submit-feedback-success"><i class="fa fa-check-circle me-2" aria-hidden="true"></i><span>' + successMessage + '</span></div>');

                    window.setTimeout(function () {
                        if (response.redirectUrl) {
                            window.location.href = response.redirectUrl;
                        } else {
                            window.location.reload();
                        }
                    }, 600);

                    return;
                }

                // If response is a string and looks like HTML, it's likely form with validation errors
                if (typeof response === "string") {
                    console.log("Received HTML response (form with validation errors)");
                    $modalBody.html(response);
                    $submitButton.removeClass("d-none");
                    $feedbackHost.html("");
                    return;
                }

                // Handle error JSON responses
                var errorMessage = (response && response.message)
                    ? response.message
                    : "Unable to submit the form. Please check your input and try again.";

                console.error("Form submission error:", errorMessage, response);

                $feedbackHost.html('<div class="campaign-submit-feedback campaign-submit-feedback-error"><i class="fa fa-exclamation-circle me-2" aria-hidden="true"></i><strong>Error:</strong> <span>' + errorMessage + '</span></div>');
                $submitButton.removeClass("d-none");
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.error("AJAX request failed:", {
                    status: jqXHR.status,
                    statusText: jqXHR.statusText,
                    textStatus: textStatus,
                    errorThrown: errorThrown,
                    responseText: jqXHR.responseText
                });

                var errorMessage = jqXHR.responseJSON && jqXHR.responseJSON.message
                    ? jqXHR.responseJSON.message
                    : "Unable to submit the form";

                if (!jqXHR.responseJSON || !jqXHR.responseJSON.message) {
                    if (jqXHR.status === 0) {
                        errorMessage = "Network error. Please check your connection.";
                    } else if (jqXHR.status === 400) {
                        errorMessage = "Invalid request. Please check your input.";
                    } else if (jqXHR.status === 401) {
                        errorMessage = "Your session has expired. Please log in again.";
                    } else if (jqXHR.status === 403) {
                        errorMessage = "You do not have permission to perform this action.";
                    } else if (jqXHR.status === 404) {
                        errorMessage = "The requested resource was not found.";
                    } else if (jqXHR.status === 500 || jqXHR.status === 502 || jqXHR.status === 503 || jqXHR.status === 504) {
                        errorMessage = "Server error. Please try again later.";
                    } else if (jqXHR.status === 408) {
                        errorMessage = "Request timeout. Please try again.";
                    }
                }

                $feedbackHost.html('<div class="campaign-submit-feedback campaign-submit-feedback-error"><i class="fa fa-exclamation-circle me-2" aria-hidden="true"></i><strong>Error:</strong> <span>' + errorMessage + '</span></div>');
                $submitButton.removeClass("d-none");
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
