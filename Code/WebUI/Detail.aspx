<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>

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
                    <img src="images/logo.png" alt="" />
                </div>
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

        <!--main begin-->
        <div class="main clearfix" style="background: #fff;">
            <div class="center">
                <div class="main_left">
                    <h1><span id="title">我是3D眼镜设计师</span></h1>
                    <div class="title_tip">
                        <span style="float: left">
                            <span id="auther">作者：杨文靖</span>&nbsp;
                            <span id="editor"></span>&nbsp;
                            <span id="puttime">发布时间：2017-12-22</span>&nbsp;
                        </span>
                    </div>
                    <div class="content">
                        <p align="left">
                            3D技术的运用，让观众走出平面的感知，获得亲临现场的立体效果，其中不得不提到3D电影的好搭档——3D眼镜。但是，
你知道3D眼镜家族中有众多风格不一的成员吗？11月的中图少儿创客空间一连举办了4场活动，小读者们在老师的指导下，一起了解3D立体电影的知识，结识
3D眼镜家族的成员，并设计创意3D眼镜，体验立体视觉的魅力。
                        </p>

                        <p align="left">
                            到场的小读者们带着满肚子的好奇：“为什么我们没有戴3D眼镜的时候屏幕是一片模糊的呢？”“为什么不是每部电影都有
3D版本呢？”小读者们别着急，中图少儿创客空间一一为你解答。立体电影，即3D电影，诞生于1922年，是利用人双眼的视角差和会聚功能制作的一种电影
形式。观众戴上特制的<a href="https://baike.baidu.com/item/%E7%AB%8B%E4%BD%93%E7%9C%BC%E9%95%9C" target="_blank">立体眼镜</a>观看时，就能有人物场景尽在周遭的身临其境的感觉。3D眼镜发展至今，制作成本相对降低，成像效果好的偏光式3D眼镜成了我们日常在电影院使用的主力军。
                        </p>

                        <p align="left">
                            了解了3D眼镜的前世今生，到了小读者大展身手的时候。从原料简单的色差式3D眼镜入手，眼镜设计师们上任啦！透明塑料
薄膜和卡纸，能变出怎样的魔法呢？除了中规中矩的“基础款”，想法多多的小创客们带来了众多惊喜之作，挂耳式镜框、头箍式镜框、面具式镜框，形状造型风格
各异的镜框百花齐放，最后贴上“左红右蓝”的塑料薄膜，作品就可以投入实际应用了。戴着自己动脑设计、动手制作、与众不同的3D眼镜，小读者们津津有味地
观看电影《侏罗纪》，感受跃出平面的真实感，兴高采烈地为恐龙们欢呼。
                        </p>
                        <p align="left">在本期创客活动中，小读者们既当了小设计师，也切身体会到科技给我们生活带来的乐趣。中图少儿创客空间将继续鼓励小读者们运用自己的双手和智慧，结合科学技术原理，为生活增趣味、添创意。</p>
                    </div>
                    <div class="article_section">
                        <a class="prev" href="Detail.aspx?dbid=2&id=2533" title="《佛山历史文化丛书》赠书仪式在我馆举行">
                            <i class="prev_btn"></i>
                            <h3>上一篇</h3>
                            <div class="abs_prev">《佛山历史文化丛书》赠书仪式在我馆举行</div>
                        </a>
                        <a class="next" href="Detail.aspx?dbid=2&id=2535" title="我是3D眼镜设计师">
                            <i class="prev_btn"></i>
                            <h3>下一篇</h3>
                            <div class="abs_next">我是3D眼镜设计师</div>
                        </a>
                    </div>

                    <div class="listmore"><a href="List.aspx?dbid=2">返回最新报道列表</a></div>
                </div>
            </div>
        </div>
        <!--main end-->
    </form>
</body>
</html>
