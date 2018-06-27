
; (function (dj, j) {
    dj.dyAirportTable = function (options) {
        this.rowIndex = 1;
        this.el = options.id;
        this.maxRowCount = 10;
        this.minRowCount = 1;
        this.listid=options.listid;
        this.conid = options.conid;
        this.airportdata = options.airportdata || [];
        var that = this;
        this.clear();
        j("#" + options.addid).click(function () {
            that.addAirportRow();
        });
        j("#" + options.minusid).click(function () {
            that.minusAirportRow();
        });
    }
    dj.dyAirportTable.prototype = {
        addAirportRow: function () {
            if (this.rowIndex < this.maxRowCount) {
                this.rowIndex++;
                j("#" + this.conid).append("<tr><td class=\"formValue\"><input id='AirportName" + this.rowIndex + "'  name='AirportName" + this.rowIndex + "'  type=\"text\" required=\"true\" maxlength=\"50\" style=\"height: 25px;width:200px\"/></td><td class=\"formValue\"><input id='CodeF" + this.rowIndex + "' name='CodeF" + this.rowIndex + "'  type=\"text\" maxlength=\"50\"  style=\"height: 25px;width:200px\"/></td><td class=\"formValue\"><input id='LatLong" + this.rowIndex + "' name='LatLong" + this.rowIndex + "'  type=\"text\" maxlength=\"50\" style=\"height: 25px;width:200px\"/></td></tr>");
            }
            else {
                $.modalAlert('行数不能超过' + this.maxRowCount, 'warning');
            }
        },
        minusAirportRow: function () {
            if (this.rowIndex > this.minRowCount) {
                this.rowIndex--;
                j("#" + this.conid + " tr:eq(" + this.rowIndex + ")").remove();

            }

        },
        init: function () {
            //   this.rowIndex = this.airportdata.length;
            var that = this;
            j.each(that.airportdata, function (n, value) {
                if (n > 0) {
                    that.addAirportRow();
                }
                that.tempN = n + 1;
                j("#AirportName" + that.tempN).val(value.Name); 
                j("#CodeF" + that.tempN).val(value.Code4);
                j("#LatLong" + that.tempN).val(value.LatLong);
            });

        },
        getJsonData: function () { 
            var data = {
                    airportArray: []
            };
            j("#" + this.conid + " tr").each(function (n, value) {
                if (j("#AirportName" + (n + 1)).val().trim() != "" || j("#LatLong" + (n + 1)).val().trim() != "") {
                    data.airportArray.push({ "AirportName": j("#AirportName" + (n + 1)).val(), "LatLong": j("#LatLong" + (n + 1)).val() });
                }
            });
           return JSON.stringify(data);      
        },
        clear: function () {
            j("#" + this.conid + " tr:gt(0)").remove();

        }
    }
})(window.dj, window.jQuery)
