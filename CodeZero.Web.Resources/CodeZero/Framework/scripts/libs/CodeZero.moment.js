var CodeZero = CodeZero || {};
(function () {
    if (!moment || !moment.tz) {
        return;
    }

    /* DEFAULTS *************************************************/

    CodeZero.timing = CodeZero.timing || {};

    /* FUNCTIONS **************************************************/

    CodeZero.timing.convertToUserTimezone = function (date) {
        var momentDate = moment(date);
        var targetDate = momentDate.clone().tz(CodeZero.timing.timeZoneInfo.iana.timeZoneId);
        return targetDate;
    };

})();