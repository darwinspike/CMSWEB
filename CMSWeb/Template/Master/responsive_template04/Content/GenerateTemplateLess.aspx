﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateTemplateLess.aspx.cs" Inherits="CMSWeb.Template.Master.responsive_template04.Content.GenerateTemplateLess" %>

/* *
* Base Layout Template
* Template Name: <%=  CMSWeb.Models.Consumable.Sites.Data().SiteAlias %>
* Notes: this file is a less file, with aspx rendering because asp.net in this server doesnt allow to use .less files in 
* your website
* */
/*
- http://google.com
URL: <%= Base64Encode("http://google.com") %>
URL: <%= EncodeURI("aHR0cDovL2dvb2dsZS5jb20=") %>
*/

.border-radius(@radius: 5px) {
    -webkit-border-radius: @radius;
    -moz-border-radius: @radius;
    border-radius: @radius;
}

@baseColor: #0069AA; /* blue color scheme */

@mainMenuColor: @baseColor;
@mainMenuColor_hover: @baseColor_darkenVariation;

@h1_color: @baseColor;
@h2_color: desaturate(darken(@baseColor, 10%), 30%);
@h3_color:desaturate(darken(@baseColor, 5%), 20%);

@masthead_bg: desaturate(darken(@baseColor, 10%), 40%);
@subnav_bg: desaturate(darken(@baseColor, 30%), 40%);

@baseColor_darkenVariation: desaturate(darken(@baseColor, 10%), 40%);


@import url("//fonts.googleapis.com/css?family=Open+Sans:700,400");

body{
    background-color: #fff;
    font-family: "Open Sans", "Source Sans Pro", tahoma, serif, arial;
}

a{
    color: @baseColor;
    &:hover, &focus, &:active{
        color: darken(@baseColor, 5%);
    }
}

.hp{
    background-color: #f6f6f6;
}

.clearfix{
    display: block;
    clear: both;
}

.navbar{
    background-color: #fff;
    min-height: 100px;
    padding: 10px 0;
    margin-bottom: 0;
}

.navbar-header{
    display: block;
    width: 100%;
    text-align: center;
    float: none;
    clear: both;
    width: 228px;
    margin: 0 auto;
    position: relative;
    z-index: 1;
}

.container>.navbar-header, .container-fluid>.navbar-header, .container>.navbar-collapse, .container-fluid>.navbar-collapse {
    margin-right: auto;
    margin-left: auto;
}

.navbar-nav>li>a {
    line-height: 45px;
    font-size: 12pt;
    text-transform: uppercase;
    padding-left: 12px;
    padding-right: 12px;
    color: @mainMenuColor;
    &:hover, &:focus{
        color: @mainMenuColor_hover;
    }
}

.nav>li>a:hover, .nav>li>a:focus, .nav>li.open>a, .nav>li.dropdow.open>a, .nav .open>a, .nav .open>a:hover, .nav .open>a:focus {
    text-decoration: none;
    background-color: #fff;
}

.dropdown-menu>li>a {
    display: block;
    padding: 5px 20px;
    clear: both;
    font-weight: 400;
    line-height: 1.42857143;
    white-space: nowrap;
    font-size: 13pt;

    color: @mainMenuColor;
    &:hover, &:focus{
        color: @mainMenuColor_hover;
    }
}

.navbar-nav>li>.dropdown-menu {
    margin-top: 15px;
}


.progressbg{
    background-color: #fff;
    border-radius: 5px;
}

.progressseek{
    height: 5px;
    background-color: #ffb400;
    border-radius: 5px;
    -webkit-transition: all .4s ease;
    -moz-transition: all .4s ease;
    -ms-transition: all .4s ease;
    -o-transition: all .4s ease;
    transition: all .4s ease;
    width: 10%;
    margin-top: 1px;
}


.container{
    position: relative;
}

.masthead .container,.navbar .container,.footer .container{
    background-color: transparent;
}

.navbar-collapse{
    position: absolute;
    width: 100%;
}

.navbar-header .navbar-brand{
    display: block;
    float: none;
}

.navbar-header .navbar-brand img{
    display: block;
    margin: 0 auto;
}

.section-colored{
    background-color: #f2f2f2;
    padding: 10px 0px;
}

.bigSlide{
    padding: 30px 0;
    background-color: #e1e1e1;
}

h1{
    color: @h1_color;
    font: 700 28px/26px "Source Sans Pro", tahoma, serif, arial;
}

h2{
    color: @h2_color;
    font: 400 23px/20px "Source Sans Pro", tahoma, serif, arial;
}

h3, h4, h5, h6{
    color: @h3_color;
}

.bigSlide h2{
    color: #ffffff
}
.pagebody{
    margin: 0px 0 0px;
}

.features{
    margin: 100px 0 50px;
}

.features .feature-item{
    background-color: #5384a3;
    background-color: rgba(83,132,163,0.8);
    padding: 0;
    margin-top: 10px;
    -webkit-transition: all .4s ease;
    -moz-transition: all .4s ease;
    -ms-transition: all .4s ease;
    -o-transition: all .4s ease;
    transition: all .4s ease;
}

.features .feature-item:hover{
    -webkit-transform: scale(1.1);
    -moz-transform: scale(1.1);
    -ms-transform: scale(1.1);
    -o-transform: scale(1.1);
    transform: scale(1.1);
}

.features .feature-item .feature-info{
    background-color: #fff;
    padding: 10px;
    margin: 0;
}

.features .feature-item .feature-info p{
    margin: 0;
    font: 500 16px/18px "Source Sans Pro", tahoma, serif, arial;
    color: #003658;
    min-height: 60px;
}

.features .feature-item .feature-info h3{
    margin: 0;
    color: #0069aa;
    font: 700 20px/18px "Source Sans Pro", tahoma, serif, arial;
}

.features .feature-item .feature-img .fa{
    font-size: 50pt;
    width: 100px;
    height: 100px;
    text-align: center;
    background-color: #c75c5c;
    margin: 20px 0px;
    border-radius: 50%;
    color: #f5cf87;
    line-height: 100px;
    text-shadow: 0px 3px 1px #803C3C;
}

.navbar-toggle{
    background-color: #B1B1B1;
}

.bigSearch{
    background-color: #0069aa;
    padding: 10px 0;
    color: #fff;
}

*[rel="async_box"]{
    padding: 40px 20px;
}

.form-control{
    padding: 10px 12px;
    height: 44px;
    border: 1px solid #424D63;
}

.bigSearch label{
    text-shadow: 0px 2px 1px #465A6B;
}

.btn{

    &.btn-primary{
        background-color: @baseColor;
        border-color: @baseColor;
        border-radius: 3px;
    }


    &.btn-secondary{
        background-color: @baseColor;
        color: #fff;
        text-transform: uppercase;
        font-weight: 700;
        padding-left: 20px;
        padding-right: 20px;
        border-radius:3px !important;

        &:hover, &:active, &:focus{
            color: #fff;
            background-color: darken(@baseColor, 5%);
        }

    }


}

.block-d{
    display: block;
}


.footer{
    background-color: @baseColor_darkenVariation;
    color: #ffffff;
    padding: 40px 0 20px;
}

.divisor{
    margin: 20px 0;
    height: 1px;
    background-color: #28487F;
}

.footer .divisor{
    background-color: darken(@baseColor_darkenVariation, 10);
}

.footer a{
    font-weight: bolder;
    color: #fff;
}

.footer ul{
    list-style: none;
    margin: 0;
    padding: 0;

    li{
        float: left;
        position: relative;

        a{
            display: inline-block;
            padding: 5px 10px;
            text-transform: uppercase;
            color: #fff;
        }

        ul{
            position: absolute;
            left: 0;
            bottom: 100%;
            width: 150px;
            background-color: #fff;
            display: none;
            li{
                float: none;
                display: block;
                a{
                    display: block;
                    color: @mainMenuColor;
                    &:hover, &:focus{
                        color: @mainMenuColor_hover;
                    }
                }
            }
        }

        &:hover{
            > ul{
                display: block;
            }
        }
    }
}


.masthead{
    background-color: @masthead_bg;
    padding: 30px 0px;
    text-align: center;
}

.masthead h1{
    color: #fff;
    padding: 0;
    margin: 0;
}

.col-sidebar{
    background: #fff;
    border-radius: 10px;
    margin-top: -50px;
    margin-bottom: 40px;
}

.subnav{
    background-color: #E9E9E9;
    border-radius: 0px;
    margin: 0px;
    min-height: 50px;
}

.subnav li{
    float: left;
}


.scroll-action{
    margin-top: 10px;
}

.addSpace{
    height: 40px;
}

/* CUSTOMIZE THE CAROUSEL
-------------------------------------------------- */

/* Carousel base class */
.carousel {
    height: 500px;
    margin-bottom: 0px;
}
/* Since positioning the image, we need to help out the caption */
.carousel-caption {
    z-index: 10;
}

.carousel-caption .btn{
    border-radius: 30px;
    padding: 10px 40px;
    border-width: 2px;
    border-color: white;
    background-color: transparent;
    background-color: rgba(100%, 100%, 100%, 0.2);
    text-shadow: 0px 1px 0px #757575;
}

.carousel-caption .btn:hover{
    background: white;
    color: #417796;
    text-shadow: none;
    opacity: 0.9;
}

/* Declare heights because of positioning of img element */
.carousel .item {
    height: 500px;
    background-color: #5B82D1;
}
.carousel-inner > .item > img {
    position: absolute;
    top: 0;
    left: 0;
    min-width: 100%;
    height: 500px;
    opacity: 0.4;
    background-position: top center;
    background-attachment: fixed;
    background-size: cover;
    background-clip: auto;
}

.carousel .container{
    height: 100%;
}

.carousel h1{
    color: #fff;
}




.or-spacer {
    width: 90%;
    margin: 80px auto 40px;
    display: block;
    position:relative;
}

.or-spacer .mask {
    overflow:hidden; height:20px;
}
.or-spacer .mask:after {
    content:'';
    display:block; margin:-25px auto 0;
    width:100%; height:25px;
    border-radius:125px / 12px;
    box-shadow:0 0 8px #939191;
}
.or-spacer span {
    width:50px; height:50px;
    position:absolute;
    bottom:100%; margin-bottom:-25px;
    left:50%; margin-left:-25px;
    border-radius:100%;
    box-shadow:0 2px 4px #939191;
    background:white;
}

.or-spacer span i {
    position:absolute;
    top:4px; bottom:4px;
    left:4px; right:4px;
    border-radius:100%;
    border:1px dashed #aaa;

    text-align:center;
    line-height:40px;
    font-style:normal;
    color:#999;
}



#page-content, .page-content {
    padding: 20px;
}

.separator {
    padding: 0px 0px;
    clear: both;
    display: block;
}




@media screen and (max-width: 767px){

    .subnav li{
        float: none;
    }

    .col-sidebar{
        margin-top: 0;
        margin-bottom: 20px;
    }

    .navbar-header{
        width: 100%;
        margin-bottom: 10px;
    }

    .navbar-collapse{
        position: relative;
    }

}


@media screen and (max-width: 981px){

    .navbar-nav>li>a{
        font-size: 10pt;
    }

    .navbar-header .navbar-brand img{
        max-width: 120px;
    }

}

.btnct {
    font-family: Avenir, Futura, "Gill Sans", Calibri, "Century Gothic", "AppleGothic", Helvetica, Arial, sans-serif;
    font-weight: 500;
    color: #0b04a9;
    display: inline-block;
    padding: 0.78125em 1.35em;
    margin-bottom: 0;
    font-size: 1.125em;
    text-align: center;
    vertical-align: middle;
    cursor: pointer;
    border: none;
    line-height: 1.125em;
    -webkit-transition: all 0.3s linear;
    -moz-transition: all 0.3s linear;
    -o-transition: all 0.3s linear;
    transition: all 0.3s linear;
    background-image: linear-gradient(bottom, #f7f7f7 1%, #ffffff 50%, #ffffff 99%);
    background-image: -o-linear-gradient(bottom, #f7f7f7 1%, #ffffff 50%, #ffffff 99%);
    background-image: -moz-linear-gradient(bottom, #f7f7f7 1%, #ffffff 50%, #ffffff 99%);
    background-image: -webkit-linear-gradient(bottom, #f7f7f7 1%, #ffffff 50%, #ffffff 99%);
    background-image: -ms-linear-gradient(bottom, #f7f7f7 1%, #ffffff 50%, #ffffff 99%);
    background-image: -webkit-gradient(linear, left bottom, left top, color-stop(1%, #f7f7f7), color-stop(50%, #ffffff), color-stop(99%, #ffffff));
    text-transform: uppercase;
    font-weight: 700;
    letter-spacing: 0.07em;
    padding: 15px 1.5em;
    line-height: 1.5em;
    color: white;
    background: #050245;
    -webkit-border-radius: 2em;
    -moz-border-radius: 2em;
    border-radius: 2em;
    -webkit-box-shadow: 0 0 0 rgba(0, 0, 0, 0.4);
    -moz-box-shadow: 0 0 0 rgba(0, 0, 0, 0.4);
    box-shadow: 0 0 0 rgba(0, 0, 0, 0.4);
    text-decoration:none;
}
.col-colored {
    padding: 20px 10px;
    border-radius: 10px;
    background-color: #f2f2f2;
    text-align: center;
}

.testblock {
    width: 100%;
    height: auto;
    padding: 20px;
    background: #eee;
    -webkit-border-radius: 5px;
    -moz-border-radius: 5px;
    border-radius: 5px;
    font-family: Georgia, "Times New Roman", Times, serif;
    font-style: italic;
    position: relative;
    line-height: 28px;
    font-size: 16px;
    -moz-transition: all 0.6s ease;
    -webkit-transition: all 0.6s ease;
    -ms-transition: all 0.6s ease;
    -o-transition: all 0.6s ease;
    transition: all 0.6s ease;


    &:after {
        top: 100%;
        left: 20%;
        border: solid transparent;
        content: " ";
        height: 0;
        width: 0;
        position: absolute;
        pointer-events: none;
        border-color: rgba(255, 255, 255, 0);
        border-top-color: #eee;
        border-width: 15px;
        margin-left: -15px;
    }



    &:hover {
        -moz-transform: scale(1.05);
        -webkit-transform: scale(1.05);
        -o-transform: scale(1.05);
        -ms-transform: scale(1.05);
        transform: scale(1.05);
        z-index: 2;
        background-color: #cc9900;
        color: #000000;

        &:before{
            border-top-color: #cc9900;
        }
    }


}

.footerCliner p{  
    color: #fff;
    font-size: 12px !important;
  }



.page-content, #page-content{
    .navbar-nav{
        float: none;
        margin-bottom: 20px;

        > li{
            > a{
                line-height: 45px;
                font-size: 12pt;
                text-transform: uppercase;
                padding-left: 12px;
                padding-right: 12px;
                color: #777777;
                padding-top: 0px;
                padding-bottom: 0;
            }

            > .dropdown-menu {
                margin-top: 0px;
            }
        }
    }

    .vertical-nav{
        float: left;
        width: 30%;
        padding-right: 30px;
        padding-bottom: 40px;
        @media screen and (max-width: 768px){
            width: 100%;
            float: none;
            padding: 0px;
        }

        .navbar-nav{
            float: left;
            width: 100%;
            @media screen and (max-width: 768px){
                float: none;
            }

            > li{
                float: none;

                > .dropdown-menu{
                    right: 0px;
                    opacity: 0;
                    display: block;
                    overflow: hidden;
                    max-height: 0px;
                    padding: 0px;
                    border-top: 0px;
                    border-radius: 0px;
                    box-shadow: none;
                }

                > a{
                    float: none;
                    width: 100%;
                    border: 1px solid transparent;
                    border-bottom-color: #c3c3c3;
                    display: block;
                }

                &.open{
                    > .dropdown-menu{
                        opacity: 1;
                        max-height: 300px;
                        overflow: auto;
                        padding: 5px 0px;
                        transition: all 0.3s;
                        -webkit-transition: all 0.3s;
                        -moz-transition: all 0.3s;
                    }
                    > a{
                        border-color: #c3c3c3;
                    }
                }

            }
        }
     }

    *[class*="col-"]{
        .vertical-nav{
            width: 100%;
            float: none;
            padding-right: 0px;
        }
    }
}

/*
<%  Response.WriteFile(pathTheme); %>
