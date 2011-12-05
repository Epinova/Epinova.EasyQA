(function () {
    $('input.radioQuestion').uniform({ radioClass: 'radio question' });
    $('input.radioNo').uniform({ radioClass: 'radio no' });
    $('input.radioYes').uniform({ radioClass: 'radio yes' });
    var titleFieldLink = $('#title a');
    var qaId = $('#qaId').val();
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

    titleFieldLink.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('span').empty().append('<input type="text" class="editableTitle editing" value="' + text + '">');
    });

    var editableTitle = $('.editableTitle');
    editableTitle.live('blur', function (e) {
        var editField = $(this);
        postAjax('/Qa/UpdateTitle/', '{ "id": ' + qaId + ', "title": "' + $(this).val() + '" }', editField, function (data) {
            editField.parent().text(data.Text);
            editField.remove();
        });
    });

    $('#projectMembers').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdateProjectMembers/', '{ "qaId": ' + qaId + ', "projectMembers": "' + inputField.val() + '" }', inputField, null);
    });

    $('#presentAtReview').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdatePresentAtReview/', '{ "qaId": ' + qaId + ', "presentAtReview": "' + inputField.val() + '" }', inputField, null);
    });

    $('#summary').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdateSummary/', '{ "qaId": ' + qaId + ', "summary": "' + inputField.val() + '" }', inputField, null);
    });

    $('#misc').live('blur', function (e) {
        var inputField = $(this);
        postAjax('/Qa/UpdateMisc/', '{ "qaId": ' + qaId + ', "misc": "' + inputField.val() + '" }', inputField, null);
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
        var text = $(this).val();
        var criteriaId = $(this).closest('li').children('.criteriaInstanceId').val();
        var textArea = $(this);
        postAjax('/Qa/UpdateCriteriaComment/', '{ "criteriaId": ' + criteriaId + ', "qaId": ' + qaId + ', "text": "' + $(this).val() + '" }', textArea, null);
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
})();