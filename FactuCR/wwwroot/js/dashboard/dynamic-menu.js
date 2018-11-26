$(document).ready(function () {
    var e = document.getElementById("btn-mantenimiento");
    e.style.cursor = 'pointer';

    var e = document.getElementById("btn-facturacion");
    e.style.cursor = 'pointer';    
    
    if (checkStringInURL("Products") ||
        checkStringInURL("Categories") ||
        checkStringInURL("Families") ||
        checkStringInURL("Providers")) {
        $('#mantenimiento-menu-container').show("slow");
    } else {
        $('#mantenimiento-menu-container').hide();
    }

    if (checkStringInURL("Billing") ||
        checkStringInURL("Billing")) {
        $('#facturacion-menu-container').show("slow");
    } else {
        $('#facturacion-menu-container').hide();
    }
});

function checkStringInURL(value) {
    return window.location.href.indexOf(value) > -1;
}

$('#btn-mantenimiento').click(function () {
    $('#facturacion-menu-container').hide();
    $('#mantenimiento-menu-container').show("slow");
});

$('#btn-facturacion').click(function () {
    $('#mantenimiento-menu-container').hide();
    $('#facturacion-menu-container').show("slow");
});
