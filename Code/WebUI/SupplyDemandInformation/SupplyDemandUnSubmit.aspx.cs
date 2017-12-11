using BLL.SupplyDemandInformation;
using BLL.SystemManagement;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class SupplyDemandInformation_SupplyDemandUnSubmit : BasePage
{
    SupplyDemandBLL bll = new SupplyDemandBLL();
    UserInfoBLL userInfoBLL = new UserInfoBLL();
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
                case "save":
                    Save();
                    break;
                case "submit":
                    Submit();
                    break;
                case "del":
                    Delete();
                    break;
                case "init":
                    Init();
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
        int id = 0;
        if (!string.IsNullOrEmpty(Request.Form["id"]))
        {
            id = Convert.ToInt32(Request.Form["id"]);
            var model = bll.Get(id);
            model.CreateTime = DateTime.Parse(Request.Form["CreateTime"]);
            model.ExpiryDate = DateTime.Parse(Request.Form["ExpiryDate"]);
            model.Summary = Request.Form["Summary"];
            model.Catalog = Request.Form["CataLog"];
            if (bll.Update(model))
            {
                result.IsSuccess = true;
                result.Msg = "更新成功！";
            }
        }
        else
        {
            var model = new SupplyDemandInfo();
            model.CreateTime = DateTime.Parse(Request.Form["CreateTime"]);
            model.ExpiryDate = DateTime.Parse(Request.Form["ExpiryDate"]);
            model.Summary = Request.Form["Summary"];
            model.Catalog = Request.Form["CataLog"];
            model.State = "0";
            model.CompanyCode3 = User.CompanyCode3;
            model.CompanyName = Request.Form["CompanyName"];
            model.Creator = User.ID;
            model.CreateName = User.UserName;
            model.ActorID = User.ID;
            model.CreateTime = DateTime.Now;
            if (bll.Add(model))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    private void Submit()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "提交失败！";
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        if (bll.Submit(id, User.ID, User.UserName))
        {
            result.IsSuccess = true;
            result.Msg = "提交成功！";
        }
        Response.Clear();
        Response.Write(result.ToJsonString());
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var supplyDemandInfo = bll.Get(id);
        var strJSON = "";
        if (supplyDemandInfo != null)
        {
            strJSON = JsonConvert.SerializeObject(supplyDemandInfo);
        }
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryData()
    {
        int page = Request.Form["page"] != null ? Convert.ToInt32(Request.Form["page"]) : 0;
        int size = Request.Form["rows"] != null ? Convert.ToInt32(Request.Form["rows"]) : 0;
        string sort = Request.Form["sort"] ?? "";
        string order = Request.Form["order"] ?? "";
        if (page < 1) return;
        int pageCount = 0;
        int rowCount = 0;
        string orderField = sort.Replace("JSON_", "");
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
    private Expression<Func<SupplyDemandInfo, bool>> GetWhere()
    {
        Expression<Func<SupplyDemandInfo, bool>> predicate = PredicateBuilder.True<SupplyDemandInfo>();
        predicate = predicate.And(m => m.State == "0");
        predicate = predicate.And(m => m.Creator == User.ID);

        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            string strValue = Request.Form["search_value"].ToString();
            if (Request.Form["search_type"].ToString() == "CompanyName")
            {
                predicate = u => u.CompanyName.Contains(strValue);
            }
            else if (Request.Form["search_type"].ToString() == "Catalog")
            {
                predicate = u => u.Catalog.Contains(strValue);
            }
        }

        return predicate;
    }

    private void Init()
    {
        SupplyDemandInfo sdi = new SupplyDemandInfo
        {
            Creator = User.ID,
            CreateName = User.UserName,
            CreateTime = DateTime.Today
        };
        var company = userInfoBLL.GetUserCompany(User.ID);
        if (company != null)
        {
            sdi.CompanyCode3 = company.CompanyCode3;
            sdi.CompanyName = company.CompanyName;
        }
        var strJSON = string.Empty;
        strJSON = JsonConvert.SerializeObject(sdi);
        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

}