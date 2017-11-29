<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CBusiness.aspx.cs" Inherits="BasicData_Quanlification_CBusiness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
     <%--列表 start--%>
        <table id="tab_listcl">
        </table>
        <div id="tab_toolbarcl" style="padding:2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="ture" onclick="Main.OpenWin()">+新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcle="icon-remove" plain="ture" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="search_cl" menu="#search_clmune" />
                <div id="search_clmune" style="width:250px">
                    <div name="CompanyCode3">
                        公司三字码：
                    </div>
                </div>
            </div>
        </div>
   
    <%--列表 end--%>

    <%--添加 修改--%>
    <div id="edit_cb" class="easyui-dialog" style="width:1000px;height:500px" modal="ture" closed="ture" buttons="#edit_cb">
        <form id="form_editcb" method="post">
             <table class="table_editcb">
                 <tr>
                     <td class="tdal">
                         公司三字码：
                     </td>
                     <td class="tdar">
                         <input id="CompanyCode3" name="CompanyCode3" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         法人姓名：
                     </td>
                     <td class="tdar">
                         <input id="LegalName" name="LegalName" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         注册时间：
                     </td>
                     <td class="tdar">
                         <input id="JoinData" name="JoinData" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         法人身份证号：
                     </td>
                     <td class="tdar">
                         <input id="LegalCard" name="LegalCard" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         注册地址：
                     </td>
                     <td class="tdar">
                         <input id="JoinAddress" name="JoinAddress" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         法人身份证地址：
                     </td>
                     <td class="tdar">
                         <input id="CardAddress" name="CardAddress" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         注册资金：
                     </td>
                     <td class="tdar">
                         <input id="Capital" name="Capital" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         法人有效联系电话：
                     </td>
                     <td class="tdar">
                         <input id="LegalPhone" name="LegalPhone" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         有效期限：
                     </td>
                     <td class="tdar">
                         <input id="EffectiveData" name="EffectiveData" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         法人委托人：
                     </td>
                     <td class="tdar">
                         <input id="LagalConsignor" name="LagelConsignor" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         指定联系人：
                     </td>
                     <td class="tdar">
                         <input id="Contacts" name="Contacts" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         委托人身份证地址：
                     </td>
                     <td class="tdar">
                         <input id="ConsignorAddress" name="ConsignorAdress" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         委托人姓名：
                     </td>
                     <td class="tdar">
                         <input id="ConsignorName" name="ConsignorName" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                     <td class ="tdal">
                         法人身份证复印件：
                     </td>
                 </tr>
                 <tr>
                     <td class="tdal">
                         委托人身份证号：
                     </td>
                     <td class="tdar">
                         <input id="ConsignorCard" name="ConsignorCard" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         法人委托书原件：
                     </td>
                 </tr>
                 <tr >
                     <td class="tdal">
                         委托人有效联系电话：
                     </td>
                     <td class="tdar">
                         <input id="ConsignorPhone" name="ConsignorPhone" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                     </td>
                     <td class="tdal">
                         委托人身份证复印件：
                     </td>
                 </tr>
             </table>
        </form>
    </div>

</asp:Content>

