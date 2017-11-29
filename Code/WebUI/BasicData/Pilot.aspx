<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="Pilot.aspx.cs" Inherits="BasicData_Pilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%--列表 start--%>
    <table id="tab_listp">
        </table>
        <div id="tab_toolbarp" style="padding: 2px 2px;">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
            <div style="float:right">
                <input id="search_p" menu="#search_menup"/>
                <div id="search_menu" style="width: 200px">
                    <div name="Pilots">
                        飞行员姓名：
                    </div>
                </div>
            </div>
        </div>
    <%--列表 end--%>
    <%--添加 修改 start--%>
    <div id="edit_pilot" class="easyui-dialog" title="新增飞行员" style="width: 500px; height: 350px;"
        modal="true" closed="true" buttons="#edit-buttonp">
        <form id="form_editp" name="form_editp" method="post" url="Pilot.aspx">
            <div>
                <table class="table_editp">
                    <tr>
                        <td class="tdal">飞行员：                                                                           
                        </td>
                        <td class="tdar">
                            <input id="Pilots" name="Pilots" type="text" class="easyui-textbox" 
                                required="true" />
                        </td>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                    </tr>
                    <tr>
                        <td class="tdal">身份证号：
                        </td>
                        <td class="tdar">
                            <input id="PilotCardNo" name="PilotCardNo"  class="auto-datatimebox"
                                required="true" />
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">出生日期：
                        </td>
                        <td class="tdar">
                            <input id="PilotDT" name="PilotDT" type="text" class="auto-validatebox"
                                required="true" />
                        </td>

                    </tr>
                    
                    <tr>   
                        <td class="tdar">联系电话：
                        </td>
                        <td class="tdar">
                            <input id="PhoneNo" name="PhoneNo"  class="easyui-textbox"
                                required="true" />
                        </td>

                    </tr>
                    <tr>   
                        <td class="tdar">执照编号：
                        </td>
                        <td class="tdar">
                        <input id="LicenseNo" name="LicenseNo" type="text" class="easyui-textbox" 
                                required="true" />
                        </td>
                    </tr>
                    <tr>   
                        <td class="tdar">签发单位：
                        </td>
                        <td class="tdar">
                        <input id="Sign" name="Sign" type="text" class="easyui-textbox" 
                                required="true" />
                        </td>
                    </tr>
                    <tr>   
                        <td class="tdar">执照类别：
                        </td>
                        <td class="tdar">
                            <select id="Licensesort" class="easyui-combobox" name="Licensesort">
                                <option value="0">航线运输驾驶执照</option>
                                <option value="1" selected="selected">商用飞机驾照</option>
                                <option value="2" selected="selected">私用飞机驾照</option>
                            </select>
                        </td>

                    </tr>
                    
                    <tr>   
                        <td class="tdar">所属企业：
                        </td>
                        <td class="tdar">
                            <select id="CompanyName" class="easyui-combobox" name="CompanyName">
                                <option value="0">通航1</option>
                                <option value="1" selected="selected">通航2</option>
                                <option value="2" selected="selected">通航3</option>
                                <option value="3" selected="selected">通航4</option>
                            </select>
                        </td>

                    </tr>
                    <tr>   
                        <td class="tdar">性别：
                        </td>
                        <td class="tdar">
                            <select id="Sex" class="easyui-combobox" name="Sex">
                                <option value="0">男</option>
                                <option value="1" selected="selected">女</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class ="tdar">执照复印件：
                        </td>
                        <td class ="tdal">



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