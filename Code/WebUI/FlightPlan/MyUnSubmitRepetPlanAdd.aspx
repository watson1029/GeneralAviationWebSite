 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitRepetPlanAdd.aspx.cs" Inherits="FlightPlan1_MyUnSubmitRepetPlanAdd" %> 
  <form id="form_edit" method="post">
      <div class="widget-body">
        <div id="wizard" class="wizard" data-target="#wizard-steps" style="border-left: none; border-top: none; border-right: none;">
            <ul class="steps">
                <li data-target="#step-1" class="active"><span class="step">1</span>基本信息<span class="chevron"></span></li>
                <li data-target="#step-2"><span class="step">2</span>附件上传<span class="chevron"></span></li>
            </ul>
        </div>
        <div class="step-content" id="wizard-steps" style="border-left: none; border-bottom: none; border-right: none;">

            <div class="step-pane active" id="step-1" style="margin: 10px; margin-bottom: 0px;">
                <div class="alert alert-danger" style="text-align: left; margin-bottom: 10px;">
                    <i class="fa fa-warning alert-dismissible" style="position: relative; top: 1px; font-size: 15px; padding-right: 5px;"></i>
                    请填写长期计划申报信息！
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" style="width: 98%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color:red">
                                  
                                </td>

                            </tr>
                            <tr>
                                <th class="formTitle">任务类型</th>
                                <td class="formValue">
                                    <select id="FlightType" name="FlightType" class="form-control required"></select>
                                </td>
                                <th class="formTitle">使用机型</th>
                                <td class="formValue">
                                    <input id="AircraftType" name="AircraftType" type="text" class="form-control required" maxlength="50" placeholder="请输入使用机型" />
                                </td>

                            </tr>

                            <tr>
                                <th class="formTitle">执行开始日期</th>
                                <td class="formValue">
                                    <input id="StartDate" name="StartDate" type="text" class="form-control input-wdatepicker required" onfocus="WdatePicker()" />
                                </td>
                                <th class="formTitle">执行结束日期</th>
                                <td class="formValue">
                                    <input id="EndDate" name="EndDate" type="text" class="form-control input-wdatepicker required" onfocus="WdatePicker()" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="height: 35px;">周执行计划</th>
                                <td class="formValue" style="padding-top: 1px;" colspan="3">
                                    <div class="ckbox">
                                        <input id="d1" name="WeekSchedule" value="1" type="checkbox" checked><label for="d1">星期一</label>
                                    </div>
                                    <div class="ckbox">
                                        <input id="d2" name="WeekSchedule" value="2" type="checkbox" checked><label for="d2">星期二</label>
                                    </div>
                                    <div class="ckbox">
                                        <input id="d3" name="WeekSchedule" value="3" type="checkbox" checked><label for="d3">星期三</label>
                                    </div>
                                    <div class="ckbox">
                                        <input id="d4" name="WeekSchedule" value="4" type="checkbox" checked><label for="d4">星期四</label>
                                    </div>
                                    <div class="ckbox">
                                        <input id="d5" name="WeekSchedule" value="5" type="checkbox" checked><label for="d5">星期五</label>
                                    </div>
                                    <div class="ckbox">
                                        <input id="d6" name="WeekSchedule" value="6" type="checkbox" checked><label for="d6">星期六</label>
                                    </div>
                                    <div class="ckbox">
                                        <input id="d7" name="WeekSchedule" value="7" type="checkbox" checked><label for="d7">星期日</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" valign="top" style="padding-top: 5px;">
                                    机场及临时起降点
                                </th>
                                <td colspan="3">
                                    @Html.AirportTable("airport")
                                </td>

                            </tr>

                            <tr>
                                <th class="formTitle" valign="top" style="padding-top: 5px;">
                                    备注
                                </th>
                                <td class="formValue" colspan="3">
                                    <textarea id="Remark" name="Remark" class="form-control" style="height: 100px;"></textarea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="step-pane" id="step-2">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">其他</h3>
                    </div>
                    <div class="panel-body" style="width: 98%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle" valign="top" style="padding-top: 5px;width:50px">
                                    航线
                                </th>
                                <td colspan="3">
                                    @Html.AirlineTable("airline")
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" valign="top" style="padding-top: 5px;">
                                    作业区
                                </th>
                                <td  colspan="3">
                                    <ul class="nav nav-tabs" style="padding-top: 10px;">
                                        <li class="active"><a href="javascript:void(0)">作业区(圆)</a></li>
                                        <li><a href="javascript:void(0)">作业区(点连线)</a></li>
                                        <li><a href="javascript:void(0)">作业区(航线左右)</a></li>
                                    </ul>
                                    <div id="con">
                                        <div style="padding-top: 20px; margin-right: 30px; display:block">
                                            @Html.CWorkTable("cwork")
                                        </div>
                                        <div style="padding-top: 20px; margin-right: 30px;display:none">
                                            @Html.PWorkTable("pwork")
                                        </div>
                                        <div style="padding-top: 20px; margin-right: 30px;display:none">
                                            @Html.HWorkTable("hwork")
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr style="height:80px">
                                <th class="formTitle">附件上传</th>
                                <td class="formValue">
                                    <input type="hidden" name="AttachFilesInfo" id="AttachFilesInfo" />
                                    @Html.Upload1("AttachFiles",
                    new UploadOptions()
                    {
                        AutoUpload = false,
                        Multi = true,
                        MaxCount = 1,
                        Editable = true,
                        Truncate = 18
                    })
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-button" id="wizard-actions">
            <a id="btn_last" disabled class="btn btn-default btn-prev">上一步</a>
            <a id="btn_next" class="btn btn-default btn-next">下一步</a>
            <a id="btn_finish" class="btn btn-default" style="display: none;" onclick="submitForm()">完成</a>
        </div>
    </div>

        <%--<table class="table_edit">
            <tr id="basicInfo">
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
  <th>使用机型：
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
                <th>飞行范围：
                </th>
                <td colspan="3">
                    <input id="FlightArea" name="FlightArea"  required="true" style="width: 600px; height: 150px" type="text" maxlength="200" data-options="multiline:true" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <th>其他需要说明的事项：
                </th>
                <td colspan="3">
                    <input id="Remark" name="Remark"  style="width: 600px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                </td>
            </tr>
        </table>--%>
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
