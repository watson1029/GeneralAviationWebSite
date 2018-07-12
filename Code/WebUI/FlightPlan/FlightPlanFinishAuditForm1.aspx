<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlightPlanFinishAuditForm1.aspx.cs" Inherits="FlightPlan_FlightPlanFinishAuditForm1" %>



<script>
    $(function () {
        var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {
            $.ajax({
                url: "MyFinishAuditFlightPlan.aspx",
                data: { id: pid, "action": "queryone" },
                type: 'post',
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#name").text(data.CompanyName);
                    if (!!data.Code) {
                        $("#code").text(data.Code);
                    }
                    $("#form_edit").form('load', data);
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
                    <td class="formValue" style="color: red">
                        <span id="name"></span>
                    </td>
                    <th class="formTitle">长期计划编号</th>
                    <td class="formValue" style="color: red">
                        <span id="code"></span>
                    </td>
                </tr>
                <tr>
                    <th class="formTitle">任务类型</th>
                    <td class="formValue">
                        <input id="FlightType" name="FlightType" editable="false" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                             class="easyui-combobox" style="height: 25px" />
                    </td>
                    <th class="formTitle">使用机型</th>
                    <td class="formValue">
                        <input id="AircraftType" name="AircraftType" class="easyui-textbox" style="height: 25px" />
                    </td>
                </tr>
                <tr>
                    <th class="formTitle">预计起飞时间</th>
                    <td class="formValue">
                        <input id="SOBT" name="SOBT" editable="false" class="easyui-datebox" style="height: 25px" />
                    </td>
                    <th class="formTitle">预计落地时间</th>
                    <td class="formValue">
                        <input id="SIBT" name="SIBT" editable="false"  class="easyui-datebox"  style="height: 25px" />
                    </td>
                </tr>
                <tr>
                    <th class="formTitle">起飞机场</th>
                    <td class="formValue">
                        <input id="ADEP" name="ADEP"  class="easyui-textbox" style="height: 25px" />
                    </td>
                    <th class="formTitle">目的地机场</th>
                    <td class="formValue">
                        <input id="ADES" name="ADES"  class="easyui-textbox" style="height: 25px" />
                    </td>
                </tr>
         
                <tr>
                    <th class="formTitle">航空器数量</th>
                    <td class="formValue">
                        <input id="AircraftNum" name="AircraftNum" class="easyui-textbox" style="height: 25px" />
                    </td>
                    <th class="formTitle">应答机编码</th>
                    <td class="formValue">
                        <input id="SsrCode" name="SsrCode" class="easyui-textbox" style="height: 25px" />
                    </td>
                </tr>
                <tr>
                    <th class="formTitle" style="padding-top: 5px;">航线及作业区
                    </th>
                    <td class="formValue" colspan="3">
                        <input id="AirlineWorkText" name="AirlineWorkText" style="width: 800px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                    </td>
                </tr>
                <tr>
                    <th class="formTitle" style="padding-top: 5px;">其他说明的事项
                    </th>
                    <td class="formValue" colspan="3">
                        <input id="Remark" name="Remark" style="width: 800px; height: 100px" type="text" data-options="multiline:true" class="easyui-textbox" />
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
            </table>
        </div>
    </div>
</form>
