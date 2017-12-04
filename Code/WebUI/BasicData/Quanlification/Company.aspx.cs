using BLL.BasicData;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Text;
using Untity;

public partial class BasicData_Quanlification_Company : BasePage

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
                model.ID = id.Value;
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
            var pilotid = Request.Form["id"] != null ? Convert.ToInt32(Request.Form["id"]) : 0;
            var pilot = bll.Get(pilotid);
            var strJSON = JsonConvert.SerializeObject(pilot);
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
            string orderField = sort.Replace("JSON_", "");
            string strWhere = GetWhere();
            var pageList = bll.GetList(size, page, strWhere);
            //var vms = new List<UserInfo>();
            //if (pageList != null && pageList.TotalCount > 0)
            //{
            //    vms.AddRange(pageList.Select(dto => new UserInfo
            //    {
            //        ID = dto.ID,
            //        Password = dto.Password,
            //        Status = dto.Status,
            //        CreateTime = dto.CreateTime,
            //        Phone = dto.Phone,
            //        ContactPerson = dto.ContactPerson,
            //        ContactPhone = dto.ContactPhone,
            //        ContactEmail = dto.ContactEmail,
            //        AreaName = dto.AreaName,
            //        Deposit = IFPECBasicInfoFacade.GetDisp(dto.ECCode),
            //        WillApproveStatus = IFPECDepositHistoryFacade.DepositWillApproveStatus(dto.ECCode)
            //    }));

            //}
            var strJSON = Serializer.JsonDate(new { rows = pageList, total = pageList.TotalCount });
            //   strJSON= "{ \"rows\":[ { \"JSON_ID\":\"1\",\"JSON_UserName\":\"adads\",\"JSON_Password\":\"asdasdf\",\"JSON_Mobile\":\"sdfasdf\",\"JSON_Status\":\"0\",\"JSON_CreateTime\":\"2017-11-1\",\"JSON_IsGeneralAviation\":1,\"JSON_CompanyCode3\":\"222\"} ],\"total\":1}";
            Response.Write(strJSON);
            Response.ContentType = "application/json";
            Response.End();
        }

        /// <summary>
        /// 组合搜索条件
        /// </summary>
        /// <returns></returns>
        private string GetWhere()
        {
            StringBuilder sb = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(Request.Form["search_type"]) && !string.IsNullOrEmpty(Request.Form["search_value"]))
            {
                sb.AppendFormat(" and charindex('{0}',{1})>0", Request.Form["search_value"], Request.Form["search_type"]);
            }
            else
            {
                sb.AppendFormat("");
            }
            return sb.ToString();
        }

    }



