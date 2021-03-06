﻿function displayError(errorData) {
    var errorWindow = $('<div id="errorWindow"><p>Some kind of error occured: ' + errorData + '</p></div>');
    $('#pageWrap').prepend(errorWindow);
    $('#errorWindow').show();
    $('#errorWindow').fadeOut(7000);
}

var loadingClass = 'loading';
function postAjax(url, data, elementToGetLoadingClass, successCallback) {
    if (elementToGetLoadingClass) {
        elementToGetLoadingClass.addClass(loadingClass);
    }
    $.ajax({
        url: url,
        type: 'POST',
        data: data,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (!data.Id) {
                displayError("Session has expired. Refresh to log back in.");
            }
            if (data.Error) {
                displayError(data.Error);
            }

            if (elementToGetLoadingClass) {
                elementToGetLoadingClass.removeClass(loadingClass);
            }
            if (typeof (successCallback) == 'function') {
                successCallback(data);
            }
        },
        error: function (data) {
            displayError(data);
            console.error(data);
        }
    });
}

function escapeString(input) {
    var replacement = input.replace(/\\/g, '\\\\');
    replacement = replacement.replace(/"/g, '\\"');
    return replacement;
}

function htmlEscapeString(input) {
    var replacement = input.replace(/"/g, '&quot;');
    return replacement;
}