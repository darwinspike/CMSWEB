<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Content.SiteMap" %>

<%@ Import Namespace="CMSWeb.Models.Consumable" %>
<%@ Import Namespace="CMSWeb.Models.Tools" %>

<link rel="stylesheet" href="<%= path %>Assets/CSS/sitemap.css">
</div>
</div>

<div class="masthead">
    <div class="container">
        <h1 class="sub-text">Site Map</h1>
    </div>
</div>
<div class="section-colored" style="margin-top: 0px;">
<div class="container">
    <div class="addSpace"></div>

    <div class="sitemap">
        <ul id="utilityNav">
            <li><a href="<%= link %>&content=ApplyNow">Apply Now</a></li>
            <li><a href="#" type="_blank">Manage Account</a></li>
            <li><a href="<%= link %>&content=SiteMap">Site Map</a></li>
        </ul>

        <%= MenuBuilders.menuBuilderSiteMap() %>
    </div>

    <span style="display: block; clear: both;"></span>

