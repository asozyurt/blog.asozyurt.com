(function ($) {
    "use strict";
    // Document ready

    $(document).ready(function () {

        // Sticky nav
        var header = $('.main-nav'),
            pos = header.offset();

        $(window).scroll(function () {
            if ($(this).scrollTop() > pos.top - 20 && header.hasClass('default')) {
                header.fadeOut('fast', function () {
                    $(this).removeClass('default').addClass('switched-header').slideDown(200);
                });
            } else if ($(this).scrollTop() <= pos.top - 20 && header.hasClass('switched-header')) {
                header.slideUp(200, function () {
                    $(this).removeClass('switched-header').addClass('default').fadeIn(200);
                });
            }
        });

        // Section scroll 

        $('a.scroll').smoothScroll({
            speed: 800,
            offset: -64
        });

        // Site slider 

        $("#client-carousel").owlCarousel({
            items: 6,
            itemsDesktop: [1199, 4],
            itemsDesktopSmall: [979, 4],
            itemsTablet: [768, 3],
            itemsTabletSmall: [550, 3],
            itemsMobile: [480, 2],
            pagination: false,
            autoPlay: true
        });

        $("#block-slider").owlCarousel({
            slideSpeed: 300,
            paginationSpeed: 400,
            responsiveRefreshRate: 200,
            responsiveBaseWidth: window,
            pagination: false,
            singleItem: true,
            navigation: true,
            navigationText: ["<span class='icon-left-open-big'></span>", "<span class='icon-right-open-big'></span>"]
        });

        // Background image setup

        var backgroundImage = $(".block-background-image");
        for (var i = 0; i < backgroundImage.length; i++) {
            if (backgroundImage.eq(i).attr("data-background")) {
                backgroundImage.eq(i).css("background-image", "url(" + backgroundImage.eq(i).data("background") + ")");
            }
        }

        //Portfolio setup 

        $('.popup').magnificPopup({
            type: 'image',
            fixedContentPos: false,
            fixedBgPos: false,
            mainClass: 'mfp-no-margins mfp-with-zoom',
            image: {
                verticalFit: true
            },
            zoom: {
                enabled: true,
                duration: 300
            }
        });

        var blockWorks = $('.block-works');
        $('.popup-youtube, .popup-vimeo').magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });


        $('.filter ').on("click", "li a", function () {
            $(this).addClass('active');
            $(this).parent().siblings().find('a').removeClass('active');
            var filters = $(this).attr('data-filter');
            $(this).closest(blockWorks).find('.item').removeClass('disable');

            if (filters !== 'all') {
                var selected = $(this).closest(blockWorks).find('.item');
                for (var i = 0; i < selected.length; i++) {
                    if (!selected.eq(i).hasClass(filters)) {
                        selected.eq(i).addClass('disable');
                    }
                }
            }

            return false;
        });

        // Search input

        $('.search-form i').on("click", function () {
            $(this).closest('.search-form').find('input[type="text"]').focus();
            if ($(this).closest('.search-form').find('input[type="text"]').val()) {
                $(this).closest('.search-form').find('input[type="submit"]').trigger('click');
            }
        });

        // Form validation 

        let inputName = $('input#name');
        let inputEmail = $('input#email');
        let textArea = $('textarea#message');
        let subject = $('input#subject');
        let contactForm = $('.contact-form');

        $('.submit').on("click", function () {

            $('#success').fadeOut('fast');
            $('#error').fadeOut('fast');

            inputName.removeClass("errorForm");
            textArea.removeClass("errorForm");
            inputEmail.removeClass("errorForm");

            let error = false;
            let name = inputName.val();
            if (name === "" || name === " ") {
                error = true;
                inputName.addClass("errorForm");
            }


            var msg = textArea.val();
            if (msg === "" || msg === " ") {
                error = true;
                textArea.addClass("errorForm");

            }

            var email_compare = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i;
            var email = inputEmail.val();
            if (email === "" || email === " ") {
                inputEmail.addClass("errorForm");
                error = true;
            } else if (!email_compare.test(email)) {
                inputEmail.addClass("errorForm");
                error = true;
            }

            if (error) {
                return false;
            }

            let data_string = { 'Name': name, 'Email': email, 'Message': msg, 'Subject': subject.val() };

            $.ajax({
                type: "POST",
                url: 'https://api.asozyurt.com/private/main/ContactMe',
                dataType: 'json',
                data: JSON.stringify(data_string),
                crossDomain: true,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.Status === 'OK') {
                        $('#success').fadeIn('slow');
                    }
                    else {
                        $('#error').fadeIn('slow');
                    }
                }
            });

            return false;
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