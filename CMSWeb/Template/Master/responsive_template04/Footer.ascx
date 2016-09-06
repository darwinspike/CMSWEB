<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Footer" %>
<%@ Import Namespace="CMSWeb.Models.Consumable" %>
<%@ Import Namespace="CMSWeb.Models.Tools" %>
<div class="clearfix"></div>
</div>
</div>
</div>

<div class="footer" <% if ((Request.QueryString["pageid"] ?? "").Length > 0 && Request.QueryString["content"] == "BranchDirectory")
                       { %>
    style="margin-top: 0px" <% } %>>
    <div class="container">
        <div class="row">
            <%--ADDRESS & PHONE--%>
            <div class="col-sm-6">
                <%= direction %>
            </div>

            <%--CONTENT 23 (FOOTER)--%>
            <div class="col-sm-6">
                <%=  ContentBuilders.BodyWithTags(23) %>
            </div>
        </div>

        <div class="row">
            <%--SOCIAL NETWORK--%>
            <div class="col-sm-10 text-right">
                <% if (Consultants.HasConsultant)
                   { %>
                <% if (ct.Twitter != "")
                   { %>
                        &nbsp;
                        <a href="<%= ct.Twitter %>" target="_blank">
                            <i class="fa fa-2x fa-twitter" style="font-size: 30px;"></i>
                        </a>
                <% } %>
                <% if (ct.Linkedin != "")
                   { %>
                        &nbsp;
                        <a href="<%= ct.Linkedin %>" target="_blank">
                            <i class="fa fa-2x fa-linkedin" style="font-size: 30px;"></i>
                        </a>
                <% } %>
                <% if (ct.FaceBook != "")
                   { %>
                        &nbsp; 
                        <a href="<%= ct.FaceBook %>" target="_blank">
                            <i class="fa fa-2x fa-facebook" style="font-size: 30px;"></i>
                        </a>
                <% }
                   } %>
            </div>
        </div>
    </div>

    <div class="divisor"></div>

    <div class="container containHeler">
        <span class="clearfix"></span>
        <%--FOOTER MENU--%>
        <p class="pull-right">
            <a href="<%= link %>&content=SiteMap">Site Map</a>&nbsp;&nbsp;&nbsp;
            <a href="#">Employee Login</a>&nbsp;&nbsp;&nbsp;
            <a href="<%= link %>&pageid=22">Privacy Policy</a>
        </p>

        <%--COPYRIGHT--%>
        <p>Copyright <%= cp.CompanyName %>. All rights reserved.</p>
    </div>
</div>

<script src="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
<script src="<%= master_path %>Assets/JS/jquery.backstretch.min.js"></script>
<script src="<%= master_path %>Assets/JS/fn.js"></script>
<script src="<%= master_path %>Assets/JS/global.js"></script>



<!-- THEBUILDER CLIENT PREVIEW -->
<% if (Request.QueryString["advanced"] != null || Request.QueryString["baseColor"] != null){ %>
<link rel="stylesheet/less" href="<%= master_path %>Content/GenerateTemplateLess.aspx">
<script>
    less = { env: "development", async: false, fileAsync: false };
</script>
<script src="<%= ThemePath %>js/less.js"></script>
<script src="<%= ThemePath %>js/URI.min.js"></script>
<script src="<%= ThemePath %>js/tb-cli.js"></script>
<% } %>
