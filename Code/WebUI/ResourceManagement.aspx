<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceManagement.aspx.cs" Inherits="ResourceManagement" %>

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
        $(function () {
            $('#dg').datagrid({
                url: "/Handler.ashx?action=get&type=1"
            });
            $('#dialog').dialog({
                buttons: [{
                    text: '新增',
                    iconCls: 'icon-ok',
                    handler: function () {
                        //alert('ok');
                        submitForm();
                    }
                }, {
                    text: '重置',
                    handler: function () {
                        clearForm();
                    }
                }]

            });

            close1();
        });
        function open1() {
            $('#dialog').dialog('open');
        }
        function close1() {
            $('#dialog').dialog('close');
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
            <input type="button" id="bt_Add" onclick="open1();" value="新增" />
            <button id="bt_Import">导入</button><br />
        </div>
        <div class="easyui-panel" title="资料列表">
            <div style="margin: 20px 0;"></div>
            <table id="dg" title="通航资料" style="width:950px; height: 350px" data-options="pageSize:10,rownumbers:true,singleSelect:true,pagination:true,method:'post'">
                <thead>
                    <tr>
                        <th data-options="field:'Title',width:150,align:'center'">标题</th>
                        <th data-options="field:'DealUser',width:100,align:'center'">处理人</th>
                        <th data-options="field:'ResourceType',width:300,align:'center',formatter:formatType">资料类别</th>
                        <th data-options="field:'UsefulTime',width:150,align:'center'">有效时间</th>
                        <th data-options="field:'FilePath',width:100,align:'center',formatter:formatFile">附件</th>
                        <th data-options="field:'Status',width:100,align:'center'">状态</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div title="新增通航资料" class="easyui-dialog" id="dialog" style="width: 400px; height: auto; padding: 10px 20px;">
        <div style="margin-bottom: 20px">
            <form runat="server" method="post" id="ff" action="Handler.ashx?action=add">
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
            function formatType(val, row) {
                var types = new Array('国家和民航相关通航政策、管理规定', '中南地区通航管理规定', '河南空管通航管理相关程序', '应急救援相关程序');
                return types[val];
            }
            function formatFile(val, row) {
                if (row.FilePath.length > 0) {
                    //alert(row.FilePath);
                    var btn = "<a href='/Handler.ashx?action=download&filepath=" + val + "'>下载</a>";
                } else {
                    var btn = "无";
                }
                return btn;
            }
            function submitForm() {
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
            function uploadFile() {
                var file = $('#file').value;
                if (file == null || file == "") {
                    $messager.alert('提示', '请添加附件');
                    return;
                }
                var options = {
                    type: "POST",
                    url: '/Files.ashx'
                };
                $('#ff').ajaxSubmit(options);
            }
            function clearForm() {
                $('#ff').form('clear');
            }
        </script>
    </div>

</body>
</html>
