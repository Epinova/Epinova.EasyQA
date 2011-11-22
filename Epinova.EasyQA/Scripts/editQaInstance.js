(function () {
    $('input.radioQuestion').uniform({ radioClass: 'radio question' });
    $('input.radioNo').uniform({ radioClass: 'radio no' });
    $('input.radioYes').uniform({ radioClass: 'radio yes' });
    var titleFieldLink = $('#title a');
    var qaId = $('#qaId').val();
    var loadingClass = 'loading';

    titleFieldLink.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('span').empty().append('<input type="text" class="editableTitle editing" value="' + text + '">');
    });

    var editableTitle = $('.editableTitle');
    editableTitle.live('blur', function (e) {
        var editField = $(this);
        editField.addClass(loadingClass);
        var title = $(this).val();
        $.ajax({
            url: '/Qa/UpdateTitle/',
            type: 'POST',
            data: '{ "id": ' + qaId + ', "title": "' + title + '" }',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
                editField.parent().text(data.Title);
                editField.remove();
            },
            error: function (data) {
                console.log("ERRROROOROR");
                console.log(data);
            }
        });
    });

    $('.statusChooser').live('click', function (e) {
        e.preventDefault();
        var criteriaId = $(this).closest('li').children('.criteriaInstanceId').val();
        var status = $(this).val();
        $.ajax({
            url: '/Qa/UpdateCriteriaStatus/',
            type: 'POST',
            data: '{ "criteriaId": ' + criteriaId + ', "qaId": ' + qaId + ', "status": "' + status + '" }',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
            },
            error: function (data) {
                console.log("ERRROROOROR");
                console.log(data);
            }
        });
    });


    $('.addComment').live('click', function (e) {
        e.preventDefault();
        $(this).siblings('.commentArea').toggle();
    });

    $('.commentArea textarea').live('blur', function (e) {
        var text = $(this).val();
        var criteriaId = $(this).closest('li').children('.criteriaInstanceId').val();
        var textArea = $(this);
        textArea.addClass(loadingClass);
        $.ajax({
            url: '/Qa/UpdateCriteriaComment/',
            type: 'POST',
            data: '{ "criteriaId": ' + criteriaId + ', "qaId": ' + qaId + ', "text": "' + text + '" }',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
                textArea.removeClass(loadingClass);
            },
            error: function (data) {
                console.log("ERRROROOROR");
                console.log(data);
            }
        });
    });

    $('#publishButton').live('click', function (e) {
        $('#publishLoader').show();
        $.ajax({
            url: '/Qa/Publish/',
            type: 'POST',
            data: '{ "qaId": ' + qaId + ' }',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
                $('#publishLoader').hide();
                $('#publishButton').hide();
                $('#unpublishButton').show();
            },
            error: function (data) {
                displayError(data);
                console.log(data);
            }
        });
    });

    $('#unpublishButton').live('click', function (e) {
        $('#publishLoader').show();
        $.ajax({
            url: '/Qa/UnPublish/',
            type: 'POST',
            data: '{ "qaId": ' + qaId + ' }',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
                $('#publishLoader').hide();
                $('#publishButton').show();
                $('#unpublishButton').hide();
            },
            error: function (data) {
                displayError(data);
                console.log(data);
            }
        });
    });
})();