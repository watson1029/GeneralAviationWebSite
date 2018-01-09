<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="GeneralAviationCompanyUnSubmit.aspx.cs" Inherits="SupplyDemandInformation_GeneralAviationCompanyUnSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/ueditor/ueditor.all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <table id="tab_list"></table>
    <div id="tab_toolbar" style="padding: 2px 2px;">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
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
    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width: 1050px; height: 770px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" method="post">
            <table class="table_edit">
                <tr>
                    <td style="text-align: right">宣传标题
                    </td>
                    <td colspan="3">
                         <input id="Title" name="Title" type="text" required="true" class="easyui-textbox" style="width: 900px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">宣传介绍
                    </td>
                    <td colspan="3">
                        <script id="editor" type="text/plain" style="width: 900px; height: 350px;"></script>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">撰写人</td>
                    <td>
                        <input id="ModifiedByName" name="ModifiedByName" maxlength="30" type="text" readonly="true" required="true" class="easyui-textbox" /></td>
                    <td style="text-align: right">录入日期</td>
                    <td>
                        <input id="ModifiedTime" name="ModifiedTime" style="width: 200px" type="text" readonly="true" required="true" class="easyui-datebox" /></td>
                </tr>
            </table>
        </form>
    </div>

    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <%--添加 修改 end--%>
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
                    frozenColumns: [[//冻结的列，不会随横向滚动轴移动
                        { field: 'cbx', checkbox: true },
                    ]],
                    columns: [[
                        { title: '单位名称', field: 'CompanyName', width: 200 },
                        { title: '录入日期', field: 'ModifiedTime', width: 150 },
                        { title: '宣传标题', field: 'Title', width: 200 },
                        { title: '宣传介绍', field: 'Summary', width: 500 },
                        {
                            title: '状态', field: 'State', formatter: function (value, rec, index) {
                                var str = "";
                                if (value == 0) {
                                    str = '草稿中';
                                }
                                if (value == "end") {
                                    str = "审核通过";
                                }
                                else if (value == "Deserted") {
                                    str = "审核不通过";
                                }
                                return str;
                            }, width: 60
                        },
                        {
                            title: '操作', field: 'ID', width: 80, formatter: function (value, rec) {
                                var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;<a style="color:red" href="javascript:;" onclick="Main.Submit(' + value + ');$(this).parent().click();return false;">提交</a>';
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

            //打开添加窗口
            OpenWin: function () {
                if (screen.height == 768) {
                    $("#edit").dialog({ title: '新增', top: 0, height: 500 }).dialog("open");
                }
                else {
                    $("#edit").dialog("open").dialog('setTitle', '新增');
                }
                $("#form_edit").form('clear');
                $.post(location.href, { "action": "init" }, function (data) {
                    $("#form_edit").form('load', data);
                });
                $("#btn_add").attr("onclick", "Main.Save();")
            },

            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "save", "Title": $("#Title").val(), "SummaryCode": encodeURI(UE.getEditor('editor').getContent()), "Summary": UE.getEditor('editor').getContentTxt(), "ModifiedTime": $("#ModifiedTime").datebox("getValue") });
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
            EditData: function (id) {
                if (screen.height == 768) {
                    $("#edit").dialog({ title: '编辑', top: 0, height: 500 }).dialog("open");
                }
                else {
                    $("#edit").dialog("open").dialog('setTitle', '编辑');
                }
                $("#btn_add").attr("onclick", "Main.Save(" + id + ");")
                $.post(location.href, { "action": "queryone", "id": id }, function (data) {
                    $("#form_edit").form('load', data);
                    UE.getEditor('editor').setContent(decodeURI(data.SummaryCode));
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
                    var id = selRow[i].ID;
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

                $.messager.confirm('提示', '确认提交该条供求信息？', function (r) {
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

</asp:Content>
