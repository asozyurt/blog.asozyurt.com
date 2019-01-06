(function ($) {
    "use strict";
    // Document ready

    $(document).ready(function () {

        $('[data-toggle="tooltip"]').tooltip();  

        $('li.dropdown').hover(function () {
            $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(500);
        }, function () {

            $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(500);
        });



        // Section scroll 
        $('a.scroll').smoothScroll({
            speed: 800,
            offset: -64
        });
        // To the top handler

        $().UItoTop({ easingType: 'easeOutQuart' });

        // Mobile menu 

        var mobileBtn = $('.mobile-btn');
        var nav = $('ul.menu');
        var navHeight = nav.height();
        var mobile = $('ul.menu li a');

        $(mobileBtn).on("click", function () {
            nav.slideToggle();
            mobile.addClass('mobile');
            return false;
        });

        $(window).resize(function () {
            var w = $(window).width();
            if (w > 320 && nav.is(':hidden')) {
                nav.removeAttr('style');
                mobile.removeClass('mobile');
            }
        });

        nav.on("click", function (e) {
            if ($(e.target).is('a') && $(e.target).hasClass('mobile')) {
                nav.slideToggle();
                mobile.removeClass('mobile');
            }

        });
    });

})(jQuery);

validateSearchForm = function () {
    var searchText = document.forms["searchForm"]["searchText"].value;
    if (searchText === null || searchText === "" || searchText.length < 3) {
        document.forms["searchForm"]["searchText"].style.borderColor = "#963634";
        return false;
    }
    return true;
};