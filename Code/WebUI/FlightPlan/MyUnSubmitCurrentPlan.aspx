<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyUnSubmitCurrentPlan.aspx.cs" Inherits="FlightPlan_MyUnSubmitCurrentPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="../css/fademap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;height:22px;">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
        <!--<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>-->
        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
            <div id="search_menu" style="width: 200px">
                <div name="Code">
                    长期计划编号
                </div>
            </div>
        </div>

    </div>
    <%--列表 end--%>

    <script type="text/javascript">
        $(function () {
            Main.InitGird();
            Main.InitSearch();
            baiduMap.init();
            //$(".fadediv").fadeToggle();
            //$(".fade").click(function () {
            //    $(".fadediv").fadeToggle();
            //});
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'CurrentFlightPlanID', //排序字段
                    idField: 'CurrentFlightPlanID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 380 > 0 ? $(parent.document).find("#mainPanel").height() - 380 : 300, //高度
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
                         { title: '公司名称', field: 'CompanyName', width: 200 },
                        //{ title: '长期计划编号', field: 'Code', width: 200 },
                        { title: '任务类型', field: 'FlightType', width: 80 },
                        { title: '航班号', field: 'CallSign', width: 80 },
                        { title: '使用机型', field: 'AircraftType', width: 80 },
                        { title: '起飞机场', field: 'ADEP', width: 80 },
                        { title: '目的地机场', field: 'ADES', width: 80 },
                        { title: '应答机编码', field: 'SsrCode', width: 80 },
                        { title: '航空器数量', field: 'AircraftNum', width: 80 },
                        {
                            title: '实际起飞时间', field: 'ActualStartTime', width: 120, formatter: function (value, rec, index) {
                                if (value) {
                                    var timesstamp = new Date(value.dateValFormat());
                                    return timesstamp.format("yyyy-MM-dd HH:mm:ss");
                                }
                                else return "";

                            }
                        },
                          {
                              title: '实际降落时间', field: 'ActualEndTime', width: 120, formatter: function (value, rec, index) {
                                  if (value) {
                                  var timesstamp = new Date(value.dateValFormat());
                                  return timesstamp.format("yyyy-MM-dd HH:mm:ss");
                                  }
                                  else return "";

                              }
                          },
                             //{ title: '机场及起降点', field: 'AirportText', width: 200 },
                          //{ title: '航线及作业区', field: 'AirlineWorkText', width: 200 },
                        { title: '创建人', field: 'CreatorName', width: 80 },
             
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },

                        { title: '状态', field: 'PlanState', formatter: function (value, rec, index) { return value == 0 ? '草稿中' : '' }, width: 50 },
                        {
                            title: '操作', field: 'CurrentFlightPlanID', width: 80, formatter: function (value, rec) {
                                var str = "<a style=\"color:red\" id=\"sub-btn_'" + value + "'\" href=\"javascript:;\" onclick=\"Main.Edit('" + value + "');$(this).parent().click();return false;\">编辑</a>&nbsp;&nbsp;<a style=\"color:red\" id=\"sub-btn_'" + value + "'\" href=\"javascript:;\" onclick=\"Main.Submit('" + value + "');$(this).parent().click();return false;\">提交</a>";
                                return str;
                            }
                        }
                    ]],
                    toolbar: "#tab_toolbar",
                    queryParams: { "action": "query" },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true, //行号
                    onClickRow: function (index, row) {
                        var keyValue = row["CurrentFlightPlanID"];
                        zhccMap.addCurrentPlan(keyValue);
                    }
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
                $("#edit").dialog("open").dialog('setTitle', '新增起飞申请').dialog('refresh', 'MyUnSubmitCurrentPlanAdd.aspx');
                $("#btn_add").attr("onclick", "submitForm();");
            },
            Edit: function (uid) {
                $("#edit").dialog("open").dialog('setTitle', '编辑飞行计划').dialog('refresh', 'MyUnSubmitCurrentPlanAdd.aspx?id=' + uid);
                $("#btn_add").attr("onclick", "submitForm();")
            },
            Submit: function (uid)
            {
                if (!$("#form1").form("validate")) {
                    return;
                }
                var json = $.param({ "action": "submit", "id": uid }) + '&' + $('#form1').serialize();
                $.messager.confirm('提示', '确认提交该条起飞申请？', function (r) {
                    if (r) {
                        $.post(location.href, json, function (data) {
                            if (data.isSuccess) {
                                $("#tab_list").datagrid("reload");
                                $("#edit").dialog("close");
                            }
                        });
                    }
                });

            }
        };
    </script>
    <div id="edit" class="easyui-dialog" style="width: 1100px; height: 600px;"
        modal="true" closed="true" buttons="#edit-buttons">
          <%--<form id="form1"  method="post">
          <table class="table_edit">
                    <tr>
                        <td>飞行员：
                        </td>
                        <td>
                            <input id="Pilot" name="Pilot"  maxlength="30" class="easyui-validatebox textbox"
                                required="true" style="height:20px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>联系方式：
                        </td>
                        <td>
                            <input id="ContractWay" name="ContractWay"   maxlength="20" class="easyui-validatebox textbox"
                                required="true" style="height:20px;"/>
                        </td>
                    </tr>
 <tr>
                        <td>航空器架数：
                        </td>
                        <td>

           
                    <input id="AircraftNum" name="AircraftNum" style="height: 20px" maxlength="4" type="text" required="true" class="easyui-numberbox" data-options="min:1,max:100" />

                        </td>
                    </tr>
               <tr>
                        <td>实际起飞时间：
                        </td>
                        <td>
                            <input id="ActualStartTime" name="ActualStartTime" maxlength="20" class="easyui-timespinner"
                                required="true" style="height:20px;"/>
                        </td>
                    </tr>
               <tr>
                        <td>实际降落时间：
                        </td>
                        <td>
                            <input id="ActualEndTime" name="ActualEndTime" maxlength="20" class="easyui-timespinner"
                                required="true" style="height:20px;"/>
                        </td>
                    </tr>
                </table>
             </form>--%>
               </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;"  class="easyui-linkbutton">提交</a><a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <%--<div class="fadediv"><div id="map" style="height:400px;"></div></div>
    <div class="fade"><span>地图显示/隐藏</span></div>--%>
    <div id="map" style="height:400px;"></div>
</asp:Content>