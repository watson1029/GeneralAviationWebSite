<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="BusinessCertificate.aspx.cs" Inherits="BasicData_Quanlification_BusinessCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <%--列表 start--%>
        <table id="tab_list">
        </table>
        <div id="tab_toolbar" style="padding: 2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="ipt_search" menu="#search_mune" />
                <div id="search_mune" style="width:200px">
                    <div name="CompanyCode3">
                        公司三字码：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>

    <%--添加 修改--%>
    <div id="edit" class="easyui-dialog"  style="width: 700px; height: 350px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" method="post">
            <table class="table_edit">
                <tr>
                    <td class="tdal">
                        公司三字码：
                    </td>
                    <td class="tdar">
                        <input id="CompanyCode3" name="CompanyCode3" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        法定代表人：
                    </td>
                    <td class="tdar">
                        <input id="LegalPerson" name="LegalPerson" type="text" maxlength="20" class="easyui-textbox" required="ture" />
                    </td>
                </tr>
                <tr>
                    <td class="tadl">
                        许可证编号：
                    </td>
                    <td class="tdar">
                        <input id="LicenseNo" name="LicenseNo" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        经营项目与范围：
                    </td>
                    <td class="tdar">
                        <input id="ManageItemsScope" name="ManageItemsScope" type="text" maxlength="30" class="easyui-textbox"  />
                    </td>
                </tr>
                <tr>
                    <td class="tdal">
                        基地机场：
                    </td>
                    <td class="tdar">
                        <input id="BaseAirport" name="BaseAirport" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        有效期限：
                    </td>
                    <td class="tdar">
                        <input id="DealLine" name="DealLine" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                </tr>
                <tr >
                    <td class="tdal">
                        企业类别：
                    </td>
                    <td class="tdar">
                        <input id="CompanyType" name="CompanyType" type="text" maxlength="30" class="esayui-textbox"  />
                    </td>
                    <td class="tdal">
                        颁发日期：
                    </td>
                    <td class="tdar">
                        <input id="PresentationDate" name="PresentationDate" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                </tr>
                <tr>
                    <td class="tdal">
                        注册资本：
                    </td>
                    <td class="tdar">
                        <input id="RegisteredCapital" name="RegisteredCapital" type="text" maxlength="30" class="esayui-textbox"  />
                    </td>
                    <td class="tdal">
                        许可机关印章：
                    </td>
                    <td class="tdar">
                            <input type="hidden" name="AttchFilesInfo" id="AttchFilesInfo" required="true" />
                            <input type="file" id="LicensedSeal" name="LicensedSeal" />
                            <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" onclick="dj.getCmp('LicensedSeal').uploadFiles()">上传</a>
                            <div id="LicensedSeal-fileQueue"></div>
                            <div id="LicensedSeal-fileList" style="margin-top: 2px; zoom: 1"></div>
                        </td>
                </tr>
                <tr>
                    <td class="tdal">
                        <p>购置航空器的</p>
                        <p>自有资金额度：</p>
                    </td>
                    <td class="tdar">
                        <input id="CapitalLimit" name="CapitalLimit" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存 </a> 
        <a href="javascript:;" class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
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

            new dj.upload({
                id: "LicensedSeal",
                maxSize: 5,
                multi: true,
                queueId: "LicensedSeal-fileQueue",
                listId: "LicensedSeal-fileList",
                truncate: "18",
                maxCount: "1",
                uploadPath: "Files/LicensedSeal/"
            });

        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '企业营业执照列表', //表格标题
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
                        { title: '公司三字码', field: 'CompanyCode3', width: 150 },
                        { title: '许可证编号', field: 'LicenseNo', width: 150 },
                        { title: '基地机场', field: 'BaseAirport', width: 150 },
                        { title: '企业类别', field: 'ConpanyType', width: 150 },
                        { title: '注册资本', field: 'RegisteredCapital', width: 150 },
                        { title: '购置航空器的自有资金额度', field: 'CapitalLimit', width: 150 },
                        { title: '法定代表人', field: 'Legalperson', width: 150 },
                        { title: '经营项目与范围', field: 'ManageItemsScope', width: 150 },
                        { title: '有效日期', field: 'DealLine', width: 150 },
                        { title: '颁发日期', field: 'PresentationDate', width: 150 },
                        { title: '许可机关印章', field: 'LicensedSeal', width: 150 },
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
                debugger;
                $("#edit").dialog("open").dialog('setTitle', '新增营业执照信息');
                $("#form_edit").form('clear');
                $("#btn_add").attr("onclick", "Main.Save();")
            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "submit" }) + '&' + $('#form_edit').serialize();

                $.post("BusinessCertificate.aspx", json, function (data) {
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
                $("#edit").dialog("open").dialog('setTitle', '编辑营业执照信息');
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")

                $.post("BusinessCertificate.aspx", { "action": "queryone", "id": uid }, function (data) {
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
                        $.post("BusinessCertificate.aspx", { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

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


