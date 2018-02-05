 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitRepetPlanAdd.aspx.cs" Inherits="FlightPlan_MyUnSubmitRepetPlanAdd" %> 
  <form id="form_edit" method="post">
      <input type="hidden" name="PlanCode" id="PlanCode" value=""/>
        <table class="table_edit">
            <tr id="basicInfo">
                <th>申请单号：
                </th>
                <td id="code">
                
                </td>
                 <th>填写单位：
                </th>
                <td id="name">
                
                </td>
            </tr>
            <tr>
                <th>任务类型：
                </th>
                <td>
                    <input id="FlightType" name="FlightType" editable="false" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                        required="true" class="easyui-combobox" style="height:25px"/>
                </td>
  <th>航空器类型：
                </th>
                <td>
                    <%--  <input id="AircraftType" name="AircraftType"  maxlength="30" type="text"  required="true" class="easyui-textbox" />--%>
                    <input id="AircraftType" name="AircraftType" editable="false" data-options="url:'GetComboboxData.ashx?type=2',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                        required="true" class="easyui-combobox" style="height:25px"/>
                </td>
            </tr>
              <tr>
                <th>飞行范围：
                </th>
                <td>
                    <input id="FlightArea" name="FlightArea" maxlength="50"  type="text" required="true" class="easyui-validatebox textbox" />
                </td>
  <th>飞行高度（米）：
                </th>
                <td>
                 <input id="FlightHeight" name="FlightHeight" style="height:25px" maxlength="5" type="text" required="true" class="easyui-numberbox" data-options="min:1,max:50000"/>
                </td>
            </tr>
            <tr>
<%--                <th>航线走向和飞行高度：
                </th>
                <td>
                    <input id="FlightDirHeight" name="FlightDirHeight" maxlength="30"  type="text" required="true" class="easyui-validatebox textbox" />
                </td>--%>
  <th>注册号：
                </th>
                <td>
                    <input id="CallSign" name="CallSign" maxlength="30" type="text" required="true" class="easyui-validatebox textbox" />
                </td>
            </tr>
            <tr>
                <th>起飞点：
                </th>
                <td>
                    <input id="ADEP" name="ADEP" maxlength="30" type="text"   required="true" class="easyui-validatebox textbox" />
                </td>
 <th>降落点：
                </th>
                <td>
                    <input id="ADES" name="ADES" maxlength="30" type="text"   required="true" class="easyui-validatebox textbox" />
                </td>
            </tr>          
            <tr>
                <th>预计开始日期：
                </th>
                <td>
                    <input id="StartDate" name="StartDate" editable="false"  required="true"  class="easyui-datebox" style="height:25px"/>
                </td>
                <th>预计结束日期：
                </th>
                <td>
                    <input id="EndDate" name="EndDate"  editable="false" required="true" class="easyui-datebox" validtype="md['#StartDate']" style="height:25px"/>
                </td>
            </tr>
            <tr>
                <th >起飞时刻：
                </th>
                <td>
                    <input id="SOBT" name="SOBT"  required="true" class="easyui-timespinner" showSeconds="true" style="height:25px"/>
                </td>
<th>降落时刻：
                </th>
                <td >
                    <input id="SIBT" name="SIBT"  required="true" class="easyui-timespinner" showSeconds="true" style="height:25px"/>
                </td>

            </tr>

            <tr>
                <th>批件：
                </th>
                <td colspan="3">
                    <input type="hidden" name="AttchFilesInfo" id="AttchFilesInfo" />
                    <input type="file" id="AttchFiles" name="AttchFiles" />
                    <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('AttchFiles').uploadFiles()">上传</a>
                    <div id="AttchFiles-fileQueue"></div>
                    <div id="AttchFiles-fileList" style="margin-top: 2px; zoom: 1"></div>
                </td>
            </tr>
               <tr>
                <th>其他批件：
                </th>
                <td colspan="3">
                    <input type="hidden" name="OtherAttchFilesInfo" id="OtherAttchFilesInfo" />
                    <input type="file" id="OtherAttchFiles" name="OtherAttchFiles" />
                    <a id="btn_upload1" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('OtherAttchFiles').uploadFiles()">上传</a>
                    <div id="OtherAttchFiles-fileQueue"></div>
                    <div id="OtherAttchFiles-fileList" style="margin-top: 2px; zoom: 1"></div>
                </td>
            </tr>
            <tr>
                <th>周执行计划：
                </th>
                <td  colspan="3">
                    <input id="d1" type="checkbox" name="WeekSchedule" value="1" style="width:20px"/>星期一
                             <input id="d2" type="checkbox" name="WeekSchedule" value="2" style="width:20px"/>星期二
                             <input id="d3" type="checkbox" name="WeekSchedule" value="3" style="width:20px"/>星期三
                             <input id="d4" type="checkbox" name="WeekSchedule" value="4" style="width:20px"/>星期四
                             <input id="d5" type="checkbox" name="WeekSchedule" value="5" style="width:20px"/>星期五
                             <input id="d6" type="checkbox" name="WeekSchedule" value="6" style="width:20px"/>星期六
                             <input id="d7" type="checkbox" name="WeekSchedule" value="7" style="width:20px"/>星期日
                </td>

            </tr>
            <tr>
                <th>其他需要说明的事项：
                </th>
                <td colspan="3">
                    <input id="Remark" name="Remark"  style="width: 600px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                </td>
            </tr>
        </table>

    </form>
<style type="text/css">
    .table_edit tr#basicInfo td{
        color:red;
    }
</style>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var pid = '<%=Request.QueryString["id"] %>';

            if (pid) {
                $.post("MyUnSubmitRepetPlanAdd.aspx", { "action": "queryone", "id": pid }, function (data) {
                    $("#form_edit").form('load', data);
                    $("#code").html(data.PlanCode);
                    $("#name").html(data.CompanyName);
                    $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                        $("#d" + n).prop({ checked: true });
                    });
                    new dj.upload({
                        id: "AttchFiles",
                        maxSize: 5,
                        multi: true,
                        queueId: "AttchFiles-fileQueue",
                        listId: "AttchFiles-fileList",
                        truncate: "30",
                        maxCount: "1",
                        uploadPath: "Files/RepetPlan/",
                        uploadedFiles: data.AttchFile
                    });
                    new dj.upload({
                        id: "OtherAttchFiles",
                        maxSize: 5,
                        multi: true,
                        queueId: "OtherAttchFiles-fileQueue",
                        listId: "OtherAttchFiles-fileList",
                        truncate: "30",
                        maxCount: "1",
                        uploadPath: "Files/RepetPlan/",
                        uploadedFiles: data.OtherAttchFile
                    });
                });
            }
            else {
                $("#name").html('<%=User.CompanyName %>');
                $.post("MyUnSubmitRepetPlanAdd.aspx", { "action": "getplancode" }, function (data) {
                    $("#code").html(data);
                    $("#PlanCode").val(data);
                 
                });
                new dj.upload({
                        id: "AttchFiles",
                        maxSize: 5,
                        multi: true,
                        queueId: "AttchFiles-fileQueue",
                        listId: "AttchFiles-fileList",
                        truncate: "30",
                        maxCount: "1",
                        uploadPath: "Files/RepetPlan/",
                        uploadedFiles: ""
                });
                new dj.upload({
                    id: "OtherAttchFiles",
                    maxSize: 5,
                    multi: true,
                    queueId: "OtherAttchFiles-fileQueue",
                    listId: "OtherAttchFiles-fileList",
                    truncate: "30",
                    maxCount: "1",
                    uploadPath: "Files/RepetPlan/",
                    uploadedFiles: ""
                });
            }
        });


    </script>
