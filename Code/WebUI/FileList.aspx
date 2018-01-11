<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="FileList.aspx.cs" Inherits="FileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/")%>Content/JS/easyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveUrl("~/")%>css/rcrumbs.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
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
                        if (id == 0) {
                            add(1);
                        } else {
                            update(1);
                        }
                    }
                }, {
                    text: '保存并提交',
                    iconCls: 'icon-ok',
                    handler: function () {
                        if (id == 0) {
                            add(2);
                        } else {
                            update(2);
                        }
                    }
                },
                {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <div class="center">
            <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
            <script type="text/javascript" src="js/jquery.rcrumbs.js"></script>
            <div class="rcrumbs" id="breadcrumbs">
                <ul>
                    <li>当前位置：</li>
                    <li><a href="Default.aspx">首页</a><span class="divider">></span></li>
                    <li><a href="<%=Request.RawUrl %>"><%=title%></a><span class="divider">></span></li>
                </ul>
            </div>
            <script type="text/javascript">
                $("#breadcrumbs").rcrumbs({ windowResize: false });
            </script>
            <div id="toolbar" style="padding: 2px 2px;">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="open1(0);">新增</a>
                <%--<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>--%>

                <div style="float: right">
                    <select class="easyui-combobox" id="status" name="status" labelposition="left" style="width: 100px; margin: 20px;">
                        <option value="0">资料状态</option>
                        <option value="1">草稿中</option>
                        <option value="2">已提交</option>
                        <option value="3">已通过</option>
                        <option value="4">已拒绝</option>
                    </select>
                    <select class="easyui-combobox" id="type" name="type" labelposition="left" style="width: 250px; margin: 20px;">
                        <option value="0">资料类别</option>
                        <option value="1">国家和民航相关通航政策、管理规定</option>
                        <option value="2">中南地区通航管理规定</option>
                        <option value="3">河南空管通航管理相关程序</option>
                        <option value="4">应急救援相关程序</option>
                    </select>
                    <a class="easyui-linkbutton" id="bt_query" onclick="get();" data-options="iconCls:'icon-search'" style="margin-right: 10px;">查询</a>
                </div>
            </div>
            <table id="dg" title="通航资料" style="width: 99%; height: 99%" data-options="pageSize:10,rownumbers:true,singleSelect:true,pagination:true,method:'post',striped:true,toolbar:'#toolbar'">
                <thead>
                    <tr>
                        <th data-options="field:'Title',width:350,align:'center'">标题</th>
                        <th data-options="field:'DealUser',width:100,align:'center'">处理人</th>
                        <th data-options="field:'ResourceType',width:220,align:'center',formatter:formatType">资料类别</th>
                        <th data-options="field:'UsefulTime',width:200,align:'center'">有效时间</th>
                        <th data-options="field:'Created',width:155,align:'center',formatter:formatDate">发布时间</th>
                        <th data-options="field:'FilePath',width:100,align:'center',formatter:formatFile">附件</th>
                        <th data-options="field:'Status',width:100,align:'center',formatter:formatStatus">状态</th>
                        <th data-options="field:'ID',width:100,align:'center',formatter:formatOperation">操作</th>
                    </tr>
                </thead>
            </table>
            <div title="确定删除？" closed="true" class="easyui-dialog" id="hint" style="width: 300px; height: auto;">
                <h3>确定删除这条资料信息吗？</h3>
            </div>


            <div title="通航资料" class="easyui-dialog" id="dialog" style="width: 400px; height: auto; padding: 10px 20px;">
                <div style="margin-bottom: 20px">
                    <form runat="server" method="post" id="ff" action="Handler.ashx?action=add" enctype="multipart/form-data">
                        <input id="id" type="hidden" />
                        <table class="table_edit">
                            <tr>
                                <th>标题</th>
                                <td>
                                    <input id="title" class="easyui-textbox" name="title" style="width: 100%" data-options="required:true" />
                                </td>
                            </tr>
                            <tr>
                                <th>处理人</th>
                                <td>
                                    <input id="dealuser" class="easyui-textbox" name="dealuser" style="width: 100%" data-options="required:true" />
                                </td>
                            </tr>
                            <tr>
                                <th>资料类别</th>
                                <td>
                                    <select id="resourcetype" class="easyui-combobox" name="resourcetype" style="width: 100%" data-options="required:true">
                                        <option value="1">国家和民航相关通航政策、管理规定</option>
                                        <option value="2">中南地区通航管理规定</option>
                                        <option value="3">河南空管通航管理相关程序</option>
                                        <option value="4">应急救援相关程序</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <th>开始时间</th>
                                <td>
                                    <input id="started" class="easyui-datebox" name="started" data-options="required:true" style="width: 100%" />
                                </td>
                            </tr>
                            <tr>
                                <th>截止时间</th>
                                <td>
                                    <input id="ended" class="easyui-datebox" name="ended" data-options="required:true" style="width: 100%" />
                                </td>
                            </tr>
                            <%--<tr>
                        <th>附件：
                        </th>
                        <td>
                            <input type="hidden" name="ResourceFilesInfo" id="ResourceFilesInfo" />
                            <input type="file" class="dj-upload-file" id="ResourceFiles" name="ResourceFiles" />
                            <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('ResourceFiles').uploadFiles()">上传</a>
                            <div id="ResourceFiles-fileQueue"></div>
                            <div id="ResourceFiles-fileList" style="margin-top: 2px; zoom: 1"></div>
                        </td>

                    </tr>--%>
                            <tr>
                                <th>附件(小于50M)</th>
                                <td>
                                    <input id="file" name="file" type="file" value="请选择附件（不大于50M）" style="width: 80%;" />
                                </td>
                            </tr>
                        </table>
                    </form>
                </div>
                <script type="text/javascript">
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
                        $('#started').datebox('setValue', data.Started);
                        $('#ended').datebox('setValue', data.Ended);
                        open1(id);
                    }
                    function formatOperation(val, row, rowIndex) {
                        var row = $('#dg').datagrid('getData');
                        var data = row.rows[rowIndex];
                        var status = data.Status;
                        var bb;
                        if (status < 2) {
                            bb = "<a onclick='showupdate(" + val + "," + rowIndex + ");' href='javascript:void();' style='margin:5px;'>修改</a>"
                            + "<a onclick='delete1(" + val + ");' href='javascript:void();' style='margin:5px;'>删除</a>";
                        } else {
                            bb = "--";
                        }
                        return bb;
                    }
                    function formatDate(val, row, index) {
                        var value = val.substring(6, val.length - 2);
                        var date = new Date();
                        date.setTime(value);
                        return date.toLocaleDateString() + " " + date.toLocaleTimeString();
                    }
                    function formatStatus(val, row) {
                        var status = new Array('草稿中', '已提交', '已通过', '已拒绝');
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
                    function add(status) {
                        $('#ff').form('submit', {
                            url: 'Handler.ashx?action=add&status=' + status,
                            method: 'POST',
                            success: function (result) {

                                clearForm();
                                close1();
                                $.messager.show({
                                    title: result
                                });
                                setTimeout(function () { location.reload() }, 2000);
                            }
                        });
                    }
                    function update(status) {
                        $('#ff').form('submit', {
                            url: 'Handler.ashx?action=update&id=' + id + '&status=' + status,
                            method: 'POST',
                            success: function (result) {

                                clearForm();
                                close1();
                                $.messager.show({
                                    title: result
                                });
                                setTimeout(function () { location.reload() }, 2000);
                            }
                        });
                    }

                    function clearForm() {
                        $('#ff').form('clear');
                    }
                </script>
            </div>

        </div>
    </div>
</asp:Content>

