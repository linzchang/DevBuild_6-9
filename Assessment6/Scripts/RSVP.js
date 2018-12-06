$(document).ready(function () {

    //SIMPLIFY THESE 2 METHODS
    $('#firstName').keyup(function () {
        var name = $(this).val();
        //check entry length
        if (name.length < 1) {
            $('#length').removeClass('text-success').addClass('text-danger');
        } else {
            $('#length').removeClass('text-danger').addClass('text-success');
        }
        //check if entry contains numbers
        if (name.match(/^([^0-9]*)$/)) {
            $('#number').removeClass('text-danger').addClass('text-success');
        } else {
            $('#number').removeClass('text-success').addClass('text-danger');
        }
    }).focus(function () {
        $('#first').show();
    }).blur(function () {
        $('#first').hide();
        });

    $('#lastName').keyup(function () {
        var name = $(this).val();
        //check entry length
        if (name.length < 1) {
            $('#lastlength').removeClass('text-success').addClass('text-danger');
        } else {
            $('#lastlength').removeClass('text-danger').addClass('text-success');
        }
        //check if entry contains numbers
        if (name.match(/^([^0-9]*)$/)) {
            $('#lastnumber').removeClass('text-danger').addClass('text-success');
        } else {
            $('#lastnumber').removeClass('text-success').addClass('text-danger');
        }
    }).focus(function () {
        $('#last').show();
    }).blur(function () {
        $('#last').hide();
        });


    $('.attending').change(function () {
        var attending = $(this).val();
        if (attending === 'Yes') {
            $('#date').show();
            $('#plusOne').show();
            $('#farewell').hide();
        } else {
            $('#farewell').show();
            $('#date').hide();
            $('#plusOne').hide();
        }
    });

    $('.guest').change(function () {
        var plusOne = $(this).val();
        if (plusOne === 'Yes') {
            $('#plusOneInfo').show();
        } else {
            $('#plusOneInfo').hide();
        }
    });
    
});