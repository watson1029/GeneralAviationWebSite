using BLL.FlightPlan;
using DAL.FlightPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;
using Model.EF;
using System.Linq.Expressions;
using System.IO;
public partial class FlightPlan_MyAuditRepetPlan : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    WorkflowNodeInstanceDAL insdal=new WorkflowNodeInstanceDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "auditsubmit":
                    AuditSubmit();
                    break;
                case "batchaudit":
                    BatchAuditSubmit();
                    break;
                default:
                    break;
            }
        }
    }




    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryData()
    {
        int page = Convert.ToInt32(Request.Form["page"] ?? "0");
        int size = Convert.ToInt32(Request.Form["rows"] ?? "0");
       // string sort = Request.Form["sort"] ?? "";
       // string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
       // string orderField = sort.Replace("JSON_", "");
        var strWhere = GetWhere();
        var pageList = bll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    /// 
    private Expression<Func<RepetitivePlan, bool>> GetWhere()
    {

        Expression<Func<RepetitivePlan, bool>> predicate = PredicateBuilder.True<RepetitivePlan>();
        predicate = predicate.And(m => m.ActorID == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = predicate.And(m => m.PlanCode == Request.Form["search_value"]);
        }

        return predicate;
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var plan = bll.Get(planid);
        var strJSON = JsonConvert.SerializeObject(plan);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void AuditSubmit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "提交失败！";
        var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        if (Request.Form["Auditresult"] == "0")
        {
            insdal.Submit(planid, (int)TWFTypeEnum.RepetitivePlan, Request.Form["AuditComment"] ?? "", insdal.UpdateRepetPlan);
        }
        else {
            insdal.Terminate(planid, (int)TWFTypeEnum.RepetitivePlan, Request.Form["AuditComment"] ?? "", insdal.UpdateRepetPlan);
        }
        result.IsSuccess = true;
        result.Msg = "提交成功！";

        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();

    }
    private void BatchAuditSubmit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "操作失败！";
        if (Request.Form["cbx_select"] != null)
        {
          var arr=  Request.Form["cbx_select"].ToString().Split(',');

          var auditComment=Request.Form["AuditComment"] ?? "";
          if (Request.Form["Auditresult"] == "0")
          {
          foreach (var item in arr)
          {
              insdal.Submit(int.Parse(item), (int)TWFTypeEnum.RepetitivePlan, auditComment, insdal.UpdateRepetPlan);
          }
          }
          else
          {
              foreach (var item in arr)
              {
                  insdal.Terminate(int.Parse(item), (int)TWFTypeEnum.RepetitivePlan, auditComment, insdal.UpdateRepetPlan);
              }
          }
          result.IsSuccess =true;
          result.Msg = "操作成功！";

        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();

    }
    private void DownlodFile()
    {
        string fileName = "";
        string filePath = Server.MapPath("~/");
        FileStream fs = new FileStream(filePath,FileMode.Open);
        byte[] bytes = new byte[(int)fs.Length];
        fs.Read(bytes,0,bytes.Length);
        fs.Close();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition","attachment;filename="+HttpUtility.UrlEncode(fileName,System.Text.Encoding.UTF8));
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
}