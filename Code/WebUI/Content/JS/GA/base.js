
(function (j) {
    //---------------------------------string-prototype-extend---------------------------------
    j.extend(String.prototype, {
        trim: function () {
            return this.replace(/^\s+|\s+$/g, '');
        },
        delTag: function () {
            return this.replace(/<[^>]+>/g, "");
        },
        startWith: function (str) {
            return this.indexOf(str) == 0;
        },
        endWith: function (str) {
            var d = this.length - str.length; return d >= 0 && this.lastIndexOf(str) == d;
        },
        truncate: function (len, trun) {
            if (!trun) { trun = "..." }; return this.length > len ? this.substring(0, len) + trun : this;
        },
        removeNumber: function () {
            return this.replace(/[^d]/g, "");
        },
        padLeft: function (len, pad) {
            pad = pad == null ? "0" : pad; var str = this;
            if (len > this.length) { for (var i = 0; i < len - this.length; i++) { str = pad + str; } }; return str;
        },
        format: function () {
            var str = this, args = arguments;
            if (args.length > 0 && (typeof args[0] == "object")) {
                for (var i in args[0]) {
                    str = this.replace("{" + i + "}", args[0][i]);
                }
            } else {
                for (var i = 0; i < args.length; i++) {
                    str = str.replace("{" + (i) + "}", args[i]);
                };
            }
            return str;
        },
        delEnd: function (str) {
            return this.endWith(str) ? this.substring(0, this.length - str.length) : this;
        },
        delStart: function (str) {
            return this.startWith(str) ? this.substring(str.length) : this;
        },
        subRight: function (len) {
            return this.slice(this.length - len);
        },
        subLeft: function (len) {
            return this.slice(0, len);
        },
        has: function (obj) {
            return this.indexOf(obj) > -1
        },
        toDate: function () {
            return new Date(Date.parse(this.replace(/-/g, "/")));
        },
        replaceAll: function (s1, s2) {
            return this.replace(new RegExp(s1, "gm"), s2);
        },
        cap: function () {
            return this.slice(0, 1).toUpperCase() + this.slice(1);
        },
        toCharArray: function () {
            var chs = [];
            for (var i = 0; i < this.length; i++) {
                chs[i] = this.charAt(i);
            }
            return chs;
        }
    });

})(window.jQuery);




