<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUnSubmitRepetPlanAdd.aspx.cs" Inherits="FlightPlan_MyUnSubmitRepetPlanAdd" %>

<form id="form_edit" method="post">
    <div class="widget-body">
        <div id="wizard" class="wizard" data-target="#wizard-steps" style="border-left: none; border-top: none; border-right: none;">
            <ul class="steps">
                <li data-target="#step-1" class="active"><span class="step">1</span>文件解析<span class="chevron"></span></li>
                <li data-target="#step-2"><span class="step">2</span>基本信息<span class="chevron"></span></li>
                <li data-target="#step-3"><span class="step">3</span>航线及作业区<span class="chevron"></span></li>
            </ul>
        </div>
        <div class="step-content" id="wizard-steps" style="border-left: none; border-bottom: none; border-right: none;">
            <div class="step-pane active" id="step-1">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">文件解析</h3>
                    </div>
                    <div class="panel-body" style="padding: 15px;width:98%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">文档文本</th>
                                <td class="formValue">
                                    <input type="file" id="PlanFiles" name="PlanFiles" />
                                    <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" value="解析" onclick="dj.getCmp('PlanFiles').uploadFiles()">解析</a>
                                    <div id="PlanFiles-fileQueue"></div>
                                    <div id="PlanFiles-fileList" style="margin-top: 2px; zoom: 1"></div>
                                    <div>
                                        <textarea id="PlanFiles-textArea" name="PlanFiles-textArea" style="height: 475px; width: 998px"></textarea>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="step-pane" id="step-2" style="margin: 10px; margin-bottom: 0px;">
                <div class="alert alert-danger">
                    请填写长期计划申报信息！
                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">基础信息</h3>
                    </div>
                    <div class="panel-body" style="padding: 15px;width: 97%;">
                        <table class="form">
                            <tr>
                                <th class="formTitle">公司名称</th>
                                <td class="formValue" style="color: red" id="name"></td>

                            </tr>
                            <tr>
                                <th class="formTitle">任务类型</th>
                                <td class="formValue">
                                    <input id="FlightType" name="FlightType" data-options="url:'GetComboboxData.ashx?type=1',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                                        required="true" class="easyui-combobox" style="width: 250px" />
                                </td>
                                <th class="formTitle">使用机型</th>
                                <td class="formValue">
                                    <input id="AircraftType" name="AircraftType" maxlength="50" required="true" class="easyui-textbox" style="width: 250px" />
                                </td>

                            </tr>

                            <tr>
                                <th class="formTitle">执行开始日期</th>
                                <td class="formValue">
                                    <input id="StartDate" name="StartDate" editable="false" required="true" class="easyui-datebox" style="width: 250px"  />
                                </td>
                                <th class="formTitle">执行结束日期</th>
                                <td class="formValue">
                                    <input id="EndDate" name="EndDate" editable="false" required="true" class="easyui-datebox" validtype="md['#StartDate']" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">周执行计划</th>
                                <td colspan="3" class="formValue">
                                    <input id="d1" type="checkbox" checked name="WeekSchedule" value="1" style="width: 20px" />星期一
                             <input id="d2" type="checkbox" checked name="WeekSchedule" value="2" style="width: 20px" />星期二
                             <input id="d3" type="checkbox" checked name="WeekSchedule" value="3" style="width: 20px" />星期三
                             <input id="d4" type="checkbox" checked name="WeekSchedule" value="4" style="width: 20px" />星期四
                             <input id="d5" type="checkbox" checked name="WeekSchedule" value="5" style="width: 20px" />星期五
                             <input id="d6" type="checkbox" checked name="WeekSchedule" value="6" style="width: 20px" />星期六
                             <input id="d7" type="checkbox" checked name="WeekSchedule" value="7" style="width: 20px" />星期日
                                </td>
                            </tr>
                            <tr>
                                <th class="formTitle">机场及临时起降点
                                </th>
                                <td colspan="3" class="formValue">
                                    <table id="airport-List" class="table table-bordered" style="width: 90%; float: left">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">机场名称</th>
                                                <th style="text-align: center">四字码</th>
                                                <th style="text-align: center">经纬度</th>
                                            </tr>
                                        </thead>
                                        <tbody id="airport-Con">
                                            <tr>
                                                <td class="formValue">
                                                    <input id="AirportName1" name="AirportName1" required="true"  type="text" maxlength="50" style="height: 25px;width:200px" /></td>
                                                <td class="formValue">
                                                    <input id="CodeF1" name="CodeF1" maxlength="50" type="text" style="height: 25px;width:200px" /></td>
                                                <td class="formValue">
                                                    <input id="LatLong1" name="LatLong1" maxlength="50" type="text" style="height: 25px;width:200px" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a id="airport-Add" class="easyui-linkbutton" style="margin-top: 20px; margin-left: 5px;">新增行</a>
                                    <a id="airport-Minus" class="easyui-linkbutton" style="margin-top: 20px;margin-left: 5px;">删除行</a>
                                </td>

                            </tr>

                            <tr>
                                <th class="formTitle">其他需要说明的事项
                                </th>
                                <td colspan="3" class="formValue">
                                    <input id="Remark" name="Remark" style="width: 900px; height: 230px" type="text" data-options="multiline:true" class="easyui-textbox" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="step-pane" id="step-3">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">其他</h3>
                    </div>
                    <div class="panel-body" style="padding: 15px;width: 97%;">
                        <table class="form">
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
                            <tr style="height: 80px">
                                <th>附件上传</th>
                                <td>
                                      <input type="hidden" name="AttachFilesInfo" id="AttachFilesInfo" />
                    <input type="file" id="AttachFiles" name="AttachFiles" />
                    <a id="btn_upload1" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('AttachFiles').uploadFiles()">上传</a>
                    <div id="AttachFiles-fileQueue"></div>
                    <div id="AttachFiles-fileList" style="margin-top: 2px; zoom: 1"></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

    </div>
</form>   
<style type="text/css">
    .table_edit tr#basicInfo td {
        color: red;
    }
</style>
<script src="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/wizard/wizard.css?v=1.3")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/analysis.js?v=1.1")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/airportTable.js?v=1.1")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/airlineTable.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("~/Content/JS/GA/workTable.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    var obj = {
        id: "AttachFiles",
        maxSize: 5,
        multi: true,
        queueId: "AttachFiles-fileQueue",
        listId: "AttachFiles-fileList",
        truncate: "30",
        maxCount: "1",
        uploadPath: "Files/RepetPlan/",
        uploadedFiles: ""
    };
    var obj1 = {
        id: "PlanFiles",
        maxSize: 5,
        multi: true,
        queueId: "PlanFiles-fileQueue",
        listId: "PlanFiles-fileList",
        txtId: "PlanFiles-textArea",
        truncate: "30",
        maxCount: "1",
        uploadPath: "Files/ImportTemp/",
        uploadedFiles: "",
        fileExt: ".docx;"
    };
    var airportobj = {
        id: "airport",
        addid: "airport-Add",
        minusid: "airport-Minus",
        listid: "airport-List",
        conid: "airport-Con"
    };
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

    repetPlan = {
        newairportobj: null,
        newairlineobj: null,
        newworkobj: null
    };
    $(function () {
        new dj.upload(obj);
        new dj.analysis(obj1);
            $("#name").html('<%=User.CompanyName %>');
          
    });
     function SubmitForm() {
        if ($("#Remark").val().length > 200)
        {
            $.messager.alert('提示', '"其他需要说明的事项"不能超过200字符！', 'info');
            return;
        }
        if (!$("#form_edit").form("validate")) {
            return;
        }
               
        var fileInfo = dj.getCmp("AttachFiles").getUploadedFiles();
        $("#AttachFilesInfo").val(fileInfo);
        qx = $("input[name='WeekSchedule']").map(function () {
            var $this = $(this);
            if ($this.is(':checked')) {
                return $this.val();
            }
            else {
                return '*';
            }
        }).get().join('');
        var postData = $("#form_edit").serialize();

        $("#btn_finish").attr("disabled", "disabled");
        var json = $.param({ "action": "save", "qx": qx, "AirportText": repetPlan.newairportobj.getJsonData(), "AirlineText": repetPlan.newairlineobj.getJsonData(), "CWorkText": repetPlan.newworkobj.getCWorkJsonData(), "PWorkText": repetPlan.newworkobj.getPWorkJsonData(), "HWorkText": repetPlan.newworkobj.getHWorkJsonData() }) + '&' + $('#form_edit').serialize();
        $.ajax({
            type: 'post',
            url: 'MyUnSubmitRepetPlan.aspx',
            data: json,
            success: function (data) {
                $.messager.alert('提示', data.msg, 'info', function () {
                    if (data.isSuccess) {
                        $("#tab_list").datagrid("reload");
                        $("#add").dialog("close");
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
     $(".nav-tabs li").click(function () {
         var i = $(this).index();
         $(this).addClass("active").siblings().removeClass("active");
         $('#con div').eq(i).show().siblings().hide();
     });

        $('#wizard').wizard().on('change', function (e, data) {
            var $finish = $("#btn_finish");
            var $next = $("#btn_next");
            if (data.direction == "next") {
                switch (data.step) {
                    case 1:
                        var fileInfo = dj.getCmp("PlanFiles").getUploadedFiles();                     
                        if (fileInfo != "") {
                            $.ajax({
                                url: dj.root + "FlightPlan/MyUnSubmitRepetPlanAdd.aspx",
                                data: { "action": "analysis" },
                                type: 'post',
                                dataType: "json",
                                async: false,
                                success: function (data) {
                                    $("#form_edit").form('load', data);
                                    $.each("1234567".replace(/\*/g, '').toCharArray(), function (i, n) {
                                        $("#d" + n).prop({ checked: true });
                                    });
                                    $("#name").html('<%=User.CompanyName%>');
                                    airportobj['airportdata'] = data.airportList;
                                    airlineobj['airlinedata'] = data.airlineList;
                                    workobj['cworkdata'] = data.cworkList;
                                    workobj['pworkdata'] = data.pworkList;
                                    workobj['hworkdata'] = data.hworkList;
                                    airlineobj.maxCol = data.airLineMaxCol;
                                    workobj.pworkMaxCol = data.pworkMaxCol;
                                    workobj.hworkMaxCol = data.hworkMaxCol;
                                }
                            });
                        }
                        repetPlan.newairportobj = new dj.dyAirportTable(airportobj);
                        repetPlan.newairportobj.init();
                        repetPlan.newairlineobj = new dj.dyAirlineTable(airlineobj);
                        repetPlan.newairlineobj.init();
                        repetPlan.newworkobj = new dj.dyWorkTable(workobj);
                        repetPlan.newworkobj.init();
                        break;
                    case 2:
                        if (!$("#form_edit").form("validate")) {
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
    

</script> 


                    <div class="form-button" id="wizard-actions" style="padding:2px;float:right">
                <a id="btn_last" href="javascript:void(0);" disabled class="btn-prev easyui-linkbutton">上一步</a>
                <a id="btn_next" href="javascript:void(0);" class="btn-next easyui-linkbutton">下一步</a>
                <a id="btn_finish" class="easyui-linkbutton" style="display: none;" onclick="SubmitForm()">完成</a>                     
        </div>