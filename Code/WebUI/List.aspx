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
                        <li class="cur"><a href="/index.aspx" target="_blank">首页<span>Home</span></a></li>
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
                            <li>
                                <div class="list_con clearfix">
                                    <div class="list_img">
                                        <img src="%E5%B9%BF%E4%B8%9C%E7%9C%81%E7%AB%8B%E4%B8%AD%E5%B1%B1%E5%9B%BE%E4%B9%A6%E9%A6%86%E5%88%97%E8%A1%A8%E9%A1%B5_files/201712201001.jpg"></div>
                                    <div class="list_text">
                                        <dl>
                                            <dt><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2886">新书推荐第43期｜艺术与情感</a></dt>
                                            <dd class="ddtxt">
                                                <p>有一个电影导演说过，看一部好的作品，好像剥洋葱的经历，总觉得一层一层剥开，最后会突然有什么意想不到的结局，但是，其实并没有结局，结局也就在一层一层...</p>
                                            </dd>
                                            <dd class="ddclock"><span>发布时间：2017-12-20  </span></dd>
                                        </dl>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="list_con clearfix">
                                    <div class="list_img">
                                        <img src="%E5%B9%BF%E4%B8%9C%E7%9C%81%E7%AB%8B%E4%B8%AD%E5%B1%B1%E5%9B%BE%E4%B9%A6%E9%A6%86%E5%88%97%E8%A1%A8%E9%A1%B5_files/201712202000.jpg"></div>
                                    <div class="list_text">
                                        <dl>
                                            <dt><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2885">微信扫一扫借书，我们是认真的！就怕你不来！</a></dt>
                                            <dd class="ddtxt">
                                                <p>也许你在广州塔一层商业区旁的“F5未来商店”或者广州中华广场已经体验过无人超市，感受了“一机在手，天下我有”全程自助购物的新体验，其实你也可以在广州的这个图书馆得到“一机在手，图书...</p>
                                            </dd>
                                            <dd class="ddclock"><span>发布时间：2017-12-20  </span></dd>
                                        </dl>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="list_con clearfix">
                                    <div class="list_img">
                                        <img src="%E5%B9%BF%E4%B8%9C%E7%9C%81%E7%AB%8B%E4%B8%AD%E5%B1%B1%E5%9B%BE%E4%B9%A6%E9%A6%86%E5%88%97%E8%A1%A8%E9%A1%B5_files/201712191001.jpg"></div>
                                    <div class="list_text">
                                        <dl>
                                            <dt><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2884">“中山讲堂”第182讲  春秋大学问</a></dt>
                                            <dd class="ddtxt">
                                                <p>主 题：新时代社会主义金融文化的构建主 讲：张云东，前深圳证券监管委员会主席。时 间：12月23日（周六）上午10:00-12:00         地 点：文明路总馆 B区一楼 ...</p>
                                            </dd>
                                            <dd class="ddclock"><span>发布时间：2017-12-19  </span></dd>
                                        </dl>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="list_con clearfix">
                                    <div class="list_img">
                                        <img src="%E5%B9%BF%E4%B8%9C%E7%9C%81%E7%AB%8B%E4%B8%AD%E5%B1%B1%E5%9B%BE%E4%B9%A6%E9%A6%86%E5%88%97%E8%A1%A8%E9%A1%B5_files/201712133009.jpg"></div>
                                    <div class="list_text">
                                        <dl>
                                            <dt><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2883">【想读·就读·共读】第32-33期</a></dt>
                                            <dd class="ddtxt">                                                
                                                <p>简介：“想读·就读·共读”是参照我馆闭架图书预约排行榜，统计出我馆读者最想借阅的图书，同时结合当前阅读热点的荐书活动，为读者推荐精选...</p>
                                            </dd>
                                            <dd class="ddclock"><span>发布时间：2017-12-13  </span></dd>
                                        </dl>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="list_con clearfix">
                                    <div class="list_img">
                                        <img src="%E5%B9%BF%E4%B8%9C%E7%9C%81%E7%AB%8B%E4%B8%AD%E5%B1%B1%E5%9B%BE%E4%B9%A6%E9%A6%86%E5%88%97%E8%A1%A8%E9%A1%B5_files/201712121001.jpg"></div>
                                    <div class="list_text">
                                        <dl>
                                            <dt><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2882">第181讲 春秋大学问</a></dt>
                                            <dd class="ddtxt">
                                                <p>主 题：乡村文化的生活——电影《百鸟朝凤》的艺术感与历史感主 讲：罗成，中山大学中文系副教授，硕士生导师。时 间：12月16日（周六）上午10:00-12:00   地 点：一号报...</p>
                                            </dd>
                                            <dd class="ddclock"><span>发布时间：2017-12-12 </span></dd>
                                        </dl>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="list_con clearfix">
                                    <div class="list_img">
                                        <img src="%E5%B9%BF%E4%B8%9C%E7%9C%81%E7%AB%8B%E4%B8%AD%E5%B1%B1%E5%9B%BE%E4%B9%A6%E9%A6%86%E5%88%97%E8%A1%A8%E9%A1%B5_files/201712061001.jpg"></div>
                                    <div class="list_text">
                                        <dl>
                                            <dt><a href="http://www.zslib.com.cn/TempletPage/Detail.aspx?dbid=1&amp;id=2881">“中山讲堂”第180讲 健康系列</a></dt>
                                            <dd class="ddtxt">
                                                <p>主 题一：控制血压平稳主 讲：王清海，广东省第二中医院主任中医师，教授，博士生导师。主 题二：颈肩腰腿痛的保健主 讲：范德辉，广东省第二中医院主任中医师，教授，硕士研究生导师。时 ...</p>
                                            </dd>
                                            <dd class="ddclock"><span>发布时间：2017-12-06 </span></dd>
                                        </dl>
                                    </div>
                                </div>
                            </li>
                        </ul>

                        <div class="list_pager">
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
                        </div>
                        <!-- prev next-->
                    </div>
             </div>
        </div>
    </form>
</body>
</html>
