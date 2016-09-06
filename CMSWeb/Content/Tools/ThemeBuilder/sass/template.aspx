<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template.aspx.cs" Inherits="NewClientSites.UserControl.MortgageCEO_Forms.Tools.ThemeBuilder.sass.template" %>

*/

/* *
* Extended layout for ThemeBuilder
* Notes: this file is a less file, with aspx rendering because asp.net in this server doesnt allow to use .less files in 
* your website
* */


/* Theme Builder Preview specific styles */
.__theme-builder{
    .__default-palette{
        .__basic-editor{
            .__template-preview{
                .__template-wrapper{
                    .__template-body{
                        a{
                            color: @baseColor;
                            &:hover, &focus, &:active{
                                color: darken(@baseColor, 5%);
                                text-decoration: underline;
                            }
                        }
                        .__template-header{
                            .__menu{
                                ul{
                                    li{
                                        a{
                                            color: @mainMenuColor;
                                            &:hover, &:focus{
                                                color: @mainMenuColor_hover;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        .__template-masthead{
                            background-color: @masthead_bg;
                        }
                        .__template-page{
                            padding: 20px 0 40px;
                            color: #c8c8c8;

                            h1{
                                color: @h1_color;
                            }

                            h2{
                                color: @h2_color;
                            }

                            h3{
                                color: @h3_color;
                            }

                        }

                        .__container{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 0 15px;
                        }

                        .__template-footer{
                            background-color: @baseColor_darkenVariation;
                            color: #fff;
                            padding: 15px 0;
                        }
                    }

                    &.__previewing{
                        .__template-body{
                            opacity: 1;
                        }

                        .__template-preview-copy{
                            display: none;
                        }
                    }

                }

            }
        }

    }
}
