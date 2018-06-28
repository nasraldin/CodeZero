define(function () {
    return {
        load: function (name, req, onload, config) {
            var url = CodeZero.appPath + 'api/CodeZeroServiceProxies/Get?name=' + name;
            req([url], function (value) {
                onload(value);
            });
        }
    };
});