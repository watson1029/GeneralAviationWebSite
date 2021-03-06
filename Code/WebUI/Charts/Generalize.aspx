﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Generalize.aspx.cs" Inherits="Charts_Generalize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="/js/echarts/echarts.min.js"></script>
    <script src="/js/jquery-1.10.2.min.js"></script>
    <title></title>
    <script type="text/javascript">
        var chart, option;
        $(function () {
            ReSize();
            $(window).resize(function () {
                ReSize();
                chart = echarts.init(document.getElementById('chart'));
                chart.setOption(option);
            });
            // 获取图表数据
            var json = '<%=GetData()%>';
            var result = $.parseJSON(json);
            if (result.NameItem.length > 0) {
                // 基于准备好的dom，初始化echarts实例
                chart = echarts.init(document.getElementById('chart'));
                // 指定图表的配置项和数据
                option = {
                    title: {
                        text: '计划服务数据统计-柱状图'
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    color: result.ColorItem,
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    yAxis: {
                        type: 'value'
                    },
                    xAxis: {
                        type: 'category',
                        data: result.TimeItem
                    },
                    series: result.GeneralizeData
                };
                // 使用刚指定的配置项和数据显示图表。
                chart.setOption(option);
            }
        });

        function ReSize() {
            $("#chart").css("height", window.innerHeight - 50);
        }
    </script>
</head>
<body>
    <div id="chart" style="width: 100%;"></div>
</body>
</html>
