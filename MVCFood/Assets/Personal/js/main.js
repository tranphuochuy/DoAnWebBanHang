"use strict";
//Scroll Top


//Detect Mobile
var isMobile = {
    Android: function () {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function () {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function () {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function () {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function () {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function () {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};

//Like
function likeEf() {
    jQuery('.like-product').on('click', function () {
        jQuery(this).find('i').toggleClass('press');
        jQuery(this).find('i').removeClass('ion-ios-heart-outline');
        jQuery(this).find('span.like-product').toggleClass('press');
        if (jQuery(this).find('i').hasClass('press') || jQuery(this).find('i').hasClass('ion-ios-heart-outline')) {
            jQuery(this).find('.output').html(function (i, val) {
                return val * 1 + 1
            });
            jQuery(this).find('i').addClass('ion-ios-heart');
            jQuery(this).find('i').removeClass('ion-ios-heart-outline');
        } else {
            jQuery(this).find('.output').html(function (i, val) {
                return val * 1 - 1
            });
            jQuery(this).find('i').removeClass('ion-ios-heart');
            jQuery(this).find('i').addClass('ion-ios-heart-outline');

        }
    });
};

//Document Ready
jQuery(document).ready(function ($) {

    //Navigation Sub Menu Triggering
    jQuery('.menu-item-has-children, .page_item_has_children').hover(function () {
        jQuery(this).children('.sub-menu').stop().slideDown(200);
    },
        function () {
            jQuery(this).children('.sub-menu').stop().slideUp(200);
        });

    //Mobile Menu Open/Close
    jQuery('#open-mobile-menu').on('click', function () {
        var self = jQuery(this);
        var mobileMenu = jQuery('.menu-wrap-2');

        if (mobileMenu.hasClass('is-open')) {
            self.removeClass('active');
            mobileMenu.removeClass('is-open');
        } else {
            mobileMenu.addClass('is-open');
            self.addClass('active');
        }
    });

    // Switch class on filter
    var showfilter = jQuery('.works-filter');
    jQuery('button.nav-product').on('click', function () {
        var self = jQuery(this);
        self.toggleClass('open');
        showfilter.toggleClass('open');
    });
 

    //Like
    likeEf();
});

//Window Load
jQuery(window).load(function ($) {

    /*Init Portfolio*/
    var container = jQuery("#work-grid");
    if (container.length > 0) {
        container.isotope({
            layoutMode: 'masonry',
            transitionDuration: '0.7s',
            columnWidth: 60
        });
    };

    //Filter Portfolio
    jQuery('a.filter').on('click', function () {
        var to_filter = jQuery(this).attr('data-filter');
        if (to_filter == 'all') {
            container.isotope({
                filter: '.mix'
            });
        } else {
            container.isotope({
                filter: '.' + to_filter
            });
        }
    });

    //Switch Classes portfolio
    jQuery('.filter').on('click', function () {
        jQuery('a.filter').removeClass('active');
        jQuery(this).addClass('active');
    });
});
