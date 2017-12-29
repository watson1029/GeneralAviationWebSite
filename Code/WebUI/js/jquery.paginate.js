/**
 * pagination��ҳ���
 * @version 1.5.0
 * @author mss
 * @url https://github.com/Maxiaoxiang
 *
 * @���÷���
 * $(selector).pagination(option, callback);
 * -�˴�callback�ǳ�ʼ�����ã�option���callback���ǵ��ҳ������
 * 
 * -- example --
 * $(selector).pagination({
 *     ...
 *     callback: function(api){
 *         console.log('���ҳ����øûص�'); //������ӿں������������ÿ�ε������һ��
 *     }
 * }, function(){
 *     console.log('��ʼ��'); //�����ʼ��ʱ���øûص������������һ�νӿ�����ʼ����ҳ����
 * });
 */
;
(function (factory) {
    if (typeof define === "function" && (define.amd || define.cmd) && !jQuery) {
        // AMD��CMD
        define(["jquery"], factory);
    } else if (typeof module === 'object' && module.exports) {
        // Node/CommonJS
        module.exports = function (root, jQuery) {
            if (jQuery === undefined) {
                if (typeof window !== 'undefined') {
                    jQuery = require('jquery');
                } else {
                    jQuery = require('jquery')(root);
                }
            }
            factory(jQuery);
            return jQuery;
        };
    } else {
        //Browser globals
        factory(jQuery);
    }
}(function ($) {

    //���ò���
    var defaults = {
        totalData: 0, //����������
        showData: 0, //ÿҳ��ʾ������
        pageCount: 9, //��ҳ��,Ĭ��Ϊ9
        current: 1, //��ǰ�ڼ�ҳ
        prevCls: 'prev', //��һҳclass
        nextCls: 'next', //��һҳclass
        prevContent: '<', //��һҳ����
        nextContent: '>', //��һҳ����
        activeCls: 'active', //��ǰҳѡ��״̬
        coping: false, //��ҳ��βҳ
        isHide: false, //��ǰҳ��Ϊ0ҳ����1ҳʱ����ʾ��ҳ
        homePage: '', //��ҳ�ڵ�����
        endPage: '', //βҳ�ڵ�����
        keepShowPN: false, //�Ƿ�һֱ��ʾ��һҳ��һҳ
        count: 3, //��ǰҳǰ���ҳ����
        jump: false, //��ת��ָ��ҳ��
        jumpIptCls: 'jump-ipt', //�ı�������
        jumpBtnCls: 'jump-btn', //��ת��ť
        jumpBtn: '��ת', //��ת��ť�ı�
        callback: function () { } //�ص�
    };

    var Pagination = function (element, options) {
        //ȫ�ֱ���
        var opts = options, //����
            current, //��ǰҳ
            $document = $(document),
            $obj = $(element); //����

        /**
         * ������ҳ��
         * @param {int} page ҳ��
         * @return opts.pageCount ��ҳ������
         */
        this.setPageCount = function (page) {
            return opts.pageCount = page;
        };

        /**
         * ��ȡ��ҳ��
         * �����������������ÿҳ��ʾ�����������Զ�������ҳ�����Թ���ҳ�����ã���֮
         * @return {int} ��ҳ��
         */
        this.getPageCount = function () {
            return opts.totalData && opts.showData ? Math.ceil(parseInt(opts.totalData) / opts.showData) : opts.pageCount;
        };

        /**
         * ��ȡ��ǰҳ
         * @return {int} ��ǰҳ��
         */
        this.getCurrent = function () {
            return current;
        };

        /**
         * �������
         * @param {int} ҳ��
         */
        this.filling = function (index) {
            var html = '';
            current = parseInt(index) || parseInt(opts.current); //��ǰҳ��
            var pageCount = this.getPageCount(); //��ȡ����ҳ��
            if (opts.keepShowPN || current > 1) { //��һҳ
                html += '<a href="javascript:;" class="' + opts.prevCls + '">' + opts.prevContent + '</a>';
            } else {
                if (opts.keepShowPN == false) {
                    $obj.find('.' + opts.prevCls) && $obj.find('.' + opts.prevCls).remove();
                }
            }
            if (current >= opts.count + 2 && current != 1 && pageCount != opts.count) {
                var home = opts.coping && opts.homePage ? opts.homePage : '1';
                html += opts.coping ? '<a href="javascript:;" data-page="1">' + home + '</a><span>...</span>' : '';
            }
            var start = (current - opts.count) <= 1 ? 1 : (current - opts.count);
            var end = (current + opts.count) >= pageCount ? pageCount : (current + opts.count);
            for (; start <= end; start++) {
                if (start <= pageCount && start >= 1) {
                    if (start != current) {
                        html += '<a href="javascript:;" data-page="' + start + '">' + start + '</a>';
                    } else {
                        html += '<span class="' + opts.activeCls + '">' + start + '</span>';
                    }
                }
            }
            if (current + opts.count < pageCount && current >= 1 && pageCount > opts.count) {
                var end = opts.coping && opts.endPage ? opts.endPage : pageCount;
                html += opts.coping ? '<span>...</span><a href="javascript:;" data-page="' + pageCount + '">' + end + '</a>' : '';
            }
            if (opts.keepShowPN || current < pageCount) { //��һҳ
                html += '<a href="javascript:;" class="' + opts.nextCls + '">' + opts.nextContent + '</a>'
            } else {
                if (opts.keepShowPN == false) {
                    $obj.find('.' + opts.nextCls) && $obj.find('.' + opts.nextCls).remove();
                }
            }
            html += opts.jump ? '<input type="text" class="' + opts.jumpIptCls + '"><a href="javascript:;" class="' + opts.jumpBtnCls + '">' + opts.jumpBtn + '</a>' : '';
            $obj.empty().html(html);
        };

        //���¼�
        this.eventBind = function () {
            var that = this;
            var pageCount = that.getPageCount(); //��ҳ��
            var index = 1;
            $obj.off().on('click', 'a', function () {
                if ($(this).hasClass(opts.nextCls)) {
                    if ($obj.find('.' + opts.activeCls).text() >= pageCount) {
                        $(this).addClass('disabled');
                        return false;
                    } else {
                        index = parseInt($obj.find('.' + opts.activeCls).text()) + 1;
                    }
                } else if ($(this).hasClass(opts.prevCls)) {
                    if ($obj.find('.' + opts.activeCls).text() <= 1) {
                        $(this).addClass('disabled');
                        return false;
                    } else {
                        index = parseInt($obj.find('.' + opts.activeCls).text()) - 1;
                    }
                } else if ($(this).hasClass(opts.jumpBtnCls)) {
                    if ($obj.find('.' + opts.jumpIptCls).val() !== '') {
                        index = parseInt($obj.find('.' + opts.jumpIptCls).val());
                    } else {
                        return;
                    }
                } else {
                    index = parseInt($(this).data('page'));
                }
                that.filling(index);
                typeof opts.callback === 'function' && opts.callback(that);
            });
            //������ת��ҳ��
            $obj.on('input propertychange', '.' + opts.jumpIptCls, function () {
                var $this = $(this);
                var val = $this.val();
                var reg = /[^\d]/g;
                if (reg.test(val)) $this.val(val.replace(reg, ''));
                (parseInt(val) > pageCount) && $this.val(pageCount);
                if (parseInt(val) === 0) $this.val(1); //��СֵΪ1
            });
            //�س���תָ��ҳ��
            $document.keydown(function (e) {
                if (e.keyCode == 13 && $obj.find('.' + opts.jumpIptCls).val()) {
                    var index = parseInt($obj.find('.' + opts.jumpIptCls).val());
                    that.filling(index);
                    typeof opts.callback === 'function' && opts.callback(that);
                }
            });
        };

        //��ʼ��
        this.init = function () {
            this.filling(opts.current);
            this.eventBind();
            if (opts.isHide && this.getPageCount() == '1' || this.getPageCount() == '0') $obj.hide();
        };
        this.init();
    };

    $.fn.pagination = function (parameter, callback) {
        if (typeof parameter == 'function') { //����
            callback = parameter;
            parameter = {};
        } else {
            parameter = parameter || {};
            callback = callback || function () { };
        }
        var options = $.extend({}, defaults, parameter);
        return this.each(function () {
            var pagination = new Pagination(this, options);
            callback(pagination);
        });
    };

}));