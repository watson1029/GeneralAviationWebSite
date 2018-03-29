<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaiduMap.aspx.cs"  MasterPageFile="~/MasterPage.Master" Inherits="Map_BaiduMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

 
	<style type="text/css">
		body, html{width: 100%;height: 100%;margin:0;font-family:"微软雅黑";}
		#allmap{height:500px;width:100%;}
		#r-result{width:100%; font-size:14px;}
	</style>
<%--	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=您的密钥"></script>--%>
    <div id="r-result">
        经度:
        <input id="longitude1" type="text" style="width: 100px; margin-right: 10px;" />
        纬度:
        <input id="latitude1" type="text" style="width: 100px; margin-right: 10px;" />
         经度:
        <input id="longitude2" type="text" style="width: 100px; margin-right: 10px;" />
        纬度:
        <input id="latitude2" type="text" style="width: 100px; margin-right: 10px;" />
         经度:
        <input id="longitude3" type="text" style="width: 100px; margin-right: 10px;" />
        纬度:
        <input id="latitude3" type="text" style="width: 100px; margin-right: 10px;" />
         经度:
        <input id="longitude4" type="text" style="width: 100px; margin-right: 10px;" />
        纬度:
        <input id="latitude4" type="text" style="width: 100px; margin-right: 10px;" />
        <input type="button" value="查询" onclick="theLocation()" />
        <input type="button" value="清空" onclick="tclear()" />
    </div>
	<div id="allmap"></div>
<script type="text/javascript">
    // 百度地图API功能
    //var map = new BMap.Map("allmap");
    //map.centerAndZoom(new BMap.Point(116.331398, 39.897445), 11);
    //map.enableScrollWheelZoom(true);

    // 用经纬度设置地图中心点
  //  function theLocation() {
   //     alert(21);
    //    map.clearOverlays();
    //    if (document.getElementById("longitude1").value != "" && document.getElementById("latitude1").value != "") {
    //        map.clearOverlays();
    //        var new_point = new BMap.Point(document.getElementById("longitude1").value, document.getElementById("latitude1").value);
    //        var marker = new BMap.Marker(new_point);  // 创建标注
    //        map.addOverlay(marker);              // 将标注添加到地图中
    //        map.panTo(new_point);
    //    }
    //    if (document.getElementById("longitude2").value != "" && document.getElementById("latitude2").value != "") {
    //        map.clearOverlays();
    //        var new_point = new BMap.Point(document.getElementById("longitude2").value, document.getElementById("latitude2").value);
    //        var marker = new BMap.Marker(new_point);  // 创建标注
    //        map.addOverlay(marker);              // 将标注添加到地图中
    //        map.panTo(new_point);
    //    }
    //    if (document.getElementById("longitude3").value != "" && document.getElementById("latitude3").value != "") {
    //        map.clearOverlays();
    //        var new_point = new BMap.Point(document.getElementById("longitude3").value, document.getElementById("latitude3").value);
    //        var marker = new BMap.Marker(new_point);  // 创建标注
    //        map.addOverlay(marker);              // 将标注添加到地图中
    //        map.panTo(new_point);
    //    }
    //    if (document.getElementById("longitude4").value != "" && document.getElementById("latitude4").value != "") {
    //        map.clearOverlays();
    //        var new_point = new BMap.Point(document.getElementById("longitude4").value, document.getElementById("latitude4").value);
    //        var marker = new BMap.Marker(new_point);  // 创建标注
    //        map.addOverlay(marker);              // 将标注添加到地图中
    //        map.panTo(new_point);
    //    }
 //   }
    function tclear() {
        alert(1);
        // map.clearOverlays();

    }
</script>



</asp:Content>
