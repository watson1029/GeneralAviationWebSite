<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="SupplyDemandSubmit.aspx.cs" Inherits="SupplyDemandInformation_SupplyDemandSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/ueditor/ueditor.all.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;">
        <a></a>
        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
            <div id="search_menu" style="width: 200px">
                <div name="CompanyName">
                    通航公司
                </div>
                <div name="Catalog">
                    供求条件
                </div>
            </div>
        </div>

    </div>
    <%--列表 end--%>
     <%--添加 修改 start--%>
    <div id="edit" class="easyui-dialog" style="width: 1144px; height: 790px;" modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit" method="post">
            <table class="table_edit">
                <tr>
                    <td style="text-align: right;">供求标题
                    </td>
                    <td colspan="3">
                        <input id="Title" name="Title" class="easyui-textbox"required="true" style="width: 1000px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">供求简介
                    </td>
                    <td colspan="3">
                        <script id="editor" type="text/plain" style="width: 1000px; height: 400px;"></script>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">供求条件
                    </td>
                    <td>
                        <select id="CataLog" class="easyui-combobox" name="CataLog" required="true" style="width: 145px;">
                            <option value="提供">提供</option>
                            <option value="寻求" selected="true">寻求</option>
                        </select>
                    </td>
                    <td style="text-align: right; width:25%">有效期限
                    </td>
                    <td>
                        <input id="ExpiryDate" name="ExpiryDate" style="width: 145px" type="text" required="true" class="easyui-datebox" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">撰写人</td>
                    <td>
                        <input id="CreateName" name="CreateName" style="width: 145px" type="text" readonly="true" required="true" class="easyui-textbox" /></td>
                    <td style="text-align: right; width:25%">录入日期</td>
                    <td>
                        <input id="CreateTime" name="CreateTime" style="width: 145px" type="text" readonly="true" required="true" class="easyui-datebox" /></td>
                </tr>
            </table>
            <input id="Creator" name="Creator" style="display:none" type="text"/>
            <input id="CompanyName" name="CompanyName" style="display:none" type="text"/>
            <input id="CompanyCode3" name="CompanyCode3" style="display:none" type="text"/>
        </form>
    </div>

    <div id="edit-buttons">
        <a href="javascript:;" class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
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
                    title: '列表', //表格标题
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
                    ]],
                    columns: [[
                        { title: '单位名称', field: 'CompanyName', width: 300 },
                        { title: '录入日期', field: 'CreateTime', width: 150 },
                        { title: '有效期限', field: 'ExpiryDate', width: 150 },
                        { title: '供求标题', field: 'Title', width: 300 },
                        //{ title: '供求简介', field: 'Summary', width: 400 },
                        { title: '供求条件', field: 'Catalog', width: 60 },
                        {
                            title: '状态', field: 'State', formatter: function (value, rec, index) {
                                var str = "";
                                if (value == "end") {
                                    str = "审核通过";
                                }
                                else if (value == "Deserted") {
                                    str = "审核不通过";
                                }
                                else {
                                    str = value + '审核中';
                                }
                                return str;
                            }, width: 80
                        },
                          {
                              title: '操作', field: 'ID', width: 80, formatter: function (value, rec) {
                                  var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">查看</a>';
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
            //修改链接 事件
            EditData: function (id) {
                if (screen.height >= 1080) {
                    $("#edit").dialog("open").dialog('setTitle', '查看');
                }
                else {
                    $("#edit").dialog({ title: '查看', left: 0, top: 0, height: 500 }).dialog("open");
                }
                $.post(location.href, { "action": "queryone", "id": id }, function (data) {
                    $("#form_edit").form('load', data);
                    UE.getEditor('editor').setContent(decodeURI(data.SummaryCode));
                });
            }
        };
    </script>
</asp:Content>
