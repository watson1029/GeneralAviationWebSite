<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitDetail.aspx.cs" Inherits="FlightPlan_SubmitDetail" %>

<script src="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.css?v=1.3")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/js/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/js/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/js/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/js/ga/upload.js")%>" type="text/javascript"></script>

<script>
    $(function () {
        initControl();
        var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {
            $.ajax({
                url: "MySubmitRepetPlan.aspx",
                data: { id: pid, "action": "queryone" },
                type: 'post',
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#name").text(data.CompanyName);
                    if (!!data.Code) {
                        $("#code").text(data.Code);
                    }
                    $("#form1").form('load', data);
                    $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                        $("#d" + n).prop({ checked: true });
                    });
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
                    <div class="panel-body" style="padding: 15px; width: 97%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color: red">
                                    <span id="name"></span>
                                </td>
                                <th class="formTitle">长期计划编号</th>
                                <td class="formValue">
                                    <span id="code"></span>
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">任务类型</th>
                                <td class="formValue">
                                    <input id="FlightType" name="FlightType" editable="false" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                                        required="true" class="easyui-combobox" style="width: 250px" />
                                </td>
                                <th class="formTitle">使用机型</th>
                                <td class="formValue">
                                    <input id="AircraftType" name="AircraftType" maxlength="50" required="true" class="easyui-textbox" style="width: 250px" />
                                </td>
                            </tr>
                                <tr>
                                <th class="formTitle">航空器数目</th>
                                <td class="formValue">
                              <input id="AircraftNum" name="AircraftNum" maxlength="50" class="easyui-textbox" style="width: 250px" />
                                </td>
                                <th class="formTitle">注册号</th>
                                <td class="formValue">
                                    <input id="CallSign" name="CallSign" maxlength="50" class="easyui-textbox" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">预计开始日期</th>
                                <td class="formValue">
                                    <input id="StartDate" name="StartDate" editable="false" required="true" class="easyui-datebox" style="width: 250px" />
                                </td>
                                <th class="formTitle">预计结束日期</th>
                                <td class="formValue">
                                    <input id="EndDate" name="EndDate" editable="false" required="true" class="easyui-datebox" validtype="md['#StartDate']" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle" style="height: 35px;">周执行计划</th>
                                <td class="formValue" colspan="3">
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
                                <th class="formTitle" style="padding-top: 5px;">机场及临时起降点
                                </th>
                                <td class="formValue" colspan="3">
                                    <input id="AirportText" name="AirportText" style="width: 800px; height: 100px" type="text" data-options="multiline:true" class="easyui-textbox" />
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
                        </table>
                    </div>
                </div>
            </div>
            <div class="step-pane" id="step-2" style="margin: 10px; margin-bottom: 0px;">
                <div class="panel-body" style="width: 98%; border-color:#ffffff">
                    <table class="form table table-bordered" >
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
                </div>
            </div>
        </div>

    </div>
</form>
<div class="detail-button" id="wizard-actions" style="padding: 2px; float: right">
    <a id="btn_last" href="javascript:void(0);" disabled class="btn-prev easyui-linkbutton">上一步</a>
    <a id="btn_next" href="javascript:void(0);" class="btn-next easyui-linkbutton">下一步</a>
</div>
