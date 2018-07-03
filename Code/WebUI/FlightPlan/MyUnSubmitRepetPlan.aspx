<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyUnSubmitRepetPlan.aspx.cs" Inherits="FlightPlan_MyUnSubmitRepetPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link href="../css/fademap.css" rel="stylesheet" type="text/css" />
  <%-- <script type="text/javascript" src="/Content/JS/BMapInit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   <%-- <div class="gridsearch">
        <label>用户名：</label>
        <input type="text" id="s_UserName" name="s_UserName" class="easyui-validatebox" />
        <input type="button" class="searchbotton easyui-linkbutton " style="margin-left:60px" iconcls="icon-search" value="查询" onclick="Main.FunSearchData()" />
    </div>--%>
    <%--列表 start--%>
        <table id="tab_list">
        </table>
   
        <div id="tab_toolbar" style="padding: 2px 2px;" >
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="Main.OpenWin()">新增</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="Main.Delete()">删除</a>
   <%--          <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-redo" plain="true" onclick="Main.BatchImport()">导入</a>--%>
                       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-undo" plain="true" onclick="Main.Export()">导出</a>
            
            <div style="float:right">
                        <input id="ipt_search" menu="#search_menu"/>
                       <input id="ssPlanCode" name="ssPlanCode"  type="hidden" value=""/>
                        <div id="search_menu" style="width: 200px">
                            <div name="Code">
                                临专号
                            </div>
                        </div>
</div>
        </div>
    <%--列表 end--%>
  <script type="text/javascript">
      $(function () {
            Main.InitGird();
            Main.InitSearch();
          //  baiduMap.init();
            $(".fadediv").fadeToggle();
            $(".fade").click(function () {
                $(".fadediv").fadeToggle();
            });
        });
        Main = {
            //初始化表格
            InitGird: function () {
                $('#tab_list').datagrid({
                    title: '列表', //表格标题
                    url: location.href, //请求数据的页面
                    sortName: 'RepetPlanID', //排序字段
                    idField: 'RepetPlanID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 60 > 0 ? $(parent.document).find("#mainPanel").height() - 60 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序
                    frozenColumns: [[//冻结的列，不会随横向滚动轴移动
                        { field: 'cbx', checkbox: true },
                    ]],
                    columns: [[
                        { title: '公司名称', field: 'CompanyName', width: 180 },
                        { title: '任务类型', field: 'FlightType', width: 70 },
                        { title: '使用机型', field: 'AircraftType', width: 100 },       {
                            title: '执行开始时间', field: 'StartDate', width: 100, formatter: function (value, rec, index) {
                        
                            var timesstamp = new Date(value.dateValFormat());
                            return timesstamp.format("yyyy-MM-dd");
                        
                        }
                        },
                        {
                            title: '执行结束时间', field: 'EndDate', width: 100, formatter: function (value, rec, index) {
                        
                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd");
                        
                            }
                        },
                        {
                            title: '周执行计划', field: 'WeekSchedule', width: 150, formatter: function (value, rec, index) {
                                var array = [];
                                $.each(value.replace(/\*/g, '').toCharArray(), function (i, n) {
                                    array.push("星期" + n);
                                });
                              return  array.join(',');

                            }
                        },
                          { title: '机场及起降点', field: 'AirportText', width: 200 },
                          //{ title: '航线及作业区', field: 'AirlineWorkText', width: 200 },
                         { title: '创建人', field: 'CreatorName', width: 60, hidden: 'true' },
                           {
                               title: '创建时间', field: 'CreateTime', width: 120, formatter: function (value, rec, index) {

                                   var timesstamp = new Date(value.dateValFormat());
                                   return timesstamp.format("yyyy-MM-dd HH:mm:ss");

                               }
                           },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150, hidden: 'true' },

                        { title: '状态', field: 'PlanState', formatter: function (value, rec, index) { return value == 0 ? '草稿中' : '' }, width: 50 },
                        {
                            title: '操作', field: 'RepetPlanID', width: 80, formatter: function (value, rec) {
                                var str ="<a style=\"color:red\" href=\"javascript:;\" onclick=\"Main.EditData('"+value+"');$(this).parent().click();return false;\">修改</a>&nbsp;&nbsp;<a style=\"color:red\" id=\"sub-btn_'" + value + "'\" href=\"javascript:;\" onclick=\"Main.Submit('"+value+"');$(this).parent().click();return false;\">提交</a>";
                                return str;
                            }
                        }
                    ]],
                    toolbar: "#tab_toolbar",
                    queryParams: { "action": "query" },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true, //行号
                    onClickRow: function (index, row) {
                        var keyValue = row["RepetPlanID"];
                      //  zhccMap.addRepetPlan(keyValue);
                    }
                });
            },

            //初始化搜索框
            InitSearch: function () {
                $("#ipt_search").searchbox({
                    width: 250,
                    searcher: function (val, name) {
                        $('#ss' + name).val(val);
                        $('#tab_list').datagrid('options').queryParams.search_type = name;
                        $('#tab_list').datagrid('options').queryParams.search_value = val;
                        $('#tab_list').datagrid('reload');
                    },
                    prompt: '请输入要查询的信息'
                });
            },

            //打开添加窗口
            OpenWin: function () {
                $("#edit").html("");
                $("#add").dialog("open").dialog('setTitle', '新增长期计划').dialog('refresh', 'MyUnSubmitRepetPlanAdd.aspx');
            },
            //提交按钮事件
            Save: function (uid) {
                if ($("#Remark").val().length > 200)
                {
                    $.messager.alert('提示', '"其他需要说明的事项"不能超过200字符！', 'info');
                    return;
                }
                if (!$("#form_edit").form("validate")) {
                    return;
                }
               
                var fileInfo = dj.getCmp("AttchFiles").getUploadedFiles();
                $("#AttchFilesInfo").val(fileInfo);
                qx = $("input[name='WeekSchedule']").map(function () {
                    var $this = $(this);
                    if ($this.is(':checked')) {
                        return $this.val();
                    }
                    else {
                          return '*';
                    }
                }).get().join('');
                $("#btn_finish").attr("disabled", "disabled");
                var json = $.param({ "id": uid, "action": "save", "qx": qx }) + '&' + $('#form_edit').serialize();
                $.ajax({
                    type: 'post',
                    url: location.href,
                    data: json,
                    success: function (data) {
                        $.messager.alert('提示', data.msg, 'info', function () {
                                    if (data.isSuccess) {
                                        $("#tab_list").datagrid("reload");
                                        $("#edit").dialog("close");
                                    }
                                    $("#btn_add").removeAttr("disabled");
                                });
                    },
                    error: function (xhr, err) {
                        $("#btn_add").removeAttr("disabled");
                        $.messager.alert('提示', '系统繁忙，请稍后再试！', 'info');
                    }
                });
            },

            //修改链接 事件
            EditData: function (uid) {
                $("#add").html("");
                $("#edit").dialog("open").dialog('setTitle', '编辑长期计划').dialog('refresh', 'MyUnSubmitRepetPlanEdit.aspx?id=' + uid);
            },
            BatchImport: function () {
                $("#batchimport").dialog("open").dialog('setTitle', '文件导入').dialog('refresh', 'UnSubmitRepetPlanBatchImport.aspx');
            },
            Export: function () {
                var selRow = $('#tab_list').datagrid('getData');
                if (selRow.total == 0) {
                    $.messager.alert('提示', '无记录导出！', 'info');
                    return;
                }
                window.open("ExportHandler.aspx?type=1&plancode=" + $('#ssPlanCode').val());
            },
            BatchImportSumit: function () {

                var fileInfo = dj.getCmp("PlanFiles").getUploadedFiles();
                if (fileInfo == "") {
                    $.messager.alert('提示', '请先上传文件！', 'info');
                    return;
                }

                var json = $.param({ "action": "batchImport", PlanFilesPath: fileInfo });
                $.post(location.href, json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#batchimport").dialog("close");
                        }
                    });
                });
            },
            //删除按钮事件
            Delete: function () {
                var selRow = $('#tab_list').datagrid('getSelections');
                if (selRow.length == 0) {
                    $.messager.alert('提示', '请选择一条记录！', 'info');
                    return;
                }
                var idArray = [];
                for (var i = 0; i < selRow.length; i++) {
                    var id = selRow[i].RepetPlanID;
                    idArray.push(id);
                }
                $.messager.confirm('提示', '确认删除该条记录？', function (r) {
                    if (r) {
                        $.post(location.href, { "action": "del", "cbx_select": idArray.join(',') }, function (data) {

                            if (data.isSuccess) {
                                $("#tab_list").datagrid("reload");
                                selRow.length = 0;
                            }
                        });
                    }
                });
            },
   
            Submit: function (uid) {
                $.messager.confirm('提示', '确认提交该条长期计划？', function (r) {
                    if (r) {
                        $("#sub-btn_" + uid).removeAttr("onclick");
                        $.ajax({
                            type: 'post',
                            url: location.href,
                            data: { "action": "submit", "id": uid },
                            success: function (data) {
                                $.messager.alert('提示', data.msg, 'info', function () {
                                    if (data.isSuccess) {
                                        $("#tab_list").datagrid("reload");
                                    }
                                    else {
                                        $("#sub-btn_" + +uid).attr("onclick", "Main.Submit(" + uid + ")");
                                    }
                                });
                            },
                            error: function (xhr, err) {
                                $("#sub-btn_" + uid).attr("onclick", "Main.Submit(" + uid + ")");
                                $.messager.alert('提示', '系统繁忙，请稍后再试！', 'info');
                            }
                        });
                    }
                });

            }
        };
    </script>

        <%--添加 修改 start--%>
   <div id="edit" class="easyui-dialog" style="width: 1200px; height:700px;"
        modal="true" cache="false"  closed="true" >
        
    </div>
   <div id="add" class="easyui-dialog" style="width: 1200px; height:700px;"
        modal="true" cache="false" closed="true">
        
    </div>
        
     <div id="batchimport" class="easyui-dialog" style="width: 500px; height:300px;"
        modal="true" closed="true" buttons="#batchimport-buttons">
        
    </div>
    <div id="batchimport-buttons">
        <a id="btn_batchimport" href="javascript:;" onclick="Main.BatchImportSumit()" class="easyui-linkbutton">导入</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#batchimport').dialog('close');return false;">取消</a>
    </div>
        <%--添加 修改 end--%>
    <div class="fadediv"><div id="map" style="height:400px;"></div></div>
    <div class="fade"><span>地图显示/隐藏</span></div>
</asp:Content>
