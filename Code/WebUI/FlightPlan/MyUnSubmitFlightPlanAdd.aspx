<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitFlightPlanAdd.aspx.cs" Inherits="FlightPlan_MyUnSubmitFlightPlanAdd" %>

<script src="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.css?v=1.3")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/airlineTable.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/workTable.js")%>" type="text/javascript"></script>
<script>
    repetPlan = {
        newairportobj: null,
        newairlineobj: null,
        newworkobj: null
    };
    $(function () {
        $("#name").html('<%=User.CompanyName%>');
        initControl();
        var pid = '<%=Request.QueryString["id"] %>';
        if (!!pid) {
            $.ajax({
                url: "MyUnSubmitFlightPlan.aspx",
                data: { id: pid, "action": "queryone" },
                type:"post",
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#form1").form('load', data);
                    //$("#MasterIDs").bindSelect({
                    //    url: "/FlightPlan/FlyPlanNew/GetAllAirlineWork",
                    //    param: { keyValue: data.RepetPlanID },
                    //    multiple: true
                    //});
                    //$("#MasterIDs").val(data.airlineList).trigger('change');
                }
            });
        }
    })
    $(".nav-tabs li").click(function () {
        var i = $(this).index();
        $(this).addClass("active").siblings().removeClass("active");
        $('#con div').eq(i).show().siblings().hide();
    });
    function initControl() {
        var airlineobj = {
            id: "airline",
            addid: "airline-Add",
            minusid: "airline-Minus",
            addcolid: "airline-AddCol",
            minuscolid: "airline-MinusCol",
            listid: "airline-List",
            conid: "airline-Con"
        };
        var workobj = {
            id: "cwork",
            caddid: "cwork-Add",
            cminusid: "cwork-Minus",
            paddid: "pwork-Add",
            pminusid: "pwork-Minus",
            haddid: "hwork-Add",
            hminusid: "hwork-Minus",
            plistid: "pwork-List",
            hlistid: "hwork-List",
            cconid: "cwork-Con",
            pconid: "pwork-Con",
            hconid: "hwork-Con",
            paddcolid: "pwork-AddCol",
            pminuscolid: "pwork-MinusCol",
            haddcolid: "hwork-AddCol",
            hminuscolid: "hwork-MinusCol"
        };
        repetPlan.newairlineobj = new dj.dyAirlineTable(airlineobj);
        repetPlan.newairlineobj.init();
        repetPlan.newworkobj = new dj.dyWorkTable(workobj);
        repetPlan.newworkobj.init();
        $('#wizard').wizard().on('change', function (e, data) {
            var $finish = $("#btn_finish");
            var $next = $("#btn_next");
            if (data.direction == "next") {
                switch (data.step) {
                    case 1:

                        //if (!$("#form1").form("validate")) {
                        //    return false;
                        //}
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
        //if (!$('#form1').formValid()) {
        //    return false;
        //}
        //var postData = $("#form1").serialize();
        //postData["MasterIDs"] = $("#MasterIDs").val().join(',');
        $("#IsTempFlightPlan").val();
        alert($("#IsTempFlightPlan").val());
        //if()
        //{
        
        //}
        return;
        $("#btn_finish").attr("disabled", "disabled");
        var json = $.param({ "action": "save", "MasterIDs": $("#MasterIDs").combobox('getValues').join(',') }) + '&' + $('#form1').serialize();
        $.ajax({
            type: 'post',
            url: 'MyUnSubmitFlightPlan.aspx',
            data: json,
            success: function (data) {
                $.messager.alert('提示', data.msg, 'info', function () {
                    if (data.isSuccess) {
                        $("#tab_list").datagrid("reload");
                        $("#edit").dialog("close");
                    }
                    $("#btn_finish").removeAttr("disabled");
                });
            },
            error: function (xhr, err) {
                $("#btn_finish").removeAttr("disabled");
                $.messager.alert('提示', '系统繁忙，请稍后再试！', 'info');
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
                    <div class="panel-body" style="padding: 15px;width:98%;">
                        <table class="form" id="reptfrom">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color: red" id="name"></td>
                                <th class="formTitle">临专号</th>
                                <td class="formValue">
                                    <input type="hidden" name="Code" id="Code" value="" />

                                    <input id="RepetPlanID" name="RepetPlanID" editable="false" data-options="url:'GetComboboxData.ashx?type=4',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200,onSelect:function(rec){     
                                           $.ajax({
                    url: 'MySubmitRepetPlan.aspx',
                      type:'post',                  
                   data: { id: rec.id,'action': 'queryone' },
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                         $('#Code').val(data.Code);
                        $('#FlightType').textbox('setValue',data.FlightType);
                        $('#AircraftType').textbox('setValue',data.AircraftType);
                        $('#Remark').textbox('setValue',data.Remark);
                        $('#AirportText').textbox('setValue',data.AirportText);
                         $('#MasterIDs').combobox('reload','GetComboboxData.ashx?type=5&id='+rec.id);
                    }
                });  }"  class="easyui-combobox" style="height: 25px;" />
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
                                <th class="formTitle">是否临时计划</th>
                                <td colspan="3" class="formValue">
                                    <input id="IsTempFlightPlan" type="checkbox"  name="IsTempFlightPlan" value="1" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">机场及临时起降点</th>
                                <td class="formValue" colspan="3">
                                    <input id="AirportText" name="AirportText" style="width: 800px; height: 150px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>

                            <tr>
                                <th class="formTitle">航线及作业区</th>
                                <td class="formValue" colspan="3">
                                    <input id="MasterIDs" name="MasterIDs" editable="false" class="easyui-combobox" data-options="method:'get',multiple:true,valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                                        style="height: 25px; width: 600px;" />
                                </td>

                            </tr>
                            <tr>
                                <th class="formTitle" style="padding-top: 5px;">其他说明的事项
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
                <div class="alert alert-danger" style="text-align: left; margin-bottom: 10px;">
  
                    请填写飞行计划申报信息！
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" style="padding: 15px;width: 96.5%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">航班号</th>
                                <td class="formValue">
                                    <input id="CallSign" name="CallSign" required="true" class="easyui-textbox" style="height: 25px;width:200px" />
                                </td>
                                <th class="formTitle">应答机编码</th>
                                <td class="formValue">
                                    <input id="SsrCode" name="SsrCode" maxlength="5" required="true" class="easyui-textbox" style="height: 25px;width:200px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">计划撤轮挡时间</th>
                                <td class="formValue">
                                    <input id="SOBT" name="SOBT" editable="false" required="true" class="easyui-datebox" style="height: 25px;width:200px" />
                                </td>
                                <th class="formTitle">计划挡轮挡时间</th>
                                <td class="formValue">
                                    <input id="SIBT" name="SIBT" editable="false" required="true" class="easyui-datebox" style="height: 25px;width:200px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">起飞机场</th>
                                <td class="formValue">
                                    <input id="ADEP" name="ADEP" type="text" maxlength="4" required="true" class="easyui-textbox" style="height: 25px;width:200px"/>
                                </td>
                                <th class="formTitle">目的地机场</th>
                                <td class="formValue">
                                    <input id="ADES" name="ADES" type="text" maxlength="4" required="true" class="easyui-textbox" style="height: 25px;width:200px"/>
                                </td>
                            </tr>                       
                            <tr>
                                <th class="formTitle">备降机场I</th>
                                <td class="formValue">
                                    <input id="ALTN1" name="ALTN1"  maxlength="4" class="easyui-textbox" style="height: 25px;width:200px"/>
                                </td>
                                <th class="formTitle">备降机场II</th>
                                <td class="formValue">
                                    <input id="ALTN2" name="ALTN2"  maxlength="4" class="easyui-textbox" style="height: 25px;width:200px"/>
                                </td>
                            </tr>
                                 <tr>
                                <th style="padding-top: 5px; width: 50px">航线
                                </th>
                                <td colspan="3">
                                    <table id="airline-List" class="table table-bordered" style="width: 100%;">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">飞行高度</th>
                                                <th style="text-align: center">点中文名1</th>
                                                <th style="text-align: center">经纬度1</th>
                                                <th style="text-align: center">点中文名2</th>
                                                <th style="text-align: center">经纬度2</th>

                                            </tr>
                                        </thead>
                                        <tbody id="airline-Con">
                                            <tr>
                                                <td class="formValue">
                                                    <input id="AirlineFlyHeight_row1" name="AirlineFlyHeight_row1" type="text" maxlength="20" style="height: 25px;width:80px"/></td>
                                                <td class="formValue">
                                                    <input id="AirlineName_row1_col1" name="AirlineName_row1_col1" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                <td class="formValue">
                                                    <input id="AirlineLatLong_row1_col1" name="AirlineLatLong_row1_col1" type="text" maxlength="50" style="height: 25px;width:200px"/></td>
                                                <td class="formValue">
                                                    <input id="AirlineName_row1_col2" name="AirlineName_row1_col2" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                <td class="formValue">
                                                    <input id="AirlineLatLong_row1_col2" name="AirlineLatLong_row1_col2" type="text" maxlength="50" style="height: 25px;width:200px"/></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a id="airline-Add" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增行</a>
                                    <a id="airline-Minus" class="easyui-linkbutton" style="margin-top: 20px;">删除行</a>
                                    <a id="airline-AddCol" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增列</a>
                                    <a id="airline-MinusCol" class="easyui-linkbutton" style="margin-top: 20px;">删除列</a>
                                </td>
                            </tr>
                            <tr style="padding: 2px;">
                                <th  style="padding-top: 5px;">作业区
                                </th>
                                <td colspan="3">
                                    <ul class="nav nav-tabs" style="padding-top: 10px;">
                                        <li class="active"><a href="javascript:void(0)">作业区(圆)</a></li>
                                        <li><a href="javascript:void(0)">作业区(点连线)</a></li>
                                        <li><a href="javascript:void(0)">作业区(航线左右)</a></li>
                                    </ul>
                                    <div id="con">
                                        <div style="padding-top: 20px; margin-right: 30px; display: block">
                                            <table id="cwork-List" class="table table-bordered" style="width: 100%; float: left">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center">飞行高度</th>
                                                        <th style="text-align: center">半径（公里）</th>
                                                        <th style="text-align: center">点中文名1</th>
                                                        <th style="text-align: center">经纬度1</th>

                                                    </tr>
                                                </thead>
                                                <tbody id="cwork-Con">
                                                    <tr>
                                                        <td class="formValue">
                                                            <input id="CFlyHeight1" name="CFlyHeight1" type="text" maxlength="50" style="height: 25px;width:80px"/></td>
                                                        <td class="formValue">
                                                            <input id="CRadius1" name="CRadius1" type="text" maxlength="50" style="height: 25px;width:80px"/></td>
                                                        <td class="formValue">
                                                            <input id="CWorkName1" name="CWorkName1" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                        <td class="formValue">
                                                            <input id="CLatLong1" name="CLatLong1" type="text" maxlength="50" style="height: 25px;width:200px"/></td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                            <a id="cwork-Add" class="easyui-linkbutton" style="margin-top: 40px; margin-left: 5px;">新增行</a>
                                            <a id="cwork-Minus" class="easyui-linkbutton" style="margin-top: 40px;">删除行</a>
                                        </div>
                                        <div style="padding-top: 20px; margin-right: 30px; display: none">
                                            <table id="pwork-List" class="table table-bordered" style="width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center">飞行高度</th>
                                                        <th style="text-align: center">点中文名1</th>
                                                        <th style="text-align: center">经纬度1</th>
                                                        <th style="text-align: center">点中文名2</th>
                                                        <th style="text-align: center">经纬度2</th>

                                                    </tr>
                                                </thead>
                                                <tbody id="pwork-Con">
                                                    <tr>
                                                        <td class="formValue">
                                                            <input id="PFlyHeight_row1" name="PFlyHeight_row1" type="text" maxlength="20" style="height: 25px;width:80px"/></td>
                                                        <td class="formValue">
                                                            <input id="PWorkName_row1_col1" name="PWorkName_row1_col1" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                        <td class="formValue">
                                                            <input id="PWorkLatLong_row1_col1" name="PWorkLatLong_row1_col1" type="text" maxlength="50" style="height: 25px;width:200px"/></td>
                                                        <td class="formValue">
                                                            <input id="PWorkName_row1_col2" name="PWorkName_row1_col2" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                        <td class="formValue">
                                                            <input id="PWorkLatLong_row1_col2" name="PWorkLatLong_row1_col2" type="text" maxlength="50" style="height: 25px;width:200px"/></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <a id="pwork-Add" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增行</a>
                                            <a id="pwork-Minus" class="easyui-linkbutton" style="margin-top: 20px;">删除行</a>
                                            <a id="pwork-AddCol" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增列</a>
                                            <a id="pwork-MinusCol" class="easyui-linkbutton" style="margin-top: 20px;">删除列</a>
                                        </div>
                                        <div style="padding-top: 20px; margin-right: 30px; display: none">
                                            <table id="hwork-List" class="table table-bordered" style="width: 100%;">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center">飞行高度</th>
                                                        <th style="text-align: center">距离</th>
                                                        <th style="text-align: center">点中文名1</th>
                                                        <th style="text-align: center">经纬度1</th>
                                                        <th style="text-align: center">点中文名2</th>
                                                        <th style="text-align: center">经纬度2</th>

                                                    </tr>
                                                </thead>
                                                <tbody id="hwork-Con">
                                                    <tr>
                                                        <td class="formValue">
                                                            <input id="HFlyHeight_row1" name="HFlyHeight_row1" type="text" maxlength="20" style="height: 25px;width:80px"/></td>
                                                        <td class="formValue">
                                                            <input id="HDistance_row1" name="HDistance_row1" type="text" maxlength="20" style="height: 25px;width:80px"/></td>
                                                        <td class="formValue">
                                                            <input id="HWorkName_row1_col1" name="HWorkName_row1_col1" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                        <td class="formValue">
                                                            <input id="HWorkLatLong_row1_col1" name="HWorkLatLong_row1_col1" type="text" maxlength="50" style="height: 25px;width:200px"/></td>
                                                        <td class="formValue">
                                                            <input id="HWorkName_row1_col2" name="HWorkName_row1_col2" type="text" maxlength="50" style="height: 25px;width:100px"/></td>
                                                        <td class="formValue">
                                                            <input id="HWorkLatLong_row1_col2" name="HWorkLatLong_row1_col2" type="text" maxlength="50" style="height: 25px;width:200px" /></td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                            <a id="hwork-Add" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增行</a>
                                            <a id="hwork-Minus" class="easyui-linkbutton" style="margin-top: 20px;">删除行</a>
                                            <a id="hwork-AddCol" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增列</a>
                                            <a id="hwork-MinusCol" class="easyui-linkbutton" style="margin-top: 20px;">删除列</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="edit-buttons" id="wizard-actions" style="padding:2px;float:right">
            <a id="btn_last" disabled class="btn-prev easyui-linkbutton" >上一步</a>
            <a id="btn_next" class="btn-next easyui-linkbutton">下一步</a>
            <a id="btn_finish" class="easyui-linkbutton" style="display: none;" onclick="submitForm()">完成</a>
        </div>
    </div>
</form>
