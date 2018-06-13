<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="BusyTime_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
    </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width: 400px; height: 350px;" modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit"  method="post">
            <table class="table_edit">
                <tr>
                    <td>
                        日期设置：
                    </td>
                    <td>
                        <input id="BusyDate" name="BusyDate" type="text" class="easyui-datebox" required="required" style="height:20px;" />
                    </td>
                </tr>
                <tr id="pwdrow">
                    <td>
                        繁忙时间段开始时间：
                    </td>
                    <td>
                        <input id="BusyBeginTime" name="BusyBeginTime" type="text" class="easyui-timespinner" required="required" style="height:20px;" data-options="showSeconds:false"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        繁忙时间段结束时间：
                    </td>
                    <td>
                        <input id="BusyEndTime" name="BusyEndTime" type="text" class="easyui-timespinner" required="required" style="height:20px;" data-options="showSeconds:false"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a>
        <a href="javascript:;" class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
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
                    title: '繁忙时间段', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'BusyDate', //排序字段
                    idField: 'BusyID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: true,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序
                    frozenColumns: [[//冻结的列，不会随横向滚动轴移动
                        { field: 'cbx', checkbox: true },
                    ]],
                    columns: [[
                        { title: '日期', field: 'BusyDate', width: 250 },
                        { title: '繁忙时间段开始时间', field: 'BusyBeginTime', width: 250 },
                        { title: '繁忙时间段结束时间', field: 'BusyEndTime', width: 250 },
                        {
                            title: '操作', field: 'BusyID', width: 250, formatter: function (value, row, index) {
                                alert(value);
                                alert(row);
                                alert(index);
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
                $.post("Default.aspx", json, function (data) {
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
                alert(uid);
                $("#edit").dialog("open").dialog('setTitle', '编辑');
                $("#BusyDate").attr("enable", "false");
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")
                $.post("Default.aspx", { "action": "queryone", "id": uid }, function (data) {
                    $("#form_edit").form('load', data);
                });
            }
        };
    </script>
</asp:Content>

