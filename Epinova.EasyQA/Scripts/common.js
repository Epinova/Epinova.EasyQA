function displayError(errorData) {
    var errorWindow = $('<div id="errorWindow"><p>' + errorData + '</p></div>');
    $('#pageWrap').prepend(errorWindow);
    $('#errorWindow').show();
    $('#errorWindow').fadeOut(7000);
}