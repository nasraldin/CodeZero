var CodeZero = CodeZero || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    CodeZero.libs = CodeZero.libs || {};
    CodeZero.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                icon: 'info'
            },
            success: {
                icon: 'success'
            },
            warn: {
                icon: 'warning'
            },
            error: {
                icon: 'error'
            },
            confirm: {
                icon: 'warning',
                title: 'Are you sure?',
                buttons: ['Cancel', 'Yes']
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        var opts = $.extend(
            {},
            CodeZero.libs.sweetAlert.config['default'],
            CodeZero.libs.sweetAlert.config[type],
            {
                title: title,
                text: message
            }
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function () {
                $dfd.resolve();
            });
        });
    };

    CodeZero.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    CodeZero.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    CodeZero.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    CodeZero.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    CodeZero.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            CodeZero.libs.sweetAlert.config['default'],
            CodeZero.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve(isConfirmed);
            });
        });
    };

    CodeZero.event.on('CodeZero.dynamicScriptsInitialized', function () {
        CodeZero.libs.sweetAlert.config.confirm.title = CodeZero.localization.CodeZeroWeb('AreYouSure');
        CodeZero.libs.sweetAlert.config.confirm.buttons = [CodeZero.localization.CodeZeroWeb('Cancel'), CodeZero.localization.CodeZeroWeb('Yes')];
    });

})(jQuery);