(function ($) {
    "use strict";
    // Document ready

    $(document).ready(function () {

        // Sticky nav
        //var header = $('.main-nav'),
        ////    pos = header.offset();

        //header.slideUp(200, function () {
        //    $(this).removeClass('switched-header').addClass('default').fadeIn(200);
        //});

        // Section scroll 
        $('a.scroll').smoothScroll({
            speed: 800,
            offset: -64
        });
        
        ////Portfolio setup 

        //$('.popup').magnificPopup({
        //    type: 'image',
        //    fixedContentPos: false,
        //    fixedBgPos: false,
        //    mainClass: 'mfp-no-margins mfp-with-zoom',
        //    image: {
        //        verticalFit: true
        //    },
        //    zoom: {
        //        enabled: true,
        //        duration: 300
        //    }
        //});

        //var blockWorks = $('.block-works');
        //$('.popup-youtube, .popup-vimeo').magnificPopup({
        //    disableOn: 700,
        //    type: 'iframe',
        //    mainClass: 'mfp-fade',
        //    removalDelay: 160,
        //    preloader: false,
        //    fixedContentPos: false
        //});


        //$('.filter ').on("click", "li a", function () {
        //    $(this).addClass('active');
        //    $(this).parent().siblings().find('a').removeClass('active');
        //    var filters = $(this).attr('data-filter');
        //    $(this).closest(blockWorks).find('.item').removeClass('disable');

        //    if (filters !== 'all') {
        //        var selected = $(this).closest(blockWorks).find('.item');
        //        for (var i = 0; i < selected.length; i++) {
        //            if (!selected.eq(i).hasClass(filters)) {
        //                selected.eq(i).addClass('disable');
        //            }
        //        }
        //    }

        //    return false;
        //});

       
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