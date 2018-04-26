<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyFinishAuditFlightPlan.aspx.cs" Inherits="FlightPlan_MyFinishAuditFlightPlan" %>

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
    <div id="tab_toolbar" style="padding: 2px 2px; height: 22px;">
         <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-undo" plain="true" onclick="Main.Export()">导出</a> 
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
                    sortName: 'PlanID', //排序字段
                    idField: 'PlanID', //标识字段,主键
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
                                      { title: '申请单号', field: 'PlanCode', width: 200 },
                        { title: '航空器架数', field: 'AircraftNum', width: 100 },
                        { title: '机长（飞行员）姓名', field: 'Pilot', width: 150 },
                        { title: '通信联络方法', field: 'ContactWay', width: 100 },
                           {
                               title: '起飞时刻', field: 'SOBT', width: 100
                           },
                      {
                          title: '降落时刻', field: 'SIBT', width: 100
                      },
                        { title: '飞行气象条件', field: 'WeatherCondition', width: 100 },
                        { title: '空勤组人数', field: 'AircrewGroupNum', width: 100 },
                        { title: '二次雷达应答机代码', field: 'RadarCode', width: 150 },

                        { title: '公司三字码', field: 'CompanyCode3', width: 100, hidden: 'true' },
                           { title: '公司名称', field: 'CompanyName', width: 100 },
                         { title: '创建人', field: 'CreatorName', width: 80, hidden: 'true' },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150, hidden: 'true' },
                          { title: '审核意见', field: 'Comments', width: 120 },
                          { title: '审核时间', field: 'ActorTime', width: 120 },
                             {
                                 title: '操作', field: 'PlanID', width: 80, formatter: function (value, rec) {
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
            //修改链接 事件
            EditData: function (uid) {
                $("#edit").dialog("open").dialog('setTitle', '编辑');
                $("#btn_edit").attr("onclick", "Main.Save(" + uid + ");")
                $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                    $("#PlanCode").html(data.PlanCode);
                    $("#CompanyName").html(data.CompanyName);
                    $("#FlightType").html(data.FlightType);
                    $("#AircraftType").html(data.AircraftType);
                    $("#FlightArea").html(data.FlightArea);
                    $("#FlightHeight").html(data.FlightHeight);
                    $("#ADEP").html(data.ADEP);
                    $("#ADES").html(data.ADES);

                    $("#SOBT").html(new Date(data.SOBT.dateValFormat()).format("yyyy-MM-dd HH:mm:ss"));
                    $("#SIBT").html(new Date(data.SIBT.dateValFormat()).format("yyyy-MM-dd HH:mm:ss"));
                    $("#Remark").html(data.Remark);
                    $("#AircraftNum").html(data.AircraftNum);
                    $("#Pilot").html(data.Pilot);
                    $("#ContactWay").html(data.ContactWay);
                    $("#WeatherCondition").html(data.WeatherCondition);
                    $("#AircrewGroupNum").html(data.AircrewGroupNum);
                    $("#RadarCode").html(data.RadarCode);
                    $("#CallSign").html(data.CallSign);
                    $("#Alternate").html(data.Alternate);

                    if (data.State == 2) {
                        $("#Auditresult").html("审核通过");
                    }
                    if (data.State == 3) {
                        $("#Auditresult").html("审核不通过");
                    }
                    $("#AuditComment").textbox("setValue", data.Comments);
                });
            },
            Save: function (uid) {
                if ($("#AuditComment").val().length > 200) {
                    $.messager.alert('提示', '"审核意见"不能超过200字符！', 'info');
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
            Export: function () {
                var selRow = $('#tab_list').datagrid('getData');
                if (selRow.total == 0) {
                    $.messager.alert('提示', '无记录导出！', 'info');
                    return;
                }
                console.log($("#ipt_search").val() + "succ");
                window.open("ExportHandler.aspx?type=5&plancode=" + $('#ipt_search').val());
            }

        };
    </script>
    <div id="edit" class="easyui-dialog" style="width: 800px; height: 800px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" method="post">
            <table class="table_edit">
                <tr>
                    <th>申请单号：</th>
                    <td id="PlanCode" style="color: red"></td>
                    <th>公司名称：</th>
                    <td id="CompanyName" style="color: red"></td>
                </tr>
                <tr>
                    <th>任务类型：</th>
                    <td id="FlightType"></td>
                    <th>航空器类型：</th>
                    <td id="AircraftType"></td>
                </tr>
         
                <tr>
                    <%--         <th style="width:176px;">航线走向和飞行高度：</th>
                    <td id="FlightDirHeight"></td>--%>
                    <th>批件：</th>
                    <td id="AttchFile"></td>
                    <th>其他批件：</th>
                    <td id="OtherAttchFile"></td>
                </tr>
                <tr>
                    <th>起飞点：</th>
                    <td id="ADEP"></td>
                    <th>降落点：
                    </th>
                    <td id="ADES"></td>
                </tr>
                <tr>
                    <th>预计开始日期：</th>
                    <td id="StartDate"></td>
                    <th>预计结束日期：</th>
                    <td id="EndDate"></td>
                </tr>
                <tr>
                    <th>起飞时刻：</th>
                    <td id="SOBT"></td>
                    <th>降落时刻：</th>
                    <td id="SIBT"></td>
                </tr>
                <tr>
                    <th>注册号：</th>
                    <td id="CallSign"></td>
                    <th>备降点：</th>
                    <td id="Alternate"></td>
                </tr>
                       <tr>
                    <th>飞行高度：</th>
                    <td id="FlightHeight"></td>
                </tr>
                  <tr>
                    <th style="width: 176px;">飞行范围：</th>
                    <td id="FlightArea"></td>
                </tr>

                <tr>
                    <th style="width: 176px;">其他需要说明的事项：</th>
                    <td id="Remark"></td>
                </tr>
                <tr>
                    <th>航空器架数：</th>
                    <td id="AircraftNum"></td>
                    <th>机长（飞行员）姓名：</th>
                    <td id="Pilot"></td>
                </tr>
                <tr>
                    <th>通信联络方法：</th>
                    <td id="ContactWay"></td>
                    <th>飞行气象条件：</th>
                    <td id="WeatherCondition"></td>
                </tr>
                <tr>
                    <th>空勤组人数：</th>
                    <td id="AircrewGroupNum"></td>
                    <th>二次雷达应答机代码：</th>
                    <td id="RadarCode"></td>
                </tr>
                <tr>
                    <th>审核结果：</th>

                    <td id="Auditresult"></td>

                </tr>
                <tr>
                    <th>审核意见：</th>
                    <td colspan="3">
                        <input id="AuditComment" name="AuditComment" required="true" maxlength="400" style="width: 600px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                    </td>

                </tr>
            </table>
        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_edit" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
</asp:Content>

