
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>通航服务站</title>
   <script src="<%=Page.ResolveUrl("~/Content/JS/easyUI/jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/base.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/Css/login.css")%>" rel="stylesheet" type="text/css" />         
    <script src="<%=Page.ResolveUrl("~/Content/JS/Des.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        document.onkeydown = function (e) {
            var event = e || window.event;
            var code = event.keyCode || event.which || event.charCode;
            if (code == 13) {
                Main.login();
            }
        }
        $(function () {
            Main.clearData();
            $("input[name='txtUserName']").focus();
            $("#txtUserName", "#txtPassword").keypress(function () { Main.hideErr(); });

        });
        Main = {
            login: function () 
            {
                alert($("#txtUserName").val());
                if ($("input[name='txtUserName']").val().trim() == "" || $("input[name='txtPassword']").val().trim() == "") {
                    $("#showMsg").html("用户名或密码不能为空！");
                  //  $("input[name='txtUserName']").focus();

                } else {
                    
                    var userName = $("input[name='txtUserName']");
                    var password = $("input[name='txtPassword']");
                    var str = encMe(password.val().trim(), userName.val().trim());
                    $("input[name='htxtPassword']").val(str);
                    $("input[name='action']").val('submit');
                    $("#showMsg").html("登录中...");
                    $.ajax({
                        type: "POST",
                        url: location.href,
                        data: $("#loginForm").serialize(),
                        error: function (request) {
                            $("#showMsg").html(request);
                        },
                        success: function (data) {
                            if (data.isSuccess)
                                document.location = "index.aspx";
                            else {
                                $("#showMsg").html(data.msg);
                            }
                        }
                    });
                }
            },
            clearData: function () {
                $("input[name='txtUserName']").val('');
                $("input[name='txtPassword']").val('');
                $("input[name='htxtPassword']").val('');
            },
            hideErr: function () {
                $("#showMsg").html('');
            }
        };
    </script>
</head>
<body>
    <div> <div class="top">
    欢迎登录飞行服务站
    </div></div>
   <div class="Container">
       <form id="loginForm" method="post">
       <div class="login-block-wrapper">
       <input  type="text" id ="txtUserName" name="txtUserName" maxlength="40" placeholder="请输入用户名"/>
         <input  type="password" id ="txtPassword" name="txtPassword" maxlength="40"  placeholder="请输入密码"/>
              <input type="hidden" name="action" />
           <input type="hidden" name="htxtPassword" />
                  <div id="showMsg"></div>
           <div class="forgetPassWrod-block">
               <input type="button" value="登录" onclick="Main.login()"/> 
               <input type="checkbox" id="rememberme" name="rememberme" />&nbsp;记住帐号
            <a href="#">忘记密码？</a>   
           </div>
       </div>
           </form>
   </div>
    <div class="bottom">Copyright © 2017 广州市中南民航空管通信网络科技有限公司.All rights reserved.</div>

</body>
</html>