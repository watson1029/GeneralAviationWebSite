//旧版
////登陆显示隐藏
//$(document).ready(function(){
//	
//  $('.login').find('span').click(
//  function(){
//	  $('.login_box').toggle(500);
//	  }
//  )}
//);

////输入框
//$(document).ready(function(){
//$("input[class*=ipt]")
//.each(function(){   
//   var oldVal=$(this).val(); 
//   $(this)   
//   .css({"color":"#adadad"})
//   .focus(function(){   
//    if($(this).val()!=oldVal){$(this).css({"color":"#000"})}else{$(this).val("").css({"color":"#adadad"})}   
//   })   
//   .blur(function(){   
//    if($(this).val()==""){$(this).val(oldVal).css({"color":"#adadad"})}   
//   })   
//   .keydown(function(){$(this).css({"color":"#000"})})   
//     
//})
//});


//(function ($) {
//	$(function () {
//		nav(); //导航栏JS
//	});
//	
//	function nav() {
//		var $liCur = $(".nav-box>ul>li.cur"),
//			curP = $liCur.position().left,
//			curW = $liCur.outerWidth(true),
//			$slider = $(".nav-line"),
//			$targetEle = $(".nav-box>ul>li:not('.last')"),
//			$navBox = $(".nav-box");
//		$slider.stop(true, true).animate({
//			"left" : curP,
//			"width" : curW
//		});
//		$targetEle.mouseenter(function () {
//			var $_parent = $(this);//.parent(),
//			_width = $_parent.outerWidth(true),
//			posL = $_parent.position().left;
//			$slider.stop(true, true).animate({
//				"left" : posL,
//				"width" : _width
//			}, "fast");
//		});
//		$navBox.mouseleave(function (cur, wid) {
//			cur = curP;
//			wid = curW;
//			$slider.stop(true, true).animate({
//				"left" : cur,
//				"width" : wid
//			}, "fast");
//		});
//	};
//	
//})(jQuery);


//function noFlash() {
//    //登陆显示隐藏
//    $(document).ready(function() {

//        $('.login').find('span').click(
//  function() {
//      $('.login_box').toggle(500);
//  }
//  )
//    }
//);
//    $(document).ready(function() {
//        $("input[class*=ipt]")
//.each(function() {
//    var oldVal = $(this).val();
//    $(this)
//   .css({ "color": "#adadad" })
//   .focus(function() {
//       if ($(this).val() != oldVal) { $(this).css({ "color": "#000" }) } else { $(this).val("").css({ "color": "#adadad" }) }
//   })
//   .blur(function() {
//       if ($(this).val() == "") { $(this).val(oldVal).css({ "color": "#adadad" }) }
//   })
//   .keydown(function() { $(this).css({ "color": "#000" }) })

//})
//    });

//}


//登录显示隐藏
$(document).ready(function() {
    //扫码登录 
    jQuery(".login_boxb").slide({ titCell: ".logtitle li", mainCell: ".logtab", targetCell: ".more a", trigger: "click", switchLoad: "_src", delayTime: 1500 });

    //二维码提示动画
    $(".qrcode-main").mouseenter(function() {
        $(".qrcode-img").animate({ left: "40px" }, 300);
        $(".qrcode-help").show(50).animate({ marginRight: "20px" }, 500);
    });
    $(".qrcode-main").mouseleave(function() {
        $(".qrcode-img").animate({ left: "101px" });
        $(".qrcode-help").hide();
    });
    //
    $(".mdf_log").click(function() {
        $(".userid-login,#kj_log").hide();
        $(".qrcode-login,#sm_log").show(300);
        $(this).hide();
        $("#LoginFrame1_savewx").css("display", "block");
        $("#LoginFrame1_sm_log").css("display", "block");
        $("#LoginFrame1_kj_log").css("display", "none");
    });

    $('.login').find('dd').click(
  function() {
      loadXMLDoc_2wm('/LoginFrame.aspx');
      loadXMLDoc_2wmimg('/LoginFrame.aspx?img2wm=get');
      $('.login_boxb').toggle(500);
  }
  )
}
);





//输入框
$(document).ready(function() {
    $("input[class*=ipt]")
.each(function() {
    var oldVal = $(this).val();
    $(this)
   .css({ "color": "#adadad" })
   .focus(function() {
       if ($(this).val() != oldVal) { $(this).css({ "color": "#000" }) } else { $(this).val("").css({ "color": "#adadad" }) }
   })
   .blur(function() {
       if ($(this).val() == "") { $(this).val(oldVal).css({ "color": "#adadad" }) }
   })
   .keydown(function() { $(this).css({ "color": "#000" }) })

})
});


(function($) {
    $(function() {
        nav(); //导航栏JS
    });

    function nav() {
        var $liCur = $(".nav-box>ul>li.cur"),
			curP = $liCur.position().left,
			curW = $liCur.outerWidth(true),
			$slider = $(".nav-line"),
			$targetEle = $(".nav-box>ul>li:not('.last')"),
			$navBox = $(".nav-box");
        $slider.stop(true, true).animate({
            "left": curP,
            "width": curW
        });
        $targetEle.mouseenter(function() {
            var $_parent = $(this); //.parent(),
            _width = $_parent.outerWidth(true),
			posL = $_parent.position().left;
            $slider.stop(true, true).animate({
                "left": posL,
                "width": _width
            }, "fast");
        });
        $navBox.mouseleave(function(cur, wid) {
            cur = curP;
            wid = curW;
            $slider.stop(true, true).animate({
                "left": cur,
                "width": wid
            }, "fast");
        });
    };

})(jQuery);


function noFlash() {
    //登录显示隐藏
    $(document).ready(function() {
        //扫码登录 
        jQuery(".login_boxb").slide({ titCell: ".logtitle li", mainCell: ".logtab", targetCell: ".more a", trigger: "click", switchLoad: "_src", delayTime: 1500 });

        //二维码提示动画
        $(".qrcode-main").mouseenter(function() {
            $(".qrcode-img").animate({ left: "40px" }, 300);
            $(".qrcode-help").show(50).animate({ marginRight: "20px" }, 500);
        });
        $(".qrcode-main").mouseleave(function() {
            $(".qrcode-img").animate({ left: "101px" });
            $(".qrcode-help").hide();
        });
        //
        $(".mdf_log").click(function() {
            $(".userid-login,#kj_log").hide();
            $(".qrcode-login,#sm_log").show(300);
            $(this).hide();
            $("#LoginFrame1_savewx").css("display", "block");
            $("#LoginFrame1_sm_log").css("display", "block");
            $("#LoginFrame1_kj_log").css("display", "none");
        });

        $('.login').find('dd').click(
  function() {
      loadXMLDoc_2wm('/LoginFrame.aspx');
      loadXMLDoc_2wmimg('/LoginFrame.aspx?img2wm=get');
      $('.login_boxb').toggle(500);
  }
  )
    }
);





    //输入框
    $(document).ready(function() {
        $("input[class*=ipt]")
.each(function() {
    var oldVal = $(this).val();
    $(this)
   .css({ "color": "#adadad" })
   .focus(function() {
       if ($(this).val() != oldVal) { $(this).css({ "color": "#000" }) } else { $(this).val("").css({ "color": "#adadad" }) }
   })
   .blur(function() {
       if ($(this).val() == "") { $(this).val(oldVal).css({ "color": "#adadad" }) }
   })
   .keydown(function() { $(this).css({ "color": "#000" }) })

})
    });
}