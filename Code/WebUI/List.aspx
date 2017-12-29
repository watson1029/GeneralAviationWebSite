<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="copyright" content="广州市中南民航空管通信网络科技有限公司" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>河南通航飞行服务站</title>
    <link href="css/base.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery1.js"></script>
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/common.js"></script>
    <link rel="Stylesheet" type="text/css" href="css/pagination.css" />
    <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="js/jquery.paginate.js"></script>
    <script type="text/javascript">
        if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
            alert("温馨提示:您当前使用的浏览器为ie6内核,可能会影响您浏览此网站,建议使用ie7以上版本浏览器!");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--head begin-->
        <div class="head">
            <div class="center" style="position: relative">
                <div class="logo">
                    <img src="images/logo.png" alt="" /></div>
                <div class="searchboxdiv">
                    <input type="text" name="search" id="txt_request" class="searchbox ipt" value="请输入搜索关键词" onblur="setkeyword('txt_request')"
                        onkeydown="if(event.keyCode==13){document.getElementById('ibtn_txt_request').focus();}" />
                    <span class="btn_search">
                        <button id="ibtn_txt_request" type="submit" class="btn" onclick="subjs('gc');return false">搜&nbsp;索</button>
                    </span>
                </div>
                <div class="nav-box">
                    <ul>
                        <li class="cur"><a href="/default.aspx" target="_blank">首页<span>Home</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=bz" target="_blank">新闻<span>News</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=ts" target="_blank">供求<span>Supply-Demand</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=qk" target="_blank">企业<span>Company</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=qk" target="_blank">资料<span>File</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=tc" target="_blank">计划<span>Plan</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=tc" target="_blank">气象<span>Weather</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=ww" target="_blank">情报<span>Information</span></a></li>
                        <li><a href="/TempletPage/Page.aspx?column=st" target="_blank">监视<span>Surveillance</span></a></li>
                    </ul>
                    <div class="nav-line" style="left: 0px; width: 128px; display: block;"></div>
                </div>
            </div>
        </div>
        <!--head end-->

        <!--主体begin-->
        <div class="main">
            <div class="center">
                    <div class="list">
                        <ul>
                             <%
                                 foreach(var item in listModel)
	                        {%>
		                        <li>
                                    <div class="list_con clearfix">
                                        <div class="list_img">
                                            <img src="images/News.jpg" alt=""/>
                                        </div>
                                        <div class="list_text">
                                            <dl>
                                                <dt><a href="/Detail.aspx?Type=<%=item.Type%>&Id=<%=item.Id%>"><%=HtmlWorkShop.CutTitle(item.Title,20)%></a></dt>
                                                <dd class="ddtxt">
                                                    <p><%=HtmlWorkShop.CutTitle(HttpUtility.UrlDecode(item.Content),20)%></p>
                                                </dd>
                                                <dd class="ddclock"><span>发布时间: <%=item.CreateTime %></span></dd>
                                            </dl>
                                        </div>
                                    </div>
                                </li>
	                        <%  }%>
                        </ul>                       

                        <%--<div class="list_pager">
                            <div id="new_page_list" class="new_page_list">
                                <a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=1" class="current" title="第1页" data-page="1"><span>1 </span></a><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=2" title="第2页" data-page="2"><span>2 </span></a><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=3" title="第3页" data-page="3"><span>3 </span></a><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=4" title="第4页" data-page="4"><span>4 </span></a><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=5" title="第5页" data-page="5"><span>5 </span></a><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=6" title="第6页" data-page="6"><span>6 </span></a><span class="more"><span>... </span></span><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=156" title="第156页" data-page="156"><span>156 </span></a><a href="http://www.zslib.com.cn/TempletPage/List.aspx?dbid=1&amp;page=2" class="next_btn" title="下一页" data-page="5"><span>下一页 <i class="bg_png"></i></span></a>
                                <a href="javascript:;" class="pagenum">
                                    <div class="pg_search">
                                        <span style="float: left">
                                            <input name="inputPageNum" id="inputPageNum" class="pg_searchbox ipt" value="输入页数" style="color: rgb(173, 173, 173);" type="text"></span>
                                        <span class="pg_btn_search">
                                            <input name="turnPage" value="GO" id="turnPage" class="pg_btn" type="submit"></span>
                                    </div>
                                </a>
                            </div>
                        </div>--%>
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
                                showdata: 6,
                                pageCount:<%=TotalPage%>,
                                current:<%=PageIndex%>,
                                images: false,
                                mouse: 'press',
                                onChange: function (page) {
                                    //$('._current', '#paginationdemo').removeClass('_current').hide();

                                    //$('#p' + page).addClass('_current').show();
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
                                    window.location = "List.aspx?Type=<%=Type%>&PageIndex="+data.page;
                                }
                            });
                        </script>
                        <!-- prev next-->
                    </div>
             </div>
        </div>
    </form>
</body>
</html>
