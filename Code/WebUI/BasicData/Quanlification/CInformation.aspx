<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeFile="CInformation.aspx.cs" Inherits="BasicData_Quanlification_CInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" Runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
     <%--列表 start--%>
   <table id="tab_listci">
        </table>
        <div id="tab_toolbarci" style="padding: 2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="search_ci" menu="#search_menuci"/>
                <div id="search_menu" style="width: 200px">
                    <div name="CompangCode3">
                        公司三字码：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit_cinformation" class="easyui-dialog" title="通航企业信息填写：" style="width: 500px; height: 350px;"
        modal="true" closed="true" buttons="#edit-buttonci">
        <form id="form_editci" name="form_editci" method="post" url="CInformation.aspx">
            <div>
                <table class="table_editci">
                    <tr>
                        <td class="tdal">公司三字码：                                                                           
                        </td>
                        <td class="tdar">
                            <input id="CompanyCode3" name="CompanyCode3" type="text" class="easyui-textbox" 
                                required="true" />
                        </td>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                    </tr>
                    <tr>
                        <td class="tdal">公司二字码：
                        </td>
                        <td class="tdar">box
                            <input id="CompanyCode2" name="CompanyCode2"  class="auto-textbox"
                                required="true" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">公司名称：
                        </td>
                        <td class="tdar">
                            <input id="CompanyName" name="CompanyName" type="text" class="auto-textbox"
                                required="true" />
                        </td>

                    </tr>
                    
                    <tr>   
                        <td class="tdar">英文名称：
                        </td>
                        <td class="tdar">
                             <input id="EnglishName" name="EnglishName" type="text" class="auto-textbox"
                                required="true" />
                        </td>
                    </tr>
                   
                </table>
            </div>
        </form>
    </div>
    <div id="edit-buttonp">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton" onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>
    <%--添加 修改 end--%>
</asp:Content>


