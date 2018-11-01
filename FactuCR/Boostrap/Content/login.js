$(document).ready(function () {
    $('#txtLoginUserName').keyup(function () {
        if ($(this).val() == "") {
            $('#wrongUserName').addClass('hidden');
            $('#loginEmptyUserName').removeClass('hidden');
        } else {
            $('#loginEmptyUserName').addClass('hidden');
            $('#wrongUserName').addClass('hidden');
        }
    }),
    $('#txtLoginUserPassword').keyup(function () {
        if ($(this).val() == "") {
            $('#wrongPassword').addClass('hidden');
            $('#loginEmptyPassword').removeClass('hidden');
        } else {
            $('#loginEmptyPassword').addClass('hidden');
            $('#wrongPassword').addClass('hidden');
        }
    });
});

function checkTextBoxes() {
    var userEmail = document.getElementById('txtLoginUserName').value;
    var userPassword = document.getElementById('txtLoginUserPassword').value;

    console.log(userEmail);
    console.log(userPassword);

    if (userEmail == '') {
        $('#loginEmptyUserName').removeClass('hidden');
    }
    if (userPassword == '') {
        $('#loginEmptyPassword').removeClass('hidden');
    }

}