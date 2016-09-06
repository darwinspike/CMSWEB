<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Calculators.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Content.Calculators" %>

</div>
</div>

<link rel="stylesheet" type="text/css" href="<%= path %>CSS/calcs.css">
<link rel="stylesheet" type="text/css" href="<%= path %>CSS/calculators.layout.css">

<script type="text/javascript">
    var path = "<%= newpath %>"; 
</script>

<div class="masthead">
    <div class="container">
        <h1 class="sub-text">Calculators</h1>
    </div>
</div>

<div class="container">
    <div class="col-md-12 col-sm-12">
        <div rel="calculator">
            <div class="calc_head">
                Mortgage Calculators  
            </div>
            <span class="clearfix"></span>
            <div class="calculators_menu" id="calcs_menu" style="display: block;">
                <div class="spacer">
                    <ul>
                        <li><a href="javascript:;" class="active" data-calc="1">Mortgage Calculator</a></li>
                        <li><a href="javascript:;" data-calc="2">How much can I afford?</a></li>
                        <li><a href="javascript:;" data-calc="3">Amortization Schedule and Calculator</a></li>
                        <li><a href="javascript:;" data-calc="4">How much are payments?</a></li>
                        <li><a href="javascript:;" data-calc="5">How much can I borrow?</a></li>
                        <li><a href="javascript:;" data-calc="6">How much can I save by consolidating my debt?</a></li>
                        <li><a href="javascript:;" data-calc="7">How much of a down payment do I need?</a></li>
                        <li><a href="javascript:;" data-calc="8">I want to compare 3 different loans</a></li>
                        <li><a href="javascript:;" data-calc="9">Interest only calculator</a></li>
                    </ul>
                    <span class="clearfix"></span>
                </div>
            </div>
            <p class="menu_calc">
                <a href="javascript:;" class="calc_button" id="calcbtn">
                    <i class="list-icon"></i>More Calculators
                </a>
            </p>
        </div>
        <h1 class="calcs_title">Mortgage Calculator</h1>
        <div class="calc_body pure-g-r" id="calcs_body">
        </div>
    </div>
</div>

<script type="text/javascript" src="<%= path %>JS/calculators.fn.js"></script>
