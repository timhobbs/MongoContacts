$(".delete-button").on("click", function (e) {
    if (confirm("Are you sure? You cannot undo...") == false) {
        e.preventDefault();
    }
});

$("input[type='text']").on("focus", function () {
    $(this).select();
}).on("mouseup", function (e) {
    e.preventDefault();
});

$("form input[type='text']:first").focus();

$("input[type='datetime']").datepicker({
    format: 'mm/dd/yyyy'
});

$("#contact-email .delete-button").on("click", function (e) {
    e.preventDefault();
    $.post($(this).attr("href"), function (data) {
        console.log("data", data)
        location.reload();
    });
});
