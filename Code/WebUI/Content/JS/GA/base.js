(function (j) {
    var noop = function () { },
        guid = 0,
        index = 1989;
    window.dj = window.dj || {
        version: "V2.0.0",
        mgr: {},
        error: [],
        split: /[^, ]+/g,
        noop: noop,
        href: "javascript:void(0)",
        ie6: window.VBArray && !window.XMLHttpRequest,
        addCmp: function (cmp) {
            if (!dj.mgr[cmp.id]) { dj.mgr[cmp.id] = cmp; };
        },
        getCmp: function (id) {
            return dj.mgr[id] || null;
        },
        deleteCmp: function (id) {
            var cmp = dj.mgr[id];
            if (cmp) { delete dj.mgr[id]; }
        },
        hideCmp: function (type) {
            for (var i in dj.mgr) {
                var cmp = dj.mgr[i];
                (cmp instanceof type) && cmp.hide && cmp.hide();
            }
        },
        id: function () {
            return "dj-" + guid++;
        },
        zIndex: function () {
            return index++;
        },
        getClass: function () {
            return function () {
                this.init.apply(this, arguments);
            }
        },
        extend: function (parent, children, overrides) {
            //控件继承
            if (typeof (parent) != "function") { return children; };
            children.base = parent.prototype;
            children.base.constructor = parent;
            var target = noop;
            target.prototype = parent.prototype;
            children.prototype = new target();
            children.prototype.constructor = children;
            overrides && j.extend(children.prototype, overrides);
            return children;
        },
        control: function (defaults, options) {
            //控件基类            
            this.setOptions(defaults, options);
            this.init && this.init();
            this.render && this.render();
            this.loaded && this.loaded();
            dj.addCmp(this);
        },
        destroy: function () {
            //控件销毁
            for (var id in dj.mgr) {
                var cmp = dj.getCmp(id);
                cmp.destroy && cmp.destroy();
                delete dj.mgr[id];
            }
            delete dj.mgr;
        }
    };
    j.extend(dj.control.prototype, {
        events: {},
        getType: function () {
            return "dj.control";
        },
        setOptions: function (defaults, options) {
            this.type = this.getType();
            this.id = dj.id();
            j.extend(this, defaults, options);
        },
        fire: function (type) {
            //未完....
        },
        on: function (type, fn) {
            this.events[type] = fn;
            //未完....
        },
        hasEvent: function (type) {
            return this.events[type] === undefined;
        },
        unon: function (type) {
            if (type) {
                this.hasEvent(type) && delete this.events[type];
            } else {
                //未完....
            }
        },
        destroy: function () { }
    });
    //执行销毁
    j(window).bind("unload", dj.destroy);
    try {
        document.execCommand("BackgroundImageCache", false, true);
    } catch (e) { }
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




