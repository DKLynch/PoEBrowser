// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Initialize sortable tables on the page
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


// Initialize popover elements
$('[data-toggle="popover"]').popover(
    {
        trigger: 'hover',
        html: true,
        content: function () {
            var content = $(this).attr("data-popover-content");
            return $(content).html();
        },
        template: '<div class="popover" role="tooltip"><h3 class="popover-header"></h3><div class="popover-body"></div></div>'
    }
)