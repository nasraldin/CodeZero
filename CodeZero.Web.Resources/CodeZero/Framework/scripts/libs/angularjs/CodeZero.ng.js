(function (CodeZero, angular) {

    if (!angular) {
        return;
    }

    CodeZero.ng = CodeZero.ng || {};

    CodeZero.ng.http = {
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
                return CodeZero.message.error(error.details, error.message || CodeZero.ng.http.defaultError.message);
            } else {
                return CodeZero.message.error(error.message || CodeZero.ng.http.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = CodeZero.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonCodeZeroErrorResponse: function (response, defer) {
            if (response.config.CodeZeroHandleError !== false) {
                switch (response.status) {
                    case 401:
                        CodeZero.ng.http.handleUnAuthorizedRequest(
                            CodeZero.ng.http.showError(CodeZero.ng.http.defaultError401),
                            CodeZero.appPath
                        );
                        break;
                    case 403:
                        CodeZero.ng.http.showError(CodeZero.ajax.defaultError403);
                        break;
                    case 404:
                        CodeZero.ng.http.showError(CodeZero.ajax.defaultError404);
                        break;
                    default:
                        CodeZero.ng.http.showError(CodeZero.ng.http.defaultError);
                        break;
                }
            }

            defer.reject(response);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    CodeZero.ng.http.handleTargetUrl(targetUrl || CodeZero.appPath);
                });
            } else {
                CodeZero.ng.http.handleTargetUrl(targetUrl || CodeZero.appPath);
            }
        },

        handleResponse: function (response, defer) {
            var originalData = response.data;

            if (originalData.success === true) {
                response.data = originalData.result;
                defer.resolve(response);

                if (originalData.targetUrl) {
                    CodeZero.ng.http.handleTargetUrl(originalData.targetUrl);
                }
            } else if (originalData.success === false) {
                var messagePromise = null;

                if (originalData.error) {
                    if (response.config.CodeZeroHandleError !== false) {
                        messagePromise = CodeZero.ng.http.showError(originalData.error);
                    }
                } else {
                    originalData.error = defaultError;
                }

                CodeZero.ng.http.logError(originalData.error);

                response.data = originalData.error;
                defer.reject(response);

                if (response.status == 401 && response.config.CodeZeroHandleError !== false) {
                    CodeZero.ng.http.handleUnAuthorizedRequest(messagePromise, originalData.targetUrl);
                }
            } else { //not wrapped result
                defer.resolve(response);
            }
        }
    }

    var CodeZeroModule = angular.module('CodeZero', []);

    CodeZeroModule.config([
        '$httpProvider', function ($httpProvider) {
            $httpProvider.interceptors.push(['$q', function ($q) {

                return {

                    'request': function (config) {
                        if (config.url.indexOf('.cshtml') !== -1) {
                            config.url = CodeZero.appPath + 'CodeZeroAppView/Load?viewUrl=' + config.url + '&_t=' + CodeZero.pageLoadTime.getTime();
                        }

                        return config;
                    },

                    'response': function (response) {
                        if (!response.data || !response.data.__CodeZero) {
                            //Non CodeZero related return value
                            return response;
                        }

                        var defer = $q.defer();
                        CodeZero.ng.http.handleResponse(response, defer);
                        return defer.promise;
                    },

                    'responseError': function (ngError) {
                        var defer = $q.defer();

                        if (!ngError.data || !ngError.data.__CodeZero) {
                            CodeZero.ng.http.handleNonCodeZeroErrorResponse(ngError, defer);
                        } else {
                            CodeZero.ng.http.handleResponse(ngError, defer);
                        }

                        return defer.promise;
                    }

                };
            }]);
        }
    ]);

    CodeZero.event.on('CodeZero.dynamicScriptsInitialized', function () {
        CodeZero.ng.http.defaultError.message = CodeZero.localization.CodeZeroWeb('DefaultError');
        CodeZero.ng.http.defaultError.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail');
        CodeZero.ng.http.defaultError401.message = CodeZero.localization.CodeZeroWeb('DefaultError401');
        CodeZero.ng.http.defaultError401.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail401');
        CodeZero.ng.http.defaultError403.message = CodeZero.localization.CodeZeroWeb('DefaultError403');
        CodeZero.ng.http.defaultError403.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail403');
        CodeZero.ng.http.defaultError404.message = CodeZero.localization.CodeZeroWeb('DefaultError404');
        CodeZero.ng.http.defaultError404.details = CodeZero.localization.CodeZeroWeb('DefaultErrorDetail404');
    });

})((CodeZero || (CodeZero = {})), (angular || undefined));
