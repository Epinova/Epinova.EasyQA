(function () {
    $('input.radioQuestion').uniform({ radioClass: 'radio question' });
    $('input.radioNo').uniform({ radioClass: 'radio no' });
    $('input.radioYes').uniform({ radioClass: 'radio yes' });
    $('input.radioFixed').uniform({ radioClass: 'radio fixed' });
    $('input.radioWontfix').uniform({ radioClass: 'radio wontfix' });

    var titleFieldLink = $('#title a');
    var qaId = $('#qaId').val();

    titleFieldLink.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('span').empty().append('<input type="text" class="editableTitle editing" value=\'' + text + '\'>');
    });

    var editableTitle = $('.editableTitle');
    editableTitle.live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdateTitle/', '{ "id": ' + qaId + ', "title": "' + escapeString(text) + '" }', inputField, function (data) {
            inputField.parent().text(data.Text);
            inputField.remove();
        });
    });

    $('#presentAtReview').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdatePresentAtReview/', '{ "qaId": ' + qaId + ', "presentAtReview": "' + escapeString(inputField.val()) + '" }', inputField, null);
    });

    $('#summary').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdateSummary/', '{ "qaId": ' + qaId + ', "summary": "' + escapeString(inputField.val()) + '" }', inputField, null);
    });

    $('#misc').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdateMisc/', '{ "qaId": ' + qaId + ', "misc": "' + escapeString(inputField.val()) + '" }', inputField, null);
    });

    $('.statusChooser').live('click', function (e) {
        e.preventDefault();
        var criteriaId = $(this).closest('li').children('.criteriaInstanceId').val();
        var status = $(this).val();

        postAjax('/Qa/UpdateCriteriaStatus/', '{ "criteriaId": ' + criteriaId + ', "qaId": ' + qaId + ', "status": "' + status + '" }', null, null);
    });

    $('.addComment').live('click', function (e) {
        e.preventDefault();
        $(this).siblings('.commentArea').toggle();
    });

    $('.commentArea textarea').live('blur', function (e) {
        var criteriaId = $(this).closest('li').children('.criteriaInstanceId').val();
        var textArea = $(this);
        postAjax('/Qa/UpdateCriteriaComment/', '{ "criteriaId": ' + criteriaId + ', "qaId": ' + qaId + ', "text": "' + escapeString(textArea.val()) + '" }', textArea, null);
    });

    $('#publishButton').live('click', function (e) {
        $('#publishLoader').show();
        postAjax('/Qa/Publish/', '{ "qaId": ' + qaId + ' }', null, function () {
            $('#publishLoader').hide();
            $('#publishButton').hide();
            $('#unpublishButton').show();
        });
    });

    $('#unpublishButton').live('click', function (e) {
        $('#publishLoader').show();
        postAjax('/Qa/UnPublish/', '{ "qaId": ' + qaId + ' }', null, function () {
            $('#publishLoader').hide();
            $('#publishButton').show();
            $('#unpublishButton').hide();
        });
    });

    var autoSuggestList = $('#projectMembersAutoComplete ul');
    var activeSelectionClass = 'activeSelection';

    $(document).click(function () {
        $('#projectMembersAutoComplete ul').hide();
    });

    $('#projectMembersContainer div span a').live('click', function () {
        var elementToRemove = $(this).parent();
        var member = $(this).siblings('span').text();

        postAjax('/Qa/RemoveProjectMember/', '{ "qaId": ' + qaId + ', "projectMember": "' + member + '" }', null, function () {
            elementToRemove.remove();
        });
    });

    $('#projectMembers').live('keydown', function (e) {
        var inputField = $(this);
        if (inputField.val() == "")
            return;
        switch (e.keyCode) {
            case 38: // up
                e.preventDefault();
                if ($(autoSuggestList).children('.' + activeSelectionClass + '').length == 0)
                    return;

                moveSelection("up");
                return;
            case 40: // down
                e.preventDefault();
                var suggestions = $(autoSuggestList).children();
                if (suggestions.length == 0)
                    return;

                if ($(autoSuggestList).children('.' + activeSelectionClass + '').length == 0) {
                    $(suggestions[0]).addClass(activeSelectionClass);
                }
                else {
                    moveSelection("down");
                }
                return;
            case 13: // enter
            case 32: // space
            case 9: // tab
                if ($(autoSuggestList).children().length < 1)
                    return;

                e.preventDefault();
                if ($(autoSuggestList).children('.' + activeSelectionClass + '').length == 1) {
                    var text = $(autoSuggestList).children('.' + activeSelectionClass + '').text();
                    console.log("ProjectMember: " + text);
                    postAjax('/Qa/AddProjectMember/', '{ "qaId": ' + qaId + ', "projectMember": "' + text + '" }', inputField, function () {
                        inputField.val('');
                        var newElement = $('<span><span>' + text + '</span> <a href="#">X</a></span>');
                        $('#projectMembersContainer div').append(newElement);
                        $(autoSuggestList).empty();
                    });
                }
        }

        var inputField = $(this);
        var inputValue;

        if (e.keyCode != 8) // backspace
            inputValue = inputField.val() + letterFromKeyCode(e.keyCode).toLowerCase();
        else {
            inputValue = inputField.val();
            inputValue = inputValue.substr(0, inputValue.length - 1);
        }
        var currentWord = getCurrentWord(inputValue, document.getElementById('projectMembers').selectionStart);
        postAjax('/Qa/FindUser/', '{ "id": "' + currentWord + '"}', inputField, function (data) {
            autoSuggestList.empty();
            if (data.Users.length > 0) {
                console.log(data.Users);
                autoSuggestList.show();
                autoSuggestList.empty();
                for (var i = 0; i < data.Users.length; i++) {
                    autoSuggestList.append($('<li>' + data.Users[i] + '</li>'));
                }
            } else {
                autoSuggestList.hide();
            }
        });
    });

    function getCurrentWord(inputValue, caretPosition) {
        inputValue = $.trim(inputValue);
        if (inputValue.indexOf(" ") == -1)
            return inputValue;

        var closestSpaceBackwardsPosition = -1;
        var notFound = true;
        var currentSearchPosition = caretPosition;
        var currentPosition;

        while (notFound) {
            currentPosition = inputValue.indexOf(" ", currentSearchPosition);
            if (inputValue[currentSearchPosition] == " ")
                closestSpaceBackwardsPosition = currentSearchPosition;

            if (caretPosition == 0) {
                closestSpaceBackwardsPosition = -1;
                notFound = false;
            }
            else if (closestSpaceBackwardsPosition != -1 || currentSearchPosition <= 0 || inputValue == "") {
                notFound = false;
            }
            currentSearchPosition--;
        }

        var closestSpaceForwardsPosition = -1;
        notFound = true;
        currentSearchPosition = caretPosition;
        while (notFound) {
            closestSpaceForwardsPosition = inputValue.indexOf(" ", currentSearchPosition);
            if (closestSpaceForwardsPosition != -1 || caretPosition == inputValue.length || currentSearchPosition >= inputValue.length || inputValue == "") {
                notFound = false;
            }
            currentSearchPosition++;
        }

        if (closestSpaceForwardsPosition == -1)
            return inputValue.slice(closestSpaceBackwardsPosition + 1);
        if (closestSpaceBackwardsPosition == -1)
            return inputValue.slice(0, closestSpaceForwardsPosition);
    }

    function moveSelection(direction) {
        var suggestions = $(autoSuggestList).children();
        var currentSelectedIndex = -1;
        for (var j = 0; j < suggestions.length; j++) {
            if ($(suggestions[j]).hasClass(activeSelectionClass)) {
                currentSelectedIndex = j;
            }
        }

        $(suggestions[currentSelectedIndex]).removeClass(activeSelectionClass);
        if (direction == "down")
            $(suggestions[currentSelectedIndex + 1]).addClass(activeSelectionClass);
        else
            $(suggestions[currentSelectedIndex - 1]).addClass(activeSelectionClass);
    }

    function letterFromKeyCode(n) {
        if (n >= 65 && n < 127)
            return String.fromCharCode(n);
        return "";
    }
})();