<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyUnSubmitFlightPlan.aspx.cs" Inherits="FlightPlan_MyUnSubmitFlightPlan" %>

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
        <div id="tab_toolbar" style="padding: 2px 2px;">
<%--            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>--%>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>

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
    <%--添加 修改 start--%>
   <div id="edit" class="easyui-dialog" style="width: 800px; height:600px;"
        modal="true" closed="true" buttons="#edit-buttons">
        <form id="form_edit"  method="post">
                <table class="table_edit">
      
                    <tr>
                        <td class="tdal">任务类型：
                        </td>
                        <td class="tdar">
                            <input id="FlightType" name="FlightType" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"  required="true" class="easyui-combobox" />
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">航空器类型：
                        </td>
                        <td class="tdar">
                          <%--  <input id="AircraftType" name="AircraftType"  maxlength="30" type="text"  required="true" class="easyui-textbox" />--%>
                            <input id="AircraftType" name="AircraftType" data-options="url:'GetComboboxData.ashx?type=2',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200" required="true" class="easyui-combobox" />
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">航线走向和飞行高度：
                        </td>
                        <td class="tdar">
                            <input id="FlightDirHeight" name="FlightDirHeight"  maxlength="30" type="text"  required="true" class="easyui-textbox" />
                        </td>

                    </tr>

                      <tr>
                        <td class="tdal">航空器呼号：
                        </td>
                        <td class="tdar">
                            <input id="CallSign" name="CallSign"  maxlength="30" type="text"  required="true" class="easyui-textbox" />
                        </td>

                    </tr>
                         <tr>
                        <td class="tdal">降落机场：
                        </td>
                        <td class="tdar">
                            <input id="ADES" name="ADES"  maxlength="30" type="text"  required="true" class="easyui-textbox" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">预计开始日期：
                        </td>
                        <td class="tdar">
                            <input id="StartDate" name="StartDate" style="width:200px" type="text"  required="true" class="easyui-datebox" />
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">预计结束日期：
                        </td>
                        <td class="tdar">
                            <input id="EndDate" name="EndDate"  style="width:200px"  type="text"  required="true" class="easyui-datebox" validType="md['#StartDate']"/>
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">起飞时刻：
                        </td>
                        <td class="tdar">
                            <input id="SOBT" name="SOBT" style="width:200px" type="text"  required="true" class="easyui-timespinner" />
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">降落时刻：
                        </td>
                        <td class="tdar">
                            <input id="SIBT" name="SIBT" style="width:200px" type="text"  required="true" class="easyui-timespinner" validType="md['#SOBT']"/>
                        </td>

                    </tr>
                          <tr>
                        <td class="tdal">批件：
                        </td>
                        <td class="tdar">

<input type="hidden" name="AttchFileInfo" id="AttchFileInfo"/>

 <input type="file" id="AttchFile" name="AttchFile"/>
<input type="button" class="btn" value="上传" onclick="dj.getCmp('AttchFile').uploadFiles()"/>
<div id="AttchFile-fileQueue"></div>
<div id="AttchFile-fileList" style="margin-top: 2px;zoom:1"></div>

                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">周执行计划：
                        </td>
                        <td class="tdar">
                               <input id="d1" type="checkbox" name ="WeekSchedule" value="1"/>星期一
                             <input id="d2" type="checkbox" name ="WeekSchedule" value="2"/>星期二
                             <input id="d3" type="checkbox" name ="WeekSchedule" value="3"/>星期三
                             <input id="d4" type="checkbox" name ="WeekSchedule" value="4"/>星期四
                             <input id="d5" type="checkbox" name ="WeekSchedule" value="5"/>星期五
                             <input id="d6" type="checkbox" name ="WeekSchedule" value="6"/>星期六
                             <input id="d7" type="checkbox" name ="WeekSchedule" value="7"/>星期七
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">其他需要说明的事项：
                        </td>
                        <td class="tdar">
                            <input id="Remark" name="Remark"  maxlength="200" style="width:600px;height:150px" type="text" data-options="multiline:true"  class="easyui-textbox" />
                        </td>

                    </tr>
                </table>

        </form>
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
<script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
    <%--添加 修改 end--%>
  <script type="text/javascript">

      $(function () {
          Main.InitGird();
          Main.InitSearch();
          new dj.upload({
              id: "AttchFile",
              maxSize: 5,
              multi: true,
              queueId: "AttchFile-fileQueue",
              listId: "AttchFile-fileList",
              truncate: "18",
              maxCount: "1",
              uploadPath: "Files/PJ/"
          });
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
                      { field: 'cbx', checkbox: true },
                  ]],
                  columns: [[
                      { title: '申请单号', field: 'PlanCode', width: 180 },
                      { title: '任务类型', field: 'FlightType', width: 60 },
                      { title: '航空器呼号', field: 'CallSign', width: 80 },
                      { title: '使用机型', field: 'AircraftType', width: 60 },
                      { title: '航线走向和飞行高度', field: 'FlightDirHeight', width: 150 },
                      {
                          title: '预计开始时间', field: 'StartDate', width: 100, formatter: function (value, rec, index) {

                              var timesstamp = new Date(value);
                              return timesstamp.toLocaleDateString();

                          }
                      },
                      {
                          title: '预计结束时间', field: 'EndDate', width: 100, formatter: function (value, rec, index) {

                              var timesstamp = new Date(value);
                              return timesstamp.toLocaleDateString();

                          }
                      },
                      {
                          title: '起飞时刻', field: 'SOBT', width: 100, formatter: function (value, rec, index) {

                              var timesstamp = new Date(value);
                              return timesstamp.toLocaleTimeString();

                          }
                      },
                      {
                          title: '降落时刻', field: 'SIBT', width: 100, formatter: function (value, rec, index) {

                              var timesstamp = new Date(value);
                              return timesstamp.toLocaleTimeString();

                          }
                      },
                      { title: '起飞机场', field: 'ADEP', width: 80 },
                      { title: '降落机场', field: 'ADES', width: 80 },

                      {
                          title: '周执行计划', field: 'WeekSchedule', width: 150, formatter: function (value, rec, index) {
                              var array = [];
                              $.each(value.toCharArray(), function (i, n) {

                                  array.push("星期" + n);
                              });
                              return array.join(',');

                          }
                      },
                       { title: '创建人', field: 'CreatorName', width: 60 },
                        { title: '其他需要说明的事项', field: 'Remark', width: 150 },

                      { title: '状态', field: 'PlanState', formatter: function (value, rec, index) { return value == 0 ? '草稿中' : '' }, width: 50 },
                      {
                          title: '操作', field: 'RepetPlanID', width: 80, formatter: function (value, rec) {
                              var str = '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;<a style="color:red" href="javascript:;" onclick="Main.Submit(' + value + ');$(this).parent().click();return false;">提交</a>';
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

          //打开添加窗口
          OpenWin: function () {
              $("#edit").dialog("open").dialog('setTitle', '新增');
              $("#form_edit").form('clear');
              $("#btn_add").attr("onclick", "Main.Save();")
          },
          //提交按钮事件
          Save: function (uid) {
              if (!$("#form_edit").form("validate")) {
                  return;
              }
              var fileInfo = dj.getCmp("AttchFile").getUploadedFiles();
              $("#AttchFileInfo").val(fileInfo);
              qx = $("input[name='WeekSchedule']").map(function () {
                  var $this = $(this);
                  if ($this.is(':checked')) {
                      return $this.val();
                  }
                  else {
                      return '*';
                  }
              }).get().join('');
              var json = $.param({ "id": uid, "action": "save", "qx": qx }) + '&' + $('#form_edit').serialize();
              $.post(location.href, json, function (data) {
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
              $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")

              $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                  $("#form_edit").form('load', data);
                  $.each(data.WeekSchedule.toCharArray(), function (i, n) {
                      $("#d" + n).attr("checked", true);
                  });
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
                      $.post(location.href, { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

                          if (data.isSuccess) {
                              $("#tab_list").datagrid("reload");
                              selRow.length = 0;
                          }
                      });
                  }
              });
          },
          Submit: function (uid) {

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
</asp:Content>
