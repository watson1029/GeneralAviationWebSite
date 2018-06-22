<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="FlightPlanStatistics.aspx.cs" Inherits="FlightPlan_FlightPlanStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px; height: 22px;">
        <div style="float: left">
            <input id="started" class="easyui-datebox"  name="started" data-options="required:true,validType:'equaldDate1[\'#ended\']'" style="width: 250px" label="开始时间" />
            <input id="ended" class="easyui-datebox" name="ended" data-options="required:true,validType:'equaldDate[\'#started\']'" style="width: 250px" label="结束时间" />
            <a href="#" class="easyui-linkbutton" onclick="query();">查询</a>
              <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-undo" plain="true" onclick="Main.Export()">导出</a> 
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#started").datebox('setValue', getNowFormatDate());
            $("#ended").datebox('setValue', getNowFormatDate(1));
            Main.InitGird();
            $.extend($.fn.validatebox.defaults.rules, {
                equaldDate: {
                    validator: function (value, param) {
                        if (param == undefined) {
                            return $("#ended").datebox('getValue')>value;
                        }
                        else {
                            var start = $(param[0]).datetimebox('getValue');  //获取开始时间    
                            return value > start;
                        }//有效范围为当前时间大于开始时间
                        //var start =new Date($("#started").datebox('getValue').replace(/-/g,"/"));
                        //var end =new Date($("#ended").datebox('getValue').replace(/-/g, "/"));
                        //return end - start>0;
                    },
                    message: '结束日期应大于开始日期!'                     //匹配失败消息  
                },
                //equaldDate1: {
                //    validator: function (value, param) {
                //        var end = $(param[0]).datetimebox('getValue');  //获取开始时间    
                //        return end > value;                             //有效范围为当前时间大于开始时间   
                //    },
                //    message: '结束日期应大于开始日期!'                     //匹配失败消息  
                //},
            });
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'ActorTime', //排序字段
                    idField: 'ID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序

                    columns: [[
                        { title: '航空公司', field: 'CompanyName', width: 150 },
                        { title: '飞行时长', field: 'SecondDiff', width: 80 },
                        { title: '飞行架次(数)', field: 'AircraftNum', width: 100 },
                                                     {
                                                         title: '操作', field: 'Creator', width: 80, formatter: function (value, rec) {
                                                             var str = '<a style="color:red" href="javascript:;" onclick="Main.Detail(' + value + ');$(this).parent().click();return false;">查看</a>';
                                                             return str;
                                                         }
                                                     }
                    ]],
                    toolbar: "#tab_toolbar",
                    queryParams: { "action": "query", "started": $("#started").datebox("getValue"), "ended": $("#ended").datebox("getValue") },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true //行号
                });
            },

            //初始化表格
            InitGirdDetail: function (uid) {
                $('#tab_list1').datagrid({
                    title: '详情列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'Creator', //排序字段
                    idField: 'Creator', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '95%', //宽度
                    height: 150, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠

                    columns: [[
                        { title: '航空公司', field: 'CompanyName', width: 150 },
                        { title: '飞行架次(数)', field: 'AircraftNum', width: 150 },
                        { title: '起飞时刻', field: 'SOBT', width: 150 },
                        { title: '降落时刻', field: 'SIBT', width: 150 },
                        { title: '时长', field: 'StepID', width: 150 }
                    ]],
                    queryParams: { "action": "getinstance", "Creator": uid, "started": $("#started").datebox("getValue"), "ended": $("#ended").datebox("getValue") },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true //行号
                });
            },
            Detail: function (uid) {
                $("#detail").dialog("open").dialog('setTitle', '查看');
                Main.InitGirdDetail(uid);
            },
            ////初始化搜索框
            Export: function () {
                var selRow = $('#tab_list').datagrid('getData');
                if (selRow.total == 0) {
                    $.messager.alert('提示', '无记录导出！', 'info');
                    return;
                }
                //console.log($("#ipt_search").val() + "succ");
                window.open("ExportHandler.aspx?type=FlightPlanStatistics&started=" + $("#started").datebox("getValue") + "&ended=" + $("#ended").datebox("getValue"));
            }

        };
        function query() {
            if ($("#started").datebox("getValue") < $("#ended").datebox("getValue")) {
                Main.InitGird();
            }
            //$('#tab_list').datagrid('reload');
        }
        function getNowFormatDate(obj) {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (obj != undefined && obj != null) {
                //strDate = date.setDate(strDate+parseInt(obj));
                strDate = strDate + parseInt(obj)
            }
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            //var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            //        + " " + date.getHours() + seperator2 + date.getMinutes()
            //        + seperator2 + date.getSeconds();
            var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate;
            return currentdate;
        }
    </script>

    <div id="detail" class="easyui-dialog" style="width: 700px; height: 700px;"
        modal="true" closed="true" buttons="#detail-buttons">
        <div id="con" style="margin-left: 15px;">
            <div id="tab_list1">
            </div>
        </div>
    </div>
    <div id="detail-buttons">
        <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#detail').dialog('close');return false;">取消</a>
    </div>
</asp:Content>
