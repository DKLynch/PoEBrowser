// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $("#sortableTable").tablesorter({
        theme: 'bootstrap',
        sortList: [[1, 0]]
    });
});


// Overflow check for divination card flavour text
$('.divCardFlavourText').each(function(){
    var el = $(this);

    if (el.height() > 150) {
        el.css('font-size', '8pt');
    }
    else if (el.height() > 120) {
        el.css('font-size', '10pt');
    }
});
