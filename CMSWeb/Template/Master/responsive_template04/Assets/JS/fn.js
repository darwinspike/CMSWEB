var loading = $('.loadingState'), helpers = {};

helpers = {
    format: function () {
        $("body").find("span:contains('#')").each(function () { var a = $(this).html(); switch (!0) { case 0 < a.indexOf("(#"): a = a.substring(0, a.indexOf("(#")); break; case -1 < a.indexOf("(#"): a = a.substring(a.indexOf(")"), a.length); break; case 0 < a.indexOf("#"): a = a.substring(0, a.indexOf("#")); break; case -1 < a.indexOf("#"): a = a.substring(a.indexOf("#"), a.length) } $(this).html(a) });
        $('a[href*="{{link}}"]').each(function (e) {
            var href = $(this).attr('href').replace('{{link}}', link);
            $(this).attr('href', href);
        });
    },
    CurrentSite: {
        hasConsultant: window.hasConsultant || false,
        hasBranch: window.hasBranch || false,
        affixer: function () {

            //if is consultant show sticky bar
            if (this.hasConsultant) {
                $('._affix').affix({
                    offset: {
                        top: function () {
                            return (this.top = $('#top').outerHeight(true) - $('._affix').outerHeight(true))
                        }
                    , bottom: 20
                    }
                });

                //by default scroll
                $(window).on('scroll', function () {
                    if ($(window).scrollTop() > 100) {
                        $('#lo-bar').animate({ opacity: 0.9, top: 0 }, 500);

                    } else {
                        $('#lo-bar').stop(true, false).animate({ opacity: 0, top: -100 }, 500);;

                    }
                });

                //other stuff
                if (window.location.search.indexOf('pageid') > -1) {
                    $("a#A").closest('table').find('a[id]').not('[id*="Top"]')
                        .css({ paddingTop: '120px', display: 'block' });
                }
            }

            //animate body on DOMReady
            $('body').animate({ opacity: 1 }, 1500);
        }
    }
}

$(document).on('ready', function () {
    $(".bigSlide").backstretch(path + "Images/bg.jpg");
    helpers.format();
    helpers.CurrentSite.affixer();

    view = helpers.CurrentSite.hasConsultant ? "losite" : (helpers.CurrentSite.hasBranch ? "branch" : "corporate");
});

$(window).on('load', function () {
    loading.fadeOut();
});