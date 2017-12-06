using BLL.BasicData;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI;
using Untity;

public partial class BasicData_Quanlification_Company : Page

{

    CompanyBLL bll = new CompanyBLL();
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
                    case "submit":
                        Save();
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
                if (bll.Delete(Request.Form["cbx_select"].ToString())>0)
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
            var model = new Company()
            {
                CompanyCode3 = Request.Form["CompanyCode3"],
                CompanyCode2 = Request.Form["CompanyCode2"],
                CompanyName = Request.Form["CompanyName"],
                EnglishName = Request.Form["EnglishName"],
            };
            if (!id.HasValue)//新增
            {
                model.CreateTime = DateTime.Now;
                if (bll.Add(model)>0)
                {
                    result.IsSuccess = true;
                    result.Msg = "增加成功！";
                }
            }
            else//编辑
            {
                model.CompanyID = id.Value;
                if (bll.Update(model)>0)
                {
                    result.IsSuccess = true;
                    result.Msg = "更新成功！";
                }
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
            var companyid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
            var company = bll.Get(companyid);
            var strJSON = JsonConvert.SerializeObject(companyid);
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



    private Expression<Func<Company, bool>> GetWhere()
    {


        Expression<Func<Company, bool>> predicate = PredicateBuilder.True<Company>();
        predicate = predicate.And(m => 1 == 1);
        //   StringBuilder sb = new StringBuilder("1=1");
        if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
        {
            predicate = u => u.CompanyID == int.Parse(Request.Form["search_value"]);

            //  sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
        }
        return predicate;
    }

}



