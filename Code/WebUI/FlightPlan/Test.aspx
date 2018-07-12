<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="FlightPlan_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" Runat="Server">
    <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>
    <link href="/css/togfloatdiv.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/togfloatdiv.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <script>
        $(function () {
            baiduMap.init();
        });
    </script>
    <div class="tog_contact">
        <div class="t_con_box">
            <div id="map" style="height:400px;"></div>
        </div>
    </div>
    <div class="tog" id="tog"><span>简  介</span></div>
</asp:Content>

