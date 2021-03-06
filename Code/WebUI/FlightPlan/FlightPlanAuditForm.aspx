﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlightPlanAuditForm.aspx.cs" Inherits="FlightPlan_FlightPlanAuditForm" %>

<script>
    $(function () {
        var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {  
            $.ajax({
                url: "MyAuditFlightPlan.aspx",
                data: { id: pid,"action": "queryone" },
                type:'post',
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#name").text(data.CompanyName);
                    $("#code").text(data.Code);
                    $("#form_audit").form('load', data);
                    $("#form_audit").find('select,input').not("#Auditresult,#AuditComment,#ControlDep").attr('readonly', 'readonly');


                }
            });
        }
    })
  
</script>
<form id="form_audit">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" style="width: 100%;">
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
                                <th class="formTitle">注册号</th>
                                <td class="formValue">
                                    <input id="CallSign" name="CallSign" maxlength="50" class="easyui-textbox" style="height: 25px" />
                                </td>
                                <th class="formTitle">应答机编码</th>
                                <td class="formValue">
                                    <input id="SsrCode" name="SsrCode" maxlength="50"  class="easyui-textbox" style="height: 25px" />
                                </td>
                            </tr>
                               <tr>
                                <th class="formTitle">预计起飞时间</th>
                                <td class="formValue">
                                    <input id="SOBT" name="SOBT" maxlength="50"  class="easyui-datebox" style="height: 25px" />
                                </td>
                                <th class="formTitle">预计落地时间</th>
                                <td class="formValue">
                                    <input id="SIBT" name="SIBT" maxlength="50"  class="easyui-datebox" style="height: 25px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">起飞机场</th>
                                <td class="formValue">
                                    <input id="ADEP" name="ADEP" maxlength="50" class="easyui-textbox" style="height: 25px" />
                                </td>
                                <th class="formTitle">目的地机场</th>
                                <td class="formValue">
                                    <input id="ADES" name="ADES" maxlength="50"  class="easyui-textbox" style="height: 25px" />
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
                          <input id="Remark" name="Remark" style="width: 800px; height: 100px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>
                              <tr>
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
                           <input id="AuditComment" name="AuditComment" maxlength="400" style="width: 800px; height: 100px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
</form>

