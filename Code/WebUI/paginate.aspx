<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paginate.aspx.cs" Inherits="paginate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/pagination.css" />
    <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="js/jquery.paginate.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="M-box2"  class="M-box2"></div>

    <script type="text/javascript">
        $('#M-box2').pagination({
            coping: true,
            homePage: '首页',
            endPage: '末页',
            prevContent: '上页',
            nextContent: '下页',
            start: 1,
            display: 7,                        
            images: false,
            mouse: 'press',
            onChange: function (page) {
                $('._current', '#paginationdemo').removeClass('_current').hide();
                $('#p' + page).addClass('_current').show();
            },
            callback: function (api) {
                var data = {
                    page: api.getCurrent(),
                    name: 'mss',
                    say: 'oh'
                };
                $.getJSON('http://localhost:3000/data.json', data, function (json) {
                    console.log(json);
                });
            }
        });
    </script>
    </form>
</body>
</html>
