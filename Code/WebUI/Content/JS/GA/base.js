//基础库--zyb
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
            this.setOptions(defaults, options);
            this.init && this.init();
            this.render && this.render();
            this.loaded && this.loaded();
            dj.addCmp(this);
        },
        destroy: function () {
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
        },
        dateValFormat: function () {

            return this.replace(/-/g, '/').replace(/T|Z/g, ' ').trim();
        }
    });

    j.extend(Object, {
        count: function (obj) {
            var count = 0;
            for (var i in obj) {
                count++;
            }
            return count;
        },
        keys: function (obj) {
            var k = [];
            for (var i in obj) {
                k.push(i);
            }
            return k;
        }
    });
    //---------------------------------date-prototype-extend---------------------------------
    j.extend(Date.prototype, {
        format: function (format) {
            var o = {
                "M+": this.getMonth() + 1, //month
                "d+": this.getDate(),    //day
                "H+": this.getHours(),   //hour
                "m+": this.getMinutes(), //minute
                "s+": this.getSeconds(), //second
                "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
                "S": this.getMilliseconds() //millisecond
            }
            if (/(y+)/.test(format)) format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(format)) {
                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
                }
            }
            return format;
        }
    });
    //---------------------------------array-extend---------------------------------
    j.extend(Array, {
        remove: function (arr, index_value) {
            if (typeof (index_value) == "number") {
                arr.splice(index_value, 1);
            } else { var i = Array.indexOf(arr, index_value); if (i > -1) { arr.splice(i, 1); } }; return arr;
        },
        clear: function (arr) {
            arr.splice(0, arr.length);
        },
        indexOf: function (arr, value) {
            for (var i = 0; i < arr.length; i++) { if (arr[i] == value) { return i; } }; return -1;
        },
        has: function (arr, value) {
            return Array.indexOf(arr, value) > -1;
        },
        max: function (arr) {
            //正常用法
            //            for (var i = 0, maxValue = Number.MIN_VALUE; i < arr.length; i++){
            //                parseInt(arr[i]) > maxValue && (maxValue = arr[i]);
            //            }
            //	        return maxValue;
            //2B用法
            return Math.max.apply(Math, arr);
        },
        min: function (arr) {
            //正常用法
            //            for (var i = 0, minValue = Number.MAX_VALUE; i < arr.length; i++){
            //                parseInt(arr[i]) < minValue && (minValue = arr[i]);
            //            }
            //	        return minValue;
            //2B用法
            return Math.max.apply(Math, arr);
        },
        sum: function (arr) {
            for (var sum = i = 0; i < arr.length; i++) {
                sum += parseInt(arr[i]);
            }
            return sum;
        },
        distinct: function (arr) {
            var results = arr.sort()
            for (var i = 1; i < results.length; i++) {
                if (results[i] === results[i - 1]) {
                    results.splice(i--, 1);
                }
            }
            return results;
        },
        hasRepeat: function (arrs) {
            var a = arrs.sort();
            for (var i = 0; i < a.length; i++) {
                if (a[i] == a[i + 1]) {
                    return true;
                }
            }
            return false;
        }
    });
    //---------------------------------dj-extend---------------------------------
    j.extend(dj, {
        dom: function (el, attrs) {
            if (typeof el === "string") {
                if (/<[^>]+>/.test(el)) {
                    var tag = el.replace(/<|>/g, '');
                    el = document.createElement(tag);
                    if (attrs) {
                        for (var name in attrs) {
                            /cls|class|className/.test(name) ? el.className = attrs[name] : el[name] = attrs[name];
                        }
                    }
                } else {
                    el = document.getElementById(el);
                }
            }
            return el;
        },
        domName: function (name) {
            return document.getElementsByName(name);
        },
        getType: function (val) {
            if (val.getType) { return val.getType(); };
            if (val == null) { return "null"; };
            if (val.nodeType && val.nodeType == 1) { return "element"; };
            return Object.prototype.toString.apply(val).replace("[object ", "").replace("]", "").toLowerCase();
        },
        isEl: function (obj) {
            return dj.getType(obj) == "element";
        },
        loaded: function (el, callback) {
            el = dj.dom(el);
            document.addEventListener ? j(el).bind("load", function () {
                j(this).unbind("load");
                callback.call(this);
                el = null;
            }) : el.onreadystatechange = function () {
                if (/loaded|complete/.test(this.readyState)) {
                    el.onreadystatechange = null;
                    callback.call(this);
                    el = null;
                }
            }
        },
        clearSelection: function () {
            window.getSelection ? window.getSelection().removeAllRanges() : document.selection.empty();
        },
        bind: function (obj, fun) {
            var args = Array.prototype.slice.call(arguments).slice(2);
            return function (result) {
                if (result) { args.push(result); }
                return fun.apply(obj, args);
            }
        },
        bindWithEvent: function (obj, fun) {
            var args = Array.prototype.slice.call(arguments).slice(2);
            return function (event) {
                return fun.apply(obj, [event || window.event].concat(args));
            }
        },
        stopEvent: function (e) {
            e.preventDefault(); e.stopPropagation();
        },
        getSpace: function (el, items) {
            var len = items.length, num = 0;
            for (var i = 0; i < len; i++) {
                switch (items[i]) {
                    case "mt": num += parseInt(el.css("margin-top")) || 0; break;
                    case "mb": num += parseInt(el.css("margin-bottom")) || 0; break;
                    case "ml": num += parseInt(el.css("margin-left")) || 0; break;
                    case "mr": num += parseInt(el.css("margin-right")) || 0; break;
                    case "pt": num += parseInt(el.css("padding-top")) || 0; break;
                    case "pb": num += parseInt(el.css("padding-bottom")) || 0; break;
                    case "pl": num += parseInt(el.css("padding-left")) || 0; break;
                    case "pr": num += parseInt(el.css("padding-right")) || 0; break;
                    case "bt": num += parseInt(el.css("border-top-width")) || 0; break;
                    case "bb": num += parseInt(el.css("border-bottom-width")) || 0; break;
                    case "bl": num += parseInt(el.css("border-left-width")) || 0; break;
                    case "br": num += parseInt(el.css("border-right-width")) || 0; break;
                }
            }
            return num;
        }
    });
    //---------------------------------position---------------------------------
    dj.position = {
        dd: function (win) {
            return (win ? win.document.documentElement : document.documentElement);
        },
        db: function (win) {
            return (win ? win.document.body : document.body);
        },
        maxClient: function (win) {
            var doc = dj.position.dd(win);
            return { width: Math.max(doc.clientWidth, doc.scrollWidth), height: Math.max(doc.clientHeight, doc.scrollHeight), top: 0, left: 0 };
        },
        client: function (win) {
            var doc = dj.position.dd(win);
            return { width: doc.clientWidth, height: doc.clientHeight, top: 0, left: 0 };
        },
        scroll: function (win) {
            var dd = dj.position.dd(win), db = dj.position.db(win);
            return { left: Math.max(dd.scrollLeft, db.scrollLeft), top: Math.max(dd.scrollTop, db.scrollTop) }
        },
        screen: function () {
            return { width: screen.availWidth, height: screen.availHeight }
        },
        toNumber: function (value, max) {
            if (value != null && typeof value === "number") {
                return value;
            }
            if (value.endWith("px")) {
                value = parseInt(value);
            } else if (value.endWith("%")) {
                value = parseInt(max * value.split("%")[0] / 100);
            }
            return value;
        },
        setRegion: function (el, options) {
            el = j(el);
            var parent = j(options.parent || window), offset = parent.offset();

            var w = el.width(), h = el.height(), maxWidth = parent.width(), maxHeight = parent.height();
            //            if(options.left=="100%"){
            //                el.width(w);
            //            };
            //            if(options.top=="100%"){
            //                el.height(h);
            //            };
            var l = dj.position.toNumber(options.left, (maxWidth - w)) + parent.scrollLeft();
            var t = dj.position.toNumber(options.top, (maxHeight - h)) + parent.scrollTop();
            if (offset) {
                l += offset.left; t += offset.top;
            }
            el.css({ top: t, left: l });
        },
        getOffsetSize: function (el) {
            el = j(el);
            var offset = el.offset() || {};
            offset["width"] = el.outerWidth();
            offset["height"] = el.outerHeight();
            return offset;
        }
    };
    //---------------------------------param---------------------------------
    dj.param = function (url, params) {
        this.url = url || window.location.href;
        this.format();
        this.params = {};
        this.get().removeSearch().add(params);
    };
    dj.param.prototype = {
        format: function () {
            this.url = this.url.replace("?&", "?").replace(/(&+)/g, "&").replace(/(#+)/g, "").delEnd("&");
            return this;
        },
        stamp: function () {
            this.params["_t"] = new Date().getTime();
            return this;
        },
        getSearch: function () {
            return this.hasSearch() ? this.url.substring(this.url.lastIndexOf("?") + 1) : null;
        },
        hasSearch: function () {
            return this.url.lastIndexOf("?") >= 0;
        },
        removeSearch: function () {
            this.hasSearch() && (this.url = this.url.substring(0, this.url.indexOf("?") + 1));
            return this;
        },
        getUrl: function () {
            this.removeSearch();
            if (!this.hasSearch()) { this.url += "?"; } else {
                this.url.endWith("?") || (this.url += "&");
            };
            for (var i in this.params) {
                this.url += i + "=" + this.params[i] + "&";
            }
            return this.url.delEnd("&");
        },
        has: function (key) {
            return !!this.params[key];
        },
        get: function () {
            var search = this.getSearch();
            if (!search) { return this; }
            var pairs = search.split("&");
            for (var i = 0; i < pairs.length; i++) {
                var arr = pairs[i].split("=");
                this.params[arr[0]] = arr[1];
            };
            return this;
        },
        add: function (key, value, isDecode) {
            if (!key) { return this; }
            if (typeof (key) == "object") {
                for (var i in key) {
                    this.add(i, key[i], isDecode);
                }
            } else {
                //默认对参数进行编码
                if (isDecode == null) {
                    isDecode = true;
                }
                this.params[key] = (isDecode ? escape(value) : value);

            }
            return this;
        },
        addVal: function (id, isDecode) {
            //根据id查询表单的值，并添加到参数集合
            var val = j("#" + id).val().trim();
            return val == "" ? this : this.add(id, val, isDecode);
        },
        addVals: function () {
            for (var i = 0; i < arguments.length; i++) {
                this.addVal(arguments[i]);
            }
            return this;
        },
        addAttr: function (id, attrName, isDecode) {
            //根据id查询表单的属性值，并添加到参数集合
            var val = j("#" + id).attr(attrName).trim();
            return val == "" ? this : this.add(attrName, val, isDecode);
        }
    }
})(window.jQuery);





