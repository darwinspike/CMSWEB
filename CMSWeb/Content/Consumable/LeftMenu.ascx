<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftMenu.ascx.cs" Inherits="CMSWeb.Content.Consumable.LeftMenu" %>

<% if (Regex.IsMatch(Request.QueryString["p"] ?? "", "UcCalcList", RegexOptions.IgnoreCase))
{ %>
<div class="boxinsidepage" style="height: 600px;">
    <p class="left_h2">RESOURCES</p>
    <ul>
        <li>
            <a href="<%= (link + "&Content=crm_for_sales_automation") %>">Calculators</a>
            <ul>
                <li><a href="#">Mortgage Approxiator</a></li>
                <li><a href="#">How Much Can I Afford?</a></li>
                <li><a href="#">How Much Can I Borrow?</a></li>
                <li><a href="#">Calculate Down Payment</a></li>
                <li><a href="#">Amoritization Scheduler</a></li>
                <li><a href="#">What's My Monthly Payment?</a></li>
                <li><a href="#">How Much Can I Save By Consolidating?</a></li>
                <li><a href="#">Interest Only Calculator</a></li>
            </ul>
        </li>
        <li><a href="<%= (link + "&Content=recruitting") %>">Printable Forms</a></li>
        <li><a href="<%= (link + "&Content=b2b") %>">Check Loan Status</a></li>
        <li><a href="<%= (link + "&Content=email") %>">Client Login</a></li>
    </ul>
<% }
else if (Regex.IsMatch(Request.QueryString["content"] ?? "", "aboutus|careers|licensing|legal-privacy|contactus", RegexOptions.IgnoreCase))
{ %>
    <div class="boxinsidepage">
        <p class="left_h2">OUR COMPANY</p>
        <ul>
            <li><a href="<%= (link + "&Content=BranchDirectory&branch=true") %>">Find an Office</a></li>
            <li><a href="<%= (link + "&Content=CareersNow") %>">Careers</a></li>
            <li><a href="<%= (link + "&Content=licensing") %>">License Information</a></li>
            <li><a href="<%= (link + "&Content=ContactUsNow") %>">Contact Us</a></li>
        </ul>
<% }
else if (Regex.IsMatch(Request.QueryString["content"] ?? "", "box-realtor|box-builders|box-loanoffice", RegexOptions.IgnoreCase))
{ %>
        <div class="boxinsidepage">
<% }
else if (Regex.IsMatch(Request.QueryString["content"] ?? "", "resources|printable-forms|SC_CALCLIST|check-loan", RegexOptions.IgnoreCase))
{ %>
            <div class="boxinsidepage">
                <p class="left_h2">RESOURCES</p>
                <ul>
                    <li><a href="<%= (link + "&Content=SC_CALCLIST") %>">Calculators</a></li>
                    <li><a href="<%= (link + "&Content=printable-forms") %>">Printable Forms</a></li>
                </ul>
<% }
else if (Regex.IsMatch(Request.QueryString["content"] ?? "", "licensing", RegexOptions.IgnoreCase))
{ %>
                <div class="boxinsidepage">
                    <p class="left_h2">COMPANY</p>
                    <ul>
                        <li><a href="<%= (link + "&Content=story") %>">The Catalyst Story</a></li>
                        <li><a href="<%= (link + "&Content=CareersNow") %>">Careers</a></li>
                        <li><a href="<%= (link + "&Content=LicenseInformation") %>">License Information</a></li>
                        <li><a href="<%= (link + "&Content=ContactUsNow") %>">Contact Us</a></li>
                    </ul>
<% }
if (Regex.IsMatch(Request.QueryString["p"] ?? "", "press_room", RegexOptions.IgnoreCase))
{ %>
                    <div style="margin: 0px; width: 250px; float: left;">
                        <p class="left_h2">APPLYING FOR A LOAN</p>
                        <div style="font-family: arial; font-weight: bold; color: rgb(61, 61, 61); width: 168px; margin-left: 75px; height: 105px; padding: 0px 0px 0px 5px;">
                            <% if (los)
                            { %>
                                <%= phone %>
                            <% }
                            else
                            { %>
                            <img src="<%= path %>image/t.png" class="t" />
                            855-347-7691
                            <% } %>
                        </div>
                    </div>
                </div>


<% }
                       else if (Regex.IsMatch(Request.QueryString["p"] ?? "", "check-loan|moving-packing", RegexOptions.IgnoreCase))
                       { %>
                <div style="margin: 0px; width: 250px; float: left;">

                    <p class="left_h2">APPLYING FOR A LOAN</p>
                    <div style="font-family: arial; font-weight: bold; color: rgb(61, 61, 61); width: 168px; margin-left: 75px; height: 105px; padding: 0px 0px 0px 5px;">
                        <% if (los)
                           { %>
                        <%= phone %>
                        <% }
                           else
                           { %>
                        <img src="<%= path %>image/t.png" class="t" />
                        855-347-7691
        <% } %>
                    </div>

                    <% if (Regex.IsMatch(Request.QueryString["content"] ?? "", "box-loanoffice|box-realtor|box-builders", RegexOptions.IgnoreCase))
                       { %><% }
                       else
                       {%>
                    <a href="<%= ( link + "&content=ApplyNow") %>">
                        <img src="<%= path %>image/btn-cta.png" class="apply-now-button" /></a>
                    <% } %>
                </div>
            </div>

<% }
               
               
               
   
   
   
   
   
   
   
   
               
                       else
                       { %>
            <div>


                <% if (Regex.IsMatch(Request.QueryString["contetn"] ?? "", "box-realtor|box-builders|box-loanoffice|moving-packing", RegexOptions.IgnoreCase))
                   { %> <%}
                   else
                   {%>
                <hr style="background: #dedede; width: 230px;">
                <% } %>

                <p class="left_h2">APPLYING FOR A LOAN</p>
                <div style="font-family: arial; font-weight: bold; color: rgb(61, 61, 61); width: 168px; margin-left: 75px; height: 105px; padding: 0px 0px 0px 5px;">
                    <% if (NewClientSites.UIF.CurrentLO.HasLo)
                       { %>
                    <%= NewClientSites.UIF.CurrentLO.Phone %>
                    <% }
                       else
                       { %>
                    <img src="<%= path %>image/t.png" class="t" />
                    855-347-7691
        <% } %>
                </div>

                <% if (Regex.IsMatch(Request.QueryString["content"] ?? "", "box-loanoffice|box-realtor|box-builders", RegexOptions.IgnoreCase))
                   { %><% }
                   else
                   {%>


                <!-- if LO -->
                <% if (lo.HasLo)
                   { %>

                <% if (NewClientSites.UIF.CurrentLO.ApplyNowLink.Length < 5)
                   { %>
                <a href="<%= NewClientSites.UIF.GetLink("loancenter.ascx") %>">
                    <img src="<%= path %>image/btn-cta.png" class="apply-now-button" /></a>
                <%}
                   else
                   {%>
                <a href="<%= NewClientSites.UIF.GetLink("loancenter.ascx") %>">
                    <img src="<%= path %>image/btn-cta.png" class="apply-now-button" /></a>
                <% } %>

                <%}
                   else
                   { %>
                <a href="<%= NewClientSites.UIF.GetLink("loancenter.ascx") %>">
                    <img src="<%= path %>image/btn-cta.png" class="apply-now-button" /></a>
                <% } %>

                <% } %>
            </div>
        </div>

        <% } %>
