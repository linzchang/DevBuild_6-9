$(document).ready(function () {

    $("#dish_submit").click(function (event) {
        var phoneNumber = $("#phone").val();
        var error_free = true;
        var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
        var isPhone = re.test(phoneNumber);
        if (!isPhone) {
            error_free = false;
        }
        if (!error_free) {
            event.preventDefault();
            $("#phoneAlert").text('Please enter a valid phone number')
        }
    });

    $('#dish').keyup(function () {
        var dish = $(this).val();
        //check entry length
        if (dish.length < 1) {
            $('#length').removeClass('text-success').addClass('text-danger');
        } else {
            $('#length').removeClass('text-danger').addClass('text-success');
        }
        //check if entry contains numbers
        if (dish.match(/^([^0-9]*)$/)) {
            $('#number').removeClass('text-danger').addClass('text-success');
        } else {
            $('#number').removeClass('text-success').addClass('text-danger');
        }
    }).focus(function () {
        $('#dishname').show();
    }).blur(function () {
        $('#dishname').hide();
        });

    $('#dishDescription').keyup(function () {
        var description = $(this).val();
        //check entry length
        if (description.length < 10) {
            $('#dlength').removeClass('text-success').addClass('text-danger');
        } else {
            $('#dlength').removeClass('text-danger').addClass('text-success');
        }
        //check if entry contains numbers
        if (description.match(/^([^0-9]*)$/)) {
            $('#dnumber').removeClass('text-danger').addClass('text-success');
        } else {
            $('#dnumber').removeClass('text-success').addClass('text-danger');
        }
    }).focus(function () {
        $('#description').show();
    }).blur(function () {
        $('#description').hide();
    });

});