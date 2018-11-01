$(window).on("load", function () {
    if ($(window).width() < 768) {
        $(".admin-panel-navbar").removeClass("navbar-fixed-top").addClass("navbar-static-top");
    } else {
        $(".admin-panel-navbar").removeClass("navbar-static-top").addClass("navbar-fixed-top");
    }
});
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $(".side-nav .collapse").on("hide.bs.collapse", function () {
        $(this).prev().find(".fa").eq(1).removeClass("fa-angle-up").addClass("fa-angle-down");
    });
    $('.side-nav .collapse').on("show.bs.collapse", function () {
        $(this).prev().find(".fa").eq(1).removeClass("fa-angle-down").addClass("fa-angle-up");
    });
});
$(window).resize(function () {
    if ($(window).width() < 768) {
        $(".admin-panel-navbar").removeClass("navbar-fixed-top").addClass("navbar-static-top");
    } else {
        $(".admin-panel-navbar").removeClass("navbar-static-top").addClass("navbar-fixed-top");
    }
});