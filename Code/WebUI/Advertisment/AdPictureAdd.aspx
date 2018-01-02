<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdPictureAdd.aspx.cs" Inherits="Advertisment_AdPictureAdd" %>

<form id="form_edit" method="post">
    <table class="table_edit">
        <tr>
            <th>图片名称：
            </th>
            <td>
                <input id="AdvSuiteName" name="AdvSuiteName" maxlength="50" type="text" required="true" class="easyui-validatebox textbox" />
            </td>
         
        </tr>
        <tr>   
              <th>是否可用：
            </th>
            <td>

                <select id="IsUsed" class="easyui-combobox" editable="false" name="IsUsed" required="true" panelheight="auto" style="width: 197px; height: 25px;">
                        <option value="1" selected>是</option> 
                     <option value="0" >否</option>
                    
                </select>
            </td>
            </tr>
            <tr>
                <th>图片：
                </th>
                <td>

                    <input type="hidden" name="PicPathsInfo" id="PicPathsInfo" />
                    <input type="file" id="PicPaths" name="PicPaths" />
                    <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('PicPaths').uploadFiles()">上传</a>
                    <div id="PicPaths-fileQueue"></div>
                    <div id="PicPaths-fileList" style="margin-top: 2px; zoom: 1"></div>
                </td>

            </tr>
      
    </table>
</form>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        var pid = '<%=Request.QueryString["id"] %>';
        if (pid) {
            $.post("AdPictureAdd.aspx", { "action": "queryone", "id": pid }, function (data) {
                $("#form_edit").form('load', data);
                new dj.upload({
                    id: "PicPaths",
                    maxSize: 5,
                    multi: true,
                    queueId: "PicPaths-fileQueue",
                    listId: "PicPaths-fileList",
                    truncate: "30",
                    maxCount: "6",
                    fileExt: ".jpg;.gif;.bmp;.png",
                    uploadPath: "Files/AdvPic/",
                    uploadedFiles: data.PicPath
                });
            });
        }
        else
        {
            new dj.upload({
                id: "PicPaths",
                maxSize: 5,
                multi: true,
                queueId: "PicPaths-fileQueue",
                listId: "PicPaths-fileList",
                truncate: "30",
                maxCount: "6",
                fileExt: ".jpg;.gif;.bmp;.png",
                uploadPath: "Files/AdvPic/",
                uploadedFiles: ""
            });

        }
    });


</script>
