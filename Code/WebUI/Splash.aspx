<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Splash.aspx.cs" Inherits="Splash" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="js/echarts/echarts.min.js"></script>
    <script type="text/javascript" src="js/echarts/china.js" ></script>
    <style>
        #china-map {
            width: 1000px;
            height: 1000px;
            margin: auto;
        }
    </style>
</head>
<body>
    <div id="china-map"></div>
<script>
    var myChart = echarts.init(document.getElementById('china-map'));
    var option = {
        title : {
            text: '通航飞行计划申报系统',
            subtext: '请点击省份进行登陆',
            x:'center'
        },
        tooltip : {//提示框组件。
            trigger: 'item'//数据项图形触发，主要在散点图，饼图等无类目轴的图表中使用。
        },
        legend: {
            orient: 'horizontal',//图例的排列方向
            x:'left',//图例的位置
            data:['塔台日高峰']
        },

        visualMap: {//颜色的设置  dataRange
            x: 'left',
            y: 'center',
            splitList: [
                {start: 1500},
                {start: 900, end: 1500},
                {start: 310, end: 1000},
                {start: 200, end: 300},
                {start: 10, end: 200, label: '10 到 200（自定义label）'},
                {start: 5, end: 5, label: '5（自定义特殊颜色）', color: 'black'},
                {end: 10}
            ],
            min: 0,
            max: 1500,
            calculable : true,//颜色呈条状
            text:['高','低'],// 文本，默认为数值文本
            color: ['#E0022B', '#E09107', '#A3E00B']
        },
        toolbox: {//工具栏
            show: true,
            orient : 'vertical',//工具栏 icon 的布局朝向
            x: 'right',
            y: 'center',
            feature : {//各工具配置项。
                mark : {show: true},
                dataView : {show: true, readOnly: false},//数据视图工具，可以展现当前图表所用的数据，编辑后可以动态更新。
                restore : {show: true},//配置项还原。
                saveAsImage : {show: true}//保存为图片。
            }
        },
        roamController: {//控制地图的上下左右放大缩小 图上没有显示
            show: true,
            x: 'right',
            mapTypeControl: {
                'china': true
            }
        },
        series : [
            {
                name: '塔台日高峰',
                type: 'map',
                mapType: 'china',
                roam: false,//是否开启鼠标缩放和平移漫游
                itemStyle:{//地图区域的多边形 图形样式
                    normal:{//是图形在默认状态下的样式
                        label:{
                            show:true,//是否显示标签
                            textStyle: {
                                color: "rgb(249, 249, 249)"
                            }
                        }
                    },
                    emphasis:{//是图形在高亮状态下的样式,比如在鼠标悬浮或者图例联动高亮时
                        label:{show:true}
                    }
                },
                top:"3%",//组件距离容器的距离
                data:[
                    {name: '河南',value: 661},
                    {name: '湖北',value: 553},
                    {name: '湖南',value: 559},
                    {name: '广西',value: 358},
                    {name: '广东',value: 1416},
                    {name: '海南',value: 682}
                ]
            }
        ]
    };
    myChart.setOption(option);
    myChart.on('mouseover', function (params) {
        var dataIndex = params.dataIndex;
        console.log(params);
    });
    myChart.on('click', function (params) {
        if (params.dataIndex == 0) {
            window.location = "Default.aspx";
        }
    });
</script>
</body>
</html>

