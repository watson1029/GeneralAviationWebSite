<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyUnSubmitFlightPlan.aspx.cs" Inherits="FlightPlan_MyUnSubmitFlightPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%-- <div class="gridsearch">
        <label>用户名：</label>
        <input type="text" id="s_UserName" name="s_UserName" class="easyui-validatebox" />
        <input type="button" class="searchbotton easyui-linkbutton " style="margin-left:60px" iconcls="icon-search" value="查询" onclick="Main.FunSearchData()" />
    </div>--%>
    <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>

        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
            <div id="search_menu" style="width: 200px">
                <div name="PlanCode">
                    申请单号
                </div>
            </div>
        </div>

    </div>
    <%--列表 end--%>

    <script type="text/javascript">
        $(function () {
            Main.InitGird();
            Main.InitSearch();
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'FlightPlanID', //排序字段
                    idField: 'FlightPlanID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序
                    frozenColumns: [[//冻结的列，不会随横向滚动轴移动
                        { field: 'cbx', checkbox: true },
                    ]],
                    columns: [[
                        { title: '申请单号', field: 'PlanCode', width: 180 },
                        { title: '任务类型', field: 'FlightType', width: 60 },
                        { title: '航空器呼号', field: 'CallSign', width: 80 },
                        { title: '使用机型', field: 'AircraftType', width: 60 },
                        { title: '航线走向和飞行高度', field: 'FlightDirHeight', width: 150 },
                        {
                            title: '预计开始时间', field: 'StartDate', width: 100, formatter: function (value, rec, index) {

                                var timesstamp = new Date(value);
                                return timesstamp.toLocaleDateString();

                            }
                        },
                        {
                            title: '预计结束时间', field: 'EndDate', width: 100, formatter: function (value, rec, index) {

                                var timesstamp = new Date(value);
                                return timesstamp.toLocaleDateString();

                            }
                        },
                        {
                            title: '起飞时刻', field: 'SOBT', width: 100
                        },
                        { title: '降落时刻', field: 'SIBT', width: 100 },
                        { title: '起飞机场', field: 'ADEP', width: 80 },
                        { title: '降落机场', field: 'ADES', width: 80 },

                        {
                            title: '周执行计划', field: 'WeekSchedule', width: 150, formatter: function (value, rec, index) {
                                var array = [];
                                $.each(value.replace(/\*/g, '').toCharArray(), function (i, n) {
                                    array.push("星期" + n);
                                });
                                return array.join(',');

                            }
                        },
                         { title: '创建人', field: 'CreatorName', width: 60 },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },

                        { title: '状态', field: 'PlanState', formatter: function (value, rec, index) { return value == 0 ? '草稿中' : '' }, width: 50 },
                        {
                            title: '操作', field: 'FlightPlanID', width: 80, formatter: function (value, rec) {
                                var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>';
                                return str;
                            }
                        }
                    ]],
                    toolbar: "#tab_toolbar",
                    queryParams: { "action": "query" },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true //行号
                });
            },

            //初始化搜索框
            InitSearch: function () {
                $("#ipt_search").searchbox({
                    width: 250,
                    searcher: function (val, name) {
                        $('#tab_list').datagrid('options').queryParams.search_type = name;
                        $('#tab_list').datagrid('options').queryParams.search_value = val;
                        $('#tab_list').datagrid('reload');
                    },
                    prompt: '请输入要查询的信息'
                });
            },

            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }


                var json = $.param({ "id": uid, "action": "save" }) + '&' + $('#form_edit').serialize();
                $.post(location.href, json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#edit").dialog("close");
                        }
                    });
                });
            },

            //修改链接 事件
            EditData: function (uid) {
                $("#edit").dialog("open").dialog('setTitle', '编辑');
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");");
                $("#btn_submit").attr("onclick", "Main.Submit(" + uid + ");");
                $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                    $("#form_edit").form('load', data);
                    $("#FlightType").html(data.FlightType);
                    $("#AircraftType").html(data.AircraftType);
                    $("#FlightDirHeight").html(data.FlightDirHeight);
                    $("#ADEP").html(data.ADEP);
                    $("#ADES").html(data.ADES);
                    $("#StartDate").html(new Date(data.StartDate).toLocaleDateString());
                    $("#EndDate").html(new Date(data.EndDate).toLocaleDateString());
                    $("#SOBT").html(data.SOBT);
                    $("#SIBT").html(data.SIBT);
                    $("#Remark").html(data.Remark);
                    var fileArray = data.AttchFile.split('|');
                    for (var i = 0; i < fileArray.length; i++) {
                        var info = fileArray[i].split(','),
                        filepath = dj.root + info[0];
                        $("#AttchFile").html('<a href="{0}" target="_blank" class="upload-filename" title="{1}">{2}</a>'.format(filepath, info[1], info[1]));
                    }
                    $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                        $("#d" + n).attr("checked", true);
                    });
                });
            },

            //删除按钮事件
            Delete: function () {
                var selRow = $('#tab_list').datagrid('getSelections');
                if (selRow.length == 0) {
                    $.messager.alert('提示', '请选择一条记录！', 'info');
                    return;
                }
                var idArray = [];
                for (var i = 0; i < selRow.length; i++) {
                    var id = selRow[i].FlightPlanID;
                    idArray.push(id);
                }
                $.messager.confirm('提示', '确认删除该条记录？', function (r) {
                    if (r) {
                        $.post(location.href, { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

                            if (data.isSuccess) {
                                $("#tab_list").datagrid("reload");
                                selRow.length = 0;
                            }
                        });
                    }
                });
            },
            Submit: function (uid) {

                $.messager.confirm('提示', '确认提交该条飞行计划？', function (r) {
                    if (r) {
                        $.post(location.href, { "action": "submit", "id": uid }, function (data) {

                            if (data.isSuccess) {
                                $("#tab_list").datagrid("reload");
                            }
                        });
                    }
                });

            }
        };
    </script>

    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width: 700px; height: 600px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" method="post">
            <table class="table_edit">
                <tr>
                    <th>任务类型：
                    </th>
                    <td id="FlightType">
                    </td>
                    <th>航空器类型：
                    </th>
                    <td id="AircraftType">
                    </td>
                </tr>
                <tr>
                    <th>航线走向和飞行高度：
                    </th>
                    <td id="FlightDirHeight">
                    </td>
                    <th>航空器呼号：
                    </th>
                    <td id="CallSign">
                    </td>
                </tr>
                <tr>
                    <th>起飞机场：
                    </th>
                    <td id="ADEP">
                    </td>
                    <th>降落机场：
                    </th>
                    <td id="ADES">
                    </td>
                </tr>
                <tr>
                    <th>预计开始日期：
                    </th>
                    <td id="StartDate">
                    </td>
                    <th>预计结束日期：
                    </th>
                    <td id="EndDate">
                    </td>
                </tr>
                <tr>
                    <th>起飞时刻：
                    </th>
                    <td id="SOBT">
                    </td>
                    <th>降落时刻：
                    </th>
                    <td id="SIBT">
                    </td>
                </tr>
                <tr>
                    <th>批件：
                    </th>
                    <td id="AttchFile">
                    </td>
                    <th>周执行计划：
                    </th>
                    <td id="WeekSchedule">
                    </td>
                </tr>
                <tr>
                    <th style="width:160px;">其他需要说明的事项：
                    </th>
                    <td id="Remark">
                    </td>
                </tr>
            </table>
            <div class="datagrid-toolbar">
            <table class="table_edit">
                <tr>
                    <th>航空器架数：
                    </th>
                    <td>
                        <input id="AircraftNum" name="AircraftNum" maxlength="4" type="text" required="true" class="easyui-numberbox" />
                    </td>
                    <th>机长（飞行员）姓名：
                    </th>
                    <td>
                        <input id="Pilot" name="Pilot" maxlength="15" type="text" required="true" class="easyui-textbox" />
                    </td>
                </tr>
                <tr>
                    <th>通信联络方法：
                    </th>
                    <td>
                        <input id="ContactWay" name="ContactWay" maxlength="15" type="text" required="true" class="easyui-textbox" />
                    </td>
                    <th>飞行气象条件：
                    </th>
                    <td>
                        <input id="WeatherCondition" name="WeatherCondition" maxlength="30" type="text" required="true" class="easyui-textbox" />
                    </td>
                </tr>
                <tr>
                    <th>空勤组人数：
                    </th>
                    <td>
                        <input id="AircrewGroupNum" name="AircrewGroupNum" maxlength="4" type="text" required="true" class="easyui-numberbox" />
                    </td>
                    <th style="width:160px;">二次雷达应答机代码：
                    </th>
                    <td>
                        <input id="RadarCode" name="RadarCode"  maxlength="4" type="text" required="true" class="easyui-textbox" />
                    </td>
                </tr>
            </table>
</div>
        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a id="btn_submit" href="javascript:;" class="easyui-linkbutton">保存并提交</a><a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <%--添加 修改 end--%>
</asp:Content>
