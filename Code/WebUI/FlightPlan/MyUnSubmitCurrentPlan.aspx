﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyUnSubmitCurrentPlan.aspx.cs" Inherits="FlightPlan_MyUnSubmitCurrentPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table id="tab_list">
    </table>
    <div id="tab_toolbar" style="padding: 2px 2px;height:22px;">
        <!--<a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>-->
        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
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
            baiduMap.init();
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'FlightPlanID', //排序字段
                    idField: 'FlightPlanID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 450 > 0 ? $(parent.document).find("#mainPanel").height() - 450 : 300, //高度
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
                        { title: '申请单号', field: 'PlanCode', width: 180 },
                        { title: '任务类型', field: 'FlightType', width: 60 },
                        { title: '注册号', field: 'CallSign', width: 80 },
                        { title: '使用机型', field: 'AircraftType', width: 60 },
                        { title: '飞行范围', field: 'FlightArea', width: 100 },
                        { title: '飞行高度', field: 'FlightHeight', width: 100 },
                        //{ title: '航线走向和飞行高度', field: 'FlightDirHeight', width: 150 },
                        {
                            title: '起飞时刻', field: 'SOBT', width: 100, formatter: function (value, rec, index) {

                                var timesstamp = new Date(value);
                                return timesstamp.toLocaleDateString();
                            }
                        },
                        {
                            title: '降落时刻', field: 'SIBT', width: 100, formatter: function (value, rec, index) {

                                var timesstamp = new Date(value);
                                return timesstamp.toLocaleDateString();
                            }
                        },
                        { title: '起飞点', field: 'ADEP', width: 80 },
                        { title: '降落点', field: 'ADES', width: 80 },

                         { title: '创建人', field: 'CreatorName', width: 60 },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },

                        { title: '状态', field: 'PlanState', formatter: function (value, rec, index) { return value == 0 ? '草稿中' : '' }, width: 50 },
                        {
                            title: '操作', field: 'FlightPlanID', width: 80, formatter: function (value, rec) {
                                var str = '<a style="color:red" id="sub-btn_' + value + '" href="javascript:;" onclick="Main.Submit(' + value + ');$(this).parent().click();return false;">提交</a>';
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

            Submit: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }
                $.messager.confirm('提示', '确认提交该条飞行计划？', function (r) {
                    if (r) {
                        $.post(location.href, { "action": "submit", "id": uid }, function (data) {

                            if (data.isSuccess) {
                                $("#tab_list").datagrid("reload");
                            }
                        });
                    }
                });

            }
        };
    </script>
    <div id="edit" class="easyui-dialog" style="width: 850px; height: 612px;"
        modal="true" closed="true" buttons="#edit-buttons">
               </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" onclick="Main.Save();" class="easyui-linkbutton">保存</a><a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <div id="map" style="height:400px;"></div>
</asp:Content>