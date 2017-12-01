<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyAuditCurrentPlan.aspx.cs" Inherits="FlightPlan_MyAuditCurrentPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;">
        <a></a>
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
      <%--添加 修改 start--%>
   <div id="audit" class="easyui-dialog" style="width: 700px; height:700px;"
        modal="true" closed="true" buttons="#audit-buttons">
        <form id="form_audit"  method="post">
                <table class="table_edit">
      
                    <tr>
                        <td class="tdal">任务类型：
                        </td>
                        <td class="tdar">
                            <input id="FlightType" name="FlightType" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200" readonly="true"  class="easyui-combobox" />
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">航空器类型：
                        </td>
                        <td class="tdar">
                          <%--  <input id="AircraftType" name="AircraftType"  maxlength="30" type="text"  required="true" class="easyui-textbox" />--%>
                            <input id="AircraftType" name="AircraftType" data-options="url:'GetComboboxData.ashx?type=2',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200" readonly="true" class="easyui-combobox" />
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">航线走向和飞行高度：
                        </td>
                        <td class="tdar">
                            <input id="FlightDirHeight" name="FlightDirHeight"  readonly="true" maxlength="30" type="text"   class="easyui-textbox" />
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">起飞机场：
                        </td>
                        <td class="tdar">
                            <input id="ADEP" name="ADEP"  maxlength="30" type="text"  readonly="true" class="easyui-textbox" />
                        </td>

                    </tr>
                         <tr>
                        <td class="tdal">降落机场：
                        </td>
                        <td class="tdar">
                            <input id="ADES" name="ADES"  maxlength="30" type="text"  readonly="true" class="easyui-textbox" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">预计开始日期：
                        </td>
                        <td class="tdar">
                            <input id="StartDate" name="StartDate" style="width:200px" type="text"  readonly="true" class="easyui-datebox" />
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">预计结束日期：
                        </td>
                        <td class="tdar">
                            <input id="EndDate" name="EndDate"  style="width:200px"  type="text"  readonly="true" class="easyui-datebox" />
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">起飞时刻：
                        </td>
                        <td class="tdar">
                            <input id="SOBT" name="SOBT" style="width:200px" type="text"  readonly="true" class="easyui-timespinner" />
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">降落时刻：
                        </td>
                        <td class="tdar">
                            <input id="SIBT" name="SIBT" style="width:200px" type="text"  readonly="true" class="easyui-timespinner" />
                        </td>

                    </tr>
                          <tr>
                        <td class="tdal">批件：
                        </td>
                        <td class="tdar">
                                 <input id="AttchFile" name="AttchFile"  maxlength="30" type="text"  readonly="true" class="easyui-textbox" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">周执行计划：
                        </td>
                        <td class="tdar">
                                 <input id="WeekSchedule" name="WeekSchedule"  maxlength="30" type="text"  readonly="true" class="easyui-textbox" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">其他需要说明的事项：
                        </td>
                        <td class="tdar">
                            <input id="Remark" name="Remark"  maxlength="200" style="width:300px;height:100px" type="text" data-options="multiline:true"  class="easyui-textbox" />
                        </td>

                    </tr>
                      <tr>   
                        <td class="tdal">审核结果：
                        </td>
                        <td class="tdar">
                            <select class="easyui-combobox" editable="false"  name="Auditresult" required="true"   panelheight="auto" style="width:100%;">   
                                <option value="0" selected="true">通过</option>
                                <option value="1">不通过</option>
                            </select>
                        </td>
</tr>
                      <tr>
                        <td class="tdal">审核意见：
                        </td>
                        <td class="tdar">
                            <input id="AuditComment" name="AuditComment"   maxlength="400" style="width:400px;height:100px" type="text" data-options="multiline:true"  class="easyui-textbox" />
                        </td>

                    </tr>
                </table>

        </form>
    </div>
    <div id="audit-buttons">
        <a id="btn_audit" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
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
                    sortName: 'ID', //排序字段
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
                        { title: '申请单号', field: 'PlanCode', width: 100 },
                        { title: '任务类型', field: 'FlightType', width: 100 },
                        { title: '使用机型', field: 'AircraftType', width: 100 },
                        { title: '航线走向和飞行高度', field: 'FlightDirHeight', width: 150 },
                        { title: '预计开始时间', field: 'StartDate', width: 100 },
                        { title: '预计结束时间', field: 'EndDate', width: 100 },
                        { title: '起飞时刻', field: 'SOBT', width: 100 },
                        { title: '降落时刻', field: 'SIBT', width: 100 },
                        { title: '起飞机场', field: 'ADEP', width: 100 },
                        { title: '降落机场', field: 'ADES', width: 100 },
                        { title: '公司三字码', field: 'CompanyCode3', width: 100 },
                         { title: '创建人', field: 'Creator', width: 100 },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },
                             {
                                 title: '操作', field: 'RepetPlanID', width: 80, formatter: function (value, rec) {
                                     var str = '<a style="color:red" href="javascript:;" onclick="Main.Audit(' + value + ');$(this).parent().click();return false;">审核</a>';
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
            //审核
            Audit: function (uid) {
                $("#audit").dialog("open").dialog('setTitle', '审核');
                $("#btn_audit").attr("onclick", "Main.AuditSubmit(" + uid + ");")

                $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                    $("#form_audit").form('load', data);
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
</asp:Content>
