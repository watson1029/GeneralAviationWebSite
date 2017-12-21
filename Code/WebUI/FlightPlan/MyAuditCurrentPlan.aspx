<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyAuditCurrentPlan.aspx.cs" Inherits="FlightPlan_MyAuditCurrentPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px; height: 22px;">
        <a href="javascript:void(0)" class="easyui-button" plain="true"></a>
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

                    columns: [[
                        { title: '申请单号', field: 'PlanCode', width: 180 },
                        { title: '航空器架数', field: 'AircraftNum', width: 100 },
                        { title: '机长（飞行员）姓名', field: 'Pilot', width: 150 },
                        { title: '通信联络方法', field: 'ContactWay', width: 100 },
                        { title: '飞行气象条件', field: 'WeatherCondition', width: 100 },
                        { title: '空勤组人数', field: 'AircrewGroupNum', width: 100 },
                        { title: '二次雷达应答机代码', field: 'RadarCode', width: 150 },

                        { title: '公司三字码', field: 'CompanyCode3', width: 100 },
                         { title: '创建人', field: 'CreatorName', width: 60 },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },
                             {
                                 title: '操作', field: 'FlightPlanID', width: 80, formatter: function (value, rec) {
                                     var str = '<a style="color:red" href="javascript:;" onclick="Main.Audit(' + value + ');$(this).parent().click();return false;">审核</a>';
                                     return str;
                                 }
                             },


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
            //审核
            Audit: function (uid) {
                $("#audit").dialog("open").dialog('setTitle', '审核');
                $("#btn_audit").attr("onclick", "Main.AuditSubmit(" + uid + ");")
                $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                    //    $("#form_audit").form('load', data);
                    $("#PlanCode").html(data.PlanCode);
                    $("#FlightType").html(data.FlightType);
                    $("#AircraftType").html(data.AircraftType);
                    $("#FlightDirHeight").html(data.FlightDirHeight);
                    $("#ADEP").html(data.ADEP);
                    $("#ADES").html(data.ADES);
                    $("#SOBT").html(new Date(data.SOBT).toDateString());
                    $("#SIBT").html(new Date(data.SIBT).toDateString());
                    $("#Remark").html(data.Remark);
                    $("#AircraftNum").html(data.AircraftNum);
                    $("#Pilot").html(data.Pilot);
                    $("#ContactWay").html(data.ContactWay);
                    $("#WeatherCondition").html(data.WeatherCondition);
                    $("#AircrewGroupNum").html(data.AircrewGroupNum);
                    $("#RadarCode").html(data.RadarCode);

                });
            },
            AuditSubmit: function (uid) {

                if (!$("#form_audit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "auditsubmit" }) + '&' + $('#form_audit').serialize();

                $.post(location.href, json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#audit").dialog("close");
                        }
                    });
                });

            }

        };
    </script>

    <%--添加 修改 start--%>
    <div id="audit" class="easyui-dialog" style="width: 700px; height: 700px;"
        modal="true" closed="true" buttons="#audit-buttons">
        <form id="form_audit" method="post">
            <table class="table_edit">
                <tr>
                    <th>任务类型：</th>
                    <td id="FlightType"></td>
                    <th>航空器类型：</th>
                    <td id="AircraftType"></td>
                </tr>
            <tr>
                    <th style="width:176px;">航线走向和飞行高度：</th>
                    <td id="FlightDirHeight"></td>
                    <th>批件：</th>
                    <td id="AttchFile"></td>
                </tr>
                  <tr>
              <th>起飞机场：</th>
                    <td id="ADEP"></td>
                    <th>降落机场：
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
                    <th style="width:176px;">其他需要说明的事项：</th>
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
                    <td >
                        <select class="easyui-combobox" editable="false" name="Auditresult" required="true" panelheight="auto" style="width: 200px;">
                            <option value="0" selected="true">通过</option>
                            <option value="1">不通过</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>审核意见：</th>
                    <td colspan="3">
                        <input id="AuditComment" name="AuditComment" required="true" maxlength="400" style="width: 400px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                    </td>

                </tr>
            </table>
        </form>
    </div>
    <div id="audit-buttons">
        <a id="btn_audit" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#audit').dialog('close');return false;">取消</a>
    </div>

</asp:Content>