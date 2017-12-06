<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    通航服务站
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="<%=Page.ResolveUrl("~/")%>Content/JS/Des.js" type="text/javascript"></script>
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
            login: function () {
                if ($("input[name='txtUserName']").val().trim() == "" || $("input[name='txtPassword']").val().trim() == "") {
                    $("#showMsg").html("用户名或密码不能为空！");
                    $("input[name='txtUserName']").focus();

                } else {
                    var userName = $("input[name='txtUserName']");
                    var password = $("input[name='txtPassword']");
                    var str = encMe(password.val().trim(), userName.val().trim());
                    $("input[name='htxtPassword']").val(str);
                    $("input[name='action']").val('submit');
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
                $('#loginForm').form('clear');
            },
            hideErr: function () {
                $("#showMsg").text('');
            }


        };
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="loginWin" class="easyui-window" title="登录" style="width: 350px; height: 188px; padding: 5px;"
        minimizable="false" maximizable="false" resizable="false" collapsible="false">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 5px; background: #fff; border: 1px solid #ccc;">
                <form id="loginForm" method="post">
                    <input type="hidden" name="action" value="" />
                    <input type="hidden" name="htxtPassword" />
                    <div style="padding: 5px 0;">
                        <label for="login">用户名:</label>
                        <input type="text" name="txtUserName" maxlength="20" style="width: 260px;"  />
                    </div>
                    <div style="padding: 5px 0;">
                        <label for="password">密&nbsp;&nbsp;&nbsp;码:</label>
                        <input type="password" name="txtPassword" maxlength="20"  style="width: 260px;"  />
                    </div>
                    <div style="padding: 5px 0; text-align: center; color: red;" id="showMsg"></div>
                </form>
            </div>
            <div region="south" border="false" style="text-align: right; padding: 5px 0;">
                <a class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="Main.login()">登录</a>
            </div>
        </div>
    </div>

</asp:Content>
