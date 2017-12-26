 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AircraftBatchImport.aspx.cs" Inherits="BasicData_AircraftBatchImport" %> 
        <table class="table_edit">       
            <tr>
                <th>请选择文件：
                </th>
                <td style="width:200px">
                    <input type="file" id="AircraftFiles" name="AircraftFiles" />
                    <a id="btn_upload" href="javascript:;" class="easyui-linkbutton" style="margin-top: -15px" onclick="dj.getCmp('AircraftFiles').uploadFiles()">上传</a>
                    <div id="AircraftFiles-fileQueue"></div>
                    <div id="AircraftFiles-fileList" style="margin-top: 2px; zoom: 1"></div>

            </tr> 
             <tr>
                 
                   <th>
                </th>
               <td>
                <p style="color:red">最多导入500条数据</p><a target="_blank" href="<%=Page.ResolveUrl("~/Files/飞行器信息填写模板.xls")%>">模板下载</a>
            </td>
                     </tr>
            
        </table>
<style type="text/css">
    .table_edit tr#basicInfo td{
        color:red;
    }
</style>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/swfobject.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/jquery.uploadify.v2.1.4.min.js")%>" type="text/javascript"></script>
    <link href="<%=Page.ResolveUrl("~/Content/JS/JqueryUpload/uploadify.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveUrl("~/Content/JS/GA/upload.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            new dj.upload({
                id: "AircraftFiles",
                maxSize: 5,
                multi: true,
                queueId: "AircraftFiles-fileQueue",
                listId: "AircraftFiles-fileList",
                truncate: "30",
                maxCount: "1",
                uploadPath: "Files/ImportTemp/",
                uploadedFiles: ""
            });
        });


    </script>
