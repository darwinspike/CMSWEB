<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BranchDirectory.ascx.cs" Inherits="CMSWeb.Content.Consumable.BranchDirectory" %>

<link rel="stylesheet" href="../../Assets/css/mp.css">
<div class="mp-parent" data-load="pid=2" style="padding: 0px;">
    <div class="mp-container">
        <input type="hidden" id="SiteID" value="<%= site %>" />
        <input type="hidden" name="address" id="address" value="<%= ws %>">
        <div class="mp-toolbar">
            <div class="mp-options">
                <div class="mp-input">
                    <input type="text" name="query" id="query" />
                    <a href="javascript:;" id="querygo">go</a>
                    <span class="clearfix"></span>
                </div>
                <div class="mp-filter">
                    <select name="searchfilter" id="searchfilter">
                        <option value="q">Search offices</option>
                        <option value="a">Show All Offices List</option>
                    </select>
                    <select name="searchby" id="searchby">
                        <option value="q">by Branch Name</option>
                        <option value="z">by Zip Code</option>
                    </select>
                    <select name="state" id="state">
                        <option value="" selected="selected">Show all states</option>
                        <option value="AL">Alabama</option>
                        <option value="AK">Alaska</option>
                        <option value="AZ">Arizona</option>
                        <option value="AR">Arkansas</option>
                        <option value="CA">California</option>
                        <option value="CO">Colorado</option>
                        <option value="CT">Connecticut</option>
                        <option value="DE">Delaware</option>
                        <option value="DC">District Of Columbia</option>
                        <option value="FL">Florida</option>
                        <option value="GA">Georgia</option>
                        <option value="HI">Hawaii</option>
                        <option value="ID">Idaho</option>
                        <option value="IL">Illinois</option>
                        <option value="IN">Indiana</option>
                        <option value="IA">Iowa</option>
                        <option value="KS">Kansas</option>
                        <option value="KY">Kentucky</option>
                        <option value="LA">Louisiana</option>
                        <option value="ME">Maine</option>
                        <option value="MD">Maryland</option>
                        <option value="MA">Massachusetts</option>
                        <option value="MI">Michigan</option>
                        <option value="MN">Minnesota</option>
                        <option value="MS">Mississippi</option>
                        <option value="MO">Missouri</option>
                        <option value="MT">Montana</option>
                        <option value="NE">Nebraska</option>
                        <option value="NV">Nevada</option>
                        <option value="NH">New Hampshire</option>
                        <option value="NJ">New Jersey</option>
                        <option value="NM">New Mexico</option>
                        <option value="NY">New York</option>
                        <option value="NC">North Carolina</option>
                        <option value="ND">North Dakota</option>
                        <option value="OH">Ohio</option>
                        <option value="OK">Oklahoma</option>
                        <option value="OR">Oregon</option>
                        <option value="PA">Pennsylvania</option>
                        <option value="RI">Rhode Island</option>
                        <option value="SC">South Carolina</option>
                        <option value="SD">South Dakota</option>
                        <option value="TN">Tennessee</option>
                        <option value="TX">Texas</option>
                        <option value="UT">Utah</option>
                        <option value="VT">Vermont</option>
                        <option value="VA">Virginia</option>
                        <option value="WA">Washington</option>
                        <option value="WV">West Virginia</option>
                        <option value="WI">Wisconsin</option>
                        <option value="WY">Wyoming</option>
                    </select>
                    <a class="btnfullscreen" href="javascript:;">fullscreen</a>
                </div>
            </div>
            <div class="mp-list" style="display: none">
                <div class="mp-list-head">Results</div>
                <div class="mp-list-list">
                    <ul>...</ul>
                </div>
            </div>
        </div>
        <div class="mp-map">
            <div id="map-canvas"></div>

            <!-- losearch -->
            <div class="loanSearch" style="background: #f2f2f2; height: 600px; max-height: 600px; overflow: auto; position: relative; z-index: 99; padding: 0px; display: none">
                <div style="position: relative; margin: 0px 20px; padding: 20px 0px">
                    <div class="col-container loresults">
                        <div class="back-btn" style="border-bottom: 1px solid #E9E9E9; padding-bottom: 10px; margin-bottom: 5px;">
                            <a href="<%= link %>&content=LoanDirectory" class="btn btn-secondary pull-right">Find a Loan Proffesional</a>
                            <a href="javascript:;" class="btn btn-secondary" onclick='$(".loanSearch").hide();$("body").hasClass("fullscreen")?$(".mp-container").css({height:"100%"}):$(".mp-container").css({height:"600px"});'>Close</a>
                        </div>
                        <div class="progressbg" style="margin-bottom: 10px">
                            <div class="progressseek">
                            </div>
                        </div>
                        <div class="mp-lo-results">
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <span class="clearfix"></span>
    </div>
</div>
<span class="clearfix"></span>
<script>
    var SiteID = "<%= CMSWeb.Models.Consumable.Sites.Data().SiteAlias %>";
    window.BranchesURL = "../../../../Content/WebServices/GetBranch.ashx";
    window.LOSURL = "../../../../Content/WebServices/GetLoan.ashx";
</script>
<script src="../../Assets/js/chosen.jquery.min.js"></script>
<script src="../../Assets/js/mp.js"></script>
<script>
    //poner true en esta variable si queremos usar el modo estricto de multiples branch en los. TOmar en cuenta que en el content #1 de cada lo debe haber estrictamente o bien vacio o bien una lista de barnches separados por ";" punto-coma.
    multiBranch = false;
    defaultAddress = false;

    //variable, platilla de cada branch en la lista
    mp.template = '<li data-id="{{id}}" class="{{customClass}}">'
            + '<h3>{{name}}</h3>'
            + '<p>{{address}}</p>'
            + '<p>'
            + '<a href="javascript:;" class="set-address" data-name="{{name}}" data-address="{{address}}" data-id="{{id}}">map</a>'
            + '&nbsp; &bull; &nbsp; <a href="javascript:;" data-id="{{id}}" class="loadofficers">Officers</a>'
            + '</p>'
            + '</li>';

    //variable plantilla de loan officer
    mp.lotemplate = '<div class="col-md-4 LO"><div class="panel panel-default"><div class="panel-body"><div class="media">'
                    + '<a class="pull-left" href="GetConsultantPhoto.aspx?id={{photo}}">'
                    + '<img class="media-object" src="GetConsultantPhoto.aspx?id={{photo}}" style="width:64px;min-height:85px;max-height: 85px" /></a>'
                    + '<div class="media-body"><h4 class="media-heading">{{name}}</h4>'
                    + '{{title}}<br>'
                    + '{{phone}}<br>'
                    + 'NMLS {{nmls}}'
                    + '</div></div></div><div class="panel-footer"><div class="btn-group">'
                    + '{{website}}'
                    + '{{email}}'
                    + '</div></div></div></div>';
</script>