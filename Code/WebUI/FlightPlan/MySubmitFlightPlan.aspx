﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MySubmitFlightPlan.aspx.cs" Inherits="FlightPlan_MySubmitFlightPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="../css/fademap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>
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
                   <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-undo" plain="true" onclick="Main.Export()">导出</a>
            <div style="float:right">
                        <input id="ipt_search" menu="#search_menu"/>
                <input id="ssPlanCode" name="ssPlanCode"  type="hidden" value=""/>
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
          //  baiduMap.init();
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
                  sortName: 'FlightPlanID', //排序字段
                  idField: 'FlightPlanID', //标识字段,主键
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
                        { title: '创建人', field: 'CreatorName', width: 80 },
                        {
                            title: '创建时间', field: 'CreateTime', width: 120, formatter: function (value, rec, index) {
                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd HH:mm:ss");

                            }
                        },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },

                      {
                          title: '状态', field: 'PlanState', formatter: function (value, rec, index) {
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
                          }, width: 100
                      },
                          {
                              title: '操作', field: 'FlightPlanID', width: 80, formatter: function (value, rec) {
                                  var str = "<a style=\"color:red\" href=\"javascript:;\" onclick=\"Main.Detail('" + value + "');$(this).parent().click();return false;\">查看</a>";
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
          //初始化表格
          InitGird1: function (uid) {
              $('#tab_list1').datagrid({
                  title: '审核记录', //表格标题
                  url: location.href, //请求数据的页面
                  sortName: 'ID', //排序字段
                  idField: 'ID', //标识字段,主键
                  iconCls: '', //标题左边的图标
                  width: '95%', //宽度
                  height: 150, //高度
                  nowrap: false, //是否换行，True 就会把数据显示在一行里
                  striped: true, //True 奇偶行使用不同背景色
                  singleSelect: false,
                  collapsible: false, //可折叠

                  columns: [[
                      { title: '审核人', field: 'ActorName', width: 150 },
                      {
                          title: '审核状态', field: 'State', formatter: function (value, rec, index) {
                              var str = "";
                              if (value == "1") {
                                  str = "审核中";
                              }
                              else if (value == "2") {
                                  str = "审核通过";
                              }
                              else if (value == "3") {
                                  str = '审核不通过';
                              }
                              return str;
                          }, width: 150
                      },
                      { title: '审核时间', field: 'ActorTime', width: 150 },
                      { title: '审核意见', field: 'Comments', width: 150 }
                  ]],
                  queryParams: { "action": "getinstance", "id": uid },
                  pagination: false, //是否开启分页
                  rownumbers: true //行号
              });
          },
          Detail: function (uid) {
              $("#detail").dialog("open").dialog('setTitle', '查看飞行计划').dialog('refresh', 'FlightPlanSubmitDetail.aspx?id=' + uid);
              //$("#detail").dialog("open").dialog('setTitle', '查看');
              //$.post(location.href, { "action": "queryone", "id": uid }, function (data) {
              //    //    $("#form_audit").form('load', data);
              //    $("#PlanCode").html(data.PlanCode); 
              //    $("#FlightType").html(data.FlightType);
              //    $("#AircraftType").html(data.AircraftType);
              //    //  $("#FlightDirHeight").html(data.FlightDirHeight);
              //    $("#FlightArea").html(data.FlightArea);
              //    $("#FlightHeight").html(data.FlightHeight);
              //    $("#ADEP").html(data.ADEP);
              //    $("#ADES").html(data.ADES);
              //    $("#SOBT").html(data.SOBT);
              //    $("#SIBT").html(data.SIBT);
              //    $("#Remark").html(data.Remark);
              //    $("#AircraftNum").html(data.AircraftNum);
              //    $("#Pilot").html(data.Pilot);
              //    $("#ContactWay").html(data.ContactWay);
              //    $("#WeatherCondition").html(data.WeatherCondition);
              //    $("#AircrewGroupNum").html(data.AircrewGroupNum);
              //    $("#RadarCode").html(data.RadarCode);
              //});
              //Main.InitGird1(uid);
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
          Export: function () {
          var selRow = $('#tab_list').datagrid('getData');
          if (selRow.total == 0) {
              $.messager.alert('提示', '无记录导出！', 'info');
              return;
          }
          window.open("ExportHandler.aspx?type=3&plancode=" + $('#ssPlanCode').val());
      }
      };
    </script>
    <div id="detail" class="easyui-dialog" style="width: 700px; height:580px;"
        modal="true" closed="true" buttons="#detail-buttons">
    </div>
    
    <div class="fadediv"><div id="map" style="height:400px;"></div></div>
    <div class="fade"><span>地图显示/隐藏</span></div>
</asp:Content>
