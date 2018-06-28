var CodeZero = CodeZero || {};
(function ($) {

    if (!$) {
        return;
    }

    /* JQUERY ENHANCEMENTS ***************************************************/

    // CodeZero.ajax -> uses $.ajax ------------------------------------------------

    CodeZero.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend(true, {}, CodeZero.ajax.defaultOpts, userOptions);
        var oldBeforeSendOption = options.beforeSend;		
        options.beforeSend = function(xhr) {
            if (oldBeforeSendOption) {
                 oldBeforeSendOption(xhr);
            }

            xhr.setRequestHeader("Pragma", "no-cache");
            xhr.setRequestHeader("Cache-Control", "no-cache");
            xhr.setRequestHeader("Expires", "Sat, 01 Jan 2000 00:00:00 GMT");
        };

        options.success = undefined;
        options.error = undefined;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    if (data.__CodeZero) {
                        CodeZero.ajax.handleResponse(data, userOptions, $dfd, jqXHR);
                    } else {
                        $dfd.resolve(data);
                        userOptions.success && userOptions.success(data);
                    }
                }).fail(function (jqXHR) {
                    if (jqXHR.responseJSON && jqXHR.responseJSON.__CodeZero) {
                        CodeZero.ajax.handleResponse(jqXHR.responseJSON, userOptions, $dfd, jqXHR);
                    } else {
                        CodeZero.ajax.handleNonCodeZeroErrorResponse(jqXHR, userOptions, $dfd);
                    }
                });
        });
    };

    $.extend(CodeZero.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        defaultError404: {
            message: 'Resource not found!',
            details: 'The resource requested could not found on the server.'
        },

        logError: function (error) {
            CodeZero.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return CodeZero.message.error(error.details, error.message);
            } else {
                return CodeZero.message.error(error.message || CodeZero.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = CodeZero.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonCodeZeroErrorResponse: function (jqXHR, userOptions, $dfd) {
            if (userOptions.CodeZeroHandleError !== false) {
                switch (jqXHR.status) {
                    case 401:
                        CodeZero.ajax.handleUnAuthorizedRequest(
                            CodeZero.ajax.showError(CodeZero.ajax.defaultError401),
                            CodeZero.appPath
                        );
                        break;
                    case 403:
                        CodeZero.ajax.showError(CodeZero.ajax.defaultError403);
                        break;
                    case 404:
                        CodeZero.ajax.showError(CodeZero.ajax.defaultError404);
                        break;
                    default:
                        CodeZero.ajax.showError(CodeZero.ajax.defaultError);
                        break;
                }
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    CodeZero.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                CodeZero.ajax.handleTargetUrl(targetUrl);
            }
        },

        handleResponse: function (data, userOptions, $dfd, jqXHR) {
            if (data) {
                if (data.success === true) {
                    $dfd && $dfd.resolve(data.result, data, jqXHR);
                    userOptions.success && userOptions.success(data.result, data, jqXHR);

                    if (data.targetUrl) {
                        CodeZero.ajax.handleTargetUrl(data.targetUrl);
                    }
                } else if (data.success === false) {
                    var messagePromise = null;

                    if (data.error) {
                        if (userOptions.CodeZeroHandleError !== false) {
                            messagePromise = CodeZero.ajax.showError(data.error);
                        }
                    } else {
                        data.error = CodeZero.ajax.defaultError;
                    }

                    CodeZero.ajax.logError(data.error);

                    $dfd && $dfd.reject(data.error, jqXHR);
                    userOptions.error && userOptions.error(data.error, jqXHR);

                    if (jqXHR.status === 401 && userOptions.CodeZeroHandleError !== false) {
                        CodeZero.ajax.handleUnAuthorizedRequest(messagePromise, data.targetUrl);
                    }
                } else { //not wrapped result
                    $dfd && $dfd.resolve(data, null, jqXHR);
                    userOptions.success && userOptions.success(data, null, jqXHR);
                }
            } else { //no data sent to back
                $dfd && $dfd.resolve(jqXHR);
                userOptions.success && userOptions.success(jqXHR);
            }
        },

        blockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //block whole page
                    CodeZero.ui.setBusy();
                } else { //block an element
                    CodeZero.ui.setBusy(options.blockUI);
                }
            }
        },

        unblockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //unblock whole page
                    CodeZero.ui.clearBusy();
                } else { //unblock an element
                    CodeZero.ui.clearBusy(options.blockUI);
                }
            }
        },

        ajaxSendHandler: function (event, request, settings) {
            var token = CodeZero.security.antiForgery.getToken();
            if (!token) {
                return;
            }

            if (!CodeZero.security.antiForgery.shouldSendToken(settings)) {
                return;
            }

            if (!settings.headers || settings.headers[CodeZero.security.antiForgery.tokenHeaderName] === undefined) {
                request.setRequestHeader(CodeZero.security.antiForgery.tokenHeaderName, token);
            }
        }
    });

    $(document).ajaxSend(function (event, request, settings) {
        return CodeZero.ajax.ajaxSendHandler(event, request, settings);
    });

    /* JQUERY PLUGIN ENHANCEMENTS ********************************************/

    /* jQuery Form Plugin 
     * http://www.malsup.com/jquery/form/
     */

    // CodeZeroAjaxForm -> uses ajaxForm ------------------------------------------

    if ($.fn.ajaxForm) {
        $.fn.CodeZeroAjaxForm = function (userOptions) {
            userOptions = userOptions || {};

            var options = $.extend({}, $.fn.CodeZeroAjaxForm.defaults, userOptions);

            options.beforeSubmit = function () {
                CodeZero.ajax.blockUI(options);
                userOptions.beforeSubmit && userOptions.beforeSubmit.apply(this, arguments);
            };

            options.success = function (data) {
                CodeZero.ajax.handleResponse(data, userOptions);
            };

            //TODO: Error?

            options.complete = function () {
                CodeZero.ajax.unblockUI(options);
                userOptions.complete && userOptions.complete.apply(this, arguments);
            };

            return this.ajaxForm(options);
        };

        $.fn.CodeZeroAjaxForm.defaults = {
            method: 'POST'
        };
    }

    CodeZero.event.on('CodeZero.dynamicScriptsInitialized', function () {
        CodeZero.ajax.defaultError.message = CodeZero.localization.CodeZeroWeb('DefaultError');
        CodeZero.ajax.defaultError.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail');
        CodeZero.ajax.defaultError401.message = CodeZero.localization.CodeZeroWeb('DefaultError401');
        CodeZero.ajax.defaultError401.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail401');
        CodeZero.ajax.defaultError403.message = CodeZero.localization.CodeZeroWeb('DefaultError403');
        CodeZero.ajax.defaultError403.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail403');
        CodeZero.ajax.defaultError404.message = CodeZero.localization.CodeZeroWeb('DefaultError404');
        CodeZero.ajax.defaultError404.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail404');
    });

})(jQuery);
