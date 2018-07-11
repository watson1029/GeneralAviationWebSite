<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Weather04.aspx.cs" Inherits="Weather_Weather04" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script type="text/javascript">
        setTimeout(function () {
            window.location = window.location;
        }, <%=interval%>);
    </script>
</body>
</html>
