/*=============================================
Table Of Contents
================================================
1. PRELOADER JS
2. BOOTSTRAP TOOLTIP 
3. MENU JS
4. HOME SLIDER JS 
5. TESTIMONIAL SLIDER
6. BLOG SLIDER
7. BRANCH LOGO
8. COUNTDOWN JS  
9. SECTIONS BACKGROUNDS JS 
10. GOOGLE MAP
11. MIXITUP JS
12. VENOBOX JS
13. WOW ANIMATION JS

Table Of Contents end
 ================================================
 */
(function($) {
    'use strict';

    jQuery(document).on('ready', function() {

        /* 1. PRELOADER JS */

        $(window).on('load', function() {
            $('.loadscreen').fadeOut();
            $('.preloader').delay(350).fadeOut('slow');
        });

        /*END PRELOADER JS*/


        /* 2. BOOTSTRAP TOOLTIP  */

        $('[data-toggle="tooltip"]').tooltip();

        /* END BOOTSTRAP TOOLTIP  */


        /* 3. START MENU JS */

        $(document).on('click', '.navbar-collapse.in', function(e) {
            if ($(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle') {
                $(this).collapse('hide');
            }
        });

        /* END MENU JS */

		
        /* 4. START HOME SLIDER JS */
        $('.carousel').carousel({
            interval: 6000
        });
       /* 4. END HOME SLIDER JS */



        /* 5. START TESTIMONIAL SLIDER  */
        $('.testimonial-slider').owlCarousel({
            autoPlay: false, //Set AutoPlay to 3 seconds
            items: 1,
            itemsDesktop: [1199, 1],
            itemsDesktopSmall: [979, 1],
            itemsTablet: [768, 1],
            pagination: true
        });

        /* END TESTIMONIAL SLIDER  */




        /* 6. START BLOG SLIDER  */
        $('.blog-slider').owlCarousel({
            autoPlay: true, //Set AutoPlay to 3 seconds
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 2],
            itemsTablet: [768, 1],
            pagination: true
        });

        /* END BLOG SLIDER  */




        /* 7. START BRANCH LOGO */
        $('.branch').owlCarousel({
            autoPlay: true, //Set AutoPlay to 3 seconds
            items: 5,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 2],
            pagination: false
        });

        /* END BRANCH LOGO */



        /* 8. START COUNTDOWN JS */
        $('.counter-section').on('inview', function(event, visible, visiblePartX, visiblePartY) {
            if (visible) {
                $(this).find('.timer').each(function() {
                    var $this = $(this);
                    $({
                        Counter: 0
                    }).animate({
                        Counter: $this.text()
                    }, {
                        duration: 2000,
                        easing: 'swing',
                        step: function() {
                            $this.text(Math.ceil(this.Counter));
                        }
                    });
                });
                $(this).unbind('inview');
            }
        });
        /* END COUNTDOWN JS */


        /* 9. SECTIONS BACKGROUNDS JS */

        var pageSection = $("section");
        pageSection.each(function(indx) {

            if ($(this).attr("data-background")) {
                $(this).css("background-image", "url(" + $(this).data("background") + ")");
            }
        });

        /* END SECTIONS BACKGROUNDS */

		
       

    });

    /* 11. START MIXITUP JS */
	
	$('.portfolio-container').mixItUp({
		load: {
			sort: 'order:asc' /* default:asc */
		},
		animation: {
			effects: 'fade rotateX(180deg)', /* fade scale */
			duration: 700 /* 600 */
		},
		selectors: {
			filter: '.filter' /* .filter */
		},
		callbacks: {
			onMixEnd: function(state) {
				console.log(state) /* null */
			}
		}
   });

    /* END MIXITUP JS */


    /* 12. START  VENOBOX JS */
   
    $('.venobox').venobox({
        numeratio: true,
        titleattr: 'data-title',
        infinigall: true
    });
	
	/* 12. END  VENOBOX JS */

    /* 13. START WOW ANIMATION JS */

    new WOW().init();

    /* END WOW ANIMATION JS */


})(jQuery);