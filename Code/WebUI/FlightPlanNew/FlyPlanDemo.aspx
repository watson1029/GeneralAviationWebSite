<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FlyPlanDemo.aspx.cs" Inherits="FlightPlanNew_FlyPlanDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;">
        <a href="javascript:void(0)" class="easyui-linkbutton" style="width:120px;" iconcls="icon-add" plain="true" onclick="Main.GeneralData()">生成飞行计划</a>
    </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width: 400px; height: 350px;" modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit"  method="post">
            <table class="table_edit">
                <tr>
                    <td>
                        飞行日期：
                    </td>
                    <td>
                        <input id="PlanDate" name="PlanDate" readonly="true" type="text" class="easyui-datebox" required="required" style="height:20px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        计划开始时间：
                    </td>
                    <td>
                        <input id="PlanBeginTime" name="PlanBeginTime" type="text" class="easyui-timespinner" required="required" style="height:20px;" data-options="showSeconds:false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        计划结束时间：
                    </td>
                    <td>
                        <input id="PlanEndTime" name="PlanEndTime" type="text" class="easyui-timespinner" required="required" style="height:20px;" data-options="showSeconds:false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        机号：
                    </td>
                    <td>
                        <input id="AircraftModel" name="AircraftModel" type="text" class="easyui-validatebox textbox" style="height:20px;"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        实际起飞时间：
                    </td>
                    <td>
                        <input id="TakeOffTime" name="TakeOffTime" type="text" class="easyui-timespinner" style="height:20px;" data-options="showSeconds:false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        实际降落时间：
                    </td>
                    <td>
                        <input id="LandTime" name="LandTime" type="text" class="easyui-timespinner" style="height:20px;" data-options="showSeconds:false"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a>
        <a href="javascript:;" class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <div id="display" class="easyui-dialog" style="width: 850px; height:800px;"
        modal="true" closed="true" buttons="#display-buttons">
        <form id="form_display" method="post">
            <input type="hidden" name="PlanCode" id="PlanCode" value=""/>
            <table class="table_edit">
                <tr>
                    <th>航班号：</th>
                    <td>
                        <input id="CallSign" name="CallSign" readonly="true"  maxlength="30" type="text"  required="true" class="easyui-textbox" style="height:25px"/>
                    </td>
                </tr>
                    <tr>
                        <th>任务类型：
                        </th>
                        <td>
                             <input id="FlightType" name="FlightType" readonly="true" maxlength="30" type="text"  required="true" class="easyui-textbox" style="height:25px"/>
                        </td>
           <th>航空器类型：
                        </th>
                        <td>
                              <input id="AircraftType" name="AircraftType" readonly="true" maxlength="30" type="text"  required="true" class="easyui-textbox" style="height:25px"/>
                        </td>
                    </tr>
            
                    <tr>
                        <th>预计开始日期：
                        </th>
                        <td>
                            <input id="StartDate" name="StartDate" editable="false" readonly="true" required="true"  class="easyui-datebox" style="height:25px"/>
                        </td>
                        <th>预计结束日期：
                        </th>
                        <td>
                            <input id="EndDate" name="EndDate"  editable="false" readonly="true" required="true" class="easyui-datebox" validtype="md['#StartDate']" style="height:25px"/>
                        </td>
                    </tr>
                     <tr>
                        <th>机场及临时起降点：
                        </th>
                        <td colspan="3">
                            <input id="Airport" name="Airport"  required="true" readonly="true" style="width: 600px; height: 150px" type="text" maxlength="200" data-options="multiline:true" class="easyui-textbox" />
                        </td>
                    </tr>   
                    <tr>
                        <th>航线及作业区：
                        </th>
                        <td colspan="3">
                            <input id="FlightArea" name="FlightArea" readonly="true" required="true" style="width: 600px; height: 150px" type="text" maxlength="200" data-options="multiline:true" class="easyui-textbox" />
                        </td>
                    </tr>   
                    <tr>
                        <th>是否紧急任务：
                        </th>
                        <td>
                         <input id="IsUrgentTask" type="checkbox" disabled="disabled" name="IsUrgentTask" value="true" style="width:20px"/>
                        </td>
               
                    </tr>    
                      <tr>   
                <th>是否跨区飞行：
                        </th>
                        <td>
                         <input id="IsCrossArea" type="checkbox" disabled="disabled" name="IsCrossArea" value="true" style="width:20px"/>
                        </td></tr> 
                    <tr>   
                <th>飞行日期跨度是否超过7天：
                        </th>
                        <td>
                         <input id="IsCrossDay" type="checkbox" disabled="disabled" name="IsCrossDay" value="true" style="width:20px"/>
                        </td></tr>
                </table>

            </form>
    </div>
    <div id="display-buttons">
        <a href="javascript:;" class="easyui-linkbutton"  onclick="$('#display').dialog('close');return false;">确定</a>
    </div>
    <%--添加 修改 end--%>

    <script type="text/javascript">
        $(function () {
            Main.InitGird();
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '飞行计划', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'PlanDate', //排序字段
                    idField: 'FlyPlanID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: true,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序
                    fitColumns: true,
                    columns: [[
                        {
                            title: '飞行日期', field: 'PlanDate', width: 200, align: 'center', formatter: function (value, rec) {
                                if (!value || value == '')
                                    return value;
                                else {
                                    var time = new Date(value);
                                    return time.getFullYear() + "年" + (time.getMonth() + 1) + "月" + time.getDate() + "日";
                                }
                            }
                        },
                        { 
                            title: '计划开始时间', field: 'PlanBeginTime', width: 200, align: 'center', formatter: function (value, rec) {
                                if (!value || value == '')
                                    return value;
                                else
                                {
                                    var time = new Date(value);
                                    return time.getHours() + "点" + time.getMinutes() + "分";
                                }
                            }
                        },
                        {
                            title: '计划结束时间', field: 'PlanEndTime', width: 200, align: 'center', formatter: function (value, rec) {
                                if (!value || value == '')
                                    return value;
                                else {
                                    var time = new Date(value);
                                    return time.getHours() + "点" + time.getMinutes() + "分";
                                }
                            }
                        },
                        { title: '机号', field: 'AircraftModel', width: 200, align: 'center' },
                        {
                            title: '实际起飞时间', field: 'TakeOffTime', width: 200, align: 'center', formatter: function (value, rec) {
                                if (!value || value == '')
                                    return value;
                                else {
                                    var time = new Date(value);
                                    return time.getHours() + "点" + time.getMinutes() + "分";
                                }
                            }
                        },
                        {
                            title: '实际降落时间', field: 'LandTime', width: 200, align: 'center', formatter: function (value, rec) {
                                if (!value || value == '')
                                    return value;
                                else {
                                    var time = new Date(value);
                                    return time.getHours() + "点" + time.getMinutes() + "分";
                                }
                            }
                        },
                        {
                            title: '操作', field: 'FlyPlanID', width: 200, align: 'center', formatter: function (value, rec) {
                                var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(&apos;' + value + '&apos;);$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;';
                                str += '<a style="color:red" href="javascript:;" onclick="Main.CheckRepet(&apos;' + value + '&apos;);$(this).parent().click();return false;">查看对应的长期计划</a>';
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
            GeneralData: function () {
                var json = $.param({ "action": "general" });
                $.post("FlyPlanDemo.aspx", json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#edit").dialog("close");
                        }
                    });
                });
            },
            //打开添加窗口
            OpenWin: function () {
                $("#edit").dialog("open").dialog('setTitle', '新增');
                $("#BusyDate").val('');
                $("#BusyBeginTime").val('');
                $("#BusyEndTime").val('');
                $("#btn_add").attr("onclick", "Main.Save();");
            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "submit" }) + '&' + $('#form_edit').serialize();
                $.post("FlyPlanDemo.aspx", json, function (data) {
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
                $("#btn_add").attr("onclick", "Main.Save('" + uid + "');")
                $.post("FlyPlanDemo.aspx", { "action": "queryone", "id": uid }, function (data) {
                    $("#form_edit").form('load', data);
                    if (data.PlanBeginTime && data.PlanBeginTime != '')
                    {
                        var time = new Date(data.PlanBeginTime);
                        $("#PlanBeginTime").timespinner('setValue', time.getHours() + ":" + time.getMinutes());
                    }
                    if (data.PlanEndTime && data.PlanEndTime != '') {
                        var time = new Date(data.PlanEndTime);
                        $("#PlanEndTime").timespinner('setValue', time.getHours() + ":" + time.getMinutes());
                    }
                    if (data.TakeOffTime && data.TakeOffTime != '') {
                        var time = new Date(data.TakeOffTime);
                        $("#TakeOffTime").timespinner('setValue', time.getHours() + ":" + time.getMinutes());
                    }
                    if (data.LandTime && data.LandTime != '') {
                        var time = new Date(data.LandTime);
                        $("#LandTime").timespinner('setValue', time.getHours() + ":" + time.getMinutes());
                    }
                });
            },
            CheckRepet: function (uid) {
                $("#display").dialog("open").dialog('setTitle', '查看长期计划');
                $.post("FlyPlanDemo.aspx", { "action": "display", "id": uid }, function (data) {
                    $("#form_display").form('load', data);
                });
                //$("#btn_add").attr("onclick", "Main.Save(" + uid + ");")
            }
        };
    </script>
</asp:Content>

