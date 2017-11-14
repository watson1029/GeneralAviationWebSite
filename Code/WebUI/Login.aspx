<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>通航服务站</title>
    <link href="css/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
        <div id="top">
            <div id="top_one">
            </div>
            <div id="top_two">
            </div>
            <div id="top_three">
            </div>
            <div id="top_four">
            </div>
        </div>
        <div id="bottom" style="">
            <div id="bottom_one">
            </div>
            <div id="bottom_two">
            </div>
            <div id="bottom_three">
                <table class="Logintable">
                    <tr>
                        <td>
                            <span class="loginspan">用户名:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" Width="148px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="loginspan">密&nbsp; 码:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="148px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="IBt_Submit" ImageUrl="~/images/login/1_11.gif" runat="server"
                                OnClick="IBt_Submit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="botton_four">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
