<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepetPlanFinishAuditForm1.aspx.cs" Inherits="FlightPlan_RepetPlanFinishAuditForm1" %>

<script src="<%=Page.ResolveUrl("~/Content/js/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/js/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/js/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/js/ga/upload.js")%>" type="text/javascript"></script>

<script>
    $(function () {
        var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {  
            $.ajax({
                url: "MyFinishAuditRepetPlan.aspx",
                data: { id: pid,"action": "queryone" },
                type:'post',
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#name").text(data.CompanyName);
                    if (!!data.Code) {
                        $("#code").text(data.Code);
                    }
                    $("#form_edit").form('load', data);
                    $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                        $("#d" + n).prop({ checked: true });
                    });
                    $("#form_edit").find('select,input').not("#Comments").attr('readonly', 'readonly');


                }
            });
        }
    })
  
</script>
<form id="form_edit">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" style="width: 96%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color:red">
                                    <span id="name"></span>
                                </td>
                                  <th class="formTitle">长期计划编号</th>
                                <td class="formValue" style="color:red">
                                    <span id="code"></span>
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">任务类型</th>
                                <td class="formValue">
                                  <input id="FlightType" name="FlightType" editable="false" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                                        required="true" class="easyui-combobox" style="height: 25px" />
                                </td>
                                <th class="formTitle">使用机型</th>
                                <td class="formValue">
                                    <input id="AircraftType" name="AircraftType" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
                                </td>
                            </tr>

                            <tr>
                                <th class="formTitle">预计开始日期</th>
                                <td class="formValue">
                                   <input id="StartDate" name="StartDate" editable="false" required="true" class="easyui-datebox" style="height: 25px" />
                                </td>
                                <th class="formTitle">预计结束日期</th>
                                <td class="formValue">
                                    <input id="EndDate" name="EndDate" editable="false" required="true" class="easyui-datebox" validtype="md['#StartDate']" style="height: 25px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="height: 35px;">周执行计划</th>
                                <td class="formValue"  colspan="3">
                                     <input id="d1" type="checkbox" name="WeekSchedule" value="1" style="width: 20px" />星期一
                             <input id="d2" type="checkbox" name="WeekSchedule" value="2" style="width: 20px" />星期二
                             <input id="d3" type="checkbox" name="WeekSchedule" value="3" style="width: 20px" />星期三
                             <input id="d4" type="checkbox" name="WeekSchedule" value="4" style="width: 20px" />星期四
                             <input id="d5" type="checkbox" name="WeekSchedule" value="5" style="width: 20px" />星期五
                             <input id="d6" type="checkbox" name="WeekSchedule" value="6" style="width: 20px" />星期六
                             <input id="d7" type="checkbox" name="WeekSchedule" value="7" style="width: 20px" />星期日
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="padding-top: 5px;">
                                    机场及临时起降点
                                </th>
                                <td class="formValue" colspan="3">
                                            <input id="AirportText" name="AirportText" style="width: 800px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="padding-top: 5px;">
                                    航线及作业区
                                </th>
                                <td class="formValue" colspan="3">
                                    <input id="AirlineWorkText" name="AirlineWorkText" style="width: 800px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="padding-top: 5px;">
                                    其他说明的事项
                                </th>
                                <td class="formValue" colspan="3">
                          <input id="Remark" name="Remark" style="width: 800px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>

                                            <%
                    if (auditList.Count() > 0)
                    {        %>
                <tr>
                    <th class="formTitle" style="padding-top: 5px;">审批记录
                    </th>
                    <td class="formValue" colspan="3">
                        <table class="form table table-bordered">
                            <thead>
                                <tr>
                                    <th style="text-align: center">审核人</th>
                                    <th style="text-align: center">审核状态</th>
                                    <th style="text-align: center">审核时间</th>
                                    <th style="text-align: center">审核意见</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% foreach (var item in auditList)
                                    {
                                        if (item.IsParallel.HasValue && item.IsParallel.Value)
                                        {
                                            foreach (var subitem in item.SubActualStepsList)
                                            {
                                %>
                                <tr>
                                    <td class="formValue"><%=subitem.ActorName%></td>
                                    <td class="formValue"><%=((int)subitem.State == 1 ? "审核中" : ((int)subitem.State == 2 ? "审核通过" : "审核不通过"))%></td>
                                    <td class="formValue"><%=subitem.ActorTime%></td>
                                    <td class="formValue"><%=subitem.Comments%></td>
                                </tr>
                                <%    }
                                    }
                                    else
                                    {
                                %>
                                <tr>
                                    <td class="formValue"><%=item.ActorName%></td>
                                    <td class="formValue"><%=((int)item.State == 1 ? "审核中" : ((int)item.State == 2 ? "审核通过" : "审核不通过"))%></td>
                                    <td class="formValue"><%=item.ActorTime%></td>
                                    <td class="formValue"><%=item.Comments%></td>
                                </tr>
                                <%  
                                        }
                                    } %>
                            </tbody>

                        </table>
                    </td>

                </tr>
                <%} %>
                        <%--      <tr>
                                <th class="formTitle" style="padding-top: 5px;">
                                    审核结果
                                </th>
                                <td class="formValue">
                           <select id="Auditresult" class="easyui-combobox" editable="false" name="Auditresult" required="true" panelheight="auto" style="width: 200px;">
                            <option value="0" selected="true">通过</option>
                            <option value="1">不通过</option>
                        </select>
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="padding-top: 5px;">
                                    审核意见
                                </th>
                                <td class="formValue">
                           <input id="Comments" name="Comments" required="true" maxlength="400" style="width: 600px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>--%>
                        </table>
                    </div>
                </div>
</form>