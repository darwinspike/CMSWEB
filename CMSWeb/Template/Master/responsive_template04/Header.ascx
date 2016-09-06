<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Header" %>
<%@ Import Namespace="CMSWeb.Models.Consumable" %>
<%@ Import Namespace="CMSWeb.Models.Tools" %>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">

<script type="text/javascript">

    window.themeOptions = {
        supported: "<%= ThemeBuilders.ThemeBuiderIsSupported() %>",
        SiteId: "<%= MasterGlobal.SiteID() %>",
        SiteAlias: "<%= st.SiteAlias %>",
        clientPath: "",
        stylefile: "<%= path %>Content/GenerateTemplateLess.aspx",
        WebServicesPath: '/Content/GenericHandler/'
    };


    var False = false,
        True = true,
        path = "<%= path %>Assets/",
        link = "<%= link %>",
        site_name = "<%= st.SiteAlias %>",
        mi_path = path,
        hasConsultant = "<%= Consultants.HasConsultant %>",
        hasBranch = "<%= Branchs.HasBranch %>";


    



</script>

<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">

<!-- <link rel="stylesheet" href="<%= path %>Assets/css/layout.css"> -->
<link rel="stylesheet" href="<%= ThemePath %>getStylesheet.aspx?SiteID=f17e866a-0003-4936-a54e-0ca2d28439fb&file=/Assets/CSS/layout.css&s=responsive_template04">

<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesnt work if you view the page via file:// -->
<script src="//oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<script src="//oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>

<div style="background: #fff url(<%= path %>Assets/Images/loading.gif) center center no-repeat; position: fixed; width: 100%; height: 100%; left: 0px; top: 0px; opacity: 0.6; z-index: 999999" class="loadingState"></div>

<% if (Consultants.HasConsultant)
   { %>
<%--RIBBON--%>
<div class="navbar navbar-fixed-top" id="lo-bar" role="banner" style="background: rgb(240, 240, 240); position: fixed !important; opacity: 0; width: 100%; padding-top: 10px;">
    <div class="container">
        <a class="pull-left" href="<%= link %>&content=ContactUsNow">
            <img src="<%= ct.Image %>" class="img-rounded" style="width: 95px; height: 95px; margin: 0px 7px 5px 0px;" />
        </a>
        <h3 style="padding: 0px; margin: 0px; height: 12pt; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; font-size: 12pt; position: relative; top: 0; max-width: 167px; line-height: 12pt;">
            <%= ct.FullName %>
        </h3>
        <small style="padding: 0px 0px 6px; margin: 0px; min-width: 260px; width: 100%; display: block">
            <%= ct.Title %>
            <br />
            NMLS# <%= ct.NMLS %>
            <br />
            <i class="fa fa-phone"></i>
            <%= ct.Phone %>
            <br />
            <%= ct.Email %>
        </small>

        <%--SOCIAL NETWORK--%>
        <div style="position: absolute; opacity: 0.7; width: 75px; margin-top: -5px; border-radius: 0px 0px 4px 4px; margin-left: 100px;">
            <% if (ct.Twitter != "")
    { %>
                        &nbsp; 
                        <a href="<%= ct.Twitter %>" target="_blank">
                            <i class="fa fa-twitter-square"></i>
                        </a>
            <% } %>
            <% if (ct.Linkedin != "")
               { %>
                        &nbsp; 
                        <a href="<%= ct.Linkedin %>" target="_blank">
                            <i class="fa fa-linkedin-square"></i>
                        </a>
            <% } %>
            <% if (ct.FaceBook != "")
               { %>
                        &nbsp; 
                        <a href="<%= ct.FaceBook %>" target="_blank">
                            <i class="fa fa-facebook-square"></i>
                        </a>
            <% } %>
        </div>

        <a id="contact-boton" href="<%= link %>&content=ContactUsNow" style="float: right; padding: 10px 25px; color: #fff; background-color: #023a5b; margin-top: -50px; font: 14px bolder Helvetica,Arial; font-weight: bolder; text-decoration: none;">Contact Me</a>
    </div>
</div>

<%--HEADER--%>
<div class="navbar" role="loinfo" style="background: rgb(240, 240, 240); width: 100%; padding-top: 10px; border: none; border-bottom: 0px !important;">
    <div class="container">
        <%--SOCIAL NETWORK--%>
        <div class="pull-right" style="font-size: 20px; margin-top: 15px;">
            <i class="fa fa-phone"></i>
            <%= ct.CellPhone %>
            <br>
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
            <% } %>
        </div>

        <a class="pull-left" href="<%= link %>&content=ContactUsNow">
            <img src="<%= ct.Image %>" class="img-rounded" style="width: 95px; height: 95px; margin: 0px 7px 5px 0px;" />
        </a>
        <h3 style="padding: 0px; margin: 0px; height: 13pt; white-space: nowrap; text-overflow: ellipsis; font-size: 22pt; position: relative; top: 0; max-width: 167px;">
            <%= ct.FullName %>
        </h3>
        <small style="padding: 15px 0px 10px; margin: 0px; min-width: 260px; width: 100%; display: block; font-size: 15px;">
            <%= ct.Title %>
            <br />
            NMLS# <%= ct.NMLS %>
            <br />
            <a href="mailto:<%= ct.Email %>">
                <%= ct.Email %>
            </a>
        </small>
    </div>
</div>
<% } %>

<div class="navbar navbar-static-top" id="top" role="banner">
    <div class="container">

        <%--MENU BUILDER--%>
        <div class="collapse navbar-collapse bs-navbar-collapse" role="navigation">
            <div class="navbar-right">
                <% 
                    Response.Write(MenuBuilders.menuBuilderHeader());
                    %>
            </div>
            <span class="clearfix"></span>
        </div>

        <%--COMPANY LOGO--%>
        <div class="navbar-header" style="margin: 0px">
            <button class="navbar-toggle" type="button" data-toggle="collapse" data-target=".bs-navbar-collapse">
                <span class="sr-only">Toggle navigation</span>
                <i class="fa fa-bars"></i>
            </button>
            <a href="<%= link %>" class="navbar-brand">
                <%= cp.CompanyImage %>
            </a>
        </div>

        <span class="clearfix"></span>
    </div>
</div>

<% if ((Request.QueryString["content"] ?? "").Length > 0 || (Request.QueryString["pageid"] ?? "").Length > 0)
   { %>
<div class="pagebody">
    <div class="container">
        <% }
   else
   { %>
        <%--HEADER SLIDER--%>
        <div class="bigSlide">
            <div class="container text-center">
                <h1>&bull; WELCOME TO ELLIEMAE &bull;</h1>
                <h2>Financing options to fit your lifestyle</h2>
                <div class="features row">

                    <%--FIRST BLOCK--%>
                    <div class="col-sm-4">
                        <div class="col-sm-12 feature-item">
                            <div class="feature-img">
                                <i class="fa fa-star"></i>
                            </div>
                            <div class="feature-info">
                                <h3>About <%= IterateConjuction() %></h3>
                                <% if (Consultants.HasConsultant)
                                   {
                                       string bio = ct.Biography;
                                           Response.Write(bio.Substring(0, 100));
                                       }
                                   else
                                   {
                                       Response.Write(CMSWeb.Models.Temporary.ContentBuilders.BodyWithTags(25));
                                   } 
                                %>
                            </div>
                        </div>
                    </div>

                    <%--SECUND BLOCK--%>
                    <div class="col-sm-4">
                        <div class="col-sm-12 feature-item">
                            <div class="feature-img">
                                <i class="fa fa-flag"></i>
                            </div>
                            <div class="feature-info">
                                <%=  CMSWeb.Models.Temporary.ContentBuilders.BodyWithTags(26) %>
                            </div>
                        </div>
                    </div>

                    <%--THIRD BLOCK--%>
                    <div class="col-sm-4">
                        <div class="col-sm-12 feature-item">
                            <div class="feature-img">
                                <i class="fa fa-cog"></i>
                            </div>
                            <div class="feature-info">
                                <%= CMSWeb.Models.Temporary.ContentBuilders.BodyWithTags(27) %>
                            </div>
                        </div>
                    </div>

                    <span class="clearfix"></span>
                </div>
            </div>
        </div>

        <%--HEADER SEARCH--%>
        <div class="bigSearch masthead" style="text-align: left; padding: 15px;">
            <div class="container">
                <div class="row">
                    <div class="col-sm-4 form-group">
                        <label>Name:</label>
                        <input ID="FirstName" runat="server" class="form-control" type="text" />
                    </div>
                    <div class="col-sm-4 form-group">
                        <label>Email:</label>
                        <input ID="EmailForm" runat="server" class="form-control" type="text" />
                    </div>
                    <div class="col-sm-4 form-group">
                        <label class="block-d">&nbsp;</label>
                        <input ID="Button1" runat="server" value="Apply Now" OnClick="search_Click" class="btn btn-secondary" type="button"  />
                    </div>
                </div>
            </div>
        </div>

        <div class="pagebody hp">
            <div class="container text-center">
                <% } %>
