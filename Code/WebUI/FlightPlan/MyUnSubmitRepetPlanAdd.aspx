﻿ <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitRepetPlanAdd.aspx.cs" Inherits="FlightPlan_MyUnSubmitRepetPlanAdd" %> 
  <form id="form_edit" method="post">
        <table class="table_edit">

            <tr>
                <th>任务类型：
                </th>
                <td>
                    <input id="FlightType" name="FlightType" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                        required="true" class="easyui-combobox" />
                </td>
  <th>航空器类型：
                </th>
                <td>
                    <%--  <input id="AircraftType" name="AircraftType"  maxlength="30" type="text"  required="true" class="easyui-textbox" />--%>
                    <input id="AircraftType" name="AircraftType" data-options="url:'GetComboboxData.ashx?type=2',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                        required="true" class="easyui-combobox" />
                </td>
            </tr>
            <tr>
              

            </tr>
            <tr>
                <th>航线走向和飞行高度：
                </th>
                <td>
                    <input id="FlightDirHeight" name="FlightDirHeight" maxlength="30" type="text" required="true" class="easyui-textbox" />
                </td>
  <th>航空器呼号：
                </th>
                <td>
                    <input id="CallSign" name="CallSign" maxlength="30" type="text" required="true" class="easyui-textbox" />
                </td>
            </tr>

            <tr>
              

            </tr>
            <tr>
                <th>起飞机场：
                </th>
                <td>
                    <input id="ADEP" name="ADEP" maxlength="30" type="text" required="true" class="easyui-textbox" />
                </td>
 <th>降落机场：
                </th>
                <td>
                    <input id="ADES" name="ADES" maxlength="30" type="text" required="true" class="easyui-textbox" />
                </td>
            </tr>
           
            <tr>
                <th>预计开始日期：
                </th>
                <td>
                    <input id="StartDate" name="StartDate" style="width: 200px" type="text" required="true" class="easyui-datebox" />
                </td>
                <th>预计结束日期：
                </th>
                <td>
                    <input id="EndDate" name="EndDate" style="width: 200px" type="text" required="true" class="easyui-datebox" validtype="md['#StartDate']" />
                </td>
            </tr>

            <tr>
                <td class="tdal">起飞时刻：
                </td>
                <td class="tdar">
                    <input id="SOBT" name="SOBT" style="width: 200px" type="text" required="true" class="easyui-timespinner" />
                </td>
<td class="tdal">降落时刻：
                </td>
                <td class="tdar">
                    <input id="SIBT" name="SIBT" style="width: 200px" type="text" required="true" class="easyui-timespinner" />
                </td>

            </tr>

            <tr>
                <th>批件：
                </th>
                <td>

                    <input type="hidden" name="AttchFilesInfo" id="AttchFilesInfo" />
                    <input type="file" id="AttchFiles" name="AttchFiles" />
                    <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('AttchFiles').uploadFiles()">上传</a>
                    <div id="AttchFiles-fileQueue"></div>
                    <div id="AttchFiles-fileList" style="margin-top: 2px; zoom: 1"></div>
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
                             <input id="d7" type="checkbox" name="WeekSchedule" value="7" style="width:20px"/>星期七
                </td>

            </tr>
            <tr>
                <th>其他需要说明的事项：
                </th>
                <td colspan="3">
                    <input id="Remark" name="Remark" maxlength="200" style="width: 600px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                </td>

            </tr>
        </table>

    </form>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var id = '<%=Request.QueryString["id"] %>';

            if (id) {
                $.post(location.href, { "action": "queryone", "id": id }, function (data) {
                    $("#form_edit").form('load', data);
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
                        uploadPath: "Files/PJ/",
                        uploadedFiles: data.AttchFile
                    });
                });
            }
            else {
                new dj.upload({
                    id: "AttchFiles",
                    maxSize: 5,
                    multi: true,
                    queueId: "AttchFiles-fileQueue",
                    listId: "AttchFiles-fileList",
                    truncate: "30",
                    maxCount: "1",
                    uploadPath: "Files/PJ/",
                    uploadedFiles: ""
                });
            }
        });


    </script>
