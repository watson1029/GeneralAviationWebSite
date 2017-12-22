<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyFinishAuditFlightPlan.aspx.cs" Inherits="FlightPlan_MyFinishAuditFlightPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
                                申请单号
                            </div>
                        </div>
</div>
        </div>
    <%--列表 end--%>
    <script type="text/javascript">

        $(function () {
            Main.InitGird();
            Main.InitSearch();
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
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序

                    columns: [[
                                      { title: '申请单号', field: 'PlanCode', width: 200 },
                        { title: '航空器架数', field: 'AircraftNum', width: 100 },
                        { title: '机长（飞行员）姓名', field: 'Pilot', width: 150 },
                        { title: '通信联络方法', field: 'ContactWay', width: 100 },
                           {
                               title: '起飞时刻', field: 'SOBT', width: 100
                           },
                      {
                          title: '降落时刻', field: 'SIBT', width: 100
                      },
                        { title: '飞行气象条件', field: 'WeatherCondition', width: 100 },
                        { title: '空勤组人数', field: 'AircrewGroupNum', width: 100 },
                        { title: '二次雷达应答机代码', field: 'RadarCode', width: 150 },

                        { title: '公司三字码', field: 'CompanyCode3', width: 100 },
                           { title: '公司名称', field: 'CompanyName', width: 100 },
                         { title: '创建人', field: 'CreatorName', width: 80 },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },
                          { title: '审核意见', field: 'Comments', width: 120 },
                          { title: '审核时间', field: 'ActorTime', width: 120 },
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
            }

        };
    </script>

</asp:Content>

