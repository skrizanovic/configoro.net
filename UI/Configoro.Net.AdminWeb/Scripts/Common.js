function CallRemoteMethod(method, url, data, callbackSuccess) {

    jsnData = data != null ? JSON.stringify(data) : null;
    $.ajax({
        type: method,
        url: url,
        data: jsnData,
        contentType: 'application/json',
        success: callbackSuccess,
        error: function(result){
            alert('an error has occured\n\n' + result.responseText)
        }
    });
}