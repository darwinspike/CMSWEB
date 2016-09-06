<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoanDirectory.ascx.cs" Inherits="CMSWeb.Content.Consumable.LoanDirectory" %>


<link rel="stylesheet" href="../../Assets/css/lo.layout.css">

<div class="container">
    <div class="lo-container">
        <div class="lo-toolbar lo-row">
            <div class="lo-col-4">
                <select id="lo-filter">
                    <optgroup label="All Loan Officers">
                        <option value="0">All Officers</option>
                        <option value="1">Search Officers</option>
                    </optgroup>
                </select>
            </div>
            <div class="lo-col-4 lo-search">
                <input type="text" name="query" id="query" class="lo-col-12">
                <input type="hidden" id="SiteID" value="<%= site %>" />
                <span>&nbsp;</span>
            </div>
        </div>
        <div class="lo-col-4">&nbsp;</div>
        <span class="clearfix"></span>
        <div class="lo-progressbar">
            <div class="lo-progressbarseek"></div>
        </div>
        <div class="lo-row lo-results">
            <div class="lo-results-list">
                <p>Working..</p>
            </div>
            <span class="clearfix"></span>
            <a href="javascript:lo.showlos(12);" rel="moreresults" style="display: none">Show more</a>
        </div>
        <span class="clearfix"></span>
    </div>
</div>


<script>
    var SiteID = "<%= CMSWeb.Models.Consumable.Sites.Data().SiteAlias %>";
    window.BranchesURL = "../../../../Content/WebServices/GetBranch.ashx";
    window.LOSURL = "../../../../Content/WebServices/GetLoan.ashx";
</script>



<script src="../../Assets/js/chosen.jquery.min.js"></script>
<script src="../../Assets/js/lo.fn.js"></script>
<script>
    lo._d.detailer = '<div class="clearfix lo-detailer">'
                + '<a href="javascript:void(0);" class="lo-pull-right" rel="close">&times;</a>'
                + '<div class="lo-col-4">'
                + '<div class="lo-detailer-head">'
                + '<span>{{name}}</span>'
                + '{{title}}<br />'
                + '{{website}} {{email}}'
                + '</div><div class="lo-detailer-subhead">'
                + '{{nmls}} &bull; {{phone}}'
                + '</div>'
                + '{{address}}'
                + '</div><div class="lo-col-4"><h4>About</h4>'
                + '<div class="lo-bio"></div>'
                + '</div><div class="lo-col-4"><h4>&nbsp;</h4><div class="lo-bio"></div>'
                + '<span class="clearfix"></span><br /><br /><a href="javascript:void(0);" rel="readmore">Read more</a>'
                + '</div><span class="clearfix"></span></div>';

    lo._d.html = '<div class="lo-col-4 lo-col-xs-6 lo-officer-item" data-name="{{name}}"><div class="lo-col-12 lo-officer">'
                + '<a href="javascript:void(0);">'
                + '<div class="lo-officer-data">'
                + '<input type="hidden" name="fullname" value="{{name}}" />'
                + '<input type="hidden" name="title" value="{{title}}" />'
                + '<input type="hidden" name="phone" value="{{phone}}" />'
                + '<input type="hidden" name="nmls" value="{{nmls}}" />'
                + '<input type="hidden" name="address" value="{{address}}" />'
                + '<input type="hidden" name="addressline" value="{{addressline}}" />'
                + '<input type="hidden" name="city" value="{{city}}" />'
                + '<input type="hidden" name="state" value="{{state}}" />'
                + '<input type="hidden" name="zipcode" value="{{zipcode}}" />'
                + '<input type="hidden" name="bio" value="{{bio}}" />'
                + '<input type="hidden" name="email" value="{{email}}">'
                + '<input type="hidden" name="website" value="{{website}}">'
                + '</div>'
                + '<div class="lo-img"><img src="Content/WebServices/getImage.ashx?ID={{id}}&type=2"></div>'
                + '<span>{{name}}&nbsp;</span>'
                + '{{title}}&nbsp;<br />'
                + '</a></div></div>';
</script>


