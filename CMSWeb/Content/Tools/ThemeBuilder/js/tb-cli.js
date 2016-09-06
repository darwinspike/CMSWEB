/*
* ThemeBuilder Client, only works for previewing colors
* @autor: Adiel Hercules | @adielhercules
* @copyrights: EllieMae Inc.
*/

//Parse Current URL as a query collection
var $_GET = URI(window.location).query(true);


//Define the TBClient
var TBCli = {};

/*
* Check if the TBMaster sent variables or not
* @return bool
*/
TBCli.hasConnection = function(){
    return ($_GET["advanced"] != undefined || $_GET["baseColor"] != undefined);
};

/*
* Create the preview settings
*/
TBCli.createConnection = function () {
    var _this = this;

    var settings = {
        advanced: false,
        baseColor: '',
        variables: []
    };

    _this.addNotification();
    _this.linksQuery();

    /*
    If basic mode
    */
    if ($_GET["advanced"] == false || $_GET["advanced"] == "0") {
        if ($_GET["baseColor"] != false && $_GET["baseColor"] != null) {
            settings.baseColor = _this.parseColor($_GET["baseColor"]);

            _this.loadStyle(settings);
        }
    } else {
        /*
        If Advanced mode
        */
        for (var i = 0; i < $_GET['variable'].length; i++) {
            var variable = $_GET['variable'][i].split("-_-");
            settings.variables.push([variable[0], _this.parseColor(variable[1])]);
        }
        settings.advanced = true;

        _this.loadStyle(settings);
    }

};

/*
* Add a small notification saying it is a preview
*/
TBCli.addNotification = function () {
    function _resizeNotification() {
        var h = $('body .tbcli-err-msg').length ? $('body .tbcli-err-msg').outerHeight() : 0;
        if (h > 0) {
            $('body').css({ paddingTop: h });
        }
    }

    $('body').append('<div class="tbcli-err-msg" '
        + 'style=" position: fixed; top: 0px; background: #ff0; right: 0px; left: 0px; padding: 0 15px; color: #0A0A0A; '
        + 'text-align: center; z-index: 999999;  opacity: 0.9; min-height: 40px; line-height: 40px;">'
        + 'This is only a preview of your current configuration in the theme editor panel. '
        + '<a href="javascript:;" onclick="window.close()">Click here to close this preview.</a></div>');

    _resizeNotification();
    $(window).resize(function () { _resizeNotification(); });
};


TBCli.linksQuery = function () {
    var $a = $('a');

    $a.each(function () {
        var link = $(this)[0].href;
        var q = URI(link).query(true);
        var _nstring = link;
        var allowed = ['advanced', 'baseColor', 'variable'];

        for (var key in $_GET) {
            if (typeof q.s != "undefined" || typeof q.p != "undefined" || typeof q.page != "undefined") {
                if ($_GET.hasOwnProperty(key) && allowed.indexOf(key) > -1) {
                    if (key == "variable") {
                        for (var i = 0; i < $_GET[key].length; i++) {
                            _nstring += '&' + key + '=' + $_GET[key][i];
                        }
                    } else {
                        _nstring += '&' + key + '=' + $_GET[key];
                    }
                }
            }
        }

        var normalized = URI(_nstring).normalize();
        $(this).attr('href', normalized);
    });
};


/*
* Set scheme based on single base color
* @param String color
*/
TBCli.setBaseColor = function (color) {
    less.modifyVars({ '@baseColor': color });
}

/*
* Set scheme based on all variables
* @param ObjectArray vars
*/
TBCli.changeVariable = function (vars) {
    less.modifyVars(vars);
}

/*
* Parse a color string (add hash to hexa string)
* @param String string
* @return string hexa formatted color
*/
TBCli.parseColor = function (string) {
    return "#" + string.replace(/#/g, "");
};

/*
* Load the settings and mount them, execute the lessparser
* @param Object settings
*/
TBCli.loadStyle = function (settings) {
    if (less != null) {
        setTimeout(function () {
            if (settings.advanced == true || settings.advanced == "true") {
                var variables = {};
                for (var i = 0; i < settings.variables.length; i++) {
                    variables["@" + settings.variables[i][0]] = settings.variables[i][1];
                }

                TBCli.changeVariable(variables);
            } else {
                TBCli.setBaseColor(settings.baseColor);
            }
        }, 100);
    }
}

/*
* Start the preview
*/
function startPreview() {
    if (TBCli.hasConnection()) {
        TBCli.createConnection();
    }
}

document.body.style.opacity = 0.2;
$(window).on('load', function () {
    if (less != null) {
        startPreview();
    }
});