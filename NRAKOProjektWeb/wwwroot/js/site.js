// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {

    $('#selectButton').parent().addClass('selected-pricing-model');
    $('#subscriptionModelId').val($('#selectButton').attr('data-id'));


    $('.selectButton').click(function () {
        $('.selected-pricing-model').removeClass('selected-pricing-model');
        var id = $(this).attr('data-id');
        $('#subscriptionModelId').val(id);
        $(this).parent().addClass("selected-pricing-model");
    })
    
})