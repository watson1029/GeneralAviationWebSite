<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceAudit.aspx.cs" Inherits="ResourceAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>通航资料审核管理</title>
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
                url: "/Handler.ashx?action=get&status=2"
            });
            $('#dialog').dialog({
                buttons: [{
                    text: '同意',
                    iconCls: 'icon-ok',
                    handler: function () {
                        update(3);
                    }
                }, {
                    text: '拒绝',
                    iconCls: 'icon-no',
                    handler: function () {
                        update(4);
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
        <div class="easyui-panel" title="通航资料">
            <select class="easyui-combobox" id="status" name="status" label="资料状态:" labelposition="left" style="width: 200px; margin: 20px;">
                <option value="2">已提交</option>
                <option value="3">已通过</option>
                <option value="4">已拒绝</option>
            </select><br />
            <select class="easyui-combobox" id="type" name="type" label="资料类别:" labelposition="left" style="width: 320px; margin: 20px;">
                <option value="0">所有</option>
                <option value="1">国家和民航相关通航政策、管理规定</option>
                <option value="2">中南地区通航管理规定</option>
                <option value="3">河南空管通航管理相关程序</option>
                <option value="4">应急救援相关程序</option>
            </select><br />
            <a class="easyui-linkbutton" id="bt_query" onclick="get();">查询</a>
        </div>
        <table id="dg" title="通航资料" style="width: 1358px; height: 350px" data-options="pageSize:10,rownumbers:true,singleSelect:true,pagination:true,method:'post'">
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
        <form runat="server" method="post" id="ff" action="Handler.ashx?action=add" enctype="multipart/form-data">
            <input id="id" type="hidden" />
            <table class="table_edit">
                <tr>
                    <th>标题</th>
                    <td>
                        <input id="title" class="easyui-textbox" name="title" readonly="readonly" style="width: 100%" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>处理人</th>
                    <td>
                        <input id="dealuser" class="easyui-textbox" name="dealuser" readonly="readonly" style="width: 100%" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>资料类别</th>
                    <td>
                        <select id="resourcetype" class="easyui-combobox" name="resourcetype" disabled="disabled" style="width: 100%" data-options="required:true">
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
                        <input id="started" class="easyui-datebox" name="started" readonly="readonly" data-options="required:true" style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <th>截止时间</th>
                    <td>
                        <input id="ended" class="easyui-datebox" name="ended" readonly="readonly" data-options="required:true" style="width: 100%" />
                    </td>
                </tr>
                <tr>
                    <th>附件</th>
                    <td>
                        <input id="file" class="easyui-textbox" type="text" readonly="readonly" style="width: 100%;" />
                    </td>
                </tr>
            </table>
            <input id="Text1" name="file" type="file" hidden="hidden" style="width: 100%;" />
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
            $('#started').datebox('setValue', data.Started);
            $('#ended').datebox('setValue', data.Ended);
            $('#file').textbox('setValue', data.FilePath);

            open1(id);
        }
        function formatOperation(val, row, rowIndex) {
            var row = $('#dg').datagrid('getData');
            var data = row.rows[rowIndex];
            var status = data.Status;
            var bb;
            if (status < 3) {
                bb = "<a onclick='showupdate(" + val + "," + rowIndex + ");' href='javascript:void();' style='margin:5px;'>审核</a>";
            } else {
                bb = "<a onclick='delete1(" + val + ");' href='javascript:void();' style='margin:5px;'>删除</a>";
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
            var status = new Array('已提交', '已通过', '已拒绝');
            return status[val - 2];
        }
        function formatType(val, row) {
            var types = new Array('国家和民航相关通航政策、管理规定', '中南地区通航管理规定', '河南空管通航管理相关程序', '应急救援相关程序');
            return types[val - 1];
        }
        function formatFile(val, row, index) {
            var btn = "<a href='/Handler.ashx?action=download&filepath=" + val + "'>下载</a>";
            return btn;
        }
        function update(status) {
            $('#resourcetype').combobox({ disabled: false });

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
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            var id = '<%=Request.QueryString["id"] %>';

                if (id) {
                    $.post(location.href, { "action": "queryone", "id": id }, function (data) {
                        $("#form_edit").form('load', data);
                        $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                            $("#d" + n).prop({ checked: true });
                        });
                        new dj.upload({
                            id: "ResourceFiles",
                            maxSize: 5,
                            multi: true,
                            queueId: "ResourceFiles-fileQueue",
                            listId: "ResourceFiles-fileList",
                            truncate: "30",
                            maxCount: "1",
                            uploadPath: "File/",
                            uploadedFiles: data.AttchFile
                        });
                    });
                }
                else {
                    //alert("tt");
                    new dj.upload({
                        id: "ResourceFiles",
                        maxSize: 5,
                        multi: true,
                        queueId: "ResourceFiles-fileQueue",
                        listId: "ResourceFiles-fileList",
                        truncate: "30",
                        maxCount: "1",
                        uploadPath: "File/",
                        uploadedFiles: ""
                    });
                }
            });
    </script>
    </div>

</body>
</html>
