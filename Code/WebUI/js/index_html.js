//底部温馨提示浮窗
$(function() {
    //$('#breakingnews2').BreakingNews({
    //    title: '温馨提示',
    //    titlebgcolor: '#343434',
    //    linkhovercolor: '#fff',
    //    border: 'none',
    //    timer: 6000,
    //    effect: 'slide'
    //});
    //$('.inpop_close').click(
	//  function() {
	//      $('.index_pop').hide(500);
	//  }
	//);
    //$('#breakingnews2 > ul').find('li').click(
	//  function() {
	//      $(this).css("height", "120px");
	//      $('#breakingnews2').css('height', '120px');
	//  }
	// );
    //$('#breakingnews2').mouseleave(function() {
    //    $(this).css("height", "40px");
    //    $('#breakingnews2').find('li').css('height', '40px');
    //});
    
    //供求信息、通航企业特效		
    jQuery(".sideMenu").slide({
        titCell: "h3", //鼠标触发对象
        targetCell: "ul", //与titCell一一对应，第n个titCell控制第n个targetCell的显示隐藏
        effect: "slideDown", //targetCell下拉效果
        delayTime: 300, //效果时间
        triggerTime: 150, //鼠标延迟触发时间（默认150）
        defaultPlay: true, //默认是否执行效果（默认true）
        returnDefault: true //鼠标从.sideMen移走后返回默认状态（默认false）
    });

    //馆藏检索tab
    //jQuery(".slideTxtBox").slide();				

    //focus焦点图切换					
    //jQuery(".focusBox").hover(function(){ jQuery(this).find(".prev,.next").fadeTo("show",0.5) },function(){ jQuery(this).find(".prev,.next").fadeOut() });
    //jQuery(".focusBox").slide({ mainCell:".pic",effect:"fold", autoPlay:true, delayTime:1000,interTime:5000, trigger:"click"});
    //广告图切换
    //jQuery(".adBox").slide({ mainCell: ".adpic", effect: "fold", autoPlay: true, delayTime: 800, interTime: 5000 });

    //在线阅读TAB switchload 图片延迟加载		
    //jQuery(".switchLoad").slide({ titCell:".hd li", mainCell:".bd", targetCell:".more a", trigger:"mouseover",switchLoad:"_src",delayTime:0 });

    //电子资源滚动	
    //jQuery(".scrollBox").slide({ titCell:".list02 li", mainCell:".piclist", effect:"left",vis:8,scroll:8, autoPlay:true,delayTime:800,interTime:8000,trigger:"click",switchLoad:"_src",easing:"easeOutCirc"});	
    //电子资源tab		
    //jQuery(".switchLoad02").slide({ titCell:".hd02 li", mainCell:".bd02", targetCell:".more a", trigger:"mouseover",delayTime:0 });

    //中文新书TAB 
    //jQuery(".switchLoad03").slide({ titCell:".hd03 li", mainCell:".bd03", targetCell:".more a", trigger:"mouseover",switchLoad:"_src",delayTime:0 });

    //问卷调查
   //$('.diaocha_m').hover(function(){				 
   // 	$('.diaocha_b').toggle(100);
   // 	});

    //展览、专题js
    //$(".zhuanti").hover(function() {
    //    $(this).find(".imgtxt").stop().animate({ height: "198px" }, 400);
    //    $(this).find(".imgtxt h4").stop().animate({ paddingTop: "60px" }, 400);
    //}, function() {
    //    $(this).find(".imgtxt").stop().animate({ height: "45px" }, 400);
    //    $(this).find(".imgtxt h4").stop().animate({ paddingTop: "0px" }, 400);
    //})

});

//底部图片翻转
//$(document).ready(function(e) {
//    var turn = function(target, time, opts) {
//        target.find('a').hover(function() {
//            $(this).find('img').stop().animate(opts[0], time, function() {
//                $(this).hide().next().show();
//                $(this).next().animate(opts[1], time);
//            });
//        }, function() {
//            $(this).find('.info').animate(opts[0], time, function() {
//                $(this).hide().prev().show();
//                $(this).prev().animate(opts[1], time);
//            });
//        });
//    }
//    var verticalOpts = [{ 'width': 0 }, { 'width': '226px'}];
//    turn($('#vertical'), 150, verticalOpts);
//});

//登陆框
//$(document).ready(function(e) {
//    $.ajax({
//        type: "GET",
//        url: "/WebService/AjaxService.aspx?login=login",
//        async: true,
//        dataType: "text",
//        beforeSend: function() {
//            //$("#T1").html("<img height='30px' src='images/load.gif'/>");
//        },
//        success: function(data) {
//            var txt = data.split(",");
//            if (txt != null && txt != "") {
//                $("#LoginFrame1_Image_2wm_code").attr('src', "http://" + txt[0]);
//                if (txt[1] != null && txt[1] != "") {
//                    $("#LoginFrame1_wxuc").text(txt[1]);
//                    $("#LoginFrame1_kj_log").css("display", "block");
//                    $("#LoginFrame1_userid_login").css("display", "block");
//                    $("#LoginFrame1_mdf_log").css("display", "block");
//                    $("#LoginFrame1_qrcode_login").css("display", "none");
//                    $("#LoginFrame1_sm_log").css("display", "none");
//                    $("#LoginFrame1_savewx").css("display", "none");
//                    $("#LoginFrame1_saveInfo").attr("checked",true);
//                }
//            }
//        }
//    });
//});