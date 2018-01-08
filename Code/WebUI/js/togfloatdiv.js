$(function(){
			$('#tog').toggle(function(){
				$(this).animate({top:'403px'},320).addClass("togclose").removeClass("tog").html('<span>关  闭</span>');
				$('.tog_contact').slideDown(320);
			},function(){
				$(this).animate({top:'0px'},320).addClass("tog").removeClass("togclose").html('<span>简  介</span>');
				$('.tog_contact').slideUp(320);
			})
	$(".menu_l_list li:last").css({"background":"none"}); 
	$(".crumbNav em:last").css({"color":"#B10000","padding":"0"}); 
	$(".brea_tab th:last").css({"border-right":"none"}); 
	$(".serice_content .serice_list:last").css({"border-bottom":"none"}); 
})