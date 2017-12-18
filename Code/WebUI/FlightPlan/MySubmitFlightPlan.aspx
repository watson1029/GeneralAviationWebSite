<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MySubmitFlightPlan.aspx.cs" Inherits="FlightPlan_MySubmitFlightPlan" %>

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
                  sortName: 'FlightPlanID', //排序字段
                  idField: 'FlightPlanID', //标识字段,主键
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
                      { title: '申请单号', field: 'PlanCode', width: 100 },
                      { title: '航空器架数', field: 'AircraftNum', width: 100 },
                      { title: '机长（飞行员）姓名', field: 'Pilot', width: 150 },
                      { title: '通信联络方法', field: 'ContactWay', width: 100 },
                      { title: '飞行气象条件', field: 'WeatherCondition', width: 100 },
                      { title: '空勤组人数', field: 'AircrewGroupNum', width: 100 },
                      { title: '二次雷达应答机代码', field: 'RadarCode', width: 150 },

                      {
                          title: '周执行计划', field: 'WeekSchedule', width: 150, formatter: function (value, rec, index) {
                              var array = [];
                              $.each(value.replace(/\*/g, '').toCharArray(), function (i, n) {

                                  array.push("星期" + n);
                              });
                              return array.join(',');

                          }
                      },
                       { title: '创建人', field: 'CreatorName', width: 60 },
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
                                  var str = '<a style="color:red" href="javascript:;" onclick="Main.Detail(' + value + ');$(this).parent().click();return false;">查看</a>';
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
                      { title: '审核时间', field: 'ApplyTime', width: 150 },
                      { title: '审核意见', field: 'Comments', width: 150 }
                  ]],
                  queryParams: { "action": "getinstance", "id": uid },
                  pagination: false, //是否开启分页
                  rownumbers: true //行号
              });
          },
          Detail: function (uid) {
              $("#detail").dialog("open").dialog('setTitle', '查看');
              $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                  //    $("#form_audit").form('load', data);
                  $("#FlightType").html(data.FlightType);
                  $("#AircraftType").html(data.AircraftType);
                  $("#FlightDirHeight").html(data.FlightDirHeight);
                  $("#ADEP").html(data.ADEP);
                  $("#ADES").html(data.ADES);
                  $("#StartDate").html(new Date(data.StartDate).toLocaleDateString());
                  $("#EndDate").html(new Date(data.EndDate).toLocaleDateString());
                  $("#SOBT").html(data.SOBT);
                  $("#SIBT").html(data.SIBT);
                  $("#Remark").html(data.Remark);
                  $("#AircraftNum").html(data.AircraftNum);
                  $("#Pilot").html(data.Pilot);
                  $("#ContactWay").html(data.ContactWay);
                  $("#WeatherCondition").html(data.WeatherCondition);
                  $("#AircrewGroupNum").html(data.AircrewGroupNum);
                  $("#RadarCode").html(data.RadarCode);
                  if (!!data.AttchFile) {
                      var fileArray = data.AttchFile.split('|');
                      for (var i = 0; i < fileArray.length; i++) {
                          var info = fileArray[i].split(','),
                          filepath = dj.root + info[0];
                          $("#AttchFile").html('<a href="{0}" target="_blank" class="upload-filename" title="{1}">{2}</a>'.format(filepath, info[1], info[1]));
                      }
                  }
                  else {
                      $("#AttchFile").html('');
                  }
                  var arr = [];
                  $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                      arr.push("星期" + n);
                  });
                  $("#WeekSchedule").html(arr.join(','));

              });
              Main.InitGird1(uid);
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
    <div id="detail" class="easyui-dialog" style="width: 600px; height:500px;"
        modal="true" closed="true" buttons="#detail-buttons">
        <form id="form_detail" method="post">
            <table class="table_edit">
                             <tr>
                    <th>任务类型：</th>
                    <td id="FlightType"></td>
                    <th>航空器类型：</th>
                    <td id="AircraftType"></td>
                </tr>
            <tr>
                    <th style="width:176px;">航线走向和飞行高度：</th>
                    <td id="FlightDirHeight"></td>
                    <th>批件：</th>
                    <td id="AttchFile"></td>
                </tr>
                  <tr>
              <th>起飞机场：</th>
                    <td id="ADEP"></td>
                    <th>降落机场：
                    </th>
                    <td id="ADES"></td>
                </tr>
                <tr>
                    <th>预计开始日期：</th>
                    <td id="StartDate"></td>
                    <th>预计结束日期：</th>
                    <td id="EndDate"></td>
                </tr>
                <tr>
                    <th>起飞时刻：</th>
                    <td id="SOBT"></td>
                    <th>降落时刻：</th>
                    <td id="SIBT"></td>
                </tr>
                      <tr>
                      <th>周执行计划：</th>
                    <td id="WeekSchedule" colspan="3">
                    </td>
                     </tr>

              
                <tr>
                    <th style="width:176px;">其他需要说明的事项：</th>
                    <td id="Remark"></td>
                </tr>
                 <tr>
                    <th>航空器架数：</th>
                    <td id="AircraftNum"></td>
                    <th>机长（飞行员）姓名：</th>
                    <td id="Pilot"></td>
                </tr>
                      <tr>
                    <th>通信联络方法：</th>
                    <td id="ContactWay"></td>
                    <th>飞行气象条件：</th>
                    <td id="WeatherCondition"></td>
                </tr>
                 <tr>
                    <th>空勤组人数：</th>
                    <td id="AircrewGroupNum"></td>
                    <th>二次雷达应答机代码：</th>
                    <td id="RadarCode"></td>
                </tr>
                
            </table>
        </form>
        <div id="con" style="margin-left:15px;">
        <div id="tab_list1">
        </div>
        </div>
    </div>
    <div id="detail-buttons">
 <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#detail').dialog('close');return false;">取消</a>
    </div>
</asp:Content>
