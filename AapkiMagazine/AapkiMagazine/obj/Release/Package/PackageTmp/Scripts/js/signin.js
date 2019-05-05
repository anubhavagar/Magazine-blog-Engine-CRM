$(document).ready(function () {

    var c = new Image();
    c.src = "../../Content/images/authenticate_bg.jpg";
    c.onload = function () {
        $.vegas({
            src: '../../Content/images/authenticate_bg.jpg'
        });
    }

  

    $('#register').click(function (e) {
        e.preventDefault();
        $('#validationerror').empty();
        $('#validationerror').removeClass('alert-warning');
        $('.login-form').fadeOut(350, function () {
            $('.register-form').fadeIn(350, function () {
                $('.register-form input:first').focus();
                $('#user_email').val('');
                $('#user_pwd').val('');
            });
            //$('.frame-signin').addClass('register');
        });

    });


    $('.close').click(function () {
        //  $(this).parent().removeClass('in'); // hides alert with Bootstrap CSS3 
        $(this).parent().hide('slow', function () { $(this).empty().append($('<button type="button" class="close">&times;</button>')); });
    });


    $('#register_backtologin').click(function (e) {
        e.preventDefault();
        $('#validationerror').empty();
        $('#validationerror').removeClass('alert-warning');
        $('.register-form').fadeOut(350, function () {
            $('.login-form').fadeIn(350, function () {
                $('.login-form input:first').focus();
                $('#new_email').val('');
                $('#new_pwd').val('');
                $('#new_pwd_confirm').val('');
                $('#new_firstname').val('');
                $('#new_lastname').val('');
            });
            //$('.frame-signin').addClass('register');
        });

    });

    $('#forgotpwd_backtologin').click(function (e) {
        e.preventDefault();
        $('#validationerror').empty();
        $('#validationerror').removeClass('alert-warning');
        $('.forgotpwd-form').fadeOut(350, function () {
            $('.login-form').fadeIn(350, function () {
                $('.login-form input:first').focus();
                $('#reset_email').val('');
            });
            //$('.frame-signin').addClass('register');
        });

    });



    $('#forgotpwd').click(function (e) {
        e.preventDefault();
        $('#validationerror').empty();
        $('#validationerror').removeClass('alert-warning');
        $('.login-form').fadeOut(350, function () {
            $('.forgotpwd-form').fadeIn(350, function () {
                $('.forgotpwd-form input:first').focus();
                $('#user_email').val('');
                $('#user_pwd').val('');
            });
            //$('.frame-signin').addClass('register');
        });

    });

    function registersuccess() {

    }
    function pwdresetsuccess() {
        $("#password_reset_success").show();
        $('#password_reset_success').delay(5000).fadeOut();
    }
});