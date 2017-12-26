﻿<%@ Page  Language="C#"  AutoEventWireup="true" CodeFile="PilotAdd.aspx.cs" Inherits="BasicData_PilotAdd" %>
<form id="form_edit" method="post">
        <table class="table_edit">
            <tr><th>公司名称</th>

                         <td>
                                           <input id="CompanyCode3" name="CompanyCode3" data-options="url:'/FlightPlan/GetComboboxData.ashx?type=3',method:'get',valueField:'id',textField:'text',panelHeight:'auto'
                                ,panelMaxHeight:200"
                        required="true" class="easyui-combobox" style="height:25px"/>

                </td>
            </tr>
            <tr>
                <th>飞行员姓名：</th>
                <td>
                    
                 <input id="Pilots" name="Pilots" maxlength="10" class="easyui-validatebox textbox" data-options="required:true" style="height:20px"/>
                </td>
               <th>身份证号：</th>
                <td>
                    <input id="PilotCardNo" name="PilotCardNo" class="easyui-validatebox textbox" maxlength="18"  data-options="required:true" style="height:20px" />
                </td>
            </tr>
            <tr>
                <th>出生日期：</th>
                <td>
                    <input id="PilotDT" name="PilotDT" type="text" class="easyui-datebox"  data-options="required:true" style="height:25px"/>
                </td>
                <th>年龄：</th>
                <td>
                    <input id="PilotAge" name="PilotAge" class="easyui-numberbox" data-options="min:1,max:100,precision:0,required:true,validType:'length[1,3]'" style="height:25px" />
                </td>
            </tr>
            <tr>
                <th>联系电话：</th>
                <td>
                     <input id="PhoneNo" name="PhoneNo" class="easyui-numberbox" data-options="required:true,validType:'length[1,11]'" style="height:25px" validType='mobile'/>
                </td>
                <th>执照编号：</th>
                <td>
                    <input id="LicenseNo" name="LicenseNo" type="text" class="easyui-validatebox textbox" maxlength="30" data-options="required:true" style="height:20px" />
                </td>
            </tr>
            <tr>
                <th>签发单位：</th>
                <td>
                    <input id="Sign" name="Sign" type="text" class="easyui-validatebox textbox" maxlength="30" data-options="required:true"  style="height:20px"/>
                </td>
                <th>签发日期：</th>
                <td>
                    <input id="LicenseTime" name="LicenseTime" type="text" class="easyui-datebox" data-options="required:true" style="height:25px" />
                </td>
            </tr>
            <tr>
                <th>执照类别：</th>
                <td>
                    <select id="Licensesort" class="easyui-combobox"  name="Licensesort" data-options="panelHeight:'auto',required:true" style="height:25px" >
                        <option value="0" selected="selected">航线运输驾驶执照</option>
                        <option value="1" >商用飞机驾照</option>
                        <option value="2" >私用飞机驾照</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>性别：</th>
                <td >
                    <select id="Sex" class="easyui-combobox"  name="Sex" data-options="panelHeight:'auto',required:true" style="height:25px">
                        <option value="0" selected="selected">男</option>
                        <option value="1" >女</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>执照复印件：</th>
                <td>
                    <input type="hidden" name="LicenseImgInfo" id="LicenseImgInfo"  />
                    <input type="file" id="LicenseImg" name="LicenseImg" />
                    <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" onclick="dj.getCmp('LicenseImg').uploadFiles()">上传</a>
                    <div id="LicenseImg-fileQueue"></div>
                    <div id="LicenseImg-fileList" style="margin-top: 2px; zoom: 1"></div>
                </td>
            </tr> 
        </table>
</form>

<style type="text/css">
    .table_edit tr#WriteCompany td{
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
                $.post("PilotAdd.aspx", { "action": "queryone", "id": pid }, function (data) {
                    $("#form_edit").form('load', data);

                    new dj.upload({
                        id: "LicenseImg",
                        maxSize: 5,
                        multi: true,
                        queueId: "LicenseImg-fileQueue",
                        listId: "LicenseImg-fileList",
                        truncate: "18",
                        maxCount: "1",
                        uploadPath: "Files/LicenseImg/",
                        uploadedFiles: ""
                    });
                });
            }
            else {
                new dj.upload({
                    id: "LicenseImg",
                    maxSize: 5,
                    multi: true,
                    queueId: "LicenseImg-fileQueue",
                    listId: "LicenseImg-fileList",
                    truncate: "18",
                    maxCount: "1",
                    uploadPath: "Files/LicenseImg/",

                });
            }
        });


    </script>



