<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="PilotAdd.aspx.cs" Inherits="BasicData_PilotAdd" %>
<form id="form_edit" method="post">
      <%--<input type="hidden" name="PlanCode" id="PlanCode" value=""/> --%>
        <table class="table_edit">
             <tr id="WriteCompany">
                 <th>填写单位：</th>
                 <td>
                     <%=User.CompanyName %>
                 </td>
             </tr>
            <tr>
                <th>飞行员姓名:</th>
                <td>
                    <input id="Pilots" name="Pilots" type="text" required="true" class="easyui-textbox" />
                </td>
            </tr>
            <tr>
                <th>身份证号：</th>
                <td>
                    <input id="PilotCardNo" name="PilotCardNo" class="easyui-textbox"   required="true" />
                </td>
            </tr>
            <tr>
                <th>出生日期:</th>
                <td>
                    <input id="PilotDT" name="PilotDT" type="text" class="easyui-datebox"   required="true"/>
                </td>
            </tr>
            <tr>
                <th>年龄：</th>
                <td>
                    <input id="PilotAge" name="PilotAge" class="easyui-numberbox"   required="true"/>
                </td>
            </tr>
            <tr>
                <th>联系电话：</th>
                <td>
                     <input id="PhoneNo" name="PhoneNo" class="easyui-numberbox"   required="true"/>
                </td>
            </tr>
            <tr>
                <th>执照编号：</th>
                <td>
                    <input id="LicenseNo" name="LicenseNo" type="text" class="easyui-textbox"   required="true" />
                </td>
            </tr>
             <tr>
                <th>签发单位：</th>
                <td>
                    <input id="Sign" name="Sign" type="text" class="easyui-textbox" required="true" />
                </td>
            </tr>
            <tr>
                <th>签发日期：</th>
                <td>
                    <input id="LicenseTime" name="LicenseTime" type="text" class="easyui-datebox" required="true" />
                </td>
            </tr>
            <tr>
                <th>执照类别：</th>
                <td>
                    <select id="Licensesort" class="easyui-combobox" name="Licensesort" required="true">
                        <option value="0">航线运输驾驶执照</option>
                        <option value="1" selected="selected">商用飞机驾照</option>
                        <option value="2" selected="selected">私用飞机驾照</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>性别：</th>
                <td >
                    <select id="Sex" class="easyui-combobox" name="Sex" required="true">
                        <option value="0">男</option>
                        <option value="1" selected="selected">女</option>
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



                    $(function () {
                        $('#Pilots').textbox('textbox').attr('maxlength', 10);
                    });
                    $(function () {
                        $('#PilotCardNo').textbox('textbox').attr('maxlength', 18);
                    });
                    $(function () {
                        $('#PilotAge').numberbox('textbox').attr('maxlength', 3);
                    });
                    $(function () {
                        $('#PhoneNo').textbox('textbox').attr('maxlength', 11);
                    });
                    $(function () {
                        $('#LicenseNo').textbox('textbox').attr('maxlength', 30);
                    });
                    $(function () {
                        $('#Sign').textbox('textbox').attr('maxlength', 30);
                    });


                    new dj.upload({
                        id: "LicenseImg",
                        maxSize: 5,
                        multi: true,
                        queueId: "LicenseImg-fileQueue",
                        listId: "LicenseImg-fileList",
                        truncate: "18",
                        maxCount: "1",
                        uploadPath: "Files/LicenseImg/"
                    });
                });
            }
        });


    </script>



