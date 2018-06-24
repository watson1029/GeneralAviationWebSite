﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MySubmitRepetPlan.aspx.cs" Inherits="FlightPlan_MySubmitRepetPlan" %>

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
   
        <div id="tab_toolbar" style="padding: 2px 2px;" >

            
            <div style="float:right">
                        <input id="ipt_search" menu="#search_menu"/>
                       <input id="ssPlanCode" name="ssPlanCode"  type="hidden" value=""/>
                        <div id="search_menu" style="width: 200px">
                            <div name="Code">
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
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'RepetPlanID', //排序字段
                    idField: 'RepetPlanID', //标识字段,主键
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
                        { title: '公司名称', field: 'CompanyName', width: 180 },
                         { title: '临专号', field: 'Code', width: 180 },
                        { title: '任务类型', field: 'FlightType', width: 70 },
                        { title: '使用机型', field: 'AircraftType', width: 70 },
                        {
                            title: '执行开始时间', field: 'StartDate', width: 100, formatter: function (value, rec, index) {
                        
                            var timesstamp = new Date(value.dateValFormat());
                            return timesstamp.format("yyyy-MM-dd");
                        
                        }
                        },
                        {
                            title: '执行结束时间', field: 'EndDate', width: 100, formatter: function (value, rec, index) {
                        
                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd");
                        
                            }
                        },
                        {
                            title: '周执行计划', field: 'WeekSchedule', width: 150, formatter: function (value, rec, index) {
                                var array = [];
                                $.each(value.replace(/\*/g, '').toCharArray(), function (i, n) {
                                    array.push("星期" + n);
                                });
                              return  array.join(',');

                            }
                        },
                         { title: '创建人', field: 'CreatorName', width: 60, hidden: 'true' },
                          {
                              title: '创建时间', field: 'CreateTime', width: 100, formatter: function (value, rec, index) {

                                  var timesstamp = new Date(value.dateValFormat());
                                  return timesstamp.format("yyyy-MM-dd hh:mm:ss");

                              }
                          },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150, hidden: 'true' },

                        {
                            title: '状态', field: 'PlanState', formatter: function (value, rec, index) {
                                var str = "";
                                if (value == "end") {
                                    str = "<font color=\'red\'>审核通过</font>";
                                }
                                else if (value == "Deserted") {
                                    str = "<font color=\'red\'>审核不通过</font>";
                                }
                                else {
                                    str = '<font color=\'red\'>' + value + '审核中</font>';
                                }
                                return str;
                            }, width: 50
                        },
                        {
                            title: '操作', field: 'RepetPlanID', width: 80, formatter: function (value, rec) {
                                var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;<a style="color:red" id="sub-btn_' + value + '" href="javascript:;" onclick="Main.Submit(' + value + ');$(this).parent().click();return false;">提交</a>';
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
            EditData: function (uid) {
                $("#edit").dialog("open").dialog('setTitle', '查看长期计划').dialog('refresh', 'SubmitDetail.aspx?id=' + uid);
            },
            //初始化搜索框
            InitSearch: function () {
                $("#ipt_search").searchbox({
                    width: 250,
                    searcher: function (val, name) {
                        $('#ss' + name).val(val);
                        $('#tab_list').datagrid('options').queryParams.search_type = name;
                        $('#tab_list').datagrid('options').queryParams.search_value = val;
                        $('#tab_list').datagrid('reload');
                    },
                    prompt: '请输入要查询的信息'
                });
            },
            //打开添加窗口
            OpenWin: function () {
                $("#add").dialog("open").dialog('setTitle', '新增长期计划').dialog('refresh', 'MyUnSubmitRepetPlanAdd.aspx');
                //$("#btn_add").attr("onclick", "Main.Save();")
            }  
        };
    </script>
 <div id="detail" class="easyui-dialog" style="width: 1000px; height:700px;"
        modal="true" closed="true" buttons="#detail-buttons">
        <div class="form-button" id="wizard-actions">
                <a id="btn_last" href="javascript:void(0);" disabled class="btn-prev easyui-linkbutton">上一步</a>
                <a id="btn_next" href="javascript:void(0);" class="btn-next easyui-linkbutton">下一步</a>
        </div>
    </div>
    <div id="detail-buttons">
 <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#detail').dialog('close');return false;">取消</a>
    </div>
     

        <%--添加 修改 end--%>
</asp:Content>
