function rs() { 0 < $("#lo-bar").length && $("#lo-bar").css({ top: "-200px" }); 768 > $(window).width() ? (0 < $("#lo-bar").length && $("#lo-bar,.loinfo,[role='loinfo']").hide(), 0 < $(".sub-wrap").length && $(".sub-wrap").hide()) : (0 < $("#lo-bar").length && $("#lo-bar,.loinfo,[role='loinfo']").show(), 0 < $(".sub-wrap").length && $(".sub-wrap").show()) } $(window).resize(function () { rs() }); rs();