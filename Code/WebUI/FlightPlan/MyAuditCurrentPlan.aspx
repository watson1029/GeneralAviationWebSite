<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyAuditCurrentPlan.aspx.cs" Inherits="FlightPlan_MyAuditCurrentPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="../css/fademap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px; height: 22px;">
        <a href="javascript:void(0)" class="easyui-linkbutton" style="width:78px;" iconcls="icon-save" plain="true" onclick="Main.BatchAudit()">批量审核</a>
        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
            <div id="search_menu" style="width: 200px">
                <div name="PlanCode">
                    临专号
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

                    columns: [[
                        { field: 'cbx', checkbox: true },
                         { title: '公司名称', field: 'CompanyName', width: 200 },
                        { title: '临专号', field: 'Code', width: 200 },
                        { title: '任务类型', field: 'FlightType', width: 80 },
                        { title: '航班号', field: 'CallSign', width: 80 },
                        { title: '使用机型', field: 'AircraftType', width: 80 },
                        { title: '起飞机场', field: 'ADEP', width: 80 },
                        { title: '目的地机场', field: 'ADES', width: 80 },
                        { title: '应答机编码', field: 'SsrCode', width: 80 },
                        { title: '航空器数量', field: 'AircraftNumber', width: 80 },
                        { title: '备降机场I', field: 'ALTN1', width: 80 },
                        { title: '备降机场II', field: 'ALTN2', width: 80 },
                        {
                            title: '计划撤轮挡时间', field: 'SOBT', width: 120, formatter: function (value, rec, index) {
                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd HH:mm:ss");

                            }
                        },
                          {
                              title: '计划挡轮挡时间', field: 'SIBT', width: 120, formatter: function (value, rec, index) {
                                  var timesstamp = new Date(value.dateValFormat());
                                  return timesstamp.format("yyyy-MM-dd HH:mm:ss");

                              }
                          },
                                                     { title: '机场及起降点', field: 'AirportText', width: 200 },
                          { title: '航线及作业区', field: 'AirlineWorkText', width: 200 },
                        { title: '创建人', field: 'CreatorName', width: 80 },

                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },
                             {
                                 title: '操作', field: 'CurrentFlightPlanID', width: 80, formatter: function (value, rec) {
                                     var str = "<a style=\"color:red\" href=\"javascript:;\" onclick=\"Main.Audit('" + value + "');$(this).parent().click();return false;\">审核</a>";
                                     return str;
                                 }
                             },


                    ]],
                    toolbar: "#tab_toolbar",
                    queryParams: { "action": "query" },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true, //行号
                    onClickRow: function (index, row) {
                        var keyValue = row["FlightPlanID"];
                        zhccMap.addFlyPlan(keyValue);
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
            //批量审核
            BatchAudit: function (uid) {
                var selRow = $('#tab_list').datagrid('getSelections');
                if (selRow.length == 0) {
                    $.messager.alert('提示', '请选择一条记录！', 'info');
                    return;
                }
                $("#batchaudit").dialog("open").dialog('setTitle', '批量审核');
            },
            //审核
            Audit: function (uid) {
                $("#audit").dialog("open").dialog('setTitle', '审核').dialog('refresh', 'CurrentPlanAuditForm.aspx?id=' + uid);
                $("#btn_audit").attr("onclick", "Main.AuditSubmit('" + uid + "');");
            },
            AuditSubmit: function (uid) {

                if (!$("#form_audit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "auditsubmit" }) + '&' + $('#form_audit').serialize();

                $.post(location.href, json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#audit").dialog("close");
                        }
                    });
                });

            },
            BatchAuditSubmit: function (uid) {
                if (!$("#form_batchaudit").form("validate")) {
                    return;
                }
                var selRow = $('#tab_list').datagrid('getSelections');
                if (selRow.length == 0) {
                    $.messager.alert('提示', '请选择一条记录！', 'info');
                    return;
                }
                var idArray = [];
                for (var i = 0; i < selRow.length; i++) {
                    var id = selRow[i].CurrentFlightPlanID;
                    idArray.push(id);
                }
                $.messager.confirm('提示', '确认要提交审核结果吗？', function (r) {
                    if (r) {
                        var json = $.param({ "cbx_select": idArray.join(','), "action": "batchaudit" }) + '&' + $('#form_batchaudit').serialize();
                        $.post(location.href, json, function (data) {
                            $.messager.alert('提示', data.msg, 'info');
                            if (data.isSuccess) {
                                $("#batchaudit").dialog("close");
                                $("#tab_list").datagrid("reload");
                                selRow.length = 0;
                            }
                        });
                    }
                });
            }
        };
    </script>

    <%--添加 修改 start--%>
    <div id="audit" class="easyui-dialog" style="width: 800px; height: 700px;"
        modal="true" closed="true" buttons="#audit-buttons">
        
    </div>
    <div id="audit-buttons">
        <a id="btn_audit" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#audit').dialog('close');return false;">取消</a>
    </div>
    <div id="batchaudit" class="easyui-dialog" style="width: 600px; height:300px;"
        modal="true" closed="true" buttons="#batchaudit-buttons">
        <form id="form_batchaudit" method="post">
            <table class="table_edit">
                <tr>
                    <th>审核结果：</th>
                    <td >
                        <select class="easyui-combobox" editable="false" name="BatchAuditresult" required="true" panelheight="auto" style="width: 200px;">
                            <option value="0" selected="true">通过</option>
                            <option value="1">不通过</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>审核意见：</th>
                    <td colspan="3">
                        <input id="BatchAuditComment" name="BatchAuditComment" required="true"  maxlength="400" style="width: 400px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                    </td>

                </tr>
            </table>
        </form>
    </div>
    <div id="batchaudit-buttons">
        <a id="btn_batchaudit" href="javascript:;" class="easyui-linkbutton" onclick="Main.BatchAuditSubmit()">提交</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#batchaudit').dialog('close');return false;">取消</a>
    </div>
    <%--<div class="fadediv"><div id="map" style="height:400px;"></div></div>
    <div class="fade"><span>地图显示/隐藏</span></div>--%>
    <div id="map" style="height:400px;"></div>
</asp:Content>