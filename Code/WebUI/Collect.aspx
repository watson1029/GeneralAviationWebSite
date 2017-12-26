<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Collect.aspx.cs" Inherits="Collect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>统计界面</title>
    <script src="Content/JS/easyUI/jquery.min.js"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="Content/JS/echarts-all.js"></script>
    <style>
        #main {
            width: 900px;
            height: 400px;
            border: 1px solid #dddddd;
            margin: 10px auto;
        }

        #searchbox {
            width: 900px;
            margin: 10px auto;
            text-align: center;
        }
    </style>
</head>
<body>


    <div id="main">
    </div>
    <div id="searchbox">
        <form id="form1" runat="server" method="post" action="Handler.ashx?action=test">
            <input id="started" class="easyui-datebox" name="started" data-options="required:true" style="width: 200px" label="开始时间" />
            <input id="ended" class="easyui-datebox" name="ended" data-options="required:true" style="width: 200px" label="结束时间" />
            <input id="company" class="easyui-combobox" name="company" style="width:350px" data-options="
					url:'/Handler.ashx?action=getCompanies',
					method:'get',
					valueField:'id',
					textField:'text',
					panelHeight:'auto',
					label: '公司:',
					labelPosition: 'left'
					"/>
            <a href="#" class="easyui-linkbutton" onclick="query();">查询</a>
        </form>
        <a href="Handler.ashx?action=collect2">测试</a>
    </div>

    <script type="text/javascript">
        $('#started').val('2017-01-01');
        $('#ended').val('2018-01-01');

        //初始化echarts图表
        var myChart = echarts.init(document.getElementById('main'));

        var option = {
            //设置标题
            title: {
                text: '长期飞行计划统计表',
                textStyle: {
                    fontSize: 20,
                    fontWeight: 'bolder',
                    color: '#ffd700'
                }
            },
            //设置数值列的颜色
            color: [
                '#ff7f50', '#87cefa', '#da70d6', '#32cd32', '#6495ed',
                '#ff69b4', '#ba55d3', '#cd5c5c', '#ffa500', '#40e0d0',
                '#1e90ff', '#ff6347', '#7b68ee', '#00fa9a', '#ffd700',
                '#6b8e23', '#ff00ff', '#3cb371', '#b8860b', '#30e0e0'
            ],
            //设置是否可以拖拽重计算
            calculable: false,
            //设置提示框
            tooltip: {
                trigger: 'axis'
            },
            //设置工具箱
            toolbox: {
                show: true,
                orient: 'horizontal',
                x: 'right', // 水平安放位置
                y: 'top', // 垂直安放位置
                color: ['#1e90ff', '#22bb22', '#4b0082', '#d2691e'],
                backgroundColor: 'rgba(0,0,0,0)', // 工具箱背景颜色
                borderColor: '#ccc', // 工具箱边框颜色
                borderWidth: 0, // 工具箱边框线宽，单位px，默认为0（无边框）
                padding: 5, // 工具箱内边距，单位px，默认各方向内边距为5，
                showTitle: true,
                feature: {
                    mark: {
                        show: true,
                        title: {
                            mark: '辅助线-开关',
                            markUndo: '辅助线-删除',
                            markClear: '辅助线-清空'
                        },
                        lineStyle: {
                            width: 1,
                            color: '#1e90ff',
                            type: 'dashed'
                        }
                    },
                    dataZoom: {
                        show: true,
                        title: {
                            dataZoom: '区域缩放',
                            dataZoomReset: '区域缩放-后退'
                        }
                    },
                    magicType: {
                        show: true,
                        title: {
                            line: '动态类型切换-折线图',
                            bar: '动态类型切换-柱形图',
                            stack: '动态类型切换-堆积',
                            tiled: '动态类型切换-平铺'
                        },
                        type: ['line', 'bar', 'stack', 'tiled']
                    },
                    restore: {
                        show: true,
                        title: '还原',
                        color: 'black'
                    },
                    saveAsImage: {
                        show: true,
                        title: '保存为图片',
                        type: 'jpeg',
                        lang: ['点击本地保存']
                    }
                }
            },
            ////设置图例
            //legend: {
            //    data: ['长期飞行计划数量']
            //},
            //设置横轴数组
            xAxis: [{
                name: '公司',
                type: 'category',
                boundaryGap: true,
                data: ['fdfd', 'fsss']
            }],
            //设置纵轴数组
            yAxis: [{
                type: 'value',
                name: '长期飞行计划数量'
            }],
            series: [{
                "name": "数量",
                "type": "bar",
                "data": []
            }
            ]
        };

        // 为echarts对象加载数据 
        myChart.setOption(option);
        //myChart.setTheme(myChart.SAKURA);

        $.ajax({
            type: "post",
            async: false, //同步执行
            url: '/Handler.ashx?action=collect',
            dataType: "json", //返回数据形式为json
            data: { started: $('#started').val(), ended: $('#ended').val() },
            success: function (result) {
                //alert(result.category);
                //将返回的category和series对象赋值给options对象内的category和series
                //因为xAxis是一个数组 这里需要是xAxis[i]的形式

                option.xAxis[0].data = result.category;
                option.series = result.series;
                //   options.legend.data = result.legend;

                myChart.hideLoading();
                myChart.setOption(option);
                myChart.refresh();
            },
            error: function (errorMsg) {
                alert("图表请求数据失败!");
            }
        });
    </script>
    <script>
        function query() {
            //alert($('#company').combobox('getText'));
            $.ajax({
                type: "post",
                async: false, //同步执行
                url: '/Handler.ashx?action=collect',
                dataType: "json", //返回数据形式为json
                data: { started: $('#started').val(), ended: $('#ended').val(), company: $('#company').combobox('getText') },
                success: function (result) {
                    //alert(result.category);
                    //将返回的category和series对象赋值给options对象内的category和series
                    //因为xAxis是一个数组 这里需要是xAxis[i]的形式

                    option.xAxis[0].data = result.category;
                    option.series = result.series;
                    //   options.legend.data = result.legend;

                    myChart.hideLoading();
                    myChart.setOption(option);
                    myChart.refresh();
                },
                error: function (errorMsg) {
                    alert("图表请求数据失败!");
                }
            });
        }
    </script>
</body>
</html>
