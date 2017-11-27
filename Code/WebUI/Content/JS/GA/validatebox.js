$.extend(
$.fn.validatebox.defaults.rules, {
    mobile: {
        validator: function (value) {
            if (value == "") return true;
            return /^(13|14|15|16|17|15|18|19)\d{9}$/i.test(value);
        },
        message: '手机号格式不正确！'
    },
    md: {
        validator: function (value, param) {
            var start = $(param[0]).datetimebox('getValue');
            
            return value>start;
        },
    message:'结束时间要大于开始时间！'
    }
});