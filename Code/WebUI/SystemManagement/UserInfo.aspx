﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="UserInfo.aspx.cs" Inherits="SystemManage_UserInfo" %>

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
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>

            <div style="float:right">
                        <input id="ipt_search" menu="#search_menu"/>
                        <div id="search_menu" style="width: 200px">
                            <div name="UserName">
                                用户名称
                            </div>
                        </div>
</div>
 
        </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
   <div id="edit" class="easyui-dialog" style="width: 400px; height: 350px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit"  method="post">
                <table class="table_edit">
                    <tr>
                        <td>用户名：
                        </td>
                        <td>
                            <input id="UserName" name="UserName" type="text" maxlength="30" class="easyui-validatebox textbox"
                                required="true" style="height:20px;" />
                        </td>
                    </tr>
                    <tr id="pwdrow">
                        <td>密码：
                        </td>
                        <td>
                            <input id="Password" name="Password" type="text"  maxlength="30" class="easyui-validatebox textbox"
                                required="true" style="height:20px;"/>
                        </td>

                    </tr>
                    <tr>
                        <td>手机号码：
                        </td>
                        <td>
                            <input id="Mobile" name="Mobile" maxlength="11" type="text" validType='mobile' required="true" class="easyui-validatebox textbox" style="height:20px;"/>
                        </td>

                    </tr>
                    <tr>   
                        <td>状态：
                        </td>
                        <td>
                            <select id="Status" class="easyui-combobox" editable="false" name="Status" required="true"  panelheight="auto" style="width:197px;height:25px;">
                                 <option value="0">正常</option>
                                 <option value="1">冻结</option>
                               
                            </select>
                        </td>
</tr>
                    <tr>   
                        <td>是否通航用户：
                        </td>
                        <td>

                               <input type="text" id="IsGeneralAviation" name="IsGeneralAviation" class="easyui-validatebox" required="true" style="height:25px;"/>
           
                        </td>
</tr>
                    <tr id="trcompanyCode3">
                         <td>公司名称：
                        </td>
                        <td>
                           <input type="text" id="CompanyCode3"  name="CompanyCode3" editable="false"  class="easyui-combobox" data-options="url:'<%=Page.ResolveUrl("~/FlightPlan/GetComboboxData.ashx?type=3")%>',method:'get',valueField:'id',textField:'text',panelHeight:'auto',panelMaxHeight:200" style="height:25px;"/>
                            <%--<select id="CompanyCode3" class="easyui-combobox" editable="false" name="CompanyCode3"  panelheight=200 style="width:197px;height:25px;"></select>--%>
                        </td>
                    </tr>
                </table>

        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <%--添加 修改 end--%>

        <%--设置角色 start--%>

    <div id="setrole" class="easyui-dialog" style="width: 500px; height: 350px;"
        modal="true" closed="true" buttons="#setrole-buttons">

            <ul id="tt" class="easyui-tree"></ul>
    </div>
    <div id="setrole-buttons">
        <a id="btn_set" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#setrole').dialog('close');return false;">取消</a>
    </div>

   <div id="editpwd" class="easyui-dialog" style="width: 400px; height: 200px;"
        modal="true" closed="true" buttons="#editpwd-buttons">
        <form id="form_editpwd"  method="post">
                <table class="table_edit">
                    <tr>
                        <td>旧密码：
                        </td>
                        <td>
                            <input id="OldPassword" name="OldPassword" type="password" maxlength="30" class="easyui-validatebox textbox"
                                required="true" style="height:20px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>新密码：
                        </td>
                        <td>
                            <input id="NewPassword" name="NewPassword" type="password"  maxlength="30" class="easyui-validatebox textbox"
                                required="true" style="height:20px;"/>
                        </td>
                    </tr>
 <tr>
                        <td>确认密码：
                        </td>
                        <td>
                            <input id="ComfirmPassword" name="ComfirmPassword" type="password"  maxlength="30" validType="euqalTo['#NewPassword']" class="easyui-validatebox textbox"
                                required="true" style="height:20px;"/>
                        </td>
                    </tr>
                </table>

        </form>
    </div>
    <div id="editpwd-buttons">
        <a id="btn_editpwd" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#editpwd').dialog('close');return false;">取消</a>
    </div>
    <%--设置菜单 end--%>
    <script type="text/javascript">

        $(function () {
            Main.InitGird();
            Main.InitSearch();
         $('#IsGeneralAviation').combobox({
                valueField: 'id',
                panelHeight: 'auto',
                textField: 'text',
                data: [{
                    id: 0,
                    text: "否"
                    
                },
                {
                    id: 1,
                    text: "是"
                }
                ],
                onSelect: function (record) {
                    record.id == 1 ? $("#trcompanyCode3").show() : $("#trcompanyCode3").hide();
                }
            });
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '用户列表', //表格标题
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
                        { title: '用户名', field: 'UserName', width: 150 },
                        { title: '手机号码', field: 'Mobile', width: 150 },
                        { title: '创建时间', field: 'CreateTime', width: 150 },
                        { title: '状态', field: 'Status', formatter: function (value, rec, index) { return value == 0 ? '正常' : '冻结' }, width: 150 },
                        { title: '是否通航用户', field: 'IsGeneralAviation', formatter: function (value, rec, index) { return value == 0 ? '否' : '是' }, width: 150 },
                        {
                            title: '操作', field: 'ID', width: 150, formatter: function (value, rec) {
                                var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;';
                                str += '<a style="color:red" href="javascript:;" onclick="Main.EditPassword(' + value + ');$(this).parent().click();return false;">修改密码</a>&nbsp;&nbsp;';
                                str += '<a style="color:red" href="javascript:;" onclick="Main.SetRole(' + value + ');$(this).parent().click();return false;">角色设置</a>';
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
                $("#CompanyCode3").combobox('reload');
                $("#edit").dialog("open").dialog('setTitle', '新增');
             //   $("#form_edit").form('clear');
                $("#UserName").val('');
                $("#Password").val('');
                $("#Mobile").val('');
                $('#Status').combobox('select', '0');
                $('#IsGeneralAviation').combobox('select', '0');
                $("#pwdrow").show();
                $("#trcompanyCode3").hide();
                $("#btn_add").attr("onclick", "Main.Save();")
            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "submit" }) + '&' + $('#form_edit').serialize();

                $.post("UserInfo.aspx", json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#edit").dialog("close");
                        }
                    });
                });
            },
            SetRole: function (uid) {
                $("#setrole").dialog("open").dialog('setTitle', '角色设置');
                $("#btn_set").attr("onclick", "Main.SaveUserRole(" + uid + ");")
                $('#tt').tree({
                    url: 'GetRoleTree.ashx?id=' + uid,
                    method: 'get',
                    animate: true,
                    checkbox: true,
                    cascadeCheck: true,
                    lines: true
                })

            },
            SaveUserRole: function (uid) {
                var nodes = $('#tt').tree('getChecked');

                var idarray = new Array();
                nodes.forEach(function (i) {
                    idarray.push(i.id);
                });
                var json = { id: uid, action: "setrole", newUserRoles: idarray.join() };

                $.post("UserInfo.aspx", json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#setrole").dialog("close");
                        }
                    });
                });
            },
            //修改链接 事件
            EditData: function (uid) {
                $("#CompanyCode3").combobox('reload', '<%=Page.ResolveUrl("~/FlightPlan/GetComboboxData.ashx?type=3")%>');
                $("#edit").dialog("open").dialog('setTitle', '编辑');
                $("#pwdrow").hide();
                $("#trcompanyCode3").hide();
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")
                $.post("UserInfo.aspx", { "action": "queryone", "id": uid }, function (data) {
                    $("#form_edit").form('load', data);
                    if (data.IsGeneralAviation == 1)
                    { $("#trcompanyCode3").show(); }

                });                
            },
            EditPassword: function (uid) {
                $("#form_editpwd").form('clear');
                $("#editpwd").dialog("open").dialog('setTitle', '修改密码');
                $("#btn_editpwd").attr("onclick", "Main.SavePassword(" + uid + ");")
            },
            SavePassword: function (uid) {
                if (!$("#form_editpwd").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "savepwd" }) + '&' + $('#form_editpwd').serialize();

                $.post("UserInfo.aspx", json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#editpwd").dialog("close");
                        }
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
                    var id = selRow[i].ID;
                    idArray.push(id);
                }
                $.messager.confirm('提示', '确认删除该条记录？', function (r) {
                    if (r) {
                        $.post("UserInfo.aspx", { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

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
