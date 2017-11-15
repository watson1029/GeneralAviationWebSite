<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="UserInfo.aspx.cs" Inherits="SystemManage_UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <form id="form_list" name="form_list" method="post">
        <table id="tab_list">
        </table>
        <div id="tab_toolbar" style="padding: 2px 2px;">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="padding-left: 2px">
                        <a href="#" onclick="$('#form_edit input').val('');Main.OpenWin();return false;" id="a_add"
                            class="easyui-linkbutton" iconcls="icon-add">添加</a> <a href="#" onclick="Main.Delete();return false;"
                                id="a_del" class="easyui-linkbutton" iconcls="icon-cancel">删除</a>
                    </td>
                    <td style="text-align: right; padding-right: 15px">
                        <input id="ipt_search" menu="#search_menu" />
                        <div id="search_menu" style="width: 120px">
                            <div name="UserName">
                                用户名称
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" title="新增编辑" style="width: 500px; height: 350px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" name="form_edit" method="post">
            <div>
                <table class="table_edit">
                    <tr>
                        <td class="tdal">用户名：
                        </td>
                        <td class="tdar">
                            <input id="UserName" name="UserName" type="text" class="easyui-validatebox"
                                required="true" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">密码：
                        </td>
                        <td class="tdar">
                            <input id="Password" name="Password" type="text" class="easyui-validatebox"
                                required="true" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">手机号码：
                        </td>
                        <td class="tdar">
                            <input id="Mobile" name="Mobile" style="width: 100%" type="text" class="easyui-validatebox" />
                        </td>

                    </tr>
                    <tr>   
                        <td class="tdar">状态：
                        </td>
                        <td class="tdar">
                            <select class="easyui-combobox" name="Status" labelPosition="top" panelheight="auto" style="width:100%;">
                                 <option value="1" selected>冻结</option>
                                <option value="0">正常</option>
                            </select>
                        </td>
</tr>
                    <tr>   
                        <td class="tdar">是否通航用户：
                        </td>
                        <td class="tdar">
                            <select class="easyui-combobox" name="IsGeneralAviation" labelPosition="top"  panelheight="auto" style="width:100%;">   
                                <option value="1" selected>是</option>
                                <option value="0">否</option>
                            </select>
                        </td>
</tr>
                </table>
            </div>
        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <%--添加 修改 end--%>
    <script type="text/javascript">

        $(function () {
            Main.InitGird();
            Main.InitSearch();
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '用户列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'JSON_ID', //排序字段
                    idField: 'JSON_ID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序
                    frozenColumns: [[//冻结的列，不会随横向滚动轴移动
                        { field: 'cbx', checkbox: true },
                    ]],
                    columns: [[
                        { title: '用户名', field: 'JSON_UserName', width: 100 },
                        { title: '手机号码', field: 'JSON_Mobile', width: 100 },
                        { title: '创建时间', field: 'JSON_CreateTime', width: 150 },
                        { title: '状态', field: 'JSON_Status', formatter: function (value, rec, index) { return value == 0 ? '正常' : '冻结' }, width: 100 },
                        { title: '是否通航用户', field: 'JSON_IsGeneralAviation', formatter: function (value, rec, index) { return value == 0 ? '否' : '是' }, width: 100 },
                        {
                            title: '操作', field: 'JSON_ID', width: 80, formatter: function (value, rec) {
                                return '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>';
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
                    width: 200,
                    iconCls: 'icon-save',
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
                $("#edit").dialog("open");
                $("#btn_add").attr("onclick", "Main.Save(); return false;")
            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "submit" }) + '&' + $('#form_edit').serialize();
   
                $.post("UserInfo.aspx", json, function (data) {
                    $.messager.alert('提示', data, 'info', function () {
                        if (data.indexOf("成功") > 0) {
                            $("#tab_list").datagrid("reload");
                            $("#edit").dialog("close");
                        }
                    });
                });
            },

            //修改链接 事件
            EditData: function (uid) {
                $("#edit").dialog("open");
                $("#btn_add").attr("onclick", "Main.Save(" + uid + "); return false;")

                $.post("UserInfo.aspx", { "action": "queryone", "id": uid }, function (data) {
                    var dataObj = eval("(" + data + ")"); //转换为json对象
                    $("#form_edit").form('load', dataObj);
                });
            },

            //删除按钮事件
            Delete: function () {
                var idArray = [];
                $($('#tab_list').datagrid('getSelected')).each(function () {
                    idArray.push(this.JSON_ID);
                });
                if (idArray.length <= 0) { 
                    $.messager.alert('提示', '请选择一条记录！', 'info');
                    return;
                }
                $.messager.confirm('提示', '确认删除该条记录？', function (r) {
                    if (r) {
                        $.post("UserInfo.aspx", { "action": "del", "cbx_select": idArray.join(',') }, function (data) {
                            $.messager.alert('提示', data, 'info', function () { $("#tab_list").datagrid("reload"); });
                        });
                    }
                });
            }
        };
    </script>
</asp:Content>
