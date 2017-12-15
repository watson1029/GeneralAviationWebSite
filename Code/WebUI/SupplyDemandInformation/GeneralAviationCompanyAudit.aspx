<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="GeneralAviationCompanyAudit.aspx.cs" Inherits="SupplyDemandInformation_GeneralAviationCompanyAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px; height: 22px;">
        <a href="javascript:void(0)" class="easyui-button" plain="true"></a>
        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
            <div id="search_menu" style="width: 200px">
                <div name="CompanyName">
                    通航公司
                </div>
            </div>
        </div>
    </div>
    <%--列表 end--%>

    <script type="text/javascript">

        $(function () {
            Main.InitGird();
            Main.InitSearch();
            UE.getEditor('editor');
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'CompanyID', //排序字段
                    idField: 'CompanyID', //标识字段,主键
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
                        { title: '单位名称', field: 'CompanyName', width: 200 },
                        { title: '录入日期', field: 'ModifiedTime', width: 150 },
                        { title: '业务概况', field: 'Summary', width: 500 },
                        {
                            title: '操作', field: 'CompanyID', width: 80, formatter: function (value, rec) {
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
                    $("#form_audit").form('load', data);
                    UE.getEditor('editor').setContent(decodeURI(data.SummaryCode));
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
    <div id="audit" class="easyui-dialog" style="width: 1111px; height: 825px;"
        modal="true" closed="true" buttons="#audit-buttons">
        <form id="form_audit" method="post">
            <table class="table_edit">
                 <tr>
                    <td style="text-align:right">
                        业务概况
                    </td>
                    <td colspan="3">
                        <script id="editor" type="text/plain" style="width: 1000px; height: 450px;"></script>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">撰写人</td>
                    <td><input id="ModifiedByName" name="ModifiedByName" style="width:200px" type="text" readonly="true" required="true" class="easyui-textbox" /></td>
                    <td style="text-align:right">录入日期</td>
                    <td><input id="ModifiedTime" name="ModifiedTime" style="width:200px" type="text" readonly="true" required="true" class="easyui-datebox" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">审核结果</td>
                    <td colspan="3">
                        <select class="easyui-combobox" editable="false" name="Auditresult" required="true" panelheight="auto" style="width: 200px;">
                            <option value="0" selected="true">通过</option>
                            <option value="1">不通过</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">审核意见</td>
                    <td colspan="3">
                        <input id="AuditComment" name="AuditComment" required="true" maxlength="1000" style="width: 1000px;" type="text" data-options="multiline:true" class="easyui-textbox" />
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
