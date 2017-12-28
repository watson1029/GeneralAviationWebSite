<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="Company.aspx.cs" Inherits="BasicData_Quanlification_Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" Runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
     <%--列表 start--%>
   <table id="tab_list">
        </table>
        <div id="tab_toolbar" style="padding: 2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="ipt_search" menu="#search_menu"/>
                <div id="search_menu" style="width: 200px">
                    <div name="CompangCode3">
                        公司三字码：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>
   

    <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width:800px; height:500px;"
       modal="true" closed="true" buttons="#edit-buttons">
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#edit').dialog('close');return false;">取消</a>
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
                    title: '通航企业基础信息列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'CompanyID', //排序字段
                    idField: 'CompanyID', //标识字段,主键
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
                        { title: '公司二字码', field: 'CompanyCode2', width: 150 },
                        { title: '公司名称', field: 'CompanyName', width: 150 },
                        { title: '英文名称', field: 'EnglishName', width: 150 },
                        {
                            title: '操作', field: 'CompanyID', width: 500, formatter: function (value, rec) {
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
                $("#edit").dialog("open").dialog('setTitle', '新增通航企业基本信息').dialog('refresh', 'CompanyAdd.aspx');
                $("#btn_add").attr("onclick", "Main.Save();")

            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }

                var fileInfo = dj.getCmp("LegalCardImg").getUploadedFiles();
                $("#LegalCardImgInfo").val(fileInfo);
                 fileInfo = dj.getCmp("LegalDelegateImg").getUploadedFiles();
                $("#LegalDelegateImgInfo").val(fileInfo);
                 fileInfo = dj.getCmp("DelegateCardImg").getUploadedFiles();
                $("#DelegateCardImgInfo").val(fileInfo);
                  fileInfo = dj.getCmp("LicensedSeal").getUploadedFiles();
                $("#LicensedSealInfo").val(fileInfo);


                var json = $.param({ "id": uid, "action": "submit" }) + '&' + $('#form_edit').serialize();

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
                $("#edit").dialog("open").dialog('setTitle', '编辑通航企业基本信息').dialog('refresh', 'CompanyAdd.aspx?id=' + uid);
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")              
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
                    var id = selRow[i].CompanyID;
                    idArray.push(id);
                }
                $.messager.confirm('提示', '确认删除该条记录？', function (r) {
                    if (r) {
                        $.post("Company.aspx", { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

                            if (data.isSuccess) {
                                $("#tab_list").datagrid("reload");
                                selRow.length = 0;
                            }
                        });
                    }
                });
            }


            //查看按钮事件
           



        };
    </script>


</asp:Content>


