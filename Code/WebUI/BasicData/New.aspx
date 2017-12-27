﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="New.aspx.cs" Inherits="BasicData_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
          <script src="<%=Page.ResolveUrl("~/")%>Content/JS/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/ueditor/ueditor.all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server" >

     <%--列表 start--%>
   <table id="tab_list">
        </table>
        <div id="tab_toolbar" style="padding: 2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="ipt_search" menu="#search_menu"/>
                <div id="search_menu" style="width: 200px">
                    <div name="NewTitle">
                        新闻标题
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width: 1000px; height: 800px;"
        modal="true" closed="true" buttons="#edit-buttons">   
        <form id="form_edit" method="post">
    <table class="table_edit">
        <tr>
            <th>新闻标题：
            </th>
            <td>
                <input id="NewTitle" name="NewTitle" maxlength="50" type="text" required="true" class="easyui-validatebox textbox" />
            </td>

            <th>是否置顶：
            </th>
            <td>

                <select id="IsTop" class="easyui-combobox" editable="false" name="IsTop" required="true" panelheight="auto" style="width: 197px; height: 25px;">
                          <option value="0" selected>否</option>
                    <option value="1">是</option>
                </select>
            </td>
        </tr>
        <tr>
             <th>排序：
            </th>
            <td>
              <input id="Sort" name="Sort" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="height:25px" />
            </td>
        </tr>
        <tr>
            <th>新闻内容：
            </th>
            <td colspan="3">
                  <script id="editor" type="text/plain" style="width: 800px; height: 400px;"></script>
            </td>
        </tr>
    </table>
</form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a>
        <a href="javascript:;"class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
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
                    title: '新闻列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'NewID', //排序字段
                    idField: 'NewID', //标识字段,主键
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
                        { title: '新闻标题', field: 'NewTitle', width: 100 },
                        {
                            title: '是否置顶', field: 'IsTop', width: 80, formatter: function (value, rec) {
                                return value==1 ? "是" : "否";
                            }
                        },
     
                        { title: '排序', field: 'Sort', width: 100 },
                        { title: '创建时间', field: 'CreateTime', width: 100 },
                        { title: '创建人', field: 'CreateUser', width: 80 },
                        {
                            title: '操作', field: 'NewID', width: 50, formatter: function (value, rec) {
                                var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;';
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
                $("#edit").dialog("open").dialog('setTitle', '新增新闻');
                $("#form_edit").form('clear');
                UE.getEditor('editor').setContent('');
                $('#IsTop').combobox('select', '0');
                $("#btn_add").attr("onclick", "Main.Save();")
            },
            //提交按钮事件
            Save: function (uid) {

                if (!$("#form_edit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "submit", "SummaryCode": encodeURI(UE.getEditor('editor').getContent()) }) + '&' + $('#form_edit').serialize();

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
                $("#edit").dialog("open").dialog('setTitle', '编辑新闻');
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")
                $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                    $("#form_edit").form('load', data);
                    UE.getEditor('editor').setContent(decodeURI(data.NewContent));
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
                    var id = selRow[i].NewID;
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
            }
        };
    </script>


</asp:Content>


