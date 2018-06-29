
; (function (dj, j) {
    dj.dyWorkTable = function (options) {
        this.cRowIndex = 1;
        this.pRowIndex = 1;
        this.hRowIndex = 1;
        this.pColIndex = 2;
        this.hColIndex = 2;
        this.el = options.id;
        this.cMaxRowCount = 10;
        this.cMinRowCount = 1;
        this.pMaxRowCount = 10;
        this.pMinRowCount = 1;
        this.pMaxColCount = 10;
        this.pMinColCount = 2;
        this.hMaxRowCount = 10;
        this.hMinRowCount = 1;
        this.hMaxColCount = 10;
        this.hMinColCount = 2;
        this.pworkMaxCol = options.pworkMaxCol || 2;
        this.hworkMaxCol = options.hworkMaxCol || 2;
        this.cconid = options.cconid;
        this.pconid = options.pconid;
        this.hconid = options.hconid;
        this.plistid = options.plistid;
        this.hlistid = options.hlistid;
        this.cworkdata = options.cworkdata || [];
        this.pworkdata = options.pworkdata || [];
        this.hworkdata = options.hworkdata || [];
        var that = this;
        this.clear();
        j("#" + options.caddid).click(function () {
            that.addCWorkRow();
        });
        j("#" + options.cminusid).click(function () {
            that.minusCWorkRow();
        });
        j("#" + options.paddid).click(function () {
            that.addPWorkRow();
        });
        j("#" + options.pminusid).click(function () {
            that.minusPWorkRow();
        });
        j("#" + options.paddcolid).click(function () {
            that.addPWorkCol();
        });
        j("#" + options.pminuscolid).click(function () {
            that.minusPWorkCol();
        });
        j("#" + options.haddcolid).click(function () {
            that.addHWorkCol();
        });
        j("#" + options.hminuscolid).click(function () {
            that.minusHWorkCol();
        });
        j("#" + options.haddid).click(function () {
            that.addHWorkRow();
        });
        j("#" + options.hminusid).click(function () {
            that.minusHWorkRow();
        });
    }
    dj.dyWorkTable.prototype = {
        addCWorkRow: function () {
            if (this.cRowIndex < this.cMaxRowCount) {
                this.cRowIndex++;
                j("#" + this.cconid).append("<tr><td class=\"formValue\"><input id='CFlyHeight" + this.cRowIndex + "' name='CFlyHeight" + this.cRowIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:80px\"/></td><td class=\"formValue\"><input id='CRadius" + this.cRowIndex + "' name='CRadius" + this.cRowIndex + "' type=\"text\" maxlength=\"50\" style=\"height: 25px;width:80px\"/></td><td class=\"formValue\"><input id='CWorkName" + this.cRowIndex + "'  name='CWorkName" + this.cRowIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='CLatLong" + this.cRowIndex + "' name='CLatLong" + this.cRowIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:200px\"/></td></tr>");
            }
            else {
                $.modalAlert('行数不能超过' + this.cMaxRowCount, 'warning');
            }
        },
        minusCWorkRow: function () {
            if (this.cRowIndex > this.cMinRowCount) {
                this.cRowIndex--;
                j("#" + this.cconid + " tr:eq(" + this.cRowIndex + ")").remove();
            }
        },
        addPWorkRow: function () {
            if (this.pRowIndex < this.pMaxRowCount) {
                this.pRowIndex++;
                var str = "<tr><td class=\"formValue\"><input id='PFlyHeight_row" + this.pRowIndex + "' name='PFlyHeight_row" + this.pRowIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:80px\"/></td>";
                for (var i = 1; i <= this.pColIndex; i++) {
                    str += "<td class=\"formValue\"><input id='PWorkName_row" + this.pRowIndex + "_col" + i + "'  name='PWorkName_row" + this.pRowIndex + "_col" + i + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='PWorkLatLong_row" + this.pRowIndex + "_col" + i + "' name='PWorkLatLong_row" + this.pRowIndex + "_col" + i + "' stype=\"text\" maxlength=\"50\" style=\"height: 25px;width:200px\"/></td>";
                }
                str += "</tr>";
                j("#" + this.pconid).append(str);
            }
            else {
                $.modalAlert('行数不能超过' + this.pMaxRowCount, 'warning');
            }
        },
        minusPWorkRow: function () {
            if (this.pRowIndex > this.pMinRowCount) {
                this.pRowIndex--;
                j("#" + this.pconid + " tr:eq(" + this.pRowIndex + ")").remove();

            }

        },
        addPWorkCol: function () {
            if (this.pColIndex < this.pMaxColCount) {
                this.pColIndex++;
                for (var i = 0; i < this.pRowIndex; i++) {
                    j("#" + this.pconid + " tr:eq(" + i + ") ").append("<td class=\"formValue\"><input id='PWorkName_row" + (i + 1) + "_col" + this.pColIndex + "'  name='PWorkName_row" + (i + 1) + "_col" + this.pColIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='PWorkLatLong_row" + (i + 1) + "_col" + this.pColIndex + "' name='PWorkLatLong_row" + (i + 1) + "_col" + this.pColIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:200px\"/></td>");
                }
                j("#" + this.plistid + " thead tr ").append("<th style=\"text-align:center\">中文名" + this.pColIndex + "</th><th style=\"text-align:center\">经纬度" + this.pColIndex + "</th>");
            }
            else {
                $.modalAlert('列数不能超过' + this.pMaxColCount, 'warning');
            }
        },
        minusPWorkCol: function () {
            if (this.pColIndex > this.pMinColCount) {
                this.pColIndex--;
                j("#" + this.plistid + " thead th:eq(-1)").remove();
                j("#" + this.plistid + " thead th:eq(-1)").remove();
                for (var i = 0; i < this.pRowIndex; i++) {
                    j("#" + this.pconid + " tr:eq(" + i + ") td:eq(-1)").remove();
                    j("#" + this.pconid + " tr:eq(" + i + ") td:eq(-1)").remove();
                }
            }
        },
        addHWorkRow: function () {
            if (this.hRowIndex < this.hMaxRowCount) {
                this.hRowIndex++;
                var str = "<tr><td class=\"formValue\"><input id='HFlyHeight_row" + this.hRowIndex + "' name='HFlyHeight_row" + this.hRowIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:80px\"/></td>";
                str += "<td class=\"formValue\"><input id='HDistance_row" + this.hRowIndex + "' name='HDistance_row" + this.hRowIndex + "' type=\"text\" maxlength=\"50\" style=\"height: 25px;width:80px\"/></td>";
                for (var i = 1; i <= this.hColIndex; i++) {
                    str += "<td class=\"formValue\"><input id='HWorkName_row" + this.hRowIndex + "_col" + i + "'  name='HWorkName_row" + this.hRowIndex + "_col" + i + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='HWorkLatLong_row" + this.hRowIndex + "_col" + i + "' name='HWorkLatLong_row" + this.hRowIndex + "_col" + i + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:200px\"/></td>";
                }
                str += "</tr>";
                j("#" + this.hconid).append(str);
            }
            else {
                $.modalAlert('行数不能超过' + this.hMaxRowCount, 'warning');
            }
        },
        minusHWorkRow: function () {
            if (this.hRowIndex > this.hMinRowCount) {
                this.hRowIndex--;
                j("#" + this.hconid + " tr:eq(" + this.hRowIndex + ")").remove();

            }

        },
        addHWorkCol: function () {
            if (this.hColIndex < this.hMaxColCount) {
                this.hColIndex++;
                for (var i = 0; i < this.hRowIndex; i++) {
                    j("#" + this.hconid + " tr:eq(" + i + ") ").append("<td class=\"formValue\"><input id='HWorkName_row" + (i + 1) + "_col" + this.hColIndex + "'  name='HWorkName_row" + (i + 1) + "_col" + this.hColIndex + "' maxlength=\"50\"  style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='HWorkLatLong_row" + (i + 1) + "_col" + this.hColIndex + "' name='HWorkLatLong_row" + (i + 1) + "_col" + this.hColIndex + "' type=\"text\"  maxlength=\"50\"  style=\"height: 25px;width:200px\"/></td>");
                }
                j("#" + this.hlistid + " thead tr ").append("<th style=\"text-align:center\">中文名" + this.hColIndex + "</th><th style=\"text-align:center\">经纬度" + this.hColIndex + "</th>");
            }
            else {
                $.modalAlert('列数不能超过' + this.hMaxColCount, 'warning');
            }
        },
        minusHWorkCol: function () {
            if (this.hColIndex > this.hMinColCount) {
                this.hColIndex--;
                j("#" + this.hlistid + " thead th:eq(-1)").remove();
                j("#" + this.hlistid + " thead th:eq(-1)").remove();
                for (var i = 0; i < this.hRowIndex; i++) {
                    j("#" + this.hconid + " tr:eq(" + i + ") td:eq(-1)").remove();
                    j("#" + this.hconid + " tr:eq(" + i + ") td:eq(-1)").remove();
                }
            }
        },

        init: function () {
            var that = this;
            //遍历圆作业区
            j.each(that.cworkdata, function (n, value) {
                if (n > 0) {
                    that.addCWorkRow();
                }
                if (value.pointList.length > 0) {
                    that.tempN = n + 1;
                    j("#CFlyHeight" + that.tempN).val(value.FlyHeight);
                    j("#CRadius" + that.tempN).val(value.Raidus);
                    j("#CWorkName" + that.tempN).val(value.pointList[0].Name);
                    j("#CLatLong" + that.tempN).val(value.pointList[0].LatLong);
                }
            });
            //遍历点作业区
            that.addCol = that.pworkMaxCol - that.pColIndex;
            for (var i = 0; i < that.addCol; i++) {
                that.addPWorkCol();
            }
            j.each(that.pworkdata, function (n, value) {
                if (n > 0) {
                    that.addPWorkRow();
                }
                that.tempN = n + 1;
                j("#PFlyHeight_row" + that.tempN).val(value.FlyHeight);
                j.each(value.pointList, function (n, value) {
                    that.tempCol = n + 1;
                    j("#PWorkName_row" + that.tempN + "_col" + that.tempCol + "").val(value.Name);
                    j("#PWorkLatLong_row" + that.tempN + "_col" + that.tempCol + "").val(value.LatLong);
                })

            });

            //遍历线作业区
            that.addCol = that.hworkMaxCol - that.hColIndex;
            for (var i = 0; i < that.addCol; i++) {
                that.addHWorkCol();
            }
            j.each(that.hworkdata, function (n, value) {
                if (n > 0) {
                    that.addHWorkRow();
                }
                that.tempN = n + 1;
                j("#HFlyHeight_row" + that.tempN).val(value.FlyHeight);
                j("#HDistance_row" + that.tempN).val(value.Raidus);
                j.each(value.pointList, function (n, value) {
                    that.tempCol = n + 1;
                    j("#HWorkName_row" + that.tempN + "_col" + that.tempCol + "").val(value.Name);
                    j("#HWorkLatLong_row" + that.tempN + "_col" + that.tempCol + "").val(value.LatLong);
                })

            });
        },
        getCWorkJsonData: function () {
            var s = {};
            var dataArray = [];
         
            j("#" + this.cconid + " tr ").each(function (index) {
                if (j("#CWorkName" + (index + 1)).val().trim() != "" || j("#CLatLong" + (index + 1)).val().trim() != "")
                    {
                    var workObj = {
                        airlinePointList: [],
                        flyHeight: "",
                        radius: ""
                    };
                    workObj.flyHeight = j("#CFlyHeight" + (index + 1)).val();
                    workObj.radius = j("#CRadius" + (index + 1)).val();
                    workObj.airlinePointList.push({ "PointName": j("#CWorkName" + (index + 1)).val(), "LatLong": j("#CLatLong" + (index + 1)).val() });
                    dataArray.push(workObj);
                }
            });
            s["airlineArray"] = dataArray;
            return JSON.stringify(s);
        },
        getPWorkJsonData: function () {
            var s = {};
            var dataArray = [];          
            var that = this;
            j("#" + that.pconid + " tr ").each(function (index) {
                var workObj = {
                    airlinePointList: [],
                FlyHeight: ""
            };
                var count = 1;
                j("#" + that.pconid + " tr:eq(" + index + ") td").each(function (n, value) {
                    if (n == 0) {
                        workObj.FlyHeight = j("#PFlyHeight_row" + (index + 1)).val();
                    }
                    else if (n % 2 != 0) {
                        if (j("#PWorkName_row" + (index + 1) + "_col" + (count)).val().trim() != "" || j("#PWorkLatLong_row" + (index + 1) + "_col" + (count)).val().trim() != "") {
                            workObj.airlinePointList.push({ "PointName": j("#PWorkName_row" + (index + 1) + "_col" + (count)).val(), "LatLong": j("#PWorkLatLong_row" + (index + 1) + "_col" + (count)).val() });
                        }
                        count++;
                    }
                    
                })
                
                dataArray.push(workObj);
            });
            s["airlineArray"] = dataArray;
            return JSON.stringify(s);
        },
        getHWorkJsonData: function () {
            var s = {};
            var dataArray = [];          
            var that = this;
            j("#" + that.hconid + " tr ").each(function (index) {
                var workObj = {
                    airlinePointList: [],
                FlyHeight: "",
                radius: ""
            };
                var count = 1;
                j("#" + that.hconid + " tr:eq(" + index + ") td").each(function (n, value) {
                    if (n == 0) {
                        workObj.FlyHeight = j("#HFlyHeight_row" + (index + 1)).val();
                    }
                    else if (n == 1) {
                        workObj.radius = j("#HDistance_row" + (index + 1)).val();
                    }
                    else if (n % 2 == 0) {
                        if (j("#HWorkName_row" + (index + 1) + "_col" + (count)).val().trim() != "" || j("#HWorkLatLong_row" + (index + 1) + "_col" + (count)).val().trim() != "") {
                            workObj.airlinePointList.push({ "PointName": j("#HWorkName_row" + (index + 1) + "_col" + (count)).val(), "LatLong": j("#HWorkLatLong_row" + (index + 1) + "_col" + (count)).val() });
                        }
                   count++; }
                    
                })
                dataArray.push(workObj);
            });
            s["airlineArray"] = dataArray;
            return JSON.stringify(s);
        },
        clear: function () {
            j("#" + this.cconid + " tr:gt(0)").remove();
            j("#" + this.pconid + " tr:gt(0)").remove();
            j("#" + this.hconid + " tr:gt(0)").remove();

        }
    }
})(window.dj, window.jQuery)
