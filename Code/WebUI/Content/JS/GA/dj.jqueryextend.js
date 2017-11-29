
$.fn.extend({
    disabled: function (value) {
        return value ? (this.attr("disabled", "disabled")) : this.removeAttr("disabled");
    },
    checked: function (value) {
        return value ? this.attr("checked", "checked") : this.removeAttr("checked");
    },
    isDisabled: function () {
        return this.attr("disabled") == "disabled";
    },
    toParams: function () {
        var items = this.find("input,textarea,select"),
                result = {},
                notypes = ["button", "file", "image", "reset", "submit"];
        items.each(function (i, item) {
            var item = items.eq(i),
                    id = item.attr("id"),
                    type = item.attr("type").toLowerCase();
            //过滤按钮和文件
            if (!Array.has(notypes, type)) {
                if (type == "checkbox") {
                    result[id] = item.get(0).checked ? "true" : "false";
                } else {
                    var val = item.val().trim();
                    if (val != "") {
                        result[id] = val;
                    }
                }
            }
        });
        return result;
    },
    enterQuery: function (callback) {
        this.keydown(function (e) {
            if (e.keyCode == dj.kc.enter) {
                callback && callback(e);
            }
        });
        return this;
    }
});