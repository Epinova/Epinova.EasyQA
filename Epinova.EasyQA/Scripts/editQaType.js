(function () {
    var qaTypeId = $('#qaTypeId').val();
    var sectionCountField = $('#sectionCount');
    var titleFieldLink = $('#title a');
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
        var text = $(this).val();
        $.ajax({
            url: '/QaType/UpdateQaTypeTitle/',
            type: 'POST',
            data: '{ "id": ' + qaTypeId + ', "text": "' + text + '" }',
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


    var addCriteriaButtons = $('.addCriteria');
    var criteriaMarkup = '<li><span class="content">{text}</span><a href="#" class="editButton">(Endre)</a><input type="hidden" value="{id}" class="criteriaId"></li>';
    addCriteriaButtons.live('click', function (e) {
        var list = $(this).siblings('ol');
        var thisCriteriaCategory = $(this).closest('.editableCategory').children('.categoryId').val();
        e.preventDefault();
        $.ajax({
            url: '/QaType/CreateCriteria/',
            data: { qaType: qaTypeId, categoryId: thisCriteriaCategory },
            type: 'POST',
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
                console.log(data);
                var modifiedCriteriaMarkup = criteriaMarkup.replace('{id}', data.Id)
                                                           .replace('{text}', data.Title);
                $(list).append(modifiedCriteriaMarkup);
            },
            error: function (e) {
                displayError(e);
                console.log(e);
            }
        });
    });

    var addCategoryButtons = $('.addCategory');
    var categoryMarkup = '<section class="editableCategory"><input type="hidden" value="{id}" class="categoryId"><h1><span class="sectionIndex">{index}</span> <span class="content">(uten navn)</span> <a href="#">(Endre)</a></h1><ol></ol><span class="button addItemButton addCriteria"><a href="#">Legg til nytt kriterie</a></span></section>';
    addCategoryButtons.live('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: '/QaType/CreateCategory/',
            type: 'POST',
            data: { qaType: qaTypeId },
            success: function (data) {
                if (data.Error) {
                    displayError(data.Error);
                }
                sectionCountField.val(parseInt(sectionCountField.val()) + 1);
                console.log("Section count = " + sectionCountField.val());
                var modifiedcategoryMarkup = categoryMarkup.replace('{id}', data.Id)
                                                           .replace('{index}', sectionCountField.val());
                $('#sectionWrapper').append(modifiedcategoryMarkup);
            },
            error: function (data) {
                console.log(data)
            }
        });
    });

    var editTextButtons = $('section ol li a.editButton');
    editTextButtons.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        console.log(text);
        $(this).siblings('.content').empty().append('<input type="text" class="editableCriteria editing" value="' + text + '">');
        $(this).siblings('.editableCriteria').val(text);
    });

    var editableCriteria = $('.editableCriteria');
    editableCriteria.live('blur', function (e) {
        e.preventDefault();
        var li = $(this).closest('li');
        var criteriaId = li.children('.criteriaId').val();
        var categoryId = $(this).closest('section.editableCategory').children('.categoryId').val();
        console.log("criteriaId: " + criteriaId);
        var text = $(this).val();
        $(this).addClass('loading');
        $.ajax({
            url: '/QaType/UpdateCriteriaText/',
            data: { qaType: qaTypeId, criteriaId: criteriaId, text: text },
            type: 'POST',
            success: function (data) {
                $(li).find('.editableCriteria').remove();
                $(li).attr('value', data.Sortorder);
                $(li).find('.content').text(text);
            },
            error: function (e) {
                displayError(e);
                console.log(e);
            }
        });
    });

    var editCategoryHeadingButtons = $('.editableCategory h1 a');
    editCategoryHeadingButtons.live('click', function (e) {
        e.preventDefault();
        var text = $(this).siblings('.content').text();
        $(this).siblings('.content').empty().append('<input type="text" class="editableCategoryTitle editing" value="' + text + '">');
        $(this).siblings('.editableCategoryTitle').val(text);
    });

    var editableCategoryTitle = $('.editableCategoryTitle');
    editableCategoryTitle.live('blur', function (e) {
        e.preventDefault();
        var h1field = $(this).closest('h1');
        var text = $(this).val();
        var categoryId = $(this).closest('section').find('.categoryId').val();
        if (categoryId == "")
            categoryId = 0;

        $(this).addClass(loadingClass);
        $.ajax({
            url: '/QaType/UpdateCategoryTitle/',
            data: { qaTypeId: qaTypeId, categoryId: categoryId, text: text },
            type: 'POST',
            success: function (data) {
                $(this).removeClass(loadingClass);
                if (data.Error) {
                    displayError(data.Error);
                    return;
                }
                $(h1field).find('.editableCategoryTitle').remove();
                $(h1field).find('.content').text(text);
            },
            error: function (e) {
                displayError(e.d);
                console.log(e);
            }
        });
    });
})();