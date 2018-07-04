<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlightPlanSubmitDetail.aspx.cs" Inherits="FlightPlan_FlightPlanSubmitDetail" %>

<script src="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.css?v=1.3")%>" rel="stylesheet" type="text/css" />


<script>
    $(function () {
        initControl();
        var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {
            $.ajax({
                url: "MySubmitFlightPlan.aspx",
                data: { id: pid,"action": "queryone" },
                type:'post',
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#name").text(data.CompanyName);
                    if (!!data.Code) {
                        $("#code").text(data.Code);
                    }
                    $("#form1").form('load', data);
        
                    //$("#form1").find('.form-control,select,input').attr('readonly', 'readonly');
                    //$("#form1").find('div.ckbox label').attr('for', '');


                }
            });
        }
    })
    function initControl() {
        $('#wizard').wizard().on('change', function (e, data) {
            var $next = $("#btn_next");
            if (data.direction == "next") {
                switch (data.step) {
                    case 1:
                        $next.attr('disabled', 'disabled');
                        break;
                    default:
                        break;
                }
            } else {
                $next.removeAttr('disabled');
            }
        });
    }
</script>
<form id="form1">
    <div class="widget-body">
        <div id="wizard" class="wizard" data-target="#wizard-steps" style="border-left: none; border-top: none; border-right: none;">
            <ul class="steps">
                <li data-target="#step-1" class="active"><span class="step">1</span>基本信息<span class="chevron"></span></li>
                <li data-target="#step-2"><span class="step">2</span>审批记录<span class="chevron"></span></li>
            </ul>
        </div>
        <div class="step-content" id="wizard-steps" style="border-left: none; border-bottom: none; border-right: none;">
            <div class="step-pane active" id="step-1" style="margin: 10px; margin-bottom: 0px;">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" >
                        <table class="form">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color:red">
                                    <span id="name"></span>
                                </td>
                                <th class="formTitle">临专号</th>
                                <td class="formValue">
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
                                <th class="formTitle">计划撤轮挡时间</th>
                                <td class="formValue">
                                   <input id="SOBT" name="SOBT" editable="false" required="true" class="easyui-datebox" style="height: 25px" />
                                </td>
                                <th class="formTitle">计划挡轮挡时间</th>
                                <td class="formValue">
                                    <input id="SIBT" name="SIBT" editable="false" required="true" class="easyui-datebox" validtype="md['#StartDate']" style="height: 25px" />
                                </td>
                            </tr>
                            <tr>
                            <th class="formTitle">起飞机场</th>
                                <td class="formValue">
                                    <input id="ADEP" name="ADEP" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
                                </td>
                                         <th class="formTitle">目的地机场</th>
                                <td class="formValue">
                                    <input id="ADES" name="ADES" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
                                </td>
                            </tr>
                            <tr>
                            <th class="formTitle">备降机场I</th>
                                <td class="formValue">
                                    <input id="ALTN1" name="ALTN1" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
                                </td>
                                         <th class="formTitle">备降机场II</th>
                                <td class="formValue">
                                    <input id="ALTN2" name="ALTN2" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
                                </td>
                            </tr>
                            <tr>
                                         <th class="formTitle">应答机编码</th>
                                <td class="formValue">
                                    <input id="SsrCode" name="SsrCode" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
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
                                <th class="formTitle" valign="top" style="padding-top: 5px;">
                                    其他说明的事项
                                </th>
                                <td class="formValue" colspan="3">
                          <input id="Remark" name="Remark" style="width: 800px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="step-pane" id="step-2" style="margin: 10px; margin-bottom: 0px;">
                <div class="panel-body" style="width: 96%;">
                   <table class="form">
                        <thead>
                            <tr>
                                <th>审核人</th>
                                <th>审核状态</th>
                                <th>审核时间</th>
                                <th>审核意见</th>
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
                                <td><%=subitem.ActorName%></td>
                                <td><%=((int)subitem.State == 1 ? "审核中" : ((int)subitem.State == 2 ? "审核通过" : "审核不通过"))%></td>
                                <td><%=subitem.ActorTime%></td>
                                <td><%=subitem.Comments%></td>
                            </tr>
                                    <%    }
                                    }
                                    else
                                    {
                            %>

                            <tr>
                                <td><%=item.ActorName%></td>
                                <td><%=((int)item.State == 1 ? "审核中" : ((int)item.State == 2 ? "审核通过" : "审核不通过"))%></td>
                                <td><%=item.ActorTime%></td>
                                <td><%=item.Comments%></td>
                            </tr>
                            <%  
                                    }
                                } %>
                        </tbody>

                    </table>
                </div>
            </div>
        </div>
        
    </div>
</form>
<div class="detail-button" id="wizard-actions"  style="padding: 2px; float: right">
               <a id="btn_last" href="javascript:void(0);" disabled class="btn-prev easyui-linkbutton">上一步</a>
                <a id="btn_next" href="javascript:void(0);" class="btn-next easyui-linkbutton">下一步</a>
        </div>
