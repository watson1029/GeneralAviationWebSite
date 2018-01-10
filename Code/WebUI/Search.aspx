<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" type="text/css" href="css/list.css" />
    <link rel="Stylesheet" type="text/css" href="css/pagination.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <div class="center">
            <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
            <script type="text/javascript" src="js/jquery.paginate.js"></script>
            <div class="list">
                <ul>
                    <%
                        if (listModel != null && listModel.Count()>0)
                        { 
                        foreach (var item in listModel)
                        {%>
                    <li>
                        <div class="list_con clearfix">
                            <div class="list_img">
                                <img src="<%=item.ImgPath%>" alt="" />
                            </div>
                            <div class="list_text">
                                <dl>
                                    <dt><a href="/Detail.aspx?Type=<%=item.type%>&Id=<%=item.Id%>"><%=HtmlWorkShop.GetCutDes(item.Title,50,HttpUtility.UrlDecode(content), "red")%></a></dt>
                                    <dd>
                                        <p><%=HtmlWorkShop.GetCutDes(HttpUtility.UrlDecode(item.Content),150,HttpUtility.UrlDecode(content),"red")%></p>
                                    </dd>
                                    <dd><span>发布时间: <%=item.CreateTime.Value.ToString("yyyy-MM-dd") %></span></dd>
                                </dl>
                            </div>
                        </div>
                    </li>
                    <%  }
                        }
                        else
                        {%>
                    <li>  <div class="list_con clearfix" style="text-align:center;color:red">暂无该记录！ </div>  </li>
                   <%  }%>
                </ul>
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
            <script type="text/ecmascript">
                //console.log($.fn.jquery);//输出jquery版本号，为1.10.2        
                jQuery.noConflict(true);//释放 $ 标识符的控制
                //console.log($.fn.jquery);//输出jquery版本号，为1.4.1
            </script>
        </div>
    </div>
</asp:Content>

