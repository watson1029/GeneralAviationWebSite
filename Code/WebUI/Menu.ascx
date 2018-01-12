<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Menu" %>
     <div class="center" style="position: relative">
         <input type="hidden" id="searchtype" value="news"/>
                <div class="logo">
                    <img src="images/logo.png" alt="" style="height: 90px;" />
                </div>
                <div class="searchboxdiv">
                    <input type="text" name="search" id="txt_request" class="searchbox ipt" maxlength="30" value="请输入搜索关键词" onblur="if (this.value=='')this.value='请输入搜索关键词'" onfocus="if (this.value=='请输入搜索关键词')this.value=''" />
                    <span class="btn_search">
                        <button id="ibtn_txt_request" type="submit" class="btn" onclick="searchcommit('search.aspx')">搜&nbsp;索</button>
                    </span>
                    <label id="newslabel" style="color:red" onclick="Menu.click('news')">新闻</label>  <label id="supplydemandlabel"  onclick="Menu.click('supplydemand')">供求</label>  <label  id="companyintrolabel"  onclick="Menu.click('companyintro')">企业</label>
                </div>
               <div class="nav-box">
    <ul>
        <li id="default"><a href="/default.aspx">首页<span>Home</span></a></li>
        <li id="news"><a href="/List.aspx?Type=News&PageIndex=1">新闻<span>News</span></a></li>
        <li id="supplydemand"><a href="/List.aspx?Type=SupplyDemand&PageIndex=1">供求<span>Supply-Demand</span></a></li>
        <li id="companyintro"><a href="/List.aspx?Type=CompanyIntro&PageIndex=1">企业<span>Company</span></a></li>
               <%--<li id="file"><a href="/FileList.aspx?Type=File">资料<span>File</span></a></li>--%>
           <li id="file"><a href="javascript:void(0)">资料<span>File</span></a></li>
        <li><a href="javascript:void(0)">计划<span>Plan</span></a></li>
        <li><a href="javascript:void(0)">气象<span>Weather</span></a></li>
        <li><a href="javascript:void(0)">情报<span>Information</span></a></li>
        <li><a href="javascript:void(0)">监视<span>Surveillance</span></a></li>
    </ul>
    <div class="nav-line"></div>
</div>
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
    window.Menu = {
        click: function (type) {
            $('#' + type + 'label').attr('style', 'color:red').siblings("label").removeAttr('style');
            $('#searchtype').val(type);

        }

    };
    var searchcommit = function (url) {
        var content = $("#txt_request").val();
        if (content != '') {
            location.href = url + "?Type=" + $('#searchtype').val() + "&Content=" + escape(content);
        }
    }
</script>
<script type="text/javascript" src="js/common.js"></script>
