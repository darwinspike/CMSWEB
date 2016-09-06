<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Content.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Content.Content" %>

<%@ Register TagPrefix="uc" TagName="Quick" Src="../../../../Content/Forms/QuickApp.ascx" %>

</div>
</div>

<div class="masthead" data-image="<%= path %>image/one.jpg">
    <div class="container">
        <h1 class="sub-text" id="page-title"><%= contentTitle %></h1>
    </div>
</div>
<div class="section-colored" style="margin-top: 0px;">
    <div class="container">
        <% if (((Request.QueryString["showsidebar"] ?? "").Length > 0 && Request.QueryString["showsidebar"] != "false") || Request.QueryString["showsidebar"] == "true")
           { %>
        <div class="col-md-4 col-sm-4 scroll-action" data-static-top="100">
            <div class="col-md-12" style="background: #fff; padding-top: 20px; padding-bottom: 20px">
                <div><%= sidebarContent %></div>
            </div>
        </div>
        <div class="col-md-8 col-sm-8">
            <% }
           else if (((Request.QueryString["qform"] ?? "").Length > 0 && Request.QueryString["qform"] != "false") || Request.QueryString["qform"] == "true")
           { %>
            <div class="col-md-8 col-sm-8">
                <% }
           else
           { %>
                <div class="col-sm-12">
                    <% } %>
                    <div id="page-content">
                        <%= contentBody %>
                    </div>
                </div>
                <% if (((Request.QueryString["qform"] ?? "").Length > 0 && Request.QueryString["qform"] != "false") || Request.QueryString["qform"] == "true")
                   { %>
                <div class="col-md-4 col-sm-4 scroll-action quickAppForm-wrapper">
                    <uc:Quick ID="QuickApp" runat="server" />
                </div>
                <% } %>
                <div class="separator"></div>
                <div class="or-spacer">
                    <div class="mask"></div>
                    <span><i>go</i></span>
                </div>
                <% if (CMSWeb.Models.Consumable.Consultants.HasConsultant)
                   { %>
                <div class="text-center">
                    <h3>Get in Touch with me!&nbsp;&nbsp;<a href="<%= link %>&content=ContactUsNow" class="btn btn-primary btn-lg">Contact me</a></h3>
                </div>
                <% }
                   else
                   { %>
                <div class="text-center">
                    <h3>Get in Touch with us!&nbsp;&nbsp;<a href="<%= link %>&content=ContactUsNow" class="btn btn-primary btn-lg">Contact Us</a></h3>
                </div>
                <% } %>
            </div>
