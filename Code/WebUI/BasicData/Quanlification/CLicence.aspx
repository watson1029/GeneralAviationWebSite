<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="CLicence.aspx.cs" Inherits="BasicData_Quanlification_CLicence" %>

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
                <input id="search_cl" menu="#search_munecl" />
                <div id="search_clmune" style="width:250px">
                    <div name="CompanyCode3">
                        公司三字码：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>

    <%--添加 修改--%>
    <div id="edit_cl" class="easyui-dialog" style="width:1000px; height:300px" modal="ture" closed="ture" buttons="#edit_cl">
        <form id="form_editcl" method="post">
            <table class="table_editcl">
                <tr>
                    <td class="tdal">
                        公司三字码：
                    </td>
                    <td class="tdar">
                        <input id="CompanyCode3" name="CompanyCode3" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        法定代表人：
                    </td>
                    <td class="tdar">
                        <input id="LegalPerson" name="LegalPerson" type="text" maxlength="20" class="easyui-textbox" required="ture" />
                    </td>
                </tr>
                <tr>
                    <td class="tadl">
                        许可证编号：
                    </td>
                    <td class="tdar">
                        <input id="Licence" name="Licence" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        经营项目与范围：
                    </td>
                    <td class="tdar">
                        <input id="Project" name="Project" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                    </td>
                </tr>
                <tr>
                    <td class="tdal">
                        基地机场：
                    </td>
                    <td class="tdar">
                        <input id="BaseAirport" name="BaseAirport" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        有效期限：
                    </td>
                    <td class="tdar">
                        <input id="EffectiveData" name="EffectiveData" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                </tr>
                <tr >
                    <td class="tdal">
                        企业类别：
                    </td>
                    <td class="tdar">
                        <input id="CompanyType" name="CompanyType" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        颁发日期：
                    </td>
                    <td class="tdar">
                        <input id="LssueData" name="LssueData" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                </tr>
                <tr>
                    <td class="tdal">
                        注册资本：
                    </td>
                    <td class="tdar">
                        <input id="Capital" name="Capital" type="text" maxlength="30" class="esayui-textbox" required="ture" />
                    </td>
                    <td class="tdal">
                        许可机关印章：
                    </td>
                    <td class="tdar">

                    </td>
                </tr>
                <tr>
                    <td class="tdal">
                        <p>购置航空器的</p>
                        <p>自有资金额度：</p>
                    </td>
                    <td class="tdar">
                        <input id="Quota" name="Quota" type="text" maxlength="30" class="easyui-textbox" required="ture" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="edit_cl">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存 </a> 
        <a href="javascript:;" class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>

</asp:Content>


