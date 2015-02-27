
$(function () {

    $("#dialog").dialog({ autoOpen: false, modal: true, show: "blind", hide: "blind" });


    $("#verwijderbtn").click(function () {
        confirm("test")
        //$("#dialog").dialog("open");
    });
});


function test() {
    confirm('Weet u zeker dat u het team wil verwijderen?');
}