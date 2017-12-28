 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitFlightPlanAdd.aspx.cs" Inherits="FlightPlan_MyUnSubmitFlightPlanAdd" %> 
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

            <tr >
                <th>长期计划单号：
                </th>
                <td id="trrepcode" colspan="2" style="display:none">
                    <input id="RepetPlanID" name="RepetPlanID" 
                        required="true" class="easyui-validatebox" style="height:25px;width:250px;"/>
                </td>
                <td colspan="2" id="RepPlanCode"></td>
            </tr>

            <tr>
                <th>任务类型：
                </th>
                <td id="FlightType">
                </td>
                <th>航空器类型：
                </th>
                <td id="AircraftType">

                </td>
            </tr>
            <tr>
                <th>航线走向和飞行高度：
                </th>
                <td id="FlightDirHeight">
                </td>
  <th>航空器呼号：
                </th>
                <td id="CallSign">
                </td>
            </tr>
            <tr>
                <th>起飞机场：
                </th>
                <td id="ADEP">
                </td>
 <th>降落机场：
                </th>
                <td id="ADES">
                </td>
            </tr>          
            
            <tr>
                <th>其他需要说明的事项：
                </th>
                <td colspan="3"  id="Remark">
                </td>

            </tr>
        </table>

            <div class="datagrid-toolbar">
            <table class="table_edit">
                <tr>
                <th>起飞时刻：
                </th>
                <td>
                    <input id="SOBT" name="SOBT"  editable="false" required="true"  class="easyui-datetimebox" style="height:25px"/>
                </td>
                <th>降落时刻：
                </th>
                <td>
                    <input id="SIBT" name="SIBT" editable="false"  required="true" class="easyui-datetimebox" validtype="md['#SOBT']" style="height:25px"/>
                </td>
            </tr>
                <tr>
                    <th>航空器架数：
                    </th>
                    <td>
                        <input id="AircraftNum" name="AircraftNum" style="height:25px" maxlength="4" type="text" required="true" class="easyui-numberbox" data-options="min:1,max:100"/>
                    </td>
                    <th>机长（飞行员）姓名：
                    </th>
                    <td>
                        <input id="Pilot" name="Pilot" maxlength="15" type="text" required="true" class="easyui-validatebox textbox" />
                    </td>
                </tr>
                <tr>
                    <th>通信联络方法：
                    </th>
                    <td>
                        <input id="ContactWay" name="ContactWay" maxlength="15" type="text" required="true" class="easyui-validatebox textbox" />
                    </td>
                    <th>飞行气象条件：
                    </th>
                    <td>
                        <input id="WeatherCondition" name="WeatherCondition" maxlength="50" type="text" required="true" class="easyui-validatebox textbox" />
                    </td>
                </tr>
                <tr>
                    <th>空勤组人数：
                    </th>
                    <td>
                        <input id="AircrewGroupNum" name="AircrewGroupNum" style="height:25px" maxlength="4" type="text" data-options="min:1,max:100" required="true" class="easyui-numberbox" />
                    </td>
                    <th style="width:160px;">二次雷达应答机代码：
                    </th>
                    <td>
                        <input id="RadarCode" name="RadarCode"  maxlength="4" type="text" required="true" class="easyui-validatebox textbox" />
                    </td>
                </tr>
            </table>
</div>
        </form>

<style type="text/css">
    .table_edit tr#basicInfo td{
        color:red;
    }
</style>
    <script type="text/javascript">
        var pid = '<%=Request.QueryString["id"] %>';
        $(function () {
            if (pid) {
                $('#trrepcode').hide();
                $('#RepPlanCode').show();
                $.post("MyUnSubmitFlightPlan.aspx", { "action": "queryone", "id": pid }, function (data) {
                    $("#code").html(data.PlanCode);
                    $('#RepPlanCode').html(data.RepPlanCode);
                    $("#name").html(data.CompanyName);
                    $("#form_edit").form('load', data);
                    $("#CompanyName").html(data.CompanyName);
                    $("#FlightType").html(data.FlightType);
                    $("#CallSign").html(data.CallSign);
                    $("#AircraftType").html(data.AircraftType);
                    $("#FlightDirHeight").html(data.FlightDirHeight);
                    $("#ADEP").html(data.ADEP);
                    $("#ADES").html(data.ADES);
                    $("#Remark").html(data.Remark);
      
                });
            }
            else {
                $("#name").html('<%=User.CompanyName%>');
                $('#trrepcode').show();
                $('#RepPlanCode').hide();
                $('#RepetPlanID').combobox({
                    url: 'MyUnSubmitFlightPlanAdd.aspx?type=getallplancode',
                    editable: false,
                    valueField: 'id',
                    panelHeight: 'auto',
                    textField: 'text',
                    onSelect: function (record) {
                        $.post("MyUnSubmitFlightPlanAdd.aspx", { "action": "gerrpplan", "id": record.id }, function (data) {
                            //    $("#form_audit").form('load', data);
                            $("#FlightType").html(data.FlightType);
                            $("#CallSign").html(data.CallSign);
                            $("#AircraftType").html(data.AircraftType);
                            $("#FlightDirHeight").html(data.FlightDirHeight);
                            $("#ADEP").html(data.ADEP);
                            $("#ADES").html(data.ADES);
                            $("#Remark").html(data.Remark);

                        })
                    }
                });
                $.post("MyUnSubmitFlightPlanAdd.aspx", { "action": "getplancode" }, function (data) {
                    $("#code").html(data);
                    $("#PlanCode").val(data);

                });

            }
        });

    </script>
