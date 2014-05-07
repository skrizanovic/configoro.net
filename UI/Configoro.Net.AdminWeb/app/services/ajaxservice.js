core.AjaxService = (function() {
    "use strict";

    var serviceBase = 'home/',
        getSvcUrl = function(method) {
             return serviceBase + method;
        };

    function getJson(method, jsonIn, callback) {
        $.ajax({
                url: getSvcUrl(method),
                type: "GET",
                data: ko.toJSON(jsonIn),
                dataType: "json",
                contentType: "application/json",
                success: function (json) {
                    callback(json);
                },
                error: function (result) {
                    alert('An error has occured\n\n' + result.responseText);
                }
            });
    };

    function postJson(method, jsonIn, callback) {
        $.ajax({
                url: getSvcUrl(method),
                type: "POST",
                data: ko.toJSON(jsonIn),
                dataType: "json",
                contentType: "application/json",
                success: function (json) {
                    callback(json);
                },
                error: function (result) {
                    alert('An error has occured\n\n' + result.responseText);
                }
            });
    };

    return {
        GetJson: getJson,
        PostJson: postJson
    };

}(core));