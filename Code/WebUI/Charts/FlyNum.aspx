<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlyNum.aspx.cs" Inherits="Charts_FlyNum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/js/echarts/echarts.min.js"></script>
    <script src="/js/jquery-1.10.2.min.js"></script>
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
                        text: '飞行架次数据统计-饼状图',
                        x: 'center'
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                    },
                    color: ["#1B9AF7", "#FF4351", "#FEAE1B", "#7B72E9", "#222", "#A5DE37"],
                    legend: {
                        orient: 'vertical',
                        left: 'left',
                        data: result.NameItem
                    },
                    series: [
                        {
                            name: '飞行架次',
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '60%'],
                            data: result.FlyNumData,
                            itemStyle: {
                                emphasis: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                                }
                            }
                        }
                    ]
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
