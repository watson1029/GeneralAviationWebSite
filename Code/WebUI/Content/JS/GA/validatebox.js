$.extend(
$.fn.validatebox.defaults.rules, {
    mobile: {
        validator: function (value) {
            if (value == "") return true;
            return /^(13|14|15|16|17|15|18|19)\d{9}$/i.test(value);
        },
        message: '格式不正确'
    }

});