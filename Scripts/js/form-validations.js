(function ($) {
    'use strict';
    
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).addClass('is-invalid').removeClass('is-valid');
        },
        unhighlight: function (element) {
            $(element).removeClass('is-invalid').addClass('is-valid');
        },
        errorElement: 'div',
        errorClass: 'invalid-feedback',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    
    $.validator.addMethod("equalToPassword", function (value, element) {
        var password = $("#Password").val();
        return this.optional(element) || value === password;
    }, "Las contraseñas no coinciden");

    
    $(document).ready(function () {
        $('form.needs-validation').each(function () {
            $(this).validate({
                rules: {
                    ConfirmPassword: {
                        equalToPassword: true
                    }
                }
            });

            
            $(this).on('submit', function (e) {
                if (!this.checkValidity()) {
                    e.preventDefault();
                    e.stopPropagation();
                }
                $(this).addClass('was-validated');
            });
        });
    });
})(jQuery);