$(document).ready(function () {

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
            $('#guestNo').addClass('.hide');
        } else {
            $('#plusOneInfo').hide();
            $('#guestYes').addClass('.hide');
        }
    });

});
