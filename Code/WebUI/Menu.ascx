<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Menu" %>
<div class="nav-box">
    <ul>
        <li id="default"><a href="/default.aspx">首页<span>Home</span></a></li>
        <li id="news"><a href="/List.aspx?Type=News&PageIndex=1">新闻<span>News</span></a></li>
        <li id="supplydemand"><a href="/List.aspx?Type=SupplyDemand&PageIndex=1">供求<span>Supply-Demand</span></a></li>
        <li id="companyintro"><a href="/List.aspx?Type=CompanyIntro&PageIndex=1">企业<span>Company</span></a></li>
        <li><a href="javascript:void(0)">资料<span>File</span></a></li>
        <li><a href="javascript:void(0)">计划<span>Plan</span></a></li>
        <li><a href="javascript:void(0)">气象<span>Weather</span></a></li>
        <li><a href="javascript:void(0)">情报<span>Information</span></a></li>
        <li><a href="javascript:void(0)">监视<span>Surveillance</span></a></li>
    </ul>
    <div class="nav-line"></div>
</div>
<style type="text/css">
    .nav-line {
        left: 0px;
        width: 128px;
        display: block;
    }
</style>
<script type="text/javascript">
    $(function () {
        $('#' + '<%=names%>').addClass('cur').siblings("li").removeClass('cur');
        $(".nav-line").attr('style', 'left:' + '<%=position%>');
    });
</script>
<script type="text/javascript" src="js/common.js"></script>
