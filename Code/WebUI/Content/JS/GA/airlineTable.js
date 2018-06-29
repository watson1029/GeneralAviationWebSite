
; (function (dj, j) {
    dj.dyAirlineTable = function (options) {
        this.rowIndex = 1;
        this.colIndex = 2;
        this.maxCol = options.maxCol || 2;
        this.el = options.id;
        this.maxRowCount = 10;
        this.minRowCount = 1;
        this.maxColCount = 10;
        this.minColCount = 2;
        this.listid = options.listid;
        this.conid = options.conid;
        this.airlinedata = options.airlinedata || [];
        var that = this;
        this.clear();
        j("#" + options.addid).click(function () {
            that.addAirlineRow();
        });
        j("#" + options.minusid).click(function () {
            that.minusAirlineRow();
        });
        j("#" + options.addcolid).click(function () {
            that.addAirlineCol();
        });
        j("#" + options.minuscolid).click(function () {
            that.minusAirlineCol();
        });
    }
    dj.dyAirlineTable.prototype = {
        addAirlineRow: function () {
            if (this.rowIndex < this.maxRowCount) {
                this.rowIndex++;
                var str = "<tr><td class=\"formValue\"><input id='AirlineFlyHeight_row" + this.rowIndex + "' name='AirlineFlyHeight_row" + this.rowIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:80px\"/></td>";
                for (var i = 1; i <= this.colIndex; i++) {
                    str += "<td class=\"formValue\"><input id='AirlineName_row" + this.rowIndex + "_col" + i + "'  name='AirlineName_row" + this.rowIndex + "_col" + i + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='AirlineLatLong_row" + this.rowIndex + "_col" + i + "' name='AirlineLatLong_row" + this.rowIndex + "_col" + i + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:200px\"/></td>";
                }
                str += "</tr>";
                j("#" + this.conid).append(str);
            }
            else {
                $.modalAlert('行数不能超过' + this.maxRowCount, 'warning');
            }
        },
        minusAirlineRow: function () {
            if (this.rowIndex > this.minRowCount) {
                this.rowIndex--;
                j("#" + this.conid + " tr:eq(" + this.rowIndex + ")").remove();

            }

        },
        addAirlineCol: function () {
            if (this.colIndex < this.maxColCount) {
                this.colIndex++;
                for (var i = 0; i < this.rowIndex; i++) {
                    j("#" + this.conid + " tr:eq(" + i + ") ").append("<td class=\"formValue\"><input id='AirlineName_row" + (i + 1) + "_col" + this.colIndex + "'  name='AirlineName_row" + (i + 1) + "_col" + this.colIndex + "' type=\"text\"  maxlength=\"50\" style=\"height: 25px;width:100px\"/></td><td class=\"formValue\"><input id='AirlineLatLong_row" + (i + 1) + "_col" + this.colIndex + "' name='AirlineLatLong_row" + (i + 1) + "_col" + this.colIndex + "' type=\"text\" style=\"height: 25px;width:200px\" maxlength=\"50\"/></td>");
                }
                j("#" + this.listid + " thead tr ").append("<th style=\"text-align:center\">点中文名" + this.colIndex + "</th><th style=\"text-align:center\">经纬度" + this.colIndex + "</th>");
            }
            else {
                $.modalAlert('列数不能超过' + this.maxColCount, 'warning');
            }
        },
        minusAirlineCol: function () {
            if (this.colIndex > this.minColCount) {
                this.colIndex--;
                j("#" + this.listid + " thead th:eq(-1)").remove();
                j("#" + this.listid + " thead th:eq(-1)").remove();
                for (var i = 0; i < this.rowIndex; i++) {
                    j("#" + this.conid + " tr:eq(" + i + ") td:eq(-1)").remove();
                    j("#" + this.conid + " tr:eq(" + i + ") td:eq(-1)").remove();
                }
            }
        },
        init: function () {
            var that = this;
            that.addCol = that.maxCol - that.colIndex;
            for (var i = 0; i < that.addCol; i++) {
                that.addAirlineCol();
            }
            j.each(that.airlinedata, function (n, value) {

                if (n > 0) {
                    that.addAirlineRow();
                }
                that.tempN = n + 1;
                j("#AirlineFlyHeight_row" + that.tempN).val(value.FlyHeight);
                j.each(value.pointList, function (n, value) {
                    that.tempCol = n + 1;
                    j("#AirlineName_row" + that.tempN + "_col" + that.tempCol + "").val(value.Name);
                    j("#AirlineLatLong_row" + that.tempN + "_col" + that.tempCol + "").val(value.LatLong);
                })

            });

        },
        getJsonData: function () {
            var s = {};
            var dataArray = [];      
            var that = this;
            j("#" + that.conid + " tr").each(function (index, value) {

                var airlineObj = {
                airlinePointList: [],
                FlyHeight: ""
            };
                var count = 1;
                j("#" + that.conid + " tr:eq(" + index + ") td").each(function (n, value) {
               
                    if (n == 0) {
                        airlineObj.FlyHeight = j("#AirlineFlyHeight_row" + (index + 1)).val();
                    }
                    else if (n % 2 != 0) {
                        if (j("#AirlineName_row" + (index + 1) + "_col" + (count)).val().trim() != "" || j("#AirlineLatLong_row" + (index + 1) + "_col" + (count)).val().trim() != "") {
                            airlineObj.airlinePointList.push({ "PointName": j("#AirlineName_row" + (index + 1) + "_col" + (count)).val(), "LatLong": j("#AirlineLatLong_row" + (index + 1) + "_col" + (count)).val() });
                        }
                    count++; }
                })
                dataArray.push(airlineObj);
            });
            s["airlineArray"] = dataArray;
            return JSON.stringify(s);
        },
        clear: function () {
            j("#" + this.conid + " tr:gt(0)").remove();
            j("#" + this.listid + " thead th:gt(4)").remove();
            j("#" + this.conid + " tr td:gt(4)").remove();
        }
    }
})(window.dj, window.jQuery)
