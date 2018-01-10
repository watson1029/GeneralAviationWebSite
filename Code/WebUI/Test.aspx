<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/rcrumbs.css" />
    <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="js/jquery.rcrumbs.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="rcrumbs" id="breadcrumbs">
                <ul>
                    <li><a href="javascript;">当前位置</a><span class="divider">：</span></li>
                    <li><a href="javascript;">首页</a><span class="divider">></span></li>
                    <li><a href="javascript;">列表页</a><span class="divider">></span></li>
                    <li><a href="javascript;">列表页</a><span class="divider">></span></li>
                    <li><a href="javascript;">列表页</a><span class="divider">></span></li>
                    <li><a href="javascript;">列表页</a><span class="divider">></span></li>
                    <li><a href="javascript;">列表页</a><span class="divider">></span></li>
                </ul>
            </div>
            <script type="text/javascript">
                $("#breadcrumbs").rcrumbs();
            </script>
            <asp:TextBox ID="TextBox1" runat="server" Width="30%"></asp:TextBox>
        </div>
    </form>
</body>
</html>
