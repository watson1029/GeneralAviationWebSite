using BLL.BasicData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class BasicData_Quanlification_CompanyAdd : BasePage
{
    CompanyBLL bll = new CompanyBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "queryone"://获取一条记录
                    GetData();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var id = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
        var model = bll.Get(id);
        var strJSON = "";
        if (model != null)
        {
            strJSON = JsonConvert.SerializeObject(model);
        }

        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }

}