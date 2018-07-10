var hourIsLoad = false;
var json_passed;
function changeTab() {
    $('#tabchar > li').hover(function () {
        var $this = $(this);
        $this.find('a').addClass('actived').parent().siblings().find('a').removeClass('actived');
        var $content = $('#tabcontent > div').eq($this.index());
        $content.show().siblings().hide();
        $content.next().css({ 'width': 1216 });
        if (!hourIsLoad) {
            $content.next().css({ 'width': 1216 });
            drawHour24();
            hourIsLoad = true;
        }
    }, function () {
    });
}
$(function () {
    $('#forecast .detail').click(function () {
        var $this = $(this);
        $this.siblings().find('.today').hide();
        $this.find('.day').hide();
        $this.siblings().find('.day').show();
        $this.find('.today').show();

        $('#day' + $this.index()).show().siblings().hide();

        if ($this.index() == 0) {
            $('#hour6').show();
        } else {
            $('#hour6').hide();
        }
    });

    $.getJSON('province.json', function (data) {
        var pSel = '';
        for (var i = 0; i < data.length; i++) {
            pSel += '<option value="' + data[i].code + '">' + data[i].name + '</option>';
        }
        $('#provinceSel').html(pSel);
        $("#provinceSel").val(pcode);
        $('#provinceSel').change(function () {
            var pcode = $(this).val();
            $.getJSON('AHA.json', function (data) {
                var cSel = '<option value="">请选择市区</option>';
                for (var i = 0; i < data.length; i++) {
                    cSel += '<option value="' + data[i].code + '" url="' + data[i].url + '">' + data[i].city + '</option>';
                }
                $('#citySel').html(cSel);
                $("#citySel").val(scode);
            });

        });

        $('#provinceSel option[value="' + pcode + '"]').trigger('change');
        $(document).on('change', '#citySel', function () {
            window.location.href = $(this).find('>option:selected').attr('url');
        });
    });

});
var precipitationunit = "mm";
var pressureunit = "hPa";
var tempunitunit = "℃";
var humidityunit = "%";
var defaultOptions = {
    temperaturename: "温度(" + tempunitunit + ")",
    precipitationname: "降水(" + precipitationunit + ")",
    humidityname: "相对湿度(" + humidityunit + ")",
    pressurename: "气压(" + pressureunit + ")",
    temperature_high_name: "高温(" + tempunitunit + ")",
    temperature_low_name: "低温(" + tempunitunit + ")",
    yaxistitlefont: "11px Tahoma",
    gridcolor: "#C0D0E0",
    altnernatebgcolor: "#f1f1f1",
    bgcolor: null,
    creditstext: "中央气象台",
    creditshref: "http://www.nmc.cn",
    temperaturecolor: "#f78723",//"#f3715c",
    precipitationcolor: "#10f81a",//"#2468a2",
    humiditycolor: "#09b3f1",//"#AA4643",
    pressurecolor: "#585eaa",
    temperature_high_color: "#f3715c",
    temperature_low_color: "#4572A7",
    showtemperature: true,
    showprecipitation: true,
    showhumidity: true,
    showpressure: true,
    show_temperature_high: true,
    show_temperature_low: true
};
Highcharts.setOptions({
    lang: {
        resetZoom: '<font color="blue">还原</font>',
        resetZoomTitle: "重设视图区域1:1",
        loading: "正在载入......",
        downloadPNG: "下载PNG图像",
        downloadJPEG: "下载JPEG图像",
        downloadPDF: "下载PDF文档",
        downloadSVG: "下载SVG的矢量图像",
        exportButtonTitle: "导出到栅格或矢量图像",
        printButtonTitle: "打印图表"
    },
    exporting: {
        filename: "国家气象中心检验平台",
        url: "/highcharts/downchar",
        width: 1200,
        height: 600,
        buttons: {
            printButton: {
                enabled: false
            }
        }
    },
    credits: {
        enabled: true,
        href: defaultOptions.creditshref,
        target: "_blank",
        text: defaultOptions.creditstext
    },
    loading: {
        labelStyle: {
            top: "45%"
        },
        style: {
            position: "absolute",
            backgroundColor: "red",
            opacity: 0.5,
            textAlign: "center"
        }
    }
});
function getDayOfWeek(e) {
    var a = e.substr(0, 4) + "/" + e.substr(4, 2) + "/" + e.substr(6, 2);
    var g = new Date(Date.parse(a));
    var f = new Array("周日", "周一", "周二", "周三", "周四", "周五", "周六");
    var c = new Date();
    var b = c.getFullYear();
    var i = (c.getMonth() + 1) > 9 ? (c.getMonth() + 1) : "0" + (c.getMonth() + 1);
    var d = (c.getDate() > 9) ? c.getDate() : "0" + c.getDate();
    var h = "" + b + i + d;
    if ((parseInt(e) - parseInt(h)) == 0) {
        return '<span style="color:#240481;font-weight:bold;">今天</span>'
    }
    if ((parseInt(e) - parseInt(h)) == 1) {
        return '<span style="color:#240481;font-weight:bold;">明天</span>'
    }
    if ((parseInt(e) - parseInt(h)) == 2) {
        return '<span style="color:#240481;font-weight:bold;">后天</span>'
    }
    return f[g.getDay()]
}
var chartoptions = {
    temperature: {
        options: {
            chart: {
                renderTo: "tempchart",
                backgroundColor: 'rgba(131,132,139,0.5)',
                plotBackgroundColor: 'rgba(0,0,0,0)',
                defaultSeriesType: "spline",
                margin: [50, 80, 50, 80],
                zoomType: "x"
            },
            title: {
                text: ""
            },
            subtitle: {
                text: ""
            },
            xAxis: {
                categories: [],
                labels: {
                    style: {
                        color: '#FFF',
                        fontFamily: "Microsoft YaHei"
                    },
                    formatter: function () {
                        return this.value.substr(4, 4) + "<br/>" + getDayOfWeek(this.value);
                    }
                }
            },
            yAxis: [{
                title: {
                    text: defaultOptions.temperaturename,
                    margin: 3,
                    style: {
                        color: '#FFF',//defaultOptions.temperature_high_color,
                        font: "13px Microsoft YaHei",//defaultOptions.yaxistitlefont,
                        fontWeight: "bold"
                    }
                },
                labels: {
                    style: {
                        color: '#f78723'//defaultOptions.temperature_high_color
                    }
                },
                lineWidth: 1,
                tickColor: defaultOptions.gridcolor,
                lineColor: defaultOptions.gridcolor,
                gridLineColor: defaultOptions.gridcolor,
                tickWidth: 1
            }],
            legend: {
                verticalAlign: "top",
                itemStyle: {
                    color: '#fff',
                    fontWeight: 'bold',
                    fontFamily: "Microsoft YaHei"
                }
            },
            tooltip: {
                shared: true,
                crosshairs: true
            },
            series: [{
                name: defaultOptions.temperature_high_name,
                color: '#f78723',//defaultOptions.temperature_high_color,
                visible: defaultOptions.show_temperature_high,
                lineWidth: 3
            },
            {
                name: defaultOptions.temperature_low_name,
                color: '#27a5f9',//defaultOptions.temperature_low_color,
                visible: defaultOptions.show_temperature_low,
                lineWidth: 3
            }]
        }
    },
    hours: {
        options: {
            chart: {
                renderTo: "hours",
                backgroundColor: 'rgba(131,132,139,0.5)',
                plotBackgroundColor: 'rgba(0,0,0,0)',
                defaultSeriesType: "spline",
                margin: [80, 150, 50, 150],
                zoomType: "x"
            },
            title: {
                text: ""
            },
            subtitle: {
                text: ""
            },
            xAxis: {
                gridLineWidth: 0,
                lineColor: defaultOptions.gridcolor,
                gridLineColor: defaultOptions.gridcolor,
                labels: {
                    style: {
                        color: '#FFF'
                    },
                    formatter: function () {
                        return this.value.substr(11, 2) + '时';
                    }
                }
            },
            yAxis: [{
                title: {
                    text: defaultOptions.temperaturename,
                    margin: 3,
                    style: {
                        color: defaultOptions.temperaturecolor,
                        font: "12px Microsoft YaHei"
                    }
                },
                labels: {
                    style: {
                        color: defaultOptions.temperaturecolor
                    }
                },
                offset: 0,
                tickWidth: 1
            },
            {
                title: {
                    text: defaultOptions.precipitationname,
                    margin: 3,
                    style: {
                        color: defaultOptions.precipitationcolor,
                        font: "12px Microsoft YaHei"
                    }
                },
                labels: {
                    style: {
                        color: defaultOptions.precipitationcolor
                    }
                },
                opposite: true,
                min: 0,
                tickWidth: 1
            },
            {
                title: {
                    text: defaultOptions.humidityname,
                    margin: 3,
                    style: {
                        color: defaultOptions.humiditycolor,
                        font: "12px Microsoft YaHei"
                    }
                },
                labels: {
                    style: {
                        color: defaultOptions.humiditycolor
                    }
                },
                opposite: true,
                offset: 55,
                min: 0,
                tickWidth: 1
            },
            {
                title: {
                    text: defaultOptions.pressurename,
                    margin: 3,
                    style: {
                        color: defaultOptions.pressurecolor,
                        font: "12px Microsoft YaHei"
                    }
                },
                labels: {
                    style: {
                        color: defaultOptions.pressurecolor
                    }
                },
                offset: 45,
                tickWidth: 1
            }],
            legend: {
                verticalAlign: "top",
                itemStyle: {
                    color: '#fff',
                    fontWeight: 'bold'
                }
            },
            tooltip: {
                shared: true,
                crosshairs: true
            },
            plotOptions: {
                series: {
                    events: {
                        hide: function () {
                            this.yAxis.axisTitle.hide()
                        },
                        show: function () {
                            this.yAxis.axisTitle.show()
                        }
                    }
                }
            },
            series: [{
                name: defaultOptions.temperaturename,
                yAxis: 0,
                color: defaultOptions.temperaturecolor,
                visible: defaultOptions.showtemperature
            },
            {
                name: defaultOptions.precipitationname,
                yAxis: 1,
                pointPadding: -0.2,
                groupPadding: 0.2,
                type: "column",
                color: defaultOptions.precipitationcolor,
                visible: defaultOptions.showprecipitation
            },
            {
                name: defaultOptions.humidityname,
                yAxis: 2,
                color: defaultOptions.humiditycolor,
                visible: defaultOptions.showhumidity
            },
            {
                name: defaultOptions.pressurename,
                yAxis: 3,
                color: defaultOptions.pressurecolor,
                visible: defaultOptions.showpressure
            }]
        }
    }
};
/**
 * 最新24小时内整点实况数据图表
 * @param g
 */
function drawHour24() {
    var chart = new Highcharts.Chart(chartoptions.hours.options);
    chart.showLoading();
    var data = $.parseJSON(json_passed);
    //$.getJSON(ctx + '/rest/passed/' + scode, function (data) {
        var line = '', category = [], temperature = [], humidity = [], pressure = [], wind = [], rain1h = [];
        var flag = false;
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].time.substr(11, 2) == '00') {
                line = data.length - i - 1;
                flag = true;
            }
            category.push(data[i].time);
            temperature.push((data[i].temperature >= 9999) ? null : data[i].temperature);
            humidity.push((data[i].humidity >= 9999) ? null : data[i].humidity);
            pressure.push((data[i].pressure >= 9999) ? null : data[i].pressure);
            wind.push({ direction: (data[i].windDirection >= 9999) ? null : data[i].windDirection, speed: (data[i].windSpeed >= 9999) ? null : data[i].windSpeed });
            rain1h.push((data[i].rain1h >= 9999) ? null : data[i].rain1h);
        }

        var d = temperature[23] == null ? '无数据' : temperature[23] + "℃";
        var f = rain1h[23] == null ? '无数据' : rain1h[23] + "mm";
        var e = humidity[23] == null ? '无数据' : humidity[23] + "%";
        var b = pressure[23] == null ? '无数据' : pressure[23] + "hPa";
        $("#hours_title").html("最新整点实况（" + category[23] + "时）：<font color='#f3715c'>气温：" + d + "</font> <font color='#2468a2'>降水：" + f + "</font> <font color='#AA4643'>相对湿度：" + e + "</font> <font color='#585eaa'>气压：" + b + "</font>");

        chart.xAxis[0].setCategories(category, false);
        chart.xAxis[0].addPlotLine({
            color: "green",
            width: 2,
            value: line
        });
        chart.series[0].setData(temperature, false);
        chart.series[1].setData(rain1h, false);
        chart.series[2].setData(humidity, false);
        chart.series[3].setData(pressure, false);
        chart.hideLoading();
        chart.redraw();

    //});

}
/**
 * 温度曲线图表
 * @param b
 */
function drawTemperature(scode) {
    var a = new Highcharts.Chart(chartoptions.temperature.options);
    a.showLoading();
    var category = [], maxTemp = [], minTemp = [];
    var data = $.parseJSON(scode);
    //$.getJSON(ctx + '/rest/tempchart/' + scode, function (data) {
        var from = null;
        for (var i = 0; i < data.length; i++) {
            category.push(data[i].realTime);
            if (data[i].dayImg == '9999' && data[i].nightImg == '9999') {
                from = i;
            }
            if (data[i].dayImg == '9999') {
                maxTemp.push((typeof (data[i].maxTemp) == 'undefined' || data[i].maxTemp >= 9999) ? null : data[i].maxTemp);
            } else {
                if (typeof (data[i].maxTemp) == 'undefined' || data[i].maxTemp >= 9999) {
                    maxTemp.push(null);
                } else {
                    maxTemp.push({
                        y: data[i].maxTemp,
                        marker: {
                            symbol: "url(http://image.nmc.cn/static/site/nmc/themes/basic/weather/color/day/small/" + data[i].dayImg + ".png)"
                        }
                    });
                }
            }
            if (data[i].nightImg == '9999') {
                minTemp.push((typeof (data[i].minTemp) == 'undefined' || data[i].minTemp >= 9999) ? null : data[i].minTemp);
            } else {
                if (typeof (data[i].minTemp) == 'undefined' || data[i].minTemp >= 9999) {
                    minTemp.push(null);
                } else {
                    minTemp.push({
                        y: data[i].minTemp,
                        marker: {
                            symbol: "url(http://image.nmc.cn/static/site/nmc/themes/basic/weather/color/night/small/" + data[i].nightImg + ".png)"
                        }
                    });
                }
            }
        }

        a.xAxis[0].setCategories(category, false);

        if (from != null) {
            a.xAxis[0].addPlotBand({
                from: -0.5,
                to: (from + 1) - 0.5,
                color: "rgba(18, 48, 84, 0.5)",
                id: "plot-band-1"
            });
            a.xAxis[0].addPlotBand({
                from: (from + 1) - 0.5,
                to: (from + 1 + 7) - 0.5,
                color: "rgba(119, 165, 183, 0.5)",
                id: "plot-band-2"
            });
        } else {
            a.xAxis[0].addPlotBand({
                from: -0.5,
                to: data.length - 0.5,
                color: "rgba(119, 165, 183, 0.5)",
                id: "plot-band-2"
            });
        }

        a.series[0].setData(maxTemp, false);
        a.series[1].setData(minTemp, false);
        a.hideLoading();
        a.redraw();

    //});

};

var icomfort = {
    'i9999': { label: '', color: '#e74936' },
    'i4': { label: '很热，极不适应', color: '#e74936' },
    'i3': { label: '热，很不舒适', color: '#f57f1f' },
    'i2': { label: '暖，不舒适', color: '#FF9900' },
    'i1': { label: '温暖，较舒适', color: '#00a44f' },
    'i0': { label: '舒适，最可接受', color: '#53aaae' },
    'i-1': { label: '凉爽，较舒适', color: '#0079c3' },
    'i-2': { label: '凉，不舒适', color: '#2c459c' },
    'i-3': { label: '冷，很不舒适', color: '#754783' },
    'i-4': { label: '很冷，极不适应', color: '#9b479b' }
};
function initReal(citycode) {
    var data = $.parseJSON(citycode);
    //$.ajax({
    //    url: ctx + '/rest/real/' + citycode,
    //    dataType: 'json',
    //    cache: false,
    //    success: function (data) {
    //        $('#realPublishTime').html('更新于：' + data.publish_time + '发布');
    //        $('#realTemperature').html(data.weather.temperature == 9999 ? '-' : (data.weather.temperature + '℃'));
    //        $('#realRain,#todayRain').html(data.weather.rain == 9999 ? '-' : (data.weather.rain + 'mm'));
    //        $('#realWindDirect,#todayDirect').html(data.wind.direct == 9999 ? '-' : (data.wind.direct));
    //        $('#realWindPower,#todayPower').html((data.wind.direct == 9999 || data.wind.power == 9999) ? '-' : (data.wind.power));
    //        $('#realHumidity').html(data.weather.humidity == 9999 ? '-' : (data.weather.humidity + '%'));
    //        $('#realIcomfort').html(data.weather.icomfort == 9999 ? '-' : '<span style="color:' + icomfort['i' + data.weather.icomfort].color + '">' + (icomfort['i' + data.weather.icomfort].label)) + '</span>';
    //        $('#realFeelst').html(data.weather.feelst == 9999 ? '-' : (data.weather.feelst + '℃'));
    //        if (data.warn.alert == 9999) {
    //            $('#realWarn').html('本市预警-暂无');
    //        } else {
    //            $('#realWarn').html(data.warn.alert).attr('href', data.warn.url);
    //        }
    //    }
    //});
    $('#realPublishTime').html('更新于：' + data.publish_time + '发布');
    $('#realTemperature').html(data.weather.temperature == 9999 ? '-' : (data.weather.temperature + '℃'));
    $('#realRain,#todayRain').html(data.weather.rain == 9999 ? '-' : (data.weather.rain + 'mm'));
    $('#realWindDirect,#todayDirect').html(data.wind.direct == 9999 ? '-' : (data.wind.direct));
    $('#realWindPower,#todayPower').html((data.wind.direct == 9999 || data.wind.power == 9999) ? '-' : (data.wind.power));
    $('#realHumidity').html(data.weather.humidity == 9999 ? '-' : (data.weather.humidity + '%'));
    $('#realIcomfort').html(data.weather.icomfort == 9999 ? '-' : '<span style="color:' + icomfort['i' + data.weather.icomfort].color + '">' + (icomfort['i' + data.weather.icomfort].label)) + '</span>';
    $('#realFeelst').html(data.weather.feelst == 9999 ? '-' : (data.weather.feelst + '℃'));
    if (data.warn.alert == 9999) {
        $('#realWarn').html('本市预警-暂无');
    } else {
        $('#realWarn').html(data.warn.alert).attr('href', data.warn.url);
    }
}
var AQIDICT = [];
AQIDICT[1] = { level: '优', ccolor: '#d9fed7', color: '#32f43e', tcolor: '#000', health: '空气质量令人满意,基本无空气污染。', suggestion: '各类人群可正常活动。', background: 'background-position:0 -22px', border: '#6ec129' };
AQIDICT[2] = { level: '良 ', ccolor: '#f7f9cd', color: '#e4f33e', tcolor: '#000', health: '空气质量可接受,但某些污染物可能<br/>对极少数异常,敏感人群健康有较弱影响。', suggestion: '极少数异常敏感人群应减少户外活动。', background: 'background-position:-41px -22px', border: '#e0cf22' };
AQIDICT[3] = { level: '轻度污染 ', ccolor: '#fcebd7', color: '#e19535', tcolor: '#000', health: '易感人群症状有轻度加剧,健康人群<br/>出现刺激症状。', suggestion: '儿童、老年人及心脏病、呼吸系统疾<br/>病患者应减少长时间、高强度的户<br/>外锻炼。', background: 'background-position:-82px -22px', border: '#fd5b30' };
AQIDICT[4] = { level: '中度污染', ccolor: '#f8d7d9', color: '#ec0800', tcolor: '#fff', health: '进一步加剧易感人群症状,可能对健<br/>康人群心脏、呼吸系统有影响。', suggestion: '儿童、老年人及心脏病、呼吸系统疾<br/>病患者避免长时间、高强度的户外<br/>锻炼,一般人群适量减少户外运动。', background: 'background-position:0 -48px', border: '#e10724' };
AQIDICT[5] = { level: '重度污染', ccolor: '#ebd7e3', color: '#950449', tcolor: '#fff', health: '心脏病和肺病患者症状显著加剧,运<br/>动耐受力减低,健康人群普遍出现症状。', suggestion: '老年人和心脏病、肺病患者应停留在<br/>室内，停止户外活动，一般人群减<br/>少户外活动。', background: 'background-position:-41px -48px', border: '#8f0c50' };
AQIDICT[6] = { level: '严重污染', ccolor: '#e7d7dd', color: '#7b001f', tcolor: '#fff', health: '健康人运动耐力减低,有显著强烈症<br/>状,提前出现某些疾病。', suggestion: '老年人和病人应当留在室内，避免体<br/>力消耗，一般人群应避免户外活动。', background: 'background-position:-82px -48px', border: '#410468' };


// 获取空气质量信息
function initAqi(stationcode) {
    var data = $.parseJSON(stationcode);
    //$.ajax({
    //    url: ctx + '/rest/aqi/' + stationcode,
    //    dataType: 'json',
    //    cache: false,
    //    success: function (data) {
            if (data.aq) {
                $('#aqi').html('<span style="color:' + AQIDICT[data.aq].border + '">' + AQIDICT[data.aq].level + '</span>');
            } else {
                $('#aqi').html('-');
            }
    //    }
    //});
}

function climate(maxtemp, mintemp, rain, stationname) {
    $('#container').highcharts({
        chart: {
            type: 'column',
            backgroundColor: 'rgba(131,132,139,0.5)',
            plotBackgroundColor: 'rgba(0,0,0,0)',
            style: {
                fontFamily: 'Microsoft YaHei'
            }
        },
        title: {
            text: stationname + '月平均气温和降水',
            style: {
                color: '#fff',//'#89A54E'
                fontFamily: 'Microsoft YaHei'
            }
        },
        subtitle: {
            //    text: '来源：www.nmc.cn'
        },
        legend: {
            layout: 'horizontal',
            align: 'center',
            verticalAlign: 'top',
            borderWidth: 0,
            x: 0,
            y: 30,
            itemStyle: {
                color: '#8abfdf',
                fontWeight: 'bold'
            }
        },
        xAxis: {
            categories: [
                '一月',
                '二月',
                '三月',
                '四月',
                '五月',
                '六月',
                '七月',
                '八月',
                '九月',
                '十月',
                '十一月',
                '十二月'
            ],
            labels: {
                style: {
                    color: '#FFF'
                }
            }
        },
        yAxis: [{ // Primary yAxis
            labels: {
                format: '{value}°C',
                style: {
                    color: '#f78723'//'#89A54E'
                }
            },
            title: {
                text: '温度',
                style: {
                    color: '#fff'//'#89A54E'
                }
            }
        }, { // Secondary yAxis
            title: {
                text: '降水',
                style: {
                    color: '#fff'//'#4572A7'
                }
            },
            labels: {
                format: '{value} mm',
                style: {
                    color: '#fff'//'#4572A7'
                }
            },
            opposite: true
        }],
        tooltip: {
            shared: true
        },
        series: [{
            name: '最高温度',
            color: '#f78723',//'#AA4643',
            type: 'area',
            data: maxtemp,
            tooltip: {
                valueSuffix: '°C'
            }
        }, {
            name: '最低温度',
            color: '#09b3f1',//'#89A54E',
            type: 'area',
            data: mintemp,
            tooltip: {
                valueSuffix: '°C'
            }
        }, {
            name: '降水量',
            color: '#91c91a',//'#4572A7',
            type: 'column',
            yAxis: 1,
            data: rain,
            tooltip: {
                valueSuffix: ' mm'
            }

        }]
    });
}


function sunriseset(lat, lng) {
    // 日出日落
    var now = new Date();
    var d = now.getDate(),
        m = now.getMonth() + 1,
        y = now.getFullYear(),
        z = 8;

    var obj = Cal(mjd(d, m, y, 0.0), z, lat, lng);

    var minutes = parseInt(now.getHours()) * 60 + parseInt(now.getMinutes());
    var setMinutes = parseInt(obj.set.split(':')[0]) * 60 + parseInt(obj.set.split(':')[1]);
    var riseMinutes = parseInt(obj.rise.split(':')[0]) * 60 + parseInt(obj.rise.split(':')[1]);
    var width = (setMinutes - riseMinutes) / 9;

    var hours = now.getHours() > 9 ? now.getHours() : '0' + now.getHours();
    var minute = now.getMinutes() > 9 ? now.getMinutes() : '0' + now.getMinutes();
    var currentTime = hours + ":" + minute;
    if (currentTime > obj.set || currentTime < obj.rise) {
        $('#sun').removeClass();
    } else {
        $('#sun').addClass('sun' + (9 - Math.floor((setMinutes - minutes) / width)));
    }

    $('#sunrise').text('日出' + obj.rise);
    $('#sunset').text('日落' + obj.set);
}
