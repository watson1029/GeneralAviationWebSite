﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyUnSubmitRepetPlan.aspx.cs" Inherits="FlightPlanNew_MyUnSubmitRepetPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
   
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
            
            <div style="float:right">
                        <input id="ipt_search" menu="#search_menu"/>
                       <input id="ssPlanCode" name="ssPlanCode"  type="hidden" value=""/>
                        <div id="search_menu" style="width: 200px">
                            <div name="PlanCode">
                                申请单号
                            </div>
                        </div>
</div>
        </div>
    <%--列表 end--%>

  <script type="text/javascript">
      $(function () {
            Main.InitGird();
            Main.InitSearch();
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
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
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
                        { title: '申请单号', field: 'PlanCode', width: 180 },
                        { title: '公司名称', field: 'CompanyName', width: 120 },
                        { title: '任务类型', field: 'FlightType', width: 100 },
                        { title: '航班号', field: 'CallSign', width: 100 },
                        { title: '航空器类型', field: 'AircraftType', width: 100 },
                        { title: '机场及起降点', field: 'Airport', width: 120 },
                        { title: '航线及作业区', field: 'FlightArea', width: 120 },
                        {
                            title: '预计开始时间', field: 'StartDate', width: 120, formatter: function (value, rec, index) {
                        
                            var timesstamp = new Date(value.dateValFormat());
                            return timesstamp.format("yyyy-MM-dd");
                        
                        } },
                        {
                            title: '预计结束时间', field: 'EndDate', width: 120, formatter: function (value, rec, index) {
                        
                                var timesstamp = new Date(value.dateValFormat());
                                return timesstamp.format("yyyy-MM-dd");
                        
                            }
                        },  
                         { title: '创建人', field: 'CreatorName', width: 60, hidden: 'true' },
                        { title: '状态', field: 'Status', formatter: function (value, rec, index) { return (value == 1 ? '草稿中' : (value == 2 ? '已提交' : (value == 3 ? '审核通过' : '审核不通过'))) }, width: 50 },
   { title: '审核意见', field: 'AuditComment', width: 140 },
                   {
                            title: '操作', field: 'RepetPlanID', width: 80, formatter: function (value, rec) {
                                if (rec.Status == 1) {
                                    return '<a style="color:red" href="javascript:;" onclick="Main.EditData(' + value + ');$(this).parent().click();return false;">修改</a>&nbsp;&nbsp;<a style="color:red" id="sub-btn_' + value + '" href="javascript:;" onclick="Main.Submit(' + value + ');$(this).parent().click();return false;">提交</a>';
                                }
                                else {
                                    return '';
                                }
                            }
                        }
                    ]],
                    toolbar: "#tab_toolbar",
                    queryParams: { "action": "query" },
                    pagination: true, //是否开启分页
                    pageNumber: 1, //默认索引页
                    pageSize: 10, //默认一页数据条数
                    rownumbers: true //行号
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
                $("#edit").dialog("open").dialog('setTitle', '新增长期计划').dialog('refresh', 'MyUnSubmitRepetPlanAdd.aspx');
                $("#btn_add").attr("onclick", "Main.Save();")
            },
            //提交按钮事件
            Save: function (uid) {
                if (!$("#form_edit").form("validate")) {
                    return;
                }               
                var fileInfo = dj.getCmp("AttchFiles").getUploadedFiles();
                $("#AttchFilesInfo").val(fileInfo);
                $("#btn_add").attr("disabled", "disabled");
                var json = $.param({ "id": uid, "action": "save" }) + '&' + $('#form_edit').serialize();
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
                $("#edit").dialog("open").dialog('setTitle', '编辑长期计划').dialog('refresh', 'MyUnSubmitRepetPlanAdd.aspx?id=' + uid);
                $("#btn_add").attr("onclick", "Main.Save(" + uid + ");")
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
   <div id="edit" class="easyui-dialog" style="width: 850px; height:600px;"
        modal="true" closed="true" buttons="#edit-buttons">
        
    </div>
    <div id="edit-buttons">
        <a id="btn_add" href="javascript:;" class="easyui-linkbutton">保存</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#edit').dialog('close');return false;">取消</a>
    </div>

     <div id="batchimport" class="easyui-dialog" style="width: 500px; height:300px;"
        modal="true" closed="true" buttons="#batchimport-buttons">
        
    </div>
    <div id="batchimport-buttons">
        <a id="btn_batchimport" href="javascript:;" onclick="Main.BatchImportSumit()" class="easyui-linkbutton">导入</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#batchimport').dialog('close');return false;">取消</a>
    </div>
        <%--添加 修改 end--%>
</asp:Content>
