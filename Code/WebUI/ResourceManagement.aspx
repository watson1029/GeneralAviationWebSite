﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceManagement.aspx.cs" Inherits="ResourceManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>资料录入管理</title>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script>
        var id;
        var index;
        var type;
        $(function () {
            $('#dg').datagrid({
                url: "/Handler.ashx?action=get"
            });
            $('#dialog').dialog({
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-ok',
                    handler: function () {
                        if (id ==0) {
                            add();
                        } else {
                            update();
                        }
                    }
                }, {
                    text: '重置',
                    handler: function () {
                        clearForm();
                    }
                }]

            });
            $('#hint').dialog({
                buttons: [{
                    text: '删除',
                    iconCls: 'icon-ok',
                    handler: function () {
                        $.ajax({
                            url: '/Handler.ashx?action=delete&id=' + id,
                            async: false,
                            success: function (data) {
                                alert(data);
                                location.reload();
                            }
                        });
                        $('#hint').dialog('close');
                    }
                }, {
                    text: '取消',
                    handler: function () {
                        $('#hint').dialog('close');
                    }
                }]

            });
            close1();
        });
        function open1(id) {
            this.id = id;
            if (id == 0) {
                clearForm();
            }
            $('#dialog').dialog('open');
        }
        function close1() {
            $('#dialog').dialog('close');
        }
        function get() {
            $('#dg').datagrid({
                url: "/Handler.ashx?action=get",
                queryParams: { type: +$('#type option:selected').val(), status: $('#status option:selected').val() }
            });
        }
    </script>
</head>
<body class="easyui-layout">

    <div data-options="region:'center',title:'Center'" style="margin-left: 150px; margin-right: 150px;">
        <div class="easyui-panel" title="通航资料">
            <select class="easyui-combobox" id="status" name="status" label="资料状态:" labelposition="left" style="width: 200px; margin: 20px;">
                <option value="1">草稿中</option>
                <option value="2">已提交</option>
            </select><br />
            <select class="easyui-combobox" id="type" name="type" label="资料类别:" labelposition="left" style="width: 300px; margin: 20px;">
                <option value="1">国家和民航相关通航政策、管理规定</option>
                <option value="2">中南地区通航管理规定</option>
                <option value="3">河南空管通航管理相关程序</option>
                <option value="4">应急救援相关程序</option>
            </select><br />
            <a class="easyui-linkbutton" id="bt_query" onclick="get();">查询</a>
            <a class="easyui-linkbutton" id="bt_add" onclick="open1(0);">新增</a>
        </div>
        <div class="easyui-panel" title="资料列表">
            <div style="margin: 20px 0;"></div>
            <table id="dg" title="通航资料" style="width: 1308px; height: 350px" data-options="pageSize:10,rownumbers:true,singleSelect:true,pagination:true,method:'post'">
                <thead>
                    <tr>
                        <th data-options="field:'Created',width:155,align:'center',formatter:formatDate">时间</th>
                        <th data-options="field:'Title',width:350,align:'center'">标题</th>
                        <th data-options="field:'DealUser',width:100,align:'center'">处理人</th>
                        <th data-options="field:'ResourceType',width:220,align:'center',formatter:formatType">资料类别</th>
                        <th data-options="field:'UsefulTime',width:150,align:'center'">有效时间</th>
                        <th data-options="field:'FilePath',width:100,align:'center',formatter:formatFile">附件</th>
                        <th data-options="field:'Status',width:100,align:'center',formatter:formatStatus">状态</th>
                        <th data-options="field:'ID',width:100,align:'center',formatter:formatOperation">操作</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div title="确定删除？" closed="true" class="easyui-dialog" id="hint" style="width: 300px; height: auto;">
        <h3>确定删除这条资料信息吗？</h3>
    </div>


    <div title="新增通航资料" class="easyui-dialog" id="dialog" style="width: 400px; height: auto; padding: 10px 20px;">
        <div style="margin-bottom: 20px">
            <form runat="server" method="post" id="ff" action="Handler.ashx?action=add">
                <input id="id" type="hidden" />
                <input id="title" class="easyui-textbox" name="title" style="width: 100%" data-options="label:'标题:',required:true" />
                <input id="dealuser" class="easyui-textbox" name="dealuser" style="width: 100%" data-options="label:'处理人:',required:true" />
                <select id="resourcetype" class="easyui-combobox" name="resourcetype" style="width: 100%" data-options="label:'处理类别:'">
                    <option value="1">国家和民航相关通航政策、管理规定</option>
                    <option value="2">中南地区通航管理规定</option>
                    <option value="3">河南空管通航管理相关程序</option>
                    <option value="4">应急救援相关程序</option>
                </select>
                <input id="usefultime" class="easyui-textbox" name="usefultime" style="width: 100%" data-options="label:'有效时间:',required:true" />
                附件：<asp:FileUpload ID="file" runat="server" Visible="true" />
            </form>
        </div>
        <script>
            function delete1(id) {
                this.id = id;
                $('#hint').dialog('open');
            }
            function showupdate(id, index) {
                this.id = id;
                this.index = index;

                var row = $('#dg').datagrid('getData');
                var data = row.rows[index];
                $('#title').textbox('setValue', data.Title);
                $('#id').val(id);
                $('#dealuser').textbox('setValue', data.DealUser);
                $('#resourcetype').combobox('select', data.ResourceType);
                $('#usefultime').textbox('setValue', data.UsefulTime);
                open1(id);
            }
            function formatOperation(val, row, rowIndex) {
                var btn = "<a onclick='showupdate(" + val + "," + rowIndex + ");' href='javascript:void();' style='margin:5px;'>修改</a>"
                + "<a onclick='delete1(" + val + ");' href='javascript:void();' style='margin:5px;'>删除</a>";
                return btn;
            }
            function formatDate(val, row, index) {
                var value = val.substring(6, val.length - 2);
                var date = new Date();
                date.setTime(value);
                return date.toLocaleDateString() + " " + date.toLocaleTimeString();
            }
            function formatStatus(val, row) {
                var status = new Array('草稿中', '已提交');
                return status[val - 1];
            }
            function formatType(val, row) {
                var types = new Array('国家和民航相关通航政策、管理规定', '中南地区通航管理规定', '河南空管通航管理相关程序', '应急救援相关程序');
                return types[val - 1];
            }
            function formatFile(val, row, index) {
                var btn = "<a href='/Handler.ashx?action=download&filepath=" + val + "'>下载</a>";
                return btn;
            }
            function add() {
                $('#ff').form('submit', {
                    url: 'Handler.ashx?action=add',
                    method: 'POST',
                    success: function (result) {

                        clearForm();
                        close1();
                        $.messager.show({
                            title: result
                        });
                    }
                });
            }
            function update() {
                $('#ff').form('submit', {
                    url: 'Handler.ashx?action=update&id='+id,
                    method: 'POST',
                    success: function (result) {

                        clearForm();
                        close1();
                        $.messager.show({
                            title: result
                        });
                    }
                });
            }

            function clearForm() {
                $('#ff').form('clear');
            }
        </script>
    </div>

</body>
</html>
