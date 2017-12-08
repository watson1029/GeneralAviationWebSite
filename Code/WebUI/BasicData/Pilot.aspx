<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="Pilot.aspx.cs" Inherits="BasicData_Pilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <%--列表 start--%>
   <table id="tab_list">
        </table>
        <div id="tab_toolbar" style="padding: 2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="ipt_search" menu="#search_menu"/>
                <div id="search_menu" style="width: 200px">
                    <div name="Pilots">
                        飞行员姓名：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog"  style="width: 500px; height: 500px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" name="form_edit" method="post">
            <div>
                <table class="table_edit">
                    <tr>
                        <td class="tdal">飞行员姓名：                                                                           
                        </td>
                        <td class="tdar">
                            <input id="Pilots" name="Pilots" type="text" class="easyui-textbox"  maxlength="30"
                                required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">身份证号：
                        </td>
                        <td class="tdar">
                            <input id="PilotCardNo" name="PilotCardNo" class="easyui-textbox"  maxlength="10"
                                required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">出生日期：
                        </td>
                        <td class="tdar">
                            <input id="PilotDT" name="PilotDT" type="text" class="easyui-datebox"  maxlength="10"  
                                required="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">联系电话：
                        </td>
                        <td class="tdar">
                            <input id="PhoneNo" name="PhoneNo" class="easyui-textbox"   maxlength="15"
                                required="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">执照编号：
                        </td>
                        <td class="tdar">
                            <input id="LicenseNo" name="LicenseNo" type="text" class="easyui-textbox"  maxlength="30"
                                required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">签发单位：
                        </td>
                        <td class="tdar">
                            <input id="Sign" name="Sign" type="text" class="easyui-textbox" maxlength="30"
                                required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">执照类别：
                        </td>
                        <td class="tdar">
                            <select id="Licensesort" class="easyui-combobox" name="Licensesort" required="true">
                                <option value="0">航线运输驾驶执照</option>
                                <option value="1" selected="selected">商用飞机驾照</option>
                                <option value="2" selected="selected">私用飞机驾照</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">所属企业：
                        </td>
                        <td class="tdar">
                            <select id="CompanyName" class="easyui-combobox" name="CompanyName" required="true">
                                <option value="0">通航1</option>
                                <option value="1" selected="selected">通航2</option>
                                <option value="2" selected="selected">通航3</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">性别：
                        </td>
                        <td class="tdar">
                            <select id="Sex" class="easyui-combobox" name="Sex" required="true">
                                <option value="0">男</option>
                                <option value="1" selected="selected">女</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdal">执照复印件：
                         </td>
                    <td class="tdar">
                            <input type="hidden" name="LicenseImgInfo" id="LicenseImgInfo"  />
                            <input type="file" id="LicenseImg" name="LicenseImg" />
                            <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" onclick="dj.getCmp('LicenseImg').uploadFiles()">上传</a>
                            <div id="LicenseImg-fileQueue"></div>
                            <div id="LicenseImg-fileList" style="margin-top: 2px; zoom: 1"></div>
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

    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>

    <%--添加 修改 end--%>

    <%--设置菜单 start--%>

    <div id="setrole" class="easyui-dialog" style="width: 500px; height: 350px;"
        modal="true" closed="true" buttons="#setrole-buttons">

            <ul id="tt" class="easyui-tree"></ul>
    </div>
    <div id="setrole-buttons">
        <a id="btn_set" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#setrole').dialog('close');return false;">取消</a>
    </div>
    <%--设置菜单 end--%>

    <script type="text/javascript">

        $(function () {
            Main.InitGird();
            Main.InitSearch();

            

        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '飞行员列表', //表格标题
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
                        { title: '飞行员姓名', field: 'Pilots', width: 100 },
                        { title: '身份证号', field: 'PilotCardNo', width: 200 },
                        { title: '出生日期', field: 'PilotDT', width: 150 },
                        { title: '联系电话', field: 'PhoneNo', width: 150 },
                        { title: '执照编号', field: 'LicenseNo', width: 150 },
                        { title: '签发单位', field: 'Sign', width: 150 },
                        { title: '执照类别', field: 'Licensesort',formatter: function (value, rec, index) { 
                            if(value == 1){
                                return '航线运输驾驶执照';
                            }
                            else if(value ==2){
                                return '商用飞机驾照';
                            }
                            else{
                                return '私用飞机驾照';
                            }
                            width: 200 },},
                        { title: '所属企业', field: 'CompanyName', formatter: function (value, rec, index) 
                        {
                            if (value == 1) {
                                return '通航1';
                            }
                            else if (value = 2) {
                                return '通航2';
                            }
                            else {
                                return '通航3';
                            }
                            width: 200},},
                        { title: '性别', field: 'Sex', formatter: function (value, rec, index) { return value == 0 ? '男' : '女' }, width: 50 },
                        {
                            title: '操作', field: 'ID', width: 150, formatter: function (value, rec) {
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
                $("#edit").dialog("open").dialog('setTitle', '新增飞行员信息');
                $("#form_edit").form('clear');
                $("#btn_add").attr("onclick", "Main.Save();")

                new dj.upload({
                    id: "LicenseImg",
                    maxSize: 5,
                    multi: true,
                    queueId: "LicenseImg-fileQueue",
                    listId: "LicenseImg-fileList",
                    truncate: "18",
                    maxCount: "1",
                    uploadPath: "Files/LicenseImg/"
                });

            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }

                var fileInfo = dj.getCmp("LicenseImg").getUploadedFiles();
                $("#LicenseImgInfo").val(fileInfo);


                var json = $.param({ "id": uid, "action": "submit" }) + '&' + $('#form_edit').serialize();

                $.post("Pilot.aspx", json, function (data) {
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
                $("#edit").dialog("open").dialog('setTitle', '编辑飞行员信息');
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")

                $.post("Pilot.aspx", { "action": "queryone", "id": uid }, function (data) {
                    $("#form_edit").form('load', data);
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
                        $.post("Pilot.aspx", { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

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
