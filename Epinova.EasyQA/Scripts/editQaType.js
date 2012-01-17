(function () {
    var qaTypeId = $('#qaTypeId').val();
    var sectionCountField = $('#sectionCount');
    var titleFieldLink = $('#title a');

    $('input').live('keypress', function (e) {
        if (e.keyCode == 13)
            $(this).blur();
    });

    titleFieldLink.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('span').empty().append('<input type="text" class="editableTitle editing" value=\'' + text + '\'>');
    });

    var editableTitle = $('.editableTitle');
    editableTitle.live('blur', function (e) {
        var editField = $(this);
        var text = $(this).val();
        text = text.replace(/"/g, '\\"');
        postAjax('/QaType/UpdateQaTypeTitle/', '{ "id": ' + qaTypeId + ', "text": "' + text + '" }', editField, function (data) {
            editField.parent().text(data.Text);
            editField.remove();
        });
    });

    var addCriteriaButtons = $('.addCriteria');
    var criteriaMarkup = '<li><span class="content">{text}</span><a href="#" class="editButton">(Endre)</a><input type="hidden" value="{id}" class="criteriaId"></li>';
    addCriteriaButtons.live('click', function (e) {
        e.preventDefault();
        var clickedButton = $(this);
        $(clickedButton).siblings('.loader').show();
        var list = $(this).siblings('ol');
        var thisCriteriaCategory = $(this).closest('.editableCategory').children('.categoryId').val();

        postAjax('/QaType/CreateCriteria/', '{ "qaType":' + qaTypeId + ', "categoryId":' + thisCriteriaCategory + '}', null, function (data) {
            var modifiedCriteriaMarkup = criteriaMarkup.replace('{id}', data.Id)
                                                       .replace('{text}', data.Text);
            $(list).append(modifiedCriteriaMarkup);
            $(clickedButton).siblings('.loader').hide();
        });
    });

    var editTextButtons = $('section ol li a.editButton');
    editTextButtons.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('.content').empty().append('<input type="text" class="editableCriteria editing" value=\'' + text + '\'>');
        $(this).siblings('.editableCriteria').val(text);
    });

    var editableCriteria = $('.editableCriteria');
    editableCriteria.live('blur', function (e) {
        e.preventDefault();
        var clickedElement = $(this);
        var li = $(this).closest('li');
        var criteriaId = li.children('.criteriaId').val();
        var categoryId = $(this).closest('section.editableCategory').children('.categoryId').val();
        var text = $(this).val();
        text = text.replace(/"/g, '\\"');

        postAjax('/QaType/UpdateCriteriaText/', '{ "qaType":' + qaTypeId + ', "criteriaId":' + criteriaId + ', "text": "' + text + '" }', clickedElement, function (data) {
            $(li).find('.editableCriteria').remove();
            $(li).find('.content').text(data.Text);
        });
    });

    var addCategoryButtons = $('.addCategory');
    var categoryMarkup = '<section class="editableCategory"><input type="hidden" value="{id}" class="categoryId"><h1><span class="sectionIndex">{index}</span> <span class="content">(uten navn)</span> <a href="#">(Endre)</a></h1><ol></ol><span class="button addItemButton addCriteria"><a href="#">Legg til nytt kriterie</a></span></section>';
    addCategoryButtons.live('click', function (e) {
        var clickedButton = $(this);
        $(clickedButton).siblings('.loader').show();
        e.preventDefault();
        postAjax('/QaType/CreateCategory/', '{ "qaType":' + qaTypeId + '}', null, function (data) {
            sectionCountField.val(parseInt(sectionCountField.val()) + 1);
            var modifiedcategoryMarkup = categoryMarkup.replace('{id}', data.Id)
                                                        .replace('{index}', sectionCountField.val());
            $('#sectionWrapper').append(modifiedcategoryMarkup);
            $(clickedButton).siblings('.loader').hide();
        });
    });

    var editCategoryHeadingButtons = $('.editableCategory h1 a');
    editCategoryHeadingButtons.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('.content').empty().append('<input type="text" class="editableCategoryTitle editing" value=\'' + text + '\'>');
        $(this).siblings('.editableCategoryTitle').val(text);
    });

    var editableCategoryTitle = $('.editableCategoryTitle');
    editableCategoryTitle.live('blur', function (e) {
        var h1field = $(this).closest('h1');
        var text = $(this).val();
        text = text.replace(/"/g, '\\"');
        var categoryId = $(this).closest('section').find('.categoryId').val();
        if (categoryId == "")
            categoryId = 0;
        var clickedElement = $(this);
        postAjax('/QaType/UpdateCategoryTitle/', '{ "qaTypeId": ' + qaTypeId + ', "categoryId":' + categoryId + ', "text": "' + text + '" }', clickedElement, function (data) {
            $(h1field).find('.editableCategoryTitle').remove();
            $(h1field).find('.content').text(data.Text);
        });
    });
})();