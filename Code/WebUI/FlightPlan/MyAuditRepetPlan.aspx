<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeFile="MyAuditRepetPlan.aspx.cs" Inherits="FlightPlan_MyAuditRepetPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .table_edit tr {
        line-height:20px;
        }

    </style>
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
    <div id="tab_toolbar" style="padding: 2px 2px;height:22px;">
            <a href="javascript:void(0)" class="easyui-button"  plain="true"></a>
        <div style="float: right">
            <input id="ipt_search" menu="#search_menu" />
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
                    sortName: 'ID', //排序字段
                    idField: 'ID', //标识字段,主键
                    iconCls: '', //标题左边的图标
                    width: '99%', //宽度
                    height: $(parent.document).find("#mainPanel").height() - 10 > 0 ? $(parent.document).find("#mainPanel").height() - 10 : 300, //高度
                    nowrap: false, //是否换行，True 就会把数据显示在一行里
                    striped: true, //True 奇偶行使用不同背景色
                    singleSelect: false,
                    collapsible: false, //可折叠
                    sortOrder: 'desc', //排序类型
                    remoteSort: true, //定义是否从服务器给数据排序

                    columns: [[
                        { title: '申请单号', field: 'PlanCode', width: 180 },
                        { title: '任务类型', field: 'FlightType', width: 60 },
                        { title: '使用机型', field: 'AircraftType', width: 80 },
                         { title: '航空器呼号', field: 'CallSign', width: 70 },
                        { title: '航线走向和飞行高度', field: 'FlightDirHeight', width: 150 },
                        { title: '预计开始时间', field: 'StartDate', width: 100 },
                        { title: '预计结束时间', field: 'EndDate', width: 100 },
                        { title: '起飞时刻', field: 'SOBT', width: 100 },
                        { title: '降落时刻', field: 'SIBT', width: 100 },
                        { title: '起飞机场', field: 'ADEP', width: 80 },
                        { title: '降落机场', field: 'ADES', width: 80 },
                        {
                            title: '周执行计划', field: 'WeekSchedule', width: 150, formatter: function (value, rec, index) {
                                var array = [];
                                $.each(value.replace(/\*/g, '').toCharArray(), function (i, n) {

                                    array.push("星期" + n);
                                });
                                return array.join(',');

                            }
                        },
                        { title: '公司三字码', field: 'CompanyCode3', width: 100 },
                         { title: '创建人', field: 'CreatorName', width: 60 },
                          { title: '其他需要说明的事项', field: 'Remark', width: 150 },
                             {
                                 title: '操作', field: 'RepetPlanID', width: 80, formatter: function (value, rec) {
                                     var str = '<a style="color:red" href="javascript:;" onclick="Main.Audit(' + value + ');$(this).parent().click();return false;">审核</a>';
                                     return str;
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
                        $('#tab_list').datagrid('options').queryParams.search_type = name;
                        $('#tab_list').datagrid('options').queryParams.search_value = val;
                        $('#tab_list').datagrid('reload');
                    },
                    prompt: '请输入要查询的信息'
                });
            },
            //审核
            Audit: function (uid) {
                $("#audit").dialog("open").dialog('setTitle', '审核');
                $("#btn_audit").attr("onclick", "Main.AuditSubmit(" + uid + ");")
                $.post(location.href, { "action": "queryone", "id": uid }, function (data) {
                //    $("#form_audit").form('load', data);
                    $("#FlightType").html(data.FlightType);
                    $("#AircraftType").html(data.AircraftType);
                    $("#FlightDirHeight").html(data.FlightDirHeight);
                    $("#ADEP").html(data.ADEP);
                    $("#ADES").html(data.ADES);
                    $("#StartDate").html(new Date(data.StartDate).toLocaleDateString());
                    $("#EndDate").html(new Date(data.EndDate).toLocaleDateString());
                    $("#SOBT").html(data.SOBT);
                    $("#SIBT").html(data.SIBT);
                    $("#Remark").html(data.Remark);
                    var fileArray = data.AttchFile.split('|');
                    for (var i = 0; i < fileArray.length; i++) {
                        var info = fileArray[i].split(','),
                        filepath = dj.root + info[0];
                        $("#AttchFile").html('<a href="{0}" target="_blank" class="upload-filename" title="{1}">{2}</a>'.format(filepath, info[1], info[1]));
                    }
                    $.each(data.WeekSchedule.replace(/\*/g, '').toCharArray(), function (i, n) {
                        $("#d" + n).attr("checked", true);
                    });


                });
            },
            AuditSubmit: function (uid) {

                if (!$("#form_audit").form("validate")) {
                    return;
                }
                var json = $.param({ "id": uid, "action": "auditsubmit" }) + '&' + $('#form_audit').serialize();

                $.post(location.href, json, function (data) {
                    $.messager.alert('提示', data.msg, 'info', function () {
                        if (data.isSuccess) {
                            $("#tab_list").datagrid("reload");
                            $("#audit").dialog("close");
                        }
                    });
                });

            }

        };
    </script>

      <%--添加 修改 start--%>
   <div id="audit" class="easyui-dialog" style="width: 700px; height:700px;"
        modal="true" closed="true" buttons="#audit-buttons">
        <form id="form_audit"  method="post">
                <table class="table_edit">
      
                    <tr>
                        <td class="tdal">任务类型：
                        </td>
                        <td class="tdar" id="FlightType">
                       
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">航空器类型：
                        </td>
                        <td class="tdar" id="AircraftType">
              
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">航线走向和飞行高度：
                        </td>
                        <td class="tdar" id="FlightDirHeight">
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">起飞机场：
                        </td>
                        <td class="tdar" id="ADEP">
                        </td>

                    </tr>
                         <tr>
                        <td class="tdal">降落机场：
                        </td>
                        <td class="tdar" id="ADES">
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">预计开始日期：
                        </td>
                        <td class="tdar" id="StartDate">
                        </td>

                    </tr>
                      <tr>
                        <td class="tdal">预计结束日期：
                        </td>
                        <td class="tdar"  id="EndDate">
                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">起飞时刻：
                        </td>
                        <td class="tdar" id="SOBT">

                        </td>

                    </tr>
                       <tr>
                        <td class="tdal">降落时刻：
                        </td>
                        <td class="tdar"  id="SIBT">
                        </td>

                    </tr>
                          <tr>
                        <td class="tdal">批件：
                        </td>
                        <td class="tdar" id="AttchFile">

                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">周执行计划：
                        </td>
                        <td class="tdar">
                             <input id="d1" type="checkbox" disabled="disabled" name="WeekSchedule" value="1" />星期一
                             <input id="d2" type="checkbox" disabled="disabled" name="WeekSchedule" value="2" />星期二
                             <input id="d3" type="checkbox" disabled="disabled" name="WeekSchedule" value="3" />星期三
                             <input id="d4" type="checkbox" disabled="disabled" name="WeekSchedule" value="4" />星期四
                             <input id="d5" type="checkbox" disabled="disabled" name="WeekSchedule" value="5" />星期五
                             <input id="d6" type="checkbox" disabled="disabled" name="WeekSchedule" value="6" />星期六
                             <input id="d7" type="checkbox" disabled="disabled" name="WeekSchedule" value="7" />星期七
                        </td>

                    </tr>
                    <tr>
                        <td class="tdal">其他需要说明的事项：
                        </td>
                        <td class="tdar" id="Remark">
                        </td>

                    </tr>
                      <tr>   
                        <td class="tdal">审核结果：
                        </td>
                        <td class="tdar">
                            <select class="easyui-combobox" editable="false"  name="Auditresult" required="true"   panelheight="auto" style="width:100%;">   
                                <option value="0" selected="true">通过</option>
                                <option value="1">不通过</option>
                            </select>
                        </td>
</tr>
                      <tr>
                        <td class="tdal">审核意见：
                        </td>
                        <td class="tdar">
                            <input id="AuditComment" name="AuditComment"  required="true" maxlength="400" style="width:400px;height:100px" type="text" data-options="multiline:true"  class="easyui-textbox" />
                        </td>

                    </tr>
                </table>

        </form>
    </div>
    <div id="audit-buttons">
        <a id="btn_audit" href="javascript:;" class="easyui-linkbutton">提交</a> <a href="javascript:;"
            class="easyui-linkbutton"  onclick="$('#audit').dialog('close');return false;">取消</a>
    </div>
  
</asp:Content>
