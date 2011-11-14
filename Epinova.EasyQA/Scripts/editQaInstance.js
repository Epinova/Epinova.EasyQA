$('input.radioQuestion').uniform({ radioClass: 'radio question' });
$('input.radioNo').uniform({ radioClass: 'radio no' });
$('input.radioYes').uniform({ radioClass: 'radio yes' });

$('.addComment').live('click', function (e) {
    e.preventDefault();
    $(this).siblings('.commentArea').toggle();
});