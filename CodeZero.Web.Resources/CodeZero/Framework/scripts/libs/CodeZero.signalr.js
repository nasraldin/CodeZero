var CodeZero = CodeZero || {};
(function ($) {

    //Check if SignalR is defined
    if (!$ || !$.connection) {
        return;
    }

    //Create namespaces
    CodeZero.signalr = CodeZero.signalr || {};
    CodeZero.signalr.hubs = CodeZero.signalr.hubs || {};

    //Get the common hub
    CodeZero.signalr.hubs.common = $.connection.CodeZeroCommonHub;

    var commonHub = CodeZero.signalr.hubs.common;
    if (!commonHub) {
        return;
    }

    //Register to get notifications
    commonHub.client.getNotification = function (notification) {
        CodeZero.event.trigger('CodeZero.notifications.received', notification);
    };

    //Connect to the server
    CodeZero.signalr.connect = function() {
        $.connection.hub.start().done(function () {
            CodeZero.log.debug('Connected to SignalR server!'); //TODO: Remove log
            CodeZero.event.trigger('CodeZero.signalr.connected');
            commonHub.server.register().done(function () {
                CodeZero.log.debug('Registered to the SignalR server!'); //TODO: Remove log
            });
        });
    };

    if (CodeZero.signalr.autoConnect === undefined) {
        CodeZero.signalr.autoConnect = true;
    }

    if (CodeZero.signalr.autoConnect) {
        CodeZero.signalr.connect();
    }

    //reconnect if hub disconnects
    $.connection.hub.disconnected(function () {
        if (!CodeZero.signalr.autoConnect) {
            return;
        }

        setTimeout(function () {
            if ($.connection.hub.state === $.signalR.connectionState.disconnected) {
                $.connection.hub.start();
            }
        }, 5000);
    });

})(jQuery);