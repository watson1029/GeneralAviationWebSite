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
                                ,panelMaxHeight:200" required="true" class="easyui-combobox" style="height:25px"/>
                </td>
   <th>航空器类型：
                </th>
                <td>
                      <input id="AircraftType" name="AircraftType"  maxlength="30" type="text"  required="true" class="easyui-textbox" style="height:25px"/>
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
                <th>机场及临时起降点：
                </th>
                <td colspan="3">
                    <input id="Airport" name="Airport"  required="true" style="width: 600px; height: 150px" type="text" maxlength="200" data-options="multiline:true" class="easyui-textbox" />
                </td>
            </tr>   
            <tr>
                <th>航线及作业区：
                </th>
                <td colspan="3">
                    <input id="FlightArea" name="FlightArea"  required="true" style="width: 600px; height: 150px" type="text" maxlength="200" data-options="multiline:true" class="easyui-textbox" />
                </td>
            </tr>   
            <tr>
                <th>是否紧急任务：
                </th>
                <td>
                 <input id="IsUrgentTask" type="checkbox" name="IsUrgentTask" value="true" style="width:20px"/>
                </td>
                <th>是否跨区飞行：
                </th>
                <td>
                 <input id="IsCrossArea" type="checkbox" name="IsCrossArea" value="true" style="width:20px"/>
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
               
            }
        });


    </script>
