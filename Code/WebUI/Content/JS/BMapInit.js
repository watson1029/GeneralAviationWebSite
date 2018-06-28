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
        this.addMapTypeControl();
        //加载管制区
        flyDataOfMap.dataInit();
    },
    setCenter: function (point) {
        map.centerAndZoom(point, 8);
    },
    addMapTypeControl: function () {
        var mapType = new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP] });
        map.addControl(mapType);
        map.setMapType(BMAP_HYBRID_MAP);
    },
    addOverViewControl: function(){
        var overView = new BMap.OverviewMapControl();
        var overViewOpen = new BMap.OverviewMapControl({isOpen:true, anchor: BMAP_ANCHOR_BOTTOM_RIGHT});
        //添加默认缩略地图控件
        map.addControl(overView);
        //右下角，打开
		map.addControl(overViewOpen);
    },
    addZoomControl: function(){
        var navigation = new BMap.NavigationControl();
        map.addControl(navigation);
    },
    addMarker: function (point, content) {
        var marker = new BMap.Marker(point);
        map.addOverlay(marker);
        this.clickMarkerHandler(marker, content);
    },
    addLine: function (points, option) {
        var polyline;
        if(!option)
            polyline = new BMap.Polyline(points, { strokeColor: "red", strokeWeight: 2 });
        else
            polyline = new BMap.Polyline(points, option);
        map.addOverlay(polyline);
    },
    addCircle: function (point, raidus, option) {
        var circle;
        if(!option)
            circle = new BMap.Circle(point, raidus, { fillColor: "red", strokeWeight: 0.1, strokeOpacity: 0, fillOpacity: 0.3 });
        else
            circle = new BMap.Circle(point, raidus, option);
        map.addOverlay(circle);
    },
    addArea: function (points, option) {
        var polygon;
        if(!option)
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
    }
};

var areaOption =  { strokeColor: "#E15E1B", strokeWeight: 4, strokeOpacity: 1 };
flyDataOfMap = {
    dataInit: function() {
        var area = new this.addAreaControl();
        area.csArea.addAll();
        area.gzArea.addAll();
        area.glArea.addAll();
        area.nnArea.addAll();
        area.syArea.addAll();
        area.zzArea.addAll();
    },
    addAreaControl: function() {
        this.csArea = {
            addAll: function(){
                this.ZGHAAR01();
                this.ZGHAAR02();
                this.ZGHAAR03();
                this.ZGHAAR04();
            },
            ZGHAAR01: function(){
                var points = 
                [
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667),
                    new BMap.Point(112.96333333333333333333333333, 28.018333333333333333333333333),
                    new BMap.Point(112.84666666666666666666666667, 26.536666666666666666666666667),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(111.34, 25.276666666666666666666666667),
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333),
                    new BMap.Point(109.35638888888888888888888889, 27.896666666666666666666666667),
                    new BMap.Point(111.62, 28.846666666666666666666666667),
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGHAAR02: function(){
                var points = 
                [
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667),
                    new BMap.Point(113.12, 29.383333333333333333333333333),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(114.20805555555555555555555556, 27.682222222222222222222222222),
                    new BMap.Point(113.515, 27.65),
                    new BMap.Point(112.905, 27.281666666666666666666666667),
                    new BMap.Point(112.96333333333333333333333333, 28.018333333333333333333333333),
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGHAAR03: function(){
                var points = 
                [
                    new BMap.Point(112.905, 27.281666666666666666666666667),
                    new BMap.Point(113.515, 27.65),
                    new BMap.Point(114.20805555555555555555555556, 27.682222222222222222222222222),
                    new BMap.Point(113.95, 26.7),
                    new BMap.Point(114.11666666666666666666666667, 26.05),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(112.84666666666666666666666667, 26.536666666666666666666666667),
                    new BMap.Point(112.905, 27.281666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGHAAR04: function(){
                var points = 
                [
                    new BMap.Point(109.4, 29.516666666666666666666666667),
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667),
                    new BMap.Point(111.62, 28.846666666666666666666666667),
                    new BMap.Point(109.35638888888888888888888889, 27.896666666666666666666666667),
                    new BMap.Point(109.38333333333333333333333333, 28.783333333333333333333333333),
                    new BMap.Point(109.4, 29.516666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            }
        };
        this.gzArea = {
            addAll: function(){
                this.ZGGGAR01();
                this.ZGGGAR02();
                this.ZGGGAR03();
                this.ZGGGAR04();
                this.ZGGGAR05();
                this.ZGGGAR06();
                this.ZGGGAR07();
                this.ZGGGAR08();
                this.ZGGGAR11();
                this.ZGGGAR12();
                this.ZGGGAR13();
                this.ZGGGAR14();
                this.ZGGGAR15();
                this.ZGGGAR16();
                this.ZGGGAR17();
                this.ZGGGAR18();
                this.ZGGGAR19();
                this.ZGGGAR20();
                this.ZGGGAR21();
                this.ZGGGAR22();
                this.ZGGGAR23();
                this.ZGGGAR24();
                this.ZGGGAR25();
                this.ZGGGAR26();
                this.ZGGGAR27();
                this.ZGGGAR28();
                this.ZGGGAR45();
            },
            ZGGGAR01: function(){
                var points = 
                [
                    new BMap.Point(114.85, 24.8),
                    new BMap.Point(114.96805555555555555555555556, 24.760277777777777777777777778),
                    new BMap.Point(115.41666666666666666666666667, 25.091666666666666666666666667),
                    new BMap.Point(115.40833333333333333333333333, 24.673333333333333333333333333),
                    new BMap.Point(116.21194444444444444444444444, 24.4825),
                    new BMap.Point(116.09055555555555555555555556, 24.173333333333333333333333333),
                    new BMap.Point(115.91027777777777777777777778, 23.715833333333333333333333333),
                    new BMap.Point(114.46166666666666666666666667, 23.433333333333333333333333333),
                    new BMap.Point(114.32805555555555555555555556, 23.648611111111111111111111111),
                    new BMap.Point(114.2, 23.718333333333333333333333333),
                    new BMap.Point(114.735, 24.293333333333333333333333333),
                    new BMap.Point(114.85, 24.8)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR02: function(){
                var points = 
                [
                    new BMap.Point(114.735, 24.293333333333333333333333333),
                    new BMap.Point(114.2, 23.718333333333333333333333333),
                    new BMap.Point(113.64333333333333333333333333, 23.928333333333333333333333333),
                    new BMap.Point(113.54833333333333333333333333, 24.185),
                    new BMap.Point(113.63472222222222222222222222, 25.207777777777777777777777778),
                    new BMap.Point(114.85, 24.8),
                    new BMap.Point(114.735, 24.293333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR03: function(){
                var points = 
                [
                    new BMap.Point(113.7, 25.941666666666666666666666667),
                    new BMap.Point(113.63472222222222222222222222, 25.207777777777777777777777778),
                    new BMap.Point(113.54833333333333333333333333, 24.185),
                    new BMap.Point(113.64333333333333333333333333, 23.928333333333333333333333333),
                    new BMap.Point(113.385, 23.535),
                    new BMap.Point(112.53333333333333333333333333, 23.408333333333333333333333333),
                    new BMap.Point(112.79694444444444444444444444, 23.850833333333333333333333333),
                    new BMap.Point(112.9325, 24.293611111111111111111111111),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(113.7, 25.941666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR04: function(){
                var points = 
                [
                    new BMap.Point(111.28, 24.355),
                    new BMap.Point(112.53333333333333333333333333, 23.408333333333333333333333333),
                    new BMap.Point(112.48527777777777777777777778, 23.071388888888888888888888889),
                    new BMap.Point(112.48833333333333333333333333, 22.8),
                    new BMap.Point(111.50833333333333333333333333, 21.846666666666666666666666667),
                    new BMap.Point(110.785, 22.468333333333333333333333333),
                    new BMap.Point(110.55, 22.86),
                    new BMap.Point(111.30666666666666666666666667, 23.478333333333333333333333333),
                    new BMap.Point(111.26666666666666666666666667, 24.148333333333333333333333333),
                    new BMap.Point(111.28, 24.355)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR05: function(){
                var points = 
                [
                    new BMap.Point(113.86666666666666666666666667, 22.358333333333333333333333333),
                    new BMap.Point(113, 21.6875),
                    new BMap.Point(113, 22.435),
                    new BMap.Point(112.48833333333333333333333333, 22.8),
                    new BMap.Point(112.48527777777777777777777778, 23.071388888888888888888888889),
                    new BMap.Point(112.53333333333333333333333333, 23.408333333333333333333333333),
                    new BMap.Point(113.385, 23.535),
                    new BMap.Point(113.64333333333333333333333333, 23.928333333333333333333333333),
                    new BMap.Point(114.2, 23.718333333333333333333333333),
                    new BMap.Point(114.32805555555555555555555556, 23.648611111111111111111111111),
                    new BMap.Point(114.46166666666666666666666667, 23.433333333333333333333333333),
                    new BMap.Point(114.18833333333333333333333333, 23.256666666666666666666666667),
                    new BMap.Point(114.06166666666666666666666667, 22.9),
                    new BMap.Point(114.46666666666666666666666667, 22.805),
                    new BMap.Point(114.75, 22.616666666666666666666666667),
                    new BMap.Point(114.75, 22.408333333333333333333333333),
                    new BMap.Point(114.5, 22.408333333333333333333333333),
                    new BMap.Point(113.86666666666666666666666667, 22.358333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR06: function(){
                var points = 
                [
                    new BMap.Point(114.75, 22.408333333333333333333333333),
                    new BMap.Point(115.66666666666666666666666667, 22.408333333333333333333333333),
                    new BMap.Point(116.36666666666666666666666667, 22.633333333333333333333333333),
                    new BMap.Point(115.55, 23.083333333333333333333333333),
                    new BMap.Point(115.755, 23.318333333333333333333333333),
                    new BMap.Point(115.91027777777777777777777778, 23.715833333333333333333333333),
                    new BMap.Point(114.46166666666666666666666667, 23.433333333333333333333333333),
                    new BMap.Point(114.18833333333333333333333333, 23.256666666666666666666666667),
                    new BMap.Point(114.06166666666666666666666667, 22.9),
                    new BMap.Point(114.46666666666666666666666667, 22.805),
                    new BMap.Point(114.75, 22.616666666666666666666666667),
                    new BMap.Point(114.75, 22.408333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR07: function(){
                var points = 
                [
                    new BMap.Point(110.785, 22.468333333333333333333333333),
                    new BMap.Point(111.82666666666666666666666667, 21.57),
                    new BMap.Point(111.5, 21.15),
                    new BMap.Point(111.5, 20.5),
                    new BMap.Point(110.005, 20.5),
                    new BMap.Point(110.005, 22.488333333333333333333333333),
                    new BMap.Point(110.785, 22.468333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR08: function(){
                var points = 
                [
                    new BMap.Point(117.18333333333333333333333333, 23.7),
                    new BMap.Point(117.5, 23.5),
                    new BMap.Point(117.5, 23),
                    new BMap.Point(116.36666666666666666666666667, 22.633333333333333333333333333),
                    new BMap.Point(115.55, 23.083333333333333333333333333),
                    new BMap.Point(115.755, 23.318333333333333333333333333),
                    new BMap.Point(115.91027777777777777777777778, 23.715833333333333333333333333),
                    new BMap.Point(116.09055555555555555555555556, 24.173333333333333333333333333),
                    new BMap.Point(116.21194444444444444444444444, 24.4825),
                    new BMap.Point(116.7, 24.366666666666666666666666667),
                    new BMap.Point(116.96666666666666666666666667, 24),
                    new BMap.Point(117.18333333333333333333333333, 23.7)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR11: function(){
                var points = 
                [
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333),
                    new BMap.Point(111.34, 25.276666666666666666666666667),
                    new BMap.Point(111.26666666666666666666666667, 24.148333333333333333333333333),
                    new BMap.Point(110.185, 24.128333333333333333333333333),
                    new BMap.Point(109.84, 24.668333333333333333333333333),
                    new BMap.Point(107.65, 25.7),
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR12: function(){
                var points = 
                [
                    new BMap.Point(111.78888888888888888888888889, 28.318333333333333333333333333),
                    new BMap.Point(109.35583333333333333333333333, 27.8825),
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333),
                    new BMap.Point(111.34, 25.276666666666666666666666667),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(112.63333333333333333333333333, 26.908333333333333333333333333),
                    new BMap.Point(112.29138888888888888888888889, 27.621666666666666666666666667),
                    new BMap.Point(112.20944444444444444444444444, 28.066666666666666666666666667),
                    new BMap.Point(111.78888888888888888888888889, 28.318333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR13: function(){
                var points = 
                [
                    new BMap.Point(112.19555555555555555555555556, 28.516666666666666666666666667),
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667),
                    new BMap.Point(113.12, 29.383333333333333333333333333),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(114.22388888888888888888888889, 27.750277777777777777777777778),
                    new BMap.Point(113.52861111111111111111111111, 27.628611111111111111111111111),
                    new BMap.Point(112.63333333333333333333333333, 26.908333333333333333333333333),
                    new BMap.Point(112.29138888888888888888888889, 27.621666666666666666666666667),
                    new BMap.Point(112.19555555555555555555555556, 28.516666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR14: function(){
                var points = 
                [
                    new BMap.Point(112.19555555555555555555555556, 28.516666666666666666666666667),
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667),
                    new BMap.Point(113.12, 29.383333333333333333333333333),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(113.95, 26.7),
                    new BMap.Point(114.02555555555555555555555556, 26.403888888888888888888888889),
                    new BMap.Point(113.89916666666666666666666667, 26.369444444444444444444444444),
                    new BMap.Point(113.72305555555555555555555556, 26.225555555555555555555555556),
                    new BMap.Point(113.7, 25.941666666666666666666666667),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(112.63333333333333333333333333, 26.908333333333333333333333333),
                    new BMap.Point(112.29138888888888888888888889, 27.621666666666666666666666667),
                    new BMap.Point(112.19555555555555555555555556, 28.516666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR15: function(){
                var points = 
                [
                    new BMap.Point(114.20388888888888888888888889, 30.783055555555555555555555556),
                    new BMap.Point(114.30916666666666666666666667, 30.088888888888888888888888889),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(113.12, 29.383333333333333333333333333),
                    new BMap.Point(111.13333333333333333333333333, 29.466666666666666666666666667),
                    new BMap.Point(111.47694444444444444444444444, 30.561944444444444444444444444),
                    new BMap.Point(111.78083333333333333333333333, 31.51),
                    new BMap.Point(114.20388888888888888888888889, 30.783055555555555555555555556)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR16: function(){
                var points = 
                [
                    new BMap.Point(115.81333333333333333333333333, 32.908333333333333333333333333),
                    new BMap.Point(115.93333333333333333333333333, 30.083333333333333333333333333),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(114.30916666666666666666666667, 30.088888888888888888888888889),
                    new BMap.Point(114.20388888888888888888888889, 30.783055555555555555555555556),
                    new BMap.Point(111.78083333333333333333333333, 31.51),
                    new BMap.Point(112.2525, 32.943055555555555555555555556),
                    new BMap.Point(114.07, 32.125),
                    new BMap.Point(115.81333333333333333333333333, 32.908333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR17: function(){
                var points = 
                [
                    new BMap.Point(110.86666666666666666666666667, 33.533333333333333333333333333),
                    new BMap.Point(112.2525, 32.943055555555555555555555556),
                    new BMap.Point(111.78083333333333333333333333, 31.51),
                    new BMap.Point(111.47694444444444444444444444, 30.561944444444444444444444444),
                    new BMap.Point(111.13333333333333333333333333, 29.466666666666666666666666667),
                    new BMap.Point(109.4, 29.516666666666666666666666667),
                    new BMap.Point(109.51666666666666666666666667, 31.9),
                    new BMap.Point(110.86666666666666666666666667, 33.533333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR18: function(){
                var points = 
                [
                    new BMap.Point(109.47, 22.505),
                    new BMap.Point(108.97, 21.63),
                    new BMap.Point(108.8, 21),
                    new BMap.Point(109.25, 20.5),
                    new BMap.Point(110.005, 20.5),
                    new BMap.Point(110.005, 22.488333333333333333333333333),
                    new BMap.Point(109.47, 22.505)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR19: function(){
                var points = 
                [
                    new BMap.Point(111.78888888888888888888888889, 28.318333333333333333333333333),
                    new BMap.Point(109.35583333333333333333333333, 27.8825),
                    new BMap.Point(109.38333333333333333333333333, 28.783333333333333333333333333),
                    new BMap.Point(109.4, 29.516666666666666666666666667),
                    new BMap.Point(112.16666666666666666666666667, 29.416666666666666666666666667),
                    new BMap.Point(112.19555555555555555555555556, 28.516666666666666666666666667),
                    new BMap.Point(111.78888888888888888888888889, 28.318333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR20: function(){
                var points = 
                [
                    new BMap.Point(114.11666666666666666666666667, 26.05),
                    new BMap.Point(114.02555555555555555555555556, 26.403888888888888888888888889),
                    new BMap.Point(113.89916666666666666666666667, 26.369444444444444444444444444),
                    new BMap.Point(113.72305555555555555555555556, 26.225555555555555555555555556),
                    new BMap.Point(113.7, 25.941666666666666666666666667),
                    new BMap.Point(113.63472222222222222222222222, 25.207777777777777777777777778),
                    new BMap.Point(114.85, 24.8),
                    new BMap.Point(114.98972222222222222222222222, 25.434722222222222222222222222),
                    new BMap.Point(114.11666666666666666666666667, 26.05)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR21: function(){
                var points = 
                [
                    new BMap.Point(115.81333333333333333333333333, 32.908333333333333333333333333),
                    new BMap.Point(115.93333333333333333333333333, 30.083333333333333333333333333),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(114.30916666666666666666666667, 30.088888888888888888888888889),
                    new BMap.Point(114.20388888888888888888888889, 30.783055555555555555555555556),
                    new BMap.Point(111.78083333333333333333333333, 31.51),
                    new BMap.Point(112.2525, 32.943055555555555555555555556),
                    new BMap.Point(114.07, 32.125),
                    new BMap.Point(115.81333333333333333333333333, 32.908333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR22: function(){
                var points = 
                [
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333),
                    new BMap.Point(111.34, 25.276666666666666666666666667),
                    new BMap.Point(111.26666666666666666666666667, 24.148333333333333333333333333),
                    new BMap.Point(110.185, 24.128333333333333333333333333),
                    new BMap.Point(109.84, 24.668333333333333333333333333),
                    new BMap.Point(107.65, 25.7),
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR23: function(){
                var points = 
                [
                    new BMap.Point(112.53333333333333333333333333, 23.408333333333333333333333333),
                    new BMap.Point(112.79694444444444444444444444, 23.850833333333333333333333333),
                    new BMap.Point(112.9325, 24.293611111111111111111111111),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(111.34, 25.276666666666666666666666667),
                    new BMap.Point(111.28, 24.355),
                    new BMap.Point(112.53333333333333333333333333, 23.408333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR24: function(){
                var points = 
                [
                    new BMap.Point(113, 21.6875),
                    new BMap.Point(113, 22.435),
                    new BMap.Point(112.48833333333333333333333333, 22.8),
                    new BMap.Point(111.50833333333333333333333333, 21.846666666666666666666666667),
                    new BMap.Point(111.82666666666666666666666667, 21.57),
                    new BMap.Point(111.5, 21.15),
                    new BMap.Point(111.5, 20.5),
                    new BMap.Point(113, 21.6875)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR25: function(){
                var points = 
                [
                    new BMap.Point(114.22388888888888888888888889, 27.750277777777777777777777778),
                    new BMap.Point(113.95, 26.7),
                    new BMap.Point(114.02555555555555555555555556, 26.403888888888888888888888889),
                    new BMap.Point(113.89916666666666666666666667, 26.369444444444444444444444444),
                    new BMap.Point(113.72305555555555555555555556, 26.225555555555555555555555556),
                    new BMap.Point(113.7, 25.941666666666666666666666667),
                    new BMap.Point(112.35833333333333333333333333, 25.561666666666666666666666667),
                    new BMap.Point(112.63333333333333333333333333, 26.908333333333333333333333333),
                    new BMap.Point(113.52861111111111111111111111, 27.628611111111111111111111111),
                    new BMap.Point(114.22388888888888888888888889, 27.750277777777777777777777778)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR26: function(){
                var points = 
                [
                    new BMap.Point(110.785, 22.468333333333333333333333333),
                    new BMap.Point(111.82666666666666666666666667, 21.57),
                    new BMap.Point(111.5, 21.15),
                    new BMap.Point(111.5, 20.5),
                    new BMap.Point(110.005, 20.5),
                    new BMap.Point(110.005, 22.488333333333333333333333333),
                    new BMap.Point(110.785, 22.468333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR27: function(){
                var points = 
                [
                    new BMap.Point(114.20388888888888888888888889, 30.783055555555555555555555556),
                    new BMap.Point(114.30916666666666666666666667, 30.088888888888888888888888889),
                    new BMap.Point(114.56666666666666666666666667, 29.033333333333333333333333333),
                    new BMap.Point(113.12, 29.383333333333333333333333333),
                    new BMap.Point(111.13333333333333333333333333, 29.466666666666666666666666667),
                    new BMap.Point(111.47694444444444444444444444, 30.561944444444444444444444444),
                    new BMap.Point(111.78083333333333333333333333, 31.51),
                    new BMap.Point(114.20388888888888888888888889, 30.783055555555555555555555556)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR28: function(){
                var points = 
                [
                    new BMap.Point(114.11666666666666666666666667, 26.05),
                    new BMap.Point(114.98972222222222222222222222, 25.434722222222222222222222222),
                    new BMap.Point(114.85, 24.8),
                    new BMap.Point(114.96805555555555555555555556, 24.760277777777777777777777778),
                    new BMap.Point(115.41666666666666666666666667, 25.091666666666666666666666667),
                    new BMap.Point(114.87333333333333333333333333, 25.766666666666666666666666667),
                    new BMap.Point(114.11666666666666666666666667, 26.05)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGGGAR45: function(){
                var points = 
                [
                    new BMap.Point(109.47, 22.505),
                    new BMap.Point(108.97, 21.63),
                    new BMap.Point(108.8, 21),
                    new BMap.Point(109.25, 20.5),
                    new BMap.Point(110.005, 20.5),
                    new BMap.Point(110.005, 22.488333333333333333333333333),
                    new BMap.Point(109.47, 22.505)
                ];
                baiduMap.addLine(points, areaOption);
            }
        };
        this.glArea = {
            addAll: function(){
                this.ZGGLAR01();
            },
            ZGGLAR01: function(){
                var points = 
                [
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333),
                    new BMap.Point(111.34, 25.276666666666666666666666667),
                    new BMap.Point(111.26666666666666666666666667, 24.148333333333333333333333333),
                    new BMap.Point(110.185, 24.128333333333333333333333333),
                    new BMap.Point(109.84, 24.668333333333333333333333333),
                    new BMap.Point(107.65, 25.7),
                    new BMap.Point(109.31666666666666666666666667, 26.583333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            }
        };
        this.nnArea = {
            addAll: function(){
                this.ZGNNAR01();
                this.ZGNNAR02();
                this.ZGNNAR03();
                this.ZGNNAR04();
            },
            ZGNNAR01: function(){
                var points = 
                [
                    new BMap.Point(108.90694444444444444444444444, 25.108888888888888888888888889),
                    new BMap.Point(109.84, 24.668333333333333333333333333),
                    new BMap.Point(110.185, 24.128333333333333333333333333),
                    new BMap.Point(111.26666666666666666666666667, 24.148333333333333333333333333),
                    new BMap.Point(111.30666666666666666666666667, 23.478333333333333333333333333),
                    new BMap.Point(110.55, 22.86),
                    new BMap.Point(110.785, 22.468333333333333333333333333),
                    new BMap.Point(109.47, 22.505),
                    new BMap.Point(109.21, 22.051111111111111111111111111),
                    new BMap.Point(109.13916666666666666666666667, 22.533333333333333333333333333),
                    new BMap.Point(109.075, 23.130555555555555555555555556),
                    new BMap.Point(108.84583333333333333333333333, 23.407777777777777777777777778),
                    new BMap.Point(108.80583333333333333333333333, 23.634444444444444444444444444),
                    new BMap.Point(108.08444444444444444444444444, 23.681944444444444444444444444),
                    new BMap.Point(108.16166666666666666666666667, 23.820833333333333333333333333),
                    new BMap.Point(108.90694444444444444444444444, 25.108888888888888888888888889)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGNNAR02: function(){
                var points = 
                [
                    new BMap.Point(105.54166666666666666666666667, 23.195833333333333333333333333),
                    new BMap.Point(105.8, 24.65),
                    new BMap.Point(107.65, 25.7),
                    new BMap.Point(108.90694444444444444444444444, 25.108888888888888888888888889),
                    new BMap.Point(108.16166666666666666666666667, 23.820833333333333333333333333),
                    new BMap.Point(108.08444444444444444444444444, 23.681944444444444444444444444),
                    new BMap.Point(107.805, 23.178611111111111111111111111),
                    new BMap.Point(106.13861111111111111111111111, 22.9875),
                    new BMap.Point(105.54166666666666666666666667, 23.195833333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGNNAR03: function(){
                var points = 
                [
                    new BMap.Point(106.13861111111111111111111111, 22.9875),
                    new BMap.Point(107.805, 23.178611111111111111111111111),
                    new BMap.Point(108.08444444444444444444444444, 23.681944444444444444444444444),
                    new BMap.Point(108.80583333333333333333333333, 23.634444444444444444444444444),
                    new BMap.Point(108.84583333333333333333333333, 23.407777777777777777777777778),
                    new BMap.Point(109.075, 23.130555555555555555555555556),
                    new BMap.Point(109.13916666666666666666666667, 22.533333333333333333333333333),
                    new BMap.Point(109.21, 22.051111111111111111111111111),
                    new BMap.Point(108.97, 21.63),
                    new BMap.Point(108.8, 21),
                    new BMap.Point(109.25, 20.5),
                    new BMap.Point(108.05, 20.5),
                    new BMap.Point(108.20861111111111111111111111, 21.209722222222222222222222222),
                    new BMap.Point(106.13861111111111111111111111, 22.9875)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZGNNAR04: function(){
                var points = 
                [
                    new BMap.Point(108.90694444444444444444444444, 25.108888888888888888888888889),
                    new BMap.Point(109.84, 24.668333333333333333333333333),
                    new BMap.Point(110.185, 24.128333333333333333333333333),
                    new BMap.Point(111.26666666666666666666666667, 24.148333333333333333333333333),
                    new BMap.Point(111.30666666666666666666666667, 23.478333333333333333333333333),
                    new BMap.Point(110.55, 22.86),
                    new BMap.Point(110.785, 22.468333333333333333333333333),
                    new BMap.Point(109.47, 22.505),
                    new BMap.Point(109.21, 22.051111111111111111111111111),
                    new BMap.Point(109.13916666666666666666666667, 22.533333333333333333333333333),
                    new BMap.Point(109.075, 23.130555555555555555555555556),
                    new BMap.Point(108.84583333333333333333333333, 23.407777777777777777777777778),
                    new BMap.Point(108.80583333333333333333333333, 23.634444444444444444444444444),
                    new BMap.Point(108.08444444444444444444444444, 23.681944444444444444444444444),
                    new BMap.Point(108.16166666666666666666666667, 23.820833333333333333333333333),
                    new BMap.Point(108.90694444444444444444444444, 25.108888888888888888888888889)
                ];
                baiduMap.addLine(points, areaOption);
            }
        };
        this.syArea = {
            addAll: function(){
                this.ZJSYAR01();
                this.ZJSYAR02();
                this.ZJSYAR03();
                this.ZJSYAR04();
            },
            ZJSYAR01: function(){
                var points = 
                [
                    new BMap.Point(108.05, 20.5),
                    new BMap.Point(107.92972222222222222222222222, 19.959166666666666666666666667),
                    new BMap.Point(107.18972222222222222222222222, 19.267777777777777777777777778),
                    new BMap.Point(107.68138888888888888888888889, 18.341111111111111111111111111),
                    new BMap.Point(108.43333333333333333333333333, 17.666666666666666666666666667),
                    new BMap.Point(109.66666666666666666666666667, 17.666666666666666666666666667),
                    new BMap.Point(111.5, 19.5),
                    new BMap.Point(111.5, 20.5),
                    new BMap.Point(108.05, 20.5)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZJSYAR02: function(){
                var points = 
                [
                    new BMap.Point(108.43333333333333333333333333, 17.666666666666666666666666667),
                    new BMap.Point(109.66666666666666666666666667, 17.666666666666666666666666667),
                    new BMap.Point(111.5, 19.5),
                    new BMap.Point(112.5, 18.383333333333333333333333333),
                    new BMap.Point(111.5, 17),
                    new BMap.Point(110.29277777777777777777777778, 16),
                    new BMap.Point(108.43333333333333333333333333, 17.666666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZJSYAR03: function(){
                var points = 
                [
                    new BMap.Point(110.29277777777777777777777778, 16),
                    new BMap.Point(111.5, 17),
                    new BMap.Point(112.5, 18.383333333333333333333333333),
                    new BMap.Point(114, 16.666666666666666666666666667),
                    new BMap.Point(114, 14.5),
                    new BMap.Point(112, 14.5),
                    new BMap.Point(110.29277777777777777777777778, 16)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZJSYAR04: function(){
                var points = 
                [
                    new BMap.Point(108.05, 20.5),
                    new BMap.Point(107.92972222222222222222222222, 19.959166666666666666666666667),
                    new BMap.Point(107.18972222222222222222222222, 19.267777777777777777777777778),
                    new BMap.Point(107.68138888888888888888888889, 18.341111111111111111111111111),
                    new BMap.Point(108.43333333333333333333333333, 17.666666666666666666666666667),
                    new BMap.Point(109.66666666666666666666666667, 17.666666666666666666666666667),
                    new BMap.Point(111.5, 19.5),
                    new BMap.Point(111.5, 20.5),
                    new BMap.Point(108.05, 20.5)
                ];
                baiduMap.addLine(points, areaOption);
            }
        };
        this.zzArea = {
            addAll: function(){
                this.ZHCCAR01();
                this.ZHCCAR02();
                this.ZHCCAR03();
                this.ZHCCAR04();
            },
            ZHCCAR01: function(){
                var points = 
                [
                    new BMap.Point(115.4, 36.666666666666666666666666667),
                    new BMap.Point(115.45, 35.246666666666666666666666667),
                    new BMap.Point(115.66194444444444444444444444, 34.898055555555555555555555556),
                    new BMap.Point(114.71111111111111111111111111, 34.291944444444444444444444444),
                    new BMap.Point(113.90694444444444444444444444, 34.451666666666666666666666667),
                    new BMap.Point(113.29666666666666666666666667, 34.025833333333333333333333333),
                    new BMap.Point(113.00861111111111111111111111, 35.749722222222222222222222222),
                    new BMap.Point(113.81, 36.066666666666666666666666667),
                    new BMap.Point(114.5, 36.316666666666666666666666667),
                    new BMap.Point(115.4, 36.666666666666666666666666667)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZHCCAR02: function(){
                var points = 
                [
                    new BMap.Point(114.25944444444444444444444444, 32.210555555555555555555555556),
                    new BMap.Point(114.39833333333333333333333333, 33.932222222222222222222222222),
                    new BMap.Point(114.30638888888888888888888889, 34.3725),
                    new BMap.Point(114.71111111111111111111111111, 34.291944444444444444444444444),
                    new BMap.Point(115.66194444444444444444444444, 34.898055555555555555555555556),
                    new BMap.Point(115.90833333333333333333333333, 34.516666666666666666666666667),
                    new BMap.Point(115.81333333333333333333333333, 32.908333333333333333333333333),
                    new BMap.Point(114.25944444444444444444444444, 32.210555555555555555555555556)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZHCCAR03: function(){
                var points = 
                [
                    new BMap.Point(110.86666666666666666666666667, 33.533333333333333333333333333),
                    new BMap.Point(113.03138888888888888888888889, 32.583888888888888888888888889),
                    new BMap.Point(113.29666666666666666666666667, 34.025833333333333333333333333),
                    new BMap.Point(113.00861111111111111111111111, 35.749722222222222222222222222),
                    new BMap.Point(110.25, 34.533333333333333333333333333),
                    new BMap.Point(110.48333333333333333333333333, 34.083333333333333333333333333),
                    new BMap.Point(110.86666666666666666666666667, 33.533333333333333333333333333)
                ];
                baiduMap.addLine(points, areaOption);
            },
            ZHCCAR04: function(){
                var points = 
                [
                    new BMap.Point(114.25944444444444444444444444, 32.210555555555555555555555556),
                    new BMap.Point(114.39833333333333333333333333, 33.932222222222222222222222222),
                    new BMap.Point(114.30638888888888888888888889, 34.3725),
                    new BMap.Point(113.90694444444444444444444444, 34.451666666666666666666666667),
                    new BMap.Point(113.29666666666666666666666667, 34.025833333333333333333333333),
                    new BMap.Point(113.03138888888888888888888889, 32.583888888888888888888888889),
                    new BMap.Point(114.07, 32.125),
                    new BMap.Point(114.25944444444444444444444444, 32.210555555555555555555555556)
                ];
                baiduMap.addLine(points, areaOption);
            }
        };
    },
    addAirLine: function() {

    },
    addAirport: function() {

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