using BLL.BasicData;
using BLL.FlightPlan;
using DAL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using Untity;
using System.Linq;
public partial class FlightPlan_MyUnSubmitRepetPlan : BasePage
{
    RepetitivePlanNewBLL bll = new RepetitivePlanNewBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "query"://查询数据
                    QueryData();
                    break;
                case "save":
                    Save();
                    break;
                case "submit":
              //      Submit();
                    break;
                case "del":
                    Delete();
                    break;
                default:
                    break;
            }
        }
    }

    private void Delete()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "删除失败！";
        if (Request.Form["cbx_select"] != null)
        {
            if (bll.Delete(Request.Form["cbx_select"].ToString()))
            {
                result.IsSuccess = true;
                result.Msg = "删除成功！";
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    private void Save()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        int? id = null;
        if (!string.IsNullOrEmpty(Request.Form["id"]))
        { id = Convert.ToInt32(Request.Form["id"]); }
        RepetPlanNew model = null;
        if (!id.HasValue)//新增
        {
            model = new RepetPlanNew();
            model.GetEntitySearchPars<RepetPlanNew>(this.Context);
            model.AttchFile = Request.Params["AttchFilesInfo"];
            model.Status = 1;
            model.CompanyName = User.CompanyName;
            model.Creator = User.ID;
            model.CreateName = User.UserName;
            model.CreateTime = DateTime.Now;
            //if (model.IsUrgentTask)
            //{ 
            //    if(model.IsCrossArea)
            //    {
            //    model.AuditName="运行管理中心审批";
            //    }
            //    else
            //    {
            //    model.AuditName=
            //    }
            
            //}
            //    else
            //    {
                
                
                
            //    }
            if (bll.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            model = bll.Get(id.Value);
            if (model != null)
            {
                model.GetEntitySearchPars<RepetPlanNew>(this.Context);
                model.AttchFile = Request.Params["AttchFilesInfo"];
                if (bll.Update(model))
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
            }
        };
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }
    //private void Submit()
    //{
    //    AjaxResult result = new AjaxResult();
    //    result.IsSuccess = false;
    //    result.Msg = "提交失败！";
    //    var planid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;

    //    if (insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.RepetitivePlan).Count > 0)
    //    {
    //        result.Msg = "一条长期计划无法创建两条申请流程，请联系管理员！";
    //    }
    //    else
    //    {
    //        try
    //        {
    //            wftbll.CreateWorkflowInstance((int)TWFTypeEnum.RepetitivePlan, planid, User.ID, User.UserName);
    //            insdal.Submit(planid, (int)TWFTypeEnum.RepetitivePlan, "", insdal.UpdateRepetPlan);
    //            result.IsSuccess = true;
    //            result.Msg = "提交成功！";
    //        }
    //        catch(Exception e)
    //        {
    //            result.IsSuccess = false;
    //            result.Msg = "提交失败！";
    //        }
    //    }
    //    Response.Clear();
    //    Response.Write(result.ToJsonString());
    //    Response.ContentType = "application/json";
    //    Response.End();


    //}


    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryData()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        var strWhere = GetWhere();
        var pageList = bll.GetList(page, size, out pageCount, out rowCount, strWhere);
        var strJSON = Serializer.JsonDate(new { rows = pageList, total = rowCount });
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
   // Status 1 待提交 2 已提交 3 审核通过 4 审核不通过
    /// <summary>
    /// 组合搜索条件
    /// </summary>
    /// <returns></returns>
    private Expression<Func<RepetPlanNew, bool>> GetWhere()
    {

        Expression<Func<RepetPlanNew, bool>> predicate = PredicateBuilder.True<RepetPlanNew>();
     
        predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            var val = Request.Form["search_value"].Trim();
            predicate = predicate.And(m => m.PlanCode == val);
        }

        return predicate;
    }




#region 权限编码
  public override string PageRightCode
    {
        get
        {
            return "MyUnSubmitRepetPlanNewCheck";
        }
    } 
#endregion
}