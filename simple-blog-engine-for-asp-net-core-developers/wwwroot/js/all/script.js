(function ($) {
    "use strict";
    // Document ready

    $(document).ready(function () {
        
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