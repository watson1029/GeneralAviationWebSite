<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitFlightPlanAdd.aspx.cs" Inherits="FlightPlan_MyUnSubmitFlightPlanAdd" %>

<script src="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.css?v=1.3")%>" rel="stylesheet" type="text/css" />
<script>
    $(function () {
        initControl();
         var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {
            $.ajax({
                url: "MySubmitFlightPlan.aspx",
                data: { id: pid },
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#form1").form('load', data);
                    $("#MasterIDs").bindSelect({
                        url: "/FlightPlan/FlyPlanNew/GetAllAirlineWork",
                        param: { keyValue: data.RepetPlanID },
                        multiple: true
                    });
                    $("#MasterIDs").val(data.airlineList).trigger('change');
                }
            });
        }
        $("select#RepetPlanID").change(function () {
           var val= $(this).val();
           if (val != "") {
                $.ajax({
                    url: "/FlightPlan/RepetPlanNew/GetFormJson",
                    data: { keyValue: val },
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        //$("#reptfrom").formSerialize(data);
                        $("#Code").val(data.Code);
                        $("#FlightType").val(data.FlightType);
                        $("#AircraftType").val(data.AircraftType);
                        $("#Remark").val(data.Remark);
                        $("#AirportText").val(data.AirportText);
                        $("#MasterIDs").bindSelect({
                            url: "/FlightPlan/FlyPlanNew/GetAllAirlineWork",
                            param: { keyValue: val },
                            multiple:true
                        });
                        
                    }
                });
               
            }
            else {
                $(':input', '#reptfrom').val('');
            }

        });
    })
    function initControl() {
        $("#RepetPlanID").bindSelect({
            url: "/FlightPlan/FlyPlanNew/GetAllRepPlanCode"
        });


        $('#wizard').wizard().on('change', function (e, data) {
            var $finish = $("#btn_finish");
            var $next = $("#btn_next");
            if (data.direction == "next") {
                switch (data.step) {
                    case 1:
                        if (!$("#form1").form("validate")) {
                            return false;
                        }
                        $finish.show();
                        $next.hide();
                        break;
                    default:
                        break;
                }
            } else {
                $finish.hide();
                $next.show();
            }
        });

    }
    function submitForm() {
        if (!$('#form1').formValid()) {
            return false;
        }
        var postData = $("#form1").formSerialize();      
        postData["MasterIDs"] = $("#MasterIDs").val().join(',');
        console.log(postData);
        $.submitForm({
            url: "/FlightPlan/FlyPlanNew/SubmitForm?keyValue=" + keyValue,
            param: postData,
            success: function () {
                $.currentWindow().$("#gridList").trigger("reloadGrid");
            }
        });
    }
</script>
<form id="form1">
    <div class="widget-body">
        <div id="wizard" class="wizard" data-target="#wizard-steps" style="border-left: none; border-top: none; border-right: none;">
            <ul class="steps">
                <li data-target="#step-1" class="active"><span class="step">1</span>长期计划信息<span class="chevron"></span></li>
                <li data-target="#step-2"><span class="step">2</span>基础信息<span class="chevron"></span></li>
            </ul>
        </div>
        <div class="step-content" id="wizard-steps" style="border-left: none; border-bottom: none; border-right: none;">
            <div class="step-pane active" id="step-1" style="margin: 10px; margin-bottom: 0px;">
                <div class="alert alert-danger" style="text-align: left; margin-bottom: 10px;">
                    <i class="fa fa-warning alert-dismissible" style="position: relative; top: 1px; font-size: 15px; padding-right: 5px;"></i>
                    请选择一条长期计划信息！
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">长期计划信息</h3>
                    </div>
                    <div class="panel-body" style="width: 98%;">
                        <table class="form" id="reptfrom">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color:red">
                                    <select id="CompanyName" name="CompanyName" class="form-control required"></select>
                                </td>
                                <th class="formTitle">临专号</th>
                                <td class="formValue">
                                    @Html.Hidden("Code")
                                    <select id="RepetPlanID" name="RepetPlanID" required">
                                        <option value="">请选择</option>
                                    </select>
                                </td>
                            </tr>

                            <tr>
                                <th class="formTitle">任务类型</th>
                                <td class="formValue">
                                     <input id="FlightType" name="FlightType" editable="false" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                                        required="true" class="easyui-combobox" style="height: 25px" />
                                </td>
                                <th class="formTitle">航空器类型</th>
                                <td class="formValue">
                                   <input id="AircraftType" name="AircraftType" maxlength="50" required="true" class="easyui-textbox" style="height: 25px" />
                                </td>
                            </tr>
                          
                            <tr>
                                <th class="formTitle">机场及临时起降点</th>
                                <td class="formValue" colspan="3">
                                    <textarea id="AirportText" name="AirportText" class="form-control" style="height: 100px;"></textarea>
                                </td>

                            </tr>
                            <tr>
                                <th class="formTitle">航线及作业区</th>
                                <td class="formValue" colspan="3">
                                    <select id="MasterIDs" name="MasterIDs" class="form-control required">
                                      
                                    </select>
                                </td>
                           
                            </tr>
                            <tr>
    <th class="formTitle" valign="top" style="padding-top: 5px;">
        其他说明的事项
    </th>
    <td class="formValue" colspan="3">
        <textarea id="Remark" name="Remark" class="form-control" style="height: 100px;"></textarea>
    </td>
</tr>
                        </table>

                    </div>

                </div>
            </div>
            <div class="step-pane" id="step-2" style="margin: 10px; margin-bottom: 0px;">
                <div class="alert alert-danger" style="text-align: left; margin-bottom: 10px;">
                    <i class="fa fa-warning alert-dismissible" style="position: relative; top: 1px; font-size: 15px; padding-right: 5px;"></i>
                    请填写飞行计划申报信息！
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" style="width: 98%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">航班号</th>
                                <td class="formValue">
                                    <input id="CallSign" name="CallSign" required="true"  class="easyui-textbox"/>
                                </td>
                                <th class="formTitle">应答机编码</th>
                                <td class="formValue">
                                    <input id="SsrCode" name="SsrCode"  maxlength="5" required="true"  class="easyui-textbox"/>
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">计划撤轮挡时间</th>
                                <td class="formValue">
                                     <input id="SOBT" name="SOBT" editable="false" required="true" class="easyui-datebox" style="height: 25px" />
                                </td>
                                <th class="formTitle">计划挡轮挡时间</th>
                                <td class="formValue">
                                      <input id="SIBT" name="SIBT" editable="false" required="true" class="easyui-datebox" style="height: 25px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">起飞机场</th>
                                <td class="formValue">
                                    <input id="ADEP" name="ADEP" type="text" maxlength="4" required="true"  class="easyui-textbox"/>
                                </td>
                                <th class="formTitle">目的地机场</th>
                                <td class="formValue">
                                    <input id="ADES" name="ADES" type="text" maxlength="4" required="true"  class="easyui-textbox"/>
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">航空器数量</th>
                                <td class="formValue">
                                                      <input id="AircraftNumber" name="AircraftNumber" style="height: 25px" maxlength="4" type="text" required="true" class="easyui-numberbox" data-options="min:1,max:100" />
                                </td>
  
                            </tr>
                            <tr>
                                <th class="formTitle">备降机场I</th>
                                <td class="formValue">
                                    <input id="ALTN1" name="ALTN1" type="text" maxlength="4"  class="easyui-textbox"/>
                                </td>
                                <th class="formTitle">备降机场II</th>
                                <td class="formValue">
                                    <input id="ALTN2" name="ALTN2" type="text" maxlength="4"  class="easyui-textbox"/>
                                </td>
                            </tr>
                                 
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-button" id="wizard-actions">
            <a id="btn_last" disabled class="btn-prev easyui-linkbutton">上一步</a>
            <a id="btn_next" class="btn-next easyui-linkbutton">下一步</a>
            <a id="btn_finish" class="easyui-linkbutton" style="display: none;" onclick="submitForm()">完成</a>
        </div>
    </div>
</form>