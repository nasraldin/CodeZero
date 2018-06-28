var CodeZero = CodeZero || {};
(function () {

    // Check if SignalR is defined
    if (!signalR) {
        return;
    }

    // Create namespaces
    CodeZero.signalr = CodeZero.signalr || {};
    CodeZero.signalr.hubs = CodeZero.signalr.hubs || {};

    // Configure the connection
    function configureConnection(connection) {
        // Set the common hub
        CodeZero.signalr.hubs.common = connection;

        // Reconnect if hub disconnects
        connection.onclose(function (e) {
            if (e) {
                CodeZero.log.debug('Connection closed with error: ' + e);
            }
            else {
                CodeZero.log.debug('Disconnected');
            }

            if (!CodeZero.signalr.autoConnect) {
                return;
            }

            setTimeout(function () {
                connection.start();
            }, 5000);
        });

        // Register to get notifications
        connection.on('getNotification', function (notification) {
            CodeZero.event.trigger('CodeZero.notifications.received', notification);
        });
    }

    // Connect to the server
    CodeZero.signalr.connect = function () {
        var url = CodeZero.signalr.url || '/signalr';

        // Start the connection.
        startConnection(url, configureConnection)
            .then(function (connection) {
                CodeZero.log.debug('Connected to SignalR server!'); //TODO: Remove log
                CodeZero.event.trigger('CodeZero.signalr.connected');
                // Call the Register method on the hub.
                connection.invoke('register').then(function () {
                    CodeZero.log.debug('Registered to the SignalR server!'); //TODO: Remove log
                });
            })
            .catch(function (error) {
                CodeZero.log.debug(error.message);
            });
    };

    // Starts a connection with transport fallback - if the connection cannot be started using
    // the webSockets transport the function will fallback to the serverSentEvents transport and
    // if this does not work it will try longPolling. If the connection cannot be started using
    // any of the available transports the function will return a rejected Promise.
    function startConnection(url, configureConnection) {
        if (CodeZero.signalr.remoteServiceBaseUrl) {
            url = CodeZero.signalr.remoteServiceBaseUrl + url;
        }

        // Add query string: https://github.com/aspnet/SignalR/issues/680
        if (CodeZero.signalr.qs) {
            url += '?' + CodeZero.signalr.qs;
        }

        return function start(transport) {
            CodeZero.log.debug('Starting connection using ' + signalR.HttpTransportType[transport] + ' transport');
            var connection = new signalR.HubConnectionBuilder()
                .withUrl(url, transport)
                .build();
            if (configureConnection && typeof configureConnection === 'function') {
                configureConnection(connection);
            }

            return connection.start()
                .then(function () {
                    return connection;
                })
                .catch(function (error) {
                    CodeZero.log.debug('Cannot start the connection using ' + signalR.HttpTransportType[transport] + ' transport. ' + error.message);
                    if (transport !== signalR.HttpTransportType.LongPolling) {
                        return start(transport + 1);
                    }

                    return Promise.reject(error);
                });
        }(signalR.HttpTransportType.WebSockets);
    }

    CodeZero.signalr.startConnection = startConnection;

    if (CodeZero.signalr.autoConnect === undefined) {
        CodeZero.signalr.autoConnect = true;
    }

    if (CodeZero.signalr.autoConnect) {
        CodeZero.signalr.connect();
    }

})();
