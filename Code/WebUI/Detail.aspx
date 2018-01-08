<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/main.css" rel="stylesheet" type="text/css" />
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <div class="detailcontent">
                    <%=HttpUtility.UrlDecode(currModel.Content)%>
                </div>
                <div class="article_section">
                    <a id="prev" class="prev" href="Detail.aspx?Type=<%=currModel.Type%>&Id=<%=previousModel.Id%>" title="">
                        <i class="prev_btn"></i>
                        <h3 id="h3pre">上一篇</h3>
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
</asp:Content>
