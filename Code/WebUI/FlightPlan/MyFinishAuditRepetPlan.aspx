<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyFinishAuditRepetPlan.aspx.cs" Inherits="FlightPlan_MyFinishAuditRepetPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="../css/fademap.css" rel="stylesheet" type="text/css" />
   <%-- <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   <%-- <div class="gridsearch">
        <label>用户名：</label>
        <input type="text" id="s_UserName" name="s_UserName" class="easyui-validatebox" />
        <input type="button" class="searchbotton easyui-linkbutton " style="margin-left:60px" iconcls="icon-search" value="查询" onclick="Main.FunSearchData()" />
    </div>--%>
    <%--列表 start--%>
        <table id="tab_list">
        </table>
        <div id="tab_toolbar" style="padding: 2px 2px;height:22px;">      
            <div style="float:right">
                        <input id="ipt_search" menu="#search_menu"/>
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
            // baiduMap.init();
            $(".fadediv").fadeToggle();
            $(".fade").click(function () {
                $(".fadediv").fadeToggle();
            });
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'PlanID', //排序字段
                    idField: 'PlanID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 60 > 0 ? $(parent.document).find("#mainPanel").height() - 60 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序
      
                    columns: [[
                      { title: '公司名称', field: 'CompanyName', width: 180 },
                        { title: '任务类型', field: 'FlightType', width: 70 },
                        { title: '使用机型', field: 'AircraftType', width: 100 },
                        {
                            title: '预计开始时间', field: 'StartDate', width: 100, formatter: function (value, rec, index) {

                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd");

                            }
                        },
                        {
                            title: '预计结束时间', field: 'EndDate', width: 100, formatter: function (value, rec, index) {

                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd");

                            }
                        },

                        {
                            title: '周执行计划', field: 'WeekSchedule', width: 130, formatter: function (value, rec, index) {
                                var array = [];
                                $.each(value.replace(/\*/g, '').toCharArray(), function (i, n) {

                                    array.push("星期" + n);
                                });
                                return array.join(',');

                            }
                        },
                          { title: '机场及起降点', field: 'AirportText', width: 200 },
                          { title: '航线及作业区', field: 'AirlineWorkText', width: 200 },
                         { title: '创建人', field: 'CreatorName', width: 60, hidden: 'true' },
                          { title: '其他需要说明的事项', field: 'Remark', width: 120, hidden: 'true' },
                          { title: '审核意见', field: 'Comments', width: 120 },
                          { title: '审核时间', field: 'ActorTime', width: 120 },
                                                       {
                                                           title: '操作', field: 'PlanID', width: 80, formatter: function (value, rec) {
                                                               var str = "<a style=\"color:red\" href=\"javascript:;\" onclick=\"Main.EditData('" + value + "');$(this).parent().click();return false;\">修改</a>";
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
                        var keyValue = row["PlanID"];
                        zhccMap.addRepetPlan(keyValue);
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
            //修改链接 事件
            EditData: function (uid) {
                $("#edit").dialog("open").dialog('setTitle', '编辑').dialog('refresh', 'RepetPlanFinishAuditForm.aspx?id=' + uid);
  
                $("#btn_edit").attr("onclick", "Main.Save('" + uid + "');")
            },
            Save: function (uid) {
                if ($("#Comments").val().length > 200) {
                    $.messager.alert('提示', '"审核意见"不能超过200字符！', 'info');
                    return;
                }
                var json = $.param({ "id": uid, "action": "save" }) + '&' + $('#form_edit').serialize();
                $.post(location.href, json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#edit").dialog("close");
                        }
                    });
                });
            }
        };
    </script>
        <div id="edit" class="easyui-dialog" style="width: 1000px; height: 600px;"
        modal="true" closed="true" buttons="#edit-buttons">
    
    <div id="edit-buttons">
        <a id="btn_edit" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div></div>
    <div class="fadediv"><div id="map" style="height:400px;"></div></div>
    <div class="fade"><span>地图显示/隐藏</span></div>
</asp:Content>

