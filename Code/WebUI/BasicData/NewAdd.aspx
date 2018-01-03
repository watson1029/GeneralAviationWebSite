<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewAdd.aspx.cs" Inherits="BasicData_NewAdd" %>

<form id="form_edit" method="post">
    <table class="table_edit">
        <tr>
            <th>新闻标题：
            </th>
            <td>
                <input id="NewTitle" name="NewTitle" maxlength="50" type="text" required="true" class="easyui-validatebox textbox" />
            </td>

            <th>是否置顶：
            </th>
            <td>

                <select id="IsTop" class="easyui-combobox" editable="false" name="IsTop" required="true" panelheight="auto" style="width: 197px; height: 25px;">
                          <option value="0" selected>否</option>
                    <option value="1">是</option>
                </select>
            </td>
        </tr>
        <tr>
             <th>作者：
            </th>
            <td>
                <input id="Author" name="Author" maxlength="32" type="text"  class="easyui-validatebox textbox" />
            </td>
             <th>排序：
            </th>
            <td>
              <input id="Sort" name="Sort" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="height:25px" />
            </td>
        </tr>
        <tr>
            <th>新闻内容：
            </th>
            <td colspan="3">
                  <script id="editor" type="text/plain" style="width: 800px; height: 400px;"></script>
            </td>
        </tr>
    </table>
</form>

<script type="text/javascript">
    $(function () {
        UE.getEditor('editor');
        var pid = '<%=Request.QueryString["id"] %>';
        if (pid) {
            $.post("NewAdd.aspx", { "action": "queryone", "id": pid }, function (data) {
                $("#form_edit").form('load', data);
                UE.getEditor('editor').setContent(decodeURI(data.NewContent));
            });
        }
    });


</script>
