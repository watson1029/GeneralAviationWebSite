<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="css/list.css" />
    <link rel="Stylesheet" type="text/css" href="css/pagination.css" />
    <link rel="Stylesheet" type="text/css" href="css/rcrumbs.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <div class="center">
            <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
            <script type="text/javascript" src="js/jquery.rcrumbs.js"></script>
            <div class="rcrumbs" id="breadcrumbs">
                <ul>
                    <li>当前位置：</li>
                    <li><a href="Default.aspx">首页</a><span class="divider">></span></li>
                    <li><a href="<%=Request.RawUrl %>"><%=title%></a><span class="divider">></span></li>
                </ul>
            </div>
            <script type="text/javascript">
                $("#breadcrumbs").rcrumbs({ windowResize: false });
            </script>

            <div class="list">
                <ul>
                    <%
                        if (listModel == null) return;
                        foreach (var item in listModel)
                        {%>
                    <li>
                        <div class="list_con clearfix">
                            <div class="list_img">
                                <img src="<%=item.ImgPath%>" alt="" />
                            </div>
                            <div class="list_text">
                                <dl>
                                    <dt>
                                        <%if (item.type == "File") {%>
                                        <a href="Handler.ashx?action=download&filepath=<%=item.FilePath %>" target="_blank" class="linktit"><%=item.Title%></a>
                                        <% }
                                        else
                                        { %>
                                        <a target="_self" href="/Detail.aspx?Type=<%=item.type%>&Id=<%=item.Id%>"><%=HtmlWorkShop.CutTitle(item.Title, 20)%></a></dt>
                                        <% } %>
                                    <dd>
                                        <p><%=HtmlWorkShop.CutTitle(HttpUtility.UrlDecode(item.Content),150)%></p>
                                    </dd>
                                    <dd><span>发布时间: <%=item.CreateTime.Value.ToString("yyyy-MM-dd") %></span></dd>
                                </dl>
                            </div>
                        </div>
                    </li>
                    <%  }%>
                </ul>
                <script type="text/javascript" src="js/jquery.paginate.js"></script>
                <div id="M-box2" class="M-box2"></div>
                <script type="text/javascript">
                    $('#M-box2').pagination({
                        coping: true,
                        homePage: '首页',
                        endPage: '末页',
                        prevContent: '上页',
                        nextContent: '下页',
                        start: 1,
                        display: 7,
                        showdata: 6,
                        pageCount: '<%=totalPage%>',
                        current: '<%=pageIndex%>',
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
                            window.location = "List.aspx?Type=<%=type%>&PageIndex=" + data.page;
                        }
                    });
                </script>
            </div>
            <script type="text/javascript">
                //console.log($.fn.jquery);//输出jquery版本号，为1.10.2        
                jQuery.noConflict(true);//释放 $ 标识符的控制
                //console.log($.fn.jquery);//输出jquery版本号，为1.4.1
            </script>
        </div>
    </div>
</asp:Content>

