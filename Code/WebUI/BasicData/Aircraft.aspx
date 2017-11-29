<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="Aircraft.aspx.cs" Inherits="BasicData_Aircraft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server" >
    <%--列表 start--%>
    <table id="tab_list">
        </table>
        <div id="tab_toolbara" style="padding:2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="ture" onclick="Main.OpenWin()">+新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcle="icon-remove" plain="ture" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="ipt_search" menu="#search_mune" />
                <div id="search_mune" style="width:250px">
                    <div name="AirNumber">
                        国籍和登记标志：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>

     <%--添加 修改 start--%>
    <div id="edit" class="esayui-dialog" title="新增飞行器" style="width:1000px; height:1000px">
        <form id="form_edit" name="form_edit" method="post" url="Aircraft.aspx">
            <div>
                <table class="table_edit">
                    <tr>
                        <td>
                            <td class="tdal">国籍和登记标志：
                            </td>
                            <td class="tdar">
                                <input id="AircraftID" name="AircraftID" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">最大加油量:</td>
                            <td class="tdar">
                                <input id="FuelCapacity" name="MaxCapacity" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <td class="tdal">机型：</td>
                            <td class="tdar">
                                <input id="AcfType" name="AcfType" type="text" class="easyui-textbox" 
                                     required="true">
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">航程：</td>
                            <td class="tdar">
                                 <input id="Range" name="Range" type="text" class="easyui-textbox"
                                      required="true" >
                            </td>
                        </td>
                     </tr>
                    <tr>
                        <td>
                            <td class="tdal">航空器出厂序号：</td>
                            <td class="tdar">
                                 <input id="AcfNo" name="AcfNo" type="text" class="easyui-textbox"
                                      required="true" >
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">年检日期：</td>
                            <td class="tdar">
                                <input id="ASdate" name="ASdate" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <td class="tdal">飞行器类别：</td>
                             <td class="tdar">
                                <input id="AcfClass" name="AcfClass" type="text" class="easyui-textbox" 
                                     required="true">
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">巡航高度：</td>
                            <td class="tdar">
                                <input id="CruiseAltd" name="CruiseAltd" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <td class="tdal">制造商：</td>
                            <td class="tdar"> 
                                <input id="Manufacturer" name="Manufacturer" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">巡航速度：</td>
                            <td class="tdar">
                                <input id="CruiseSpeed" name="CruiseSpeed" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td> 
                            <td class="tdal">尾流：</td>
                            <td class="tdar">
                                <input id="WakeTurbulance" name="WakeTurbulance" type="text" class="easyui-textbox" 
                                     required="true">
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">最大速度：</td>
                            <td class="tdar">
                                <input id="MaxSpeed" name="MaxSpeed" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <td class="tdal">起飞重量：</td>
                            <td class="tdar">
                                <input id="FueledWeight" name="FueledWeight" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                            <td class="auto-style1">
                            </td>
                            <td class="tdal">最小速度：</td>
                            <td class="tdar">
                                <input id="MinSpeed" name="MinSpeed" type="text" class="easyui-textbox"
                                     required="true" >
                            </td>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="edit-buttona">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
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
                        { title: '国籍和登记标志', field: 'AircraftID', width: 150 },
                        { title: '最大加油量', field: 'FuelCapacity', width: 150 },
                        { title: '机型', field: 'AcfType', width: 150 },
                        { title: '航程', field: 'Range', width: 150 },
                        { title: '航空器出厂序号', field: 'AcfNo', width: 150 },
                        { title: '年检时间', field: 'ASdate', width: 150 },
                        { title: '飞行器类别', field: 'AcfClass', width: 150 },
                        { title: '巡航高度', field: 'CruiseAltd', width: 150 },
                        { title: '制造商', field: 'Manufacture', width: 150 },
                        { title: '巡航速度', field: 'CruiseSpeed', width: 150 },
                        { title: '尾流', field: 'WakeTurbulance', width: 150 },
                        { title: '最大速度', field: 'MaxSpeed', width: 150 },
                        { title: '起飞重量', field: 'FueledWeight', width: 150 },
                        { title: '最小速度', field: 'MinSpeed', width: 150 },
                        { title: '创建时间', field: 'CreateTime', width: 150 },
                        {
                            title: '操作', field: 'ID', width: 150, formatter: function (value, rec) {
                                return '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>';
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
                $("#edit").dialog("open").dialog('setTitle', '新增');
                $("#pwdrow").show();
                $("#form_edit").form('clear');
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

            //修改链接 事件
            EditData: function (uid) {
                $("#edit").dialog("open").dialog('setTitle', '编辑');
                $("#pwdrow").hide();
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")

                $.post("UserInfo.aspx", { "action": "queryone", "id": uid }, function (data) {
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
    
