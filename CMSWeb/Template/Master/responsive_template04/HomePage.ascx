<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePage.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.HomePage" %>
<%@ Import Namespace="CMSWeb.Models.Tools" %>




<%@ Register TagPrefix="uc" TagName="Content" Src="./Content/Content.ascx" %>
<%@ Register TagPrefix="uc" TagName="Page" Src="./Content/GetPages.ascx" %>
<%@ Register TagPrefix="uc" TagName="Mini" Src="../../../Content/Forms/MiniApp.ascx" %>



<% if (Request.QueryString["pageid"] != null)
   { %>
<uc:Content ID="GetContent" runat="server" />
<% }
   else if (Request.QueryString["content"] != null)
   { %>
<uc:Page ID="GetPage" runat="server" />
<% }
   else
   { %>

<uc:Mini ID="MiniApp" runat="server" />

    <%= CMSWeb.Models.Temporary.ContentBuilders.BodyWithTags(1) %>
<% } %>