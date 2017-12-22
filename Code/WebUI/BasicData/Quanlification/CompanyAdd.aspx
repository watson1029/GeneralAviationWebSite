<%@ Page  Language="C#" AutoEventWireup="true" CodeFile="CompanyAdd.aspx.cs" Inherits="BasicData_Quanlification_CompanyAdd" %>
<form id="form_edit"  method="post">
<div class="easyui-tabs">   
    <div title="通航企业信息填写" style="padding:10px"> 
            <table class="table_edit">
                <tr>
                    <th>公司三字码:</th>
                    <td>
                          <input id="CompanyCode3" name="CompanyCode3" type="text" class="easyui-validatebox textbox" maxlength="3" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>公司二字码：</th>
                    <td>
                        <input id="CompanyCode2" name="CompanyCode2" type="text"  class="easyui-validatebox textbox" maxlength="2" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>公司名称:</th>
                    <td>
                          <input id="CompanyName" name="CompanyName" type="text" class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>英文名称：</th>
                    <td>
                          <input id="EnglishName" name="EnglishName" type="text" class="easyui-validatebox textbox" maxlength="50" />
                    </td>
                </tr>                
            </table>
    </div>
    <div title="通航企业工商营业执照信息填写" style="padding:10px">
    
            <table class="table_edit">
                <tr>
                    <th>注册时间：</th>
                    <td>
                        <input id="RegisterTime" name="RegisterTime" type="text" maxlength="30" class="easyui-datebox" data-options="required:true" />
                    </td> 
                    <th>注册地址：</th>
                    <td>
                        <input id="RegisterAddress" name="RegisterAddress" type="text" class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>注册资金：（万）</th>
                    <td>
                        <input id="RegisteredCapital" name="RegisteredCapital" type="text"  class="easyui-numberbox" data-options="min:1,max:1000000000,precision:1,required:true,validType:'length[1,10]'" />
                    </td>
                    <th>有效期限：</th>
                    <td>
                        <input id="Dealline" name="Dealline" type="text"  class="easyui-validatebox textbox"  maxlength="10" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>法人姓名：</th>
                    <td>
                        <input id="LegalPerson" name="LegalPerson" type="text" class="easyui-validatebox textbox" maxlength="10" data-options="required:true" />
                    </td>
                    <th>指定联系人：</th>
                    <td>
                        <input id="ContactPerson" name="ContactPerson" type="text"  class="easyui-validatebox textbox" maxlength="10" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>法人身份证号：</th>
                    <td>
                        <input id="LegalCardNo" name="LegalCardNo" type="text" class="easyui-validatebox textbox" maxlength="18" data-options="required:true" />
                    </td>
                    <th>法人身份证地址：</th>
                    <td>
                        <input id="LegalAddress" name="LegalAddress" type="text" class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>法人有效联系电话：</th>
                    <td>
                        <input id="LegalTelePhone" name="LegalTelePhone" type="text"  class="easyui-numberbox" data-options="precision:0,required:true,validType:'length[1,11]'" />
                    </td>
                    <th>法人委托人：</th>
                    <td>
                        <input id="LegalClientele" name="LegalClientele" type="text"  class="easyui-validatebox textbox" maxlength="10" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>委托人身份证号：</th>
                    <td>
                        <input id="DelegateCardNo" name="DelegateCardNo" type="text"  class="easyui-validatebox textbox" maxlength="18" data-options="required:true" />
                    </td>
                    <th>委托人身份证地址：</th>
                    <td>
                        <input id="DelegateAddress" name="DelegateAddress" type="text"  class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>委托人有效联系电话：</th>
                    <td>
                        <input id="DelegateTelePhone" name="DelegateTelePhone" type="text"  class="easyui-numberbox" data-options="precision:0,required:true,validType:'length[1,11]'" />
                    </td>
                </tr>
                <tr>
                    <th>法人身份证复印件：</th>
                    <td colspan="3">
                        <input type="hidden" name="LegalCardImgInfo" id="LegalCardImgInfo"/>
                        <input type="file" id="LegalCardImg" name="LegalCardImg" />
                        <a  href="javascript:;" class="easyui-linkbutton" onclick="dj.getCmp('LegalCardImg').uploadFiles()">上传</a>
                        <div id="LegalCardImg-fileQueue"></div>
                        <div id="LegalCardImg-fileList" style="margin-top: 2px; zoom: 1"></div>
                    </td>
                </tr>
                <tr>
                    <th> 法人委托书原件：</th>
                    <td colspan="3">
                        <input type="hidden" name="LegalDelegateImgInfo" id="LegalDelegateImgInfo" />
                        <input type="file" id="LegalDelegateImg" name="LegalDelegateImg" />
                        <a href="javascript:;" class="easyui-linkbutton" onclick="dj.getCmp('LegalDelegateImg').uploadFiles()">上传</a>
                        <div id="LegalDelegateImg-fileQueue"></div>
                        <div id="LegalDelegateImg-fileList" style="margin-top: 2px; zoom: 1"></div>
                    </td>
                </tr>
                <tr>
                    <th>委托人身份证复印件：</th>
                    <td colspan="3">
                        <input type="hidden" name="DelegateCardImgInfo" id="DelegateCardImgInfo" />
                        <input type="file" id="DelegateCardImg" name="DelegateCardImg" />
                        <a  href="javascript:;" class="easyui-linkbutton" onclick="dj.getCmp('DelegateCardImg').uploadFiles()">上传</a>
                        <div id="DelegateCardImg-fileQueue"></div>
                        <div id="DelegateCardImg-fileList" style="margin-top: 2px; zoom: 1"></div>
                    </td>
                </tr>                
            </table>
     
    </div>
    <div title="通航企业经营许可证信息填写" style="padding:10px">
        
            <table class="table_edit">
                <tr>
                    <th>许可证编号：</th>
                    <td>
                        <input id="LicenseNo" name="LicenseNo" type="text"  class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                    <th>企业名称：</th>
                    <td>
                        <input id="FirmName" name="FirmName" type="text"  class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>企业代码：</th>
                    <td>
                        <input id="FirmCode" name="FirmCode" type="text"  class="easyui-numberbox" data-options="precision:0,required:true,validType:'length[1,30]'" />
                    </td>
                    <th>企业地址：</th>
                    <td>
                        <input id="FirmAddress" name="FirmAddress" type="text"  class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>基地机场：</th>
                    <td>
                        <input id="BaseAirport" name="BaseAirport"  type="text"  class="easyui-validatebox textbox" maxlength="20" data-options="required:true"  />
                    </td>
                    <th>基地机场代码：</th>
                    <td>
                        <input id="BaseAirportCode" name="BaseAirportCode" type="text"  class="easyui-validatebox textbox" maxlength="30" />
                    </td>
                </tr>
                <tr>
                    <th>企业类别：</th>
                    <td>
                        <input id="CompanyType" name="CompanyType" type="text"  class="easyui-validatebox textbox" maxlength="30" data-options="required:true"  />
                    </td>
                    <th>注册资本：</th>
                    <td>
                        <input id="RegisteredFund" name="RegisteredFund" type="text"  class="easyui-numberbox" data-options="min:1,max:1000000000,precision:1,required:true,validType:'length[1,10]'"  />
                    </td>
                </tr>
                <tr>
                    <th>法定代表人：</th>
                    <td>
                        <input id="Legalperson1" name="Legalperson1" type="text"  class="easyui-validatebox textbox" maxlength="10" data-options="required:true"  />
                    </td>
                    <th>经营项目与范围：</th>
                    <td>
                        <input id="ManageItemsScope" name="ManageItemsScope" type="text"  class="easyui-validatebox textbox" maxlength="30"  />
                    </td>
                </tr>
                <tr>
                    <th>有效期限：</th>
                    <td>
                        <input id="DealLine1" name="DealLine1" type="text"  class="easyui-validatebox textbox" maxlength="10" data-options="required:true" />
                    </td>
                    <th>颁发日期：</th>
                    <td>
                        <input id="PresentationDate" name="PresentationDate" type="text"  class="easyui-datebox" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <p>购置航空器的</p>
                        <p>自有资金额度：</p></th>
                    <td>
                        <input id="CapitalLimit" name="CapitalLimit" type="text" class="easyui-numberbox" data-options="min:1,max:1000000000,precision:1,required:true,validType:'length[1,10]'" />
                    </td>
                    <th>许可证颁发机关：</th>
                    <td>
                        <input id="LicensingAuthority" name="LicensingAuthority" type="text" class="easyui-validatebox textbox" maxlength="30" data-options="required:true" />
                    </td>
                </tr>              
                <tr>
                    <th>许可机关印章：</th>
                    <td colspan="3">
                        <input type="hidden" name="LicensedSealInfo" id="LicensedSealInfo"  />
                        <input type="file" id="LicensedSeal" name="LicensedSeal" />
                        <a  id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px"  onclick="dj.getCmp('LicensedSeal').uploadFiles()">上传</a>
                        <div id="LicensedSeal-fileQueue"></div>
                        <div id="LicensedSeal-fileList" style="margin-top: 2px; zoom: 1"></div>
                    </td>
                </tr>
            </table>
      
    </div>
</div>
</form>
      <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var pid = '<%=Request.QueryString["id"] %>';

            if (pid) {
                $.post("CompanyAdd.aspx", { "action": "queryone", "id": pid }, function (data) {
                    $("#form_edit").form('load', data);
                    
                    new dj.upload({
                        id: "LicensedSeal",
                        maxSize: 5,
                        multi: true,
                        queueId: "LicensedSeal-fileQueue",
                        listId: "LicensedSeal-fileList",
                        truncate: "18",
                        maxCount: "1",
                        uploadPath: "Files/LicensedSeal/",
                        uploadedFiles: data.LicensedSeal
                    });

                    //new dj.upload({
                    //    id: "LegalCardImg",
                    //    maxSize: 5,
                    //    multi: true,
                    //    queueId: "LegalCardImg-fileQueue",
                    //    listId: "LegalCardImg-fileList",
                    //    truncate: "18",
                    //    maxCount: "1",
                    //    uploadPath: "Files/LegalCardImg/",
                    //    uploadedFiles: data.LegalCardImg
                    //});
                    //new dj.upload({
                    //    id: "LegalDelegateImg",
                    //    maxSize: 5,
                    //    multi: true,
                    //    queueId: "LegalDelegateImg-fileQueue",
                    //    listId: "LegalDelegateImg-fileList",
                    //    truncate: "18",
                    //    maxCount: "1",
                    //    uploadPath: "Files/LegalDelegateImg/",
                    //    uploadedFiles: data.LegalDelegateImg
                    //});
                    //new dj.upload({
                    //    id: "DelegateCardImg",
                    //    maxSize: 5,
                    //    multi: true,
                    //    queueId: "DelegateCardImg-fileQueue",
                    //    listId: "DelegateCardImg-fileList",
                    //    truncate: "18",
                    //    maxCount: "1",
                    //    uploadPath: "Files/DelegateCardImg/",
                    //    uploadedFiles: data.DelegateCardImg
                    //});
                });
            }

            else {
                debugger;
                new dj.upload({
                    id: "LicensedSeal",
                    maxSize: 5,
                    multi: true,
                    queueId: "LicensedSeal-fileQueue",
                    listId: "LicensedSeal-fileList",
                    truncate: "18",
                    maxCount: "1",
                    uploadPath: "Files/LicensedSeal/",
                    uploadedFiles: ""
                });
                //new dj.upload({
                //    id: "LegalCardImg",
                //    maxSize: 5,
                //    multi: true,
                //    queueId: "LegalCardImg-fileQueue",
                //    listId: "LegalCardImg-fileList",
                //    truncate: "18",
                //    maxCount: "1",
                //    uploadPath: "Files/LegalCardImg/"
                //});
                //new dj.upload({
                //    id: "LegalDelegateImg",
                //    maxSize: 5,
                //    multi: true,
                //    queueId: "LegalDelegateImg-fileQueue",
                //    listId: "LegalDelegateImg-fileList",
                //    truncate: "18",
                //    maxCount: "1",
                //    uploadPath: "Files/LegalDelegateImg/"
                //});
                //new dj.upload({
                //    id: "DelegateCardImg",
                //    maxSize: 5,
                //    multi: true,
                //    queueId: "DelegateCardImg-fileQueue",
                //    listId: "DelegateCardImg-fileList",
                //    truncate: "18",
                //    maxCount: "1",
                //    uploadPath: "Files/DelegateCardImg/"
                //});
            }
        });
    </script>

