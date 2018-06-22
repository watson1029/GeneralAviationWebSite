//封装上传控件--zyb
; (function (dj, j) {
    dj.analysis = function (options) {
        var cmp = dj.getCmp(options.id);
        //if (!!cmp) {
        //    return cmp;
        //}
        dj.analysis.base.constructor.call(this, dj.analysis.defaults, options);
    }
    j.extend(dj.analysis, {
        defaults: {
            editable: true,
            uploadedFiles: "",
            uploadPath: "GA",
            queueId: "fileQueue",
            listId: "fileList",
            txtId: "textArea",
            multi: true,
            fileExt: ".jpg;.gif;.bmp;.png;.doc;.docx;.xls;.xlsx;.ppt;.pptx;.zip;.rar;.txt;.pdf",
            fileDesc: "Web Files (.jpg,.gif,.bmp,.png,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.zip,.rar,.txt,.pdf)",
            maxSize: 5,
            maxCount: 0,
            IsMust: false,
            autoUpload: false
        },
        parse: function (el, options) {
            var defaults = {
                inputWidth: 210,
                text: "浏览..."
            };
            j.extend(defaults, options);
            //将上传控件转换成上传按钮
            el = j(el).addClass("dj-upload-file");
            var templ = ['<a href="javascript:void(0)" class="dj-upload-button" >',
                            '<div class="dj-upload-filewrap">',
                            '</div>',
                        '</a>'];
            el.wrap(templ.join(''));
            var parent = el.parent(),
                hoverCls = "dj-upload-hover";
            parent.before('<span class="dj-upload-text">' + defaults.text + '</span>');
            var wrap = el.parent().parent();
            var inputW = defaults.inputWidth.toString().replace("px", "");
            var input = $('<input type="text" class="textbox dj-upload-input" readonly="readonly" style="width:' + inputW + 'px"/>');
            wrap.before(input);
            el.bind("change", function () {
                input.val(this.value);
            });
        }
    });
    dj.extend(dj.control, dj.analysis, {
        uploadFiles: function (params) {
            if ($("#" + this.hideFileId).val().trim() != "") {
                params && this.el.uploadifySettings('scriptData', params);
                this.el.uploadifyUpload();
            } else {
                $.modalAlert('请上传文件！', 'warning');
            }
        },
        url: function (url) {
            return dj.root + url;
        },
        getExts: function () {
            var arr = this.fileExt.split(';'),
                newArray = [],
                descArray = [];
            for (var i = 0; i < arr.length; i++) {
                var ext = arr[i].delStart('.');
                newArray.push("*." + ext.toLowerCase());
                newArray.push("*." + ext.toUpperCase());
                descArray.push("." + ext.toUpperCase());
            }
            this.fileDesc = "Web Files ({0})".format(descArray.join(','));
            return newArray.join(';');
        },
        getFileByte: function () {
            return this.maxSize * 1024 * 1024;
        },
        bindFiles: function (files) {
            var val = files || j("#" + this.hideFileId).val().trim();
            if (val != "") {
                var fileArray = val.split('|');
                for (var i = 0; i < fileArray.length; i++) {
                    this.updateHideFile(fileArray[i], "Upload");
                }
            }
        },
        getUploadedFiles: function () {
            return j("#" + this.hideUploadId).val();
        },
        getUploadedFileArray: function () {
            var arr = this.getUploadedFiles();
            return arr ? arr.split('|') : [];
        },
        createDom: function () {
            this.hideFileId = this.id + "-hiddenfile";
            this.hideUploadId = this.id + "-hiddenupload";
            //  this.clearQueueFile();
            j("#" + this.hideFileId).remove();
            j("#" + this.hideUploadId).remove();
            j("<input type='hidden'>").attr({ id: this.hideFileId }).appendTo('body');
            j("<input type='hidden'>").attr({ id: this.hideUploadId }).appendTo('body');
        },
        updateHideFile: function (file, type, isSelectFile) {
            //更新选择的文件集合
            var hide = j("#" + this["hide" + type + "Id"]),
                files = hide.val();
            var fs = files == "" ? [] : files.split('|');
            if (Array.has(fs, file)) {
                Array.remove(fs, file);
            } else {
                fs.push(file);
            }
            //            if (isSelectFile) {
            //                //获取已经上传的
            //                var uploadedCount = this.getUploadedFileArray().length;
            //                //获取当前选择的数量
            //                var selectedCount = fs.length; 
            //                if (uploadedCount+selectedCount >this.maxCount) {
            //                    Array.remove(fs,file);
            //                    return false;
            //                }
            //            } 
            hide.val(fs.join('|'));
            return true;
        },

        clearQueueFile: function () {
            j("#" + this.hideFileId).val('');
            //  j("#"+this.hideUpload).val('');
        },
        render: function () {
            this.createDom();
            //绑定已经上传过的文件
            this.bindFiles(this.uploadedFiles);
            if (this.editable) {
                var that = this;
                this.el = j("#" + this.id);
                this.flag = false;
                this.el.uploadify({
                    'uploader': this.url("Content/JS/JqueryUpload/uploadify2.swf"),
                    'script': dj.root + "UploadFile/Swfupload?filePath=" + this.uploadPath,
                    'cancelImg': this.url("Content/JS/JqueryUpload/cancel.png"),
                    //'folder': '@Url.Content("~/Content/JS/JqueryUpload/TempImg")',
                    'queueID': this.queueId,
                    'auto': this.autoUpload,
                    'multi': this.multi,
                    //'fileExt': '*.jpg;*.gif;*.png;*.jpeg;',
                    'fileExt': this.getExts(),
                    //'fileDesc': "Web Files (.JPG,.JPEG , .GIF, .PNG)",
                    'fileDesc': this.fileDesc,
                    'buttonText': "选择附件",
                    'sizeLimit': this.getFileByte(),
                    'buttonImg': this.url("Content/JS/JqueryUpload/selectfile.gif"),
                    'width': 75,
                    'height': 26,
                    'onComplete': function (e, queueId, fileObj, uploadFileName, other) {
                        if (uploadFileName) {
                            $.ajax({
                                url: dj.root + "FlightPlan/RepetPlanNew/ReadFileText",
                                data: { PlanFilesPath: uploadFileName },
                                dataType: "text",
                                async: false,
                                success: function (data) {
                                    $("#" + that.txtId).html(data);
                                }
                            });
                            //绑定已经上传文件
                            that.bindFiles([uploadFileName, fileObj.name].join(','));
                            //删除队列的文件
                            that.updateHideFile([queueId, fileObj.name].join(','), "File");
                        }
                    },
                    'onSelect': function (e, queueId, fileObj) {
                        var fileName = fileObj.name,
                            ext = fileObj.type || dj.path.getExtension(fileName),
                            exts = that.getExts();
                        if (exts.indexOf(ext) <= -1) {
                            $.modalAlert('上传文件格式不正确！', 'warning');
                            //that.flag=false;
                            return false;
                        }

                        if (fileObj.size > that.getFileByte()) {
                            $.modalAlert('上传文件不能大于{0}M'.format(that.maxSize), 'warning');
                            //that.flag=false;
                            return false;
                        }
                        //that.flag=true;
                        var file = [queueId, fileName].join(",");
                        that.updateHideFile(file, "File");
                    },
                    'onCancel': function (e, queueId, fileObj, data) {
                        var file = [queueId, fileObj.name].join(",");
                        that.updateHideFile(file, "File");
                    },
                    'onError': function () {

                    }
                });
            }
        }
    });
})(window.dj, window.jQuery);