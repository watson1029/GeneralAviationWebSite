<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>
<%@ Register src="Menu.ascx" TagName="Menu" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="copyright" content="广州市中南民航空管通信网络科技有限公司" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>河南通航飞行服务站</title>
    <link href="css/base.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery1.js"></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/common.js"></script>
    <script type="text/javascript">
        if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
            alert("温馨提示:您当前使用的浏览器为ie6内核,可能会影响您浏览此网站,建议使用ie7以上版本浏览器!");
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        $(function () {
            if ("<%=previousModel.Id%>" == "0") {
                document.getElementById("h3pre").style.display = "none";
                document.getElementById("divpre").style.display = "none";
                document.getElementById("prev").style.display = "none";
            } else {
                document.getElementById("prev").style.display = "block";
                document.getElementById("h3pre").style.display = "block";
                document.getElementById("divpre").style.display = "block";
            }
            if ("<%=nextModel.Id%>" == "0") {
                document.getElementById("next").style.display = "none";
                document.getElementById("h3next").style.display = "none";
                document.getElementById("divnext").style.display = "none";
            } else {
                document.getElementById("next").style.display = "block";
                document.getElementById("h3next").style.display = "block";
                document.getElementById("divnext").style.display = "block";
            }
        });        
    </script>
    <form id="form1" runat="server">
        <!--head begin-->
        <div class="head">
            <div class="center" style="position: relative">
                <div class="logo">
                    <img src="images/logo.png" alt="" />
                </div>
                <div class="searchboxdiv">
                    <input type="text" name="search" id="txt_request" class="searchbox ipt" value="请输入搜索关键词" onblur="setkeyword('txt_request')"
                        onkeydown="if(event.keyCode==13){document.getElementById('ibtn_txt_request').focus();}" />
                    <span class="btn_search">
                        <button id="ibtn_txt_request" type="submit" class="btn" onclick="subjs('gc');return false">搜&nbsp;索</button>
                    </span>
                </div>
                   <cc1:Menu ID="ccMenu" runat="server" />
            </div>
        </div>
        <!--head end-->

        <!--main begin-->
        <div class="main clearfix" style="background: #fff;">
            <div class="center">
                <div class="main_left">
                    <h1><span id="title"><%=currModel.Title%></span></h1>
                    <div class="title_tip">
                        <span style="float: left">
                            <span id="auther">作者：<%=currModel.Creator%></span>&nbsp;
                            <span id="editor"></span>&nbsp;
                            <span id="puttime">发布时间：<%=currModel.CreateTime%></span>&nbsp;
                        </span>
                    </div>
                    <div class="content">
                        <%=HttpUtility.UrlDecode(currModel.Content)%>
                    </div>
                    <div class="article_section">
                        <a id="prev" class="prev" href="Detail.aspx?Type=<%=currModel.Type%>&Id=<%=previousModel.Id%>" title="">
                            <i class="prev_btn"></i>
                            <h3 id ="h3pre">上一篇</h3>
                            <div id="divpre" class="abs_prev"><%=previousModel.Title%></div>
                        </a>
                        <a id="next" class="next" href="Detail.aspx?Type=<%=currModel.Type%>&Id=<%=nextModel.Id%>" title="">
                            <i class="prev_btn"></i>
                            <h3 id="h3next">下一篇</h3>
                            <div id="divnext" class="abs_next"><%=nextModel.Title%></div>
                        </a>
                    </div>

                    <div class="listmore"><a href="List.aspx?Type=<%=currModel.Type%>&PageIndex=1">返回最新列表</a></div>
                </div>
            </div>
        </div>
        <!--main end-->
    </form>
</body>
</html>
