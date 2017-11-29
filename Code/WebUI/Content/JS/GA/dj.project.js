(function (dj, j) {
    j.extend(dj, {
        url: function (controller, action, params) {
            return new dj.param(dj.root + controller + "/" + action, params).stamp();
        },
        open: function (url, width, height) {
            j.extend(this, { width: 600, height: 300, left: 0, top: 0, target: "_blank" }, opts); //_blank
            this.top = ((screen.height - this.height) / 2) - 40; this.left = (screen.width - this.width) / 2;
            var sb = new dj.builder();
            sb.add("toolbar=no,location=no,directions=no,status=no,revisable=no,scrollbars=yes,menubar=no,");
            sb.addFmt("width={0}px,height={1}px,top={2}px,left={3}px", this.width, this.height, this.top, this.left);
            window.open(this.url, this.target, sb.toString());
        },
        checkAll: function (obj, name) {
            //全选
            var chs = document.getElementsByName(name);
            for (var i = 0; i < chs.length; i++) {
                chs[i].checked = obj.checked;
            }
        },
        getCheckedValue: function (name) {
            //获取选中按钮的value
            var idArray = [],
                chs = document.getElementsByName(name);
            for (var i = 0; i < chs.length; i++) {
                if (chs[i].checked) {
                    idArray.push(chs[i].value);
                }
            }
            return idArray;
        },
        setDisabled: function () {
            for (var i = 0; i < arguments.length; i++) {
                $(arguments[i]).attr("disabled", "disabled");
            }
        },
        removeDisabled: function () {
            for (var i = 0; i < arguments.length; i++) {
                $(arguments[i]).removeAttr("disabled");
            }
        }
    });

    //表单扩展
    dj.form = {
        get: function (id) {
            return id ? (typeof id == "string" ? j("#" + id) : j(id)) : j(document.forms[0]);
        },
        ajaxSubmit: function (form, callback, end) {
            var form = this.get(form);
            if (!form.find("input,textarea,select").valid()) {
                end && end();
                return false;
            }
            dj.ajax(form.attr("action"), form.serialize(), function (data) {
                callback && callback(data);
            }, function () {
                end && end();
            });
            return true;
        },
        submit: function () {
            var form = this.get(form);
            if (!form.valid()) {
                return false;
            }
            form.get(0).submit();
            return true;
        }
    }
    //全局loading
    dj.loading = {
        id: "requestLoading",
        get: function () {
            return $("#" + dj.loading.id);
        },
        setMsg: function (msg) {
            dj.loading.get().html(msg);
        },
        init: function () {
            var el = dj.loading.get();
            if (el.length <= 0) {
                el = j("<div>").attr("id", dj.loading.id).addClass("request-loading").appendTo('body');
            }
            return el;
            //dj.loading.show();
        },
        show: function (text) {
            var el = dj.loading.get();
            if (el.length <= 0) {
                el = dj.loading.init();
            }
            text && el.html(text);
            //每次显示loading都设定位置
            dj.position.setRegion(el, { top: "47%", left: "47%" });
            el.show();
        },
        hide: function () {
            dj.loading.get().hide();
            dj.loading.setMsg("正在加载数据...");
        }
    }
    $(function () {
        dj.loading.init();
        dj.loading.hide();
    });
    //异步请求，封装了loading效果
    dj.ajax = function (url, params, success, end, iscontenttype) {
        dj.loading.show();
        var options = {
            type: "POST",
            dataType: "json",
            url: url,
            data: params,
            success: function (data) {
                dj.loading.hide();
                end && end(true, data);
                success && success(data);
            },
            error: function (xhr, err) {
                dj.loading.hide();
                end && end(false);
                var defaultMsg = "系统繁忙，请稍后再试！";
                window.top.dj.alert ? window.top.dj.alert(defaultMsg) : alert(defaultMsg);
            }
        };
        if (iscontenttype) {
            options.contentType = "application/json; charset=utf-8";
        }
        j.ajax(options);
    }
})(window.dj, window.jQuery);