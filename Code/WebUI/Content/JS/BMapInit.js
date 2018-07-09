document.write("<script type='text/javascript' src='http://api.map.baidu.com/api?v=2.0&ak=H8Sf2KQqz7cThrYThPacBFuOMKOe0vxu'></script>");

var map;
var opts = {
	width : 250,     // 信息窗口宽度
	height: 120,     // 信息窗口高度
	title : "坐标信息", // 信息窗口标题
	enableMessage:true //设置允许信息窗发送短息
};

baiduMap = {
    init: function () {
        map = new BMap.Map("map");
        map.enableScrollWheelZoom();
        this.setCenter(new BMap.Point(113.28, 23.12));
        this.addMapType();
        this.addAreaControl({ strokeColor: "#001C53", strokeStyle: "dashed", strokeWeight: 2, strokeOpacity: 0.7 });
        this.addAppControl({ strokeColor: "#E96800", strokeStyle: "dashed", strokeWeight: 2, strokeOpacity: 1 });
        this.addAirLine({ strokeColor: "#6E186E", strokeWeight: 2, strokeOpacity: 0.7 });
    },
    setCenter: function (point) {
        map.centerAndZoom(point, 8);
    },
    addMapType: function () {
        var mapType = new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP] });
        map.addControl(mapType);
        map.setMapType(BMAP_HYBRID_MAP);
    },
    addMarker: function (point, content) {
        var marker = new BMap.Marker(point);
        map.addOverlay(marker);
        this.clickMarkerHandler(marker, content);
    },
    addLine: function (points, option) {
        var polyline;
        if (!option)
            polyline = new BMap.Polyline(points, { strokeColor: "red", strokeWeight: 2 });
        else
            polyline = new BMap.Polyline(points, option);
        map.addOverlay(polyline);
    },
    addCircle: function (point, raidus, option) {
        var circle;
        if (!option)
            circle = new BMap.Circle(point, raidus, { fillColor: "red", strokeWeight: 0.1, strokeOpacity: 0, fillOpacity: 0.3 });
        else
            circle = new BMap.Circle(point, raidus, option);
        map.addOverlay(circle);
    },
    addArea: function (points, option) {
        var polygon;
        if (!option)
            polygon = new BMap.Polygon(points, { fillColor: "red", strokeWeight: 0.1, strokeOpacity: 0, fillOpacity: 0.3 });
        else
            polygon = new BMap.Polygon(points, option);
        map.addOverlay(polygon);
    },
    removeFeature: function () {
        map.clearOverlays();
    },
    clickMarkerHandler: function (marker, content) {
        marker.addEventListener("click", function (e) {
            var p = e.target;
            var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
            var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象 
            map.openInfoWindow(infoWindow, point); //开启信息窗口
        });
    },
    addAreaControl: function (options) {
        this.addBaseData("/Ajax/Map/GetAreaControl.ashx", options);
    },
    addAppControl: function (options) {
        this.addBaseData("/Ajax/Map/GetAppControl.ashx", options);
    },
    addAirLine: function (options) {
        this.addBaseData("/Ajax/Map/GetAirLine.ashx", options);
    },
    addBaseData: function (url, options) {
        $.ajax({
            url: url,
            type: "get",
            dataType: "json",
            async: true,
            error: function (xml, msg) {
                alert(msg);
            },
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var points = [];
                    for (var j = 0; j < data[i].location.length; j++) {
                        points.push(new BMap.Point(data[i].location[j]["Lon"], data[i].location[j]["Lat"]));
                    }
                    baiduMap.addLine(points, options);
                }
            }
        });
    }
};

zhccMap = {
    addRepetPlan: function (keyValue) {
        $.ajax({
            url: "/Ajax/Map/GetRepetPlanData.ashx",
            type: "get",
            data: { "keyValue": keyValue },
            dataType: "json",
            async: true,
            error: function (xml, msg) {
                alert(msg);
            },
            success: function (data) {
                zhccMap.addFeature(data);
            }
        });
    },
    addFlyPlan: function (keyValue) {
        $.ajax({
            url: "/Ajax/Map/GetFlyPlanData.ashx",
            type: "get",
            data: { "keyValue": keyValue },
            dataType: "json",
            async: true,
            error: function (xml, msg) {
                alert(msg);
            },
            success: function (data) {
                zhccMap.addFeature(data);
            }
        });
    },
    addFeature: function (data) {
        // 移除覆盖物
        baiduMap.removeFeature();
        // 设置地图中心
        if (data.length > 0)
            baiduMap.setCenter(new BMap.Point(parseFloat(data[0].Location[0].Longitude), parseFloat(data[0].Location[0].Latitude)));
        else
            baiduMap.setCenter(new BMap.Point(113.28, 23.12));
        // 添加覆盖物
        for (var i = 0; i < data.length; i++) {
            switch (data[i].WorkType) {
                case "airline":
                    var points = new Array();
                    for (var j = 0; j < data[i].Location.length; j++) {
                        baiduMap.addMarker(new BMap.Point(parseFloat(data[i].Location[j].Longitude), parseFloat(data[i].Location[j].Latitude)), this.markerContent(data[i], data[i].Location[j]));
                        points.push(new BMap.Point(parseFloat(data[i].Location[j].Longitude), parseFloat(data[i].Location[j].Latitude)));
                    }
                    baiduMap.addLine(points);
                    break;
                case "circle":
                    baiduMap.addCircle(new BMap.Point(parseFloat(data[i].Location[0].Longitude), parseFloat(data[i].Location[0].Latitude)), data[i].RaidusMile * 1000);
                    baiduMap.addMarker(new BMap.Point(parseFloat(data[i].Location[0].Longitude), parseFloat(data[i].Location[0].Latitude)), this.markerContent(data[i], data[i].Location[0]));
                    break;
                case "area":
                    var points = new Array();
                    for (var j = 0; j < data[i].Location.length; j++) {
                        baiduMap.addMarker(new BMap.Point(parseFloat(data[i].Location[j].Longitude), parseFloat(data[i].Location[j].Latitude)), this.markerContent(data[i], data[i].Location[j]));
                        points.push(new BMap.Point(parseFloat(data[i].Location[j].Longitude), parseFloat(data[i].Location[j].Latitude)));
                    }
                    baiduMap.addArea(points);
                    break;
                case "airlineCircle":
                    baiduMap.addCircle(new BMap.Point(parseFloat(data[i].Location[0].Longitude), parseFloat(data[i].Location[0].Latitude)), data[i].RaidusMile * 1000);
                    baiduMap.addMarker(new BMap.Point(parseFloat(data[i].Location[0].Longitude), parseFloat(data[i].Location[0].Latitude)), this.markerContent(data[i], data[i].Location[0]));
                    break;
                case "airlineRectangle":
                    var points = new Array();
                    for (var j = 0; j < data[i].Location.length; j++) {
                        points.push(new BMap.Point(parseFloat(data[i].Location[j].Longitude), parseFloat(data[i].Location[j].Latitude)));
                    }
                    baiduMap.addArea(points);
                    break;
            }
        }
    },
    markerContent: function (data, location) {
        var content = "<div><p>当前地点：" + location.PointName + "</p><p>当前坐标：【 " + parseFloat(location.Longitude).toFixed(2) + " , " + parseFloat(location.Latitude).toFixed(2) + " 】</p><p>当前范围：";
        switch (data.WorkType) {
            case "airline":
                content += "两点间航线</p></div>";
                break;
            case "circle":
                content += "半径" + data.RaidusMile + "公里内</p></div>";
                break;
            case "area":
                content += "多点形成闭合环</p></div>";
                break;
            case "airlineCircle":
                content += "航线左右" + data.RaidusMile + "公里内</p></div>";
                break;
        }
        return content;
    }
};