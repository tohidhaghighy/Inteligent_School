$(document).ready(function () {
   

    var clickEvent = false;
    $('#CarouselFirstPage').on('click', '.nav a', function () {
        clickEvent = true;
        $('.nav li').removeClass('active');
        $(this).parent().addClass('active');
    }).on('slid.bs.carousel', function (e) {
        if (!clickEvent) {
            var count = $('.nav').children().length - 1;
            var current = $('.nav li.active');
            current.removeClass('active').next().addClass('active');
            var id = parseInt(current.data('slide-to'));
            if (id == '3') {
                $('#Android').addClass('active');
                $(".navbar")
            .css('backgroundColor', '#16a085');
            }
            if (id=='2') {
                $(".navbar")
            .css('backgroundColor', '#8e44ad');
            }
            if (id=='1') {
                $(".navbar")
            .css('backgroundColor', '#2980b9');
            }
            if (id=='0') {
                $(".navbar")
            .css('backgroundColor', '#e67e22');
            }
        }
        clickEvent = false;
    });
    $('#Android').click(function () {
        $(".navbar")
            .css('backgroundColor', '#16a085');
    });

    $('#App').click(function () {
        $(".navbar")
            .css('backgroundColor', '#e67e22');
    });
    $('#Web').click(function () {
        $(".navbar")
            .css('backgroundColor', '#2980b9');
    });
    $('#Online').click(function () {
        $(".navbar")
            .css('backgroundColor', '#8e44ad');
    });
});

