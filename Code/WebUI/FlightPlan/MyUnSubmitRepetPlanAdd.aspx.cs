using BLL.BasicData;
using BLL.FlightPlan;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;
using ViewModel.FlightPlan;

public partial class FlightPlan_MyUnSubmitRepetPlanAdd : BasePage
{
    RepetitivePlanBLL bll = new RepetitivePlanBLL();
    FlightTaskBLL fbll = new FlightTaskBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["action"] != null)
        {
            switch (Request.Form["action"])
            {
                case "queryone"://获取一条记录
                    GetData();
                    break;
                case "save":
                    Save();
                    break;
                case "readfile":
                    ReadFileText();
                    break;
                case "analysis":
                    FileAnalysis();
                    break;
                default:
                    break;
            }
        }
    }
    private void Save()
    {
        AjaxResult result = new AjaxResult();
        result.IsSuccess = false;
        result.Msg = "保存失败！";
        AirportInfoBLL airportbll = new AirportInfoBLL();
        var idList = new List<string>();
        var airportText = "";
        var airlineworkText = "";
        if (!string.IsNullOrEmpty(Request.Form["AirportText"]))
        {
            var airportList = (AirportFillTotal)JsonConvert.DeserializeObject(Request.Form["AirportText"], typeof(AirportFillTotal));
            idList = airportbll.AddOrUpdateAirport(airportList.airportArray, User.ID, ref airportText);
        }
        RepetitivePlan entity = null;
        if (string.IsNullOrEmpty(Request.Form["id"]))//新增
        {
            entity = new RepetitivePlan();
            entity.GetEntitySearchPars<RepetitivePlan>(this.Context);
            entity.RepetPlanID = Guid.NewGuid();
            entity.WeekSchedule = Request.Form["qx"];
            entity.AttachFile = Request.Params["AttachFilesInfo"];
            entity.PlanState = "0";
            entity.CompanyCode3 = User.CompanyCode3 ?? "";
            entity.CompanyName = User.CompanyName;
            entity.Creator = User.ID;
            entity.CreatorName = User.UserName;
            entity.ActorID = User.ID;
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;
            entity.AirportText = airportText;
            #region 机场起降点、航线、作业区
            bll.AddRepetitivePlanOther(idList, Request.Form["AirlineText"], Request.Form["CWorkText"], Request.Form["PWorkText"], Request.Form["HWorkText"], entity.RepetPlanID.ToString(), Request.Form["id"], ref airlineworkText);
            #endregion
            entity.AirlineWorkText = airlineworkText;
            if (bll.Add(entity))
            {
                result.IsSuccess = true;
                result.Msg = "增加成功！";
            }
        }
        else//编辑
        {
            entity = bll.Get(Guid.Parse(Request.Form["id"]));
            if (entity != null)
            {
                entity.AircraftType = Request.Form["AircraftType"];
                entity.FlightType = Request.Form["FlightType"];
                entity.StartDate = DateTime.Parse(Request.Form["StartDate"]);
                entity.EndDate = DateTime.Parse(Request.Form["EndDate"]);
                entity.ModifyTime = DateTime.Now;
                entity.Remark = Request.Form["Remark"];
                entity.AttachFile = Request.Params["AttachFilesInfo"];
                entity.WeekSchedule = Request.Form["qx"];
                entity.AirportText = airportText;
                #region 机场、起降点航线
                bll.AddRepetitivePlanOther(idList, Request.Form["AirlineText"], Request.Form["CWorkText"], Request.Form["PWorkText"], Request.Form["HWorkText"], entity.RepetPlanID.ToString(), Request.Form["id"], ref airlineworkText);
                entity.AirlineWorkText = airlineworkText;
                #endregion
                if (bll.Update(entity))
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
    /// <summary>
    /// 获取指定ID的数据
    /// </summary>
    private void GetData()
    {
        var planid =Guid.Parse(Request.Form["id"]);
        var plan = bll.Get(planid);
        var strJSON = "";
        if (plan != null)
        {
            strJSON = JsonConvert.SerializeObject(plan);
        }

        Response.Clear();
        Response.Write(strJSON);
        Response.ContentType = "application/json";
        Response.End();
    }
    private void  ReadFileText()
    {
        var filepath = Request.Params["PlanFilesPath"].Split(',')[0];
        var temppath = DownFile(filepath);
        NPOI.XWPF.UserModel.XWPFDocument doc = null;
        StringBuilder sbFileText = new StringBuilder();
        using (FileStream file = new FileStream(temppath, FileMode.Open, FileAccess.Read))
        {
            doc = new NPOI.XWPF.UserModel.XWPFDocument(file);
            foreach (NPOI.XWPF.UserModel.XWPFParagraph paragraph in doc.Paragraphs)
            {
                sbFileText.AppendLine(paragraph.ParagraphText);
            }
        }
        System.Web.HttpContext.Current.Session["session_filetext"] = sbFileText.ToString();
        Response.Clear();
        Response.Write(sbFileText.ToString());
        Response.ContentType = "text/plain";
        Response.End();
    }
    private string DownFile(string attachfile)
    {
        var localNewFileName = Path.GetFileName(attachfile);
        var localTargetCategory = Server.MapPath("~/Files/ImportTemp");
        if (string.IsNullOrEmpty(localNewFileName))
            throw new ApplicationException(string.Format("获取文件路径[{0}]中的文件名为空", attachfile));
        if (!Directory.Exists(localTargetCategory))
            Directory.CreateDirectory(localTargetCategory);

        var filePath = Path.Combine(localTargetCategory, localNewFileName);
        return filePath;
    }
    public void FileAnalysis()
    {
        RepetitivePlanVM model = new RepetitivePlanVM();
        if (System.Web.HttpContext.Current.Session["session_filetext"] != null)
        {
            var fileText = System.Web.HttpContext.Current.Session["session_filetext"].ToString();
            //日期
            MatchCollection dateMatch = Regex.Matches(fileText, FileAnalysisReg.dateReg);
            try
            {
                if (dateMatch.Count > 1)
                {
                    model.StartDate = DateTime.Parse(dateMatch[0].Value.Replace("年", "-").Replace("月", "-").Replace("日", ""));
                    model.EndDate = DateTime.Parse(dateMatch[1].Value.Replace("年", "-").Replace("月", "-").Replace("日", ""));
                }
                else if (dateMatch.Count == 1)
                {
                    var arr = dateMatch[0].Value.Split('至');
                    model.StartDate = DateTime.Parse(arr[0].Replace("年", "-").Replace("月", "-").Replace("日", ""));
                    model.EndDate = DateTime.Parse(arr[1].Replace("年", "-").Replace("月", "-").Replace("日", ""));
                }
            }
            catch
            { }
            //公司名称
            if (Regex.IsMatch(fileText, FileAnalysisReg.companyReg))
            {
                try
                {
                    model.CompanyName = Regex.Match(fileText, FileAnalysisReg.companyReg).Value + "公司";
                }
                catch
                {
                }
            }
            ////地区
            //if (Regex.IsMatch(fileText, FileAnalysisReg.areaReg))
            //{
            //    model.FlightArea = Regex.Match(fileText, FileAnalysisReg.areaReg).Value;
            //}
            //任务性质
            if (Regex.IsMatch(fileText, FileAnalysisReg.taskReg))
            {
                try
                {
                    var  list = fbll.GetList();
                
                    model.FlightType = list.Find(m => m.Description.Contains(Regex.Match(fileText, FileAnalysisReg.taskReg).Value)).TaskCode;
                }
                catch
                {
                }
            }
            //机型
            if (Regex.IsMatch(fileText, FileAnalysisReg.airplaneTypeReg))
            {
                model.AircraftType = Regex.Match(fileText, FileAnalysisReg.airplaneTypeReg).Value;
            }
            #region 临时起降点

            if (Regex.IsMatch(fileText, FileAnalysisReg.airportReg))
            {
                model.AirportText = Regex.Match(fileText, FileAnalysisReg.airportReg).Value;

                if (Regex.IsMatch(model.AirportText, FileAnalysisReg.airportReg1))
                {
                    MatchCollection airportMatches = Regex.Matches(model.AirportText, FileAnalysisReg.airportReg1);
                    foreach (Match airport in airportMatches)
                    {
                        string airportName = "", code4 = "", lngAndLat = "";
                        if (Regex.IsMatch(airport.Value, FileAnalysisReg.airportName))
                        {
                            airportName = Regex.Match(airport.Value, FileAnalysisReg.airportName).Value;
                        }
                        if (Regex.IsMatch(airport.Value, FileAnalysisReg.code4))
                        {
                            code4 = Regex.Match(airport.Value, FileAnalysisReg.code4).Value;
                        }
                        if (Regex.IsMatch(airport.Value, FileAnalysisReg.lngAndLat))
                        {
                            lngAndLat = Regex.Match(airport.Value, FileAnalysisReg.lngAndLat).Value;
                        }
                        model.airportList.Add(new AirportVM()
                        {
                            Name = airportName,
                            Code4 = code4,
                            LatLong = lngAndLat
                        });
                    }
                }
            }

            #endregion
            #region 航线分析
            if (Regex.IsMatch(fileText, @"(?<=(\uff08\u4e00\uff09\u822a\u7ebf))[.\s\S]*?(?=(\uff08\u4e8c\uff09\u4f5c\u4e1a\u533a))"))
            {
                model.AirlineText = Regex.Match(fileText, @"(?<=(\uff08\u4e00\uff09\u822a\u7ebf))[.\s\S]*?(?=(\uff08\u4e8c\uff09\u4f5c\u4e1a\u533a))").Value;
            }
            MatchCollection areaMatch = Regex.Matches(model.AirlineText, @"(?<=[1-9\uff1a][\.\u3001\r])[^\uff1b^\u3002^\;]+");
            if (areaMatch.Count > 0)
            {
                var i = 0;
                foreach (Match area in areaMatch)
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    try
                    {
                        var areaval = area.Value.Split('-');
                        AirlineVM airvm = new AirlineVM();
                        foreach (var item in areaval)
                        {
                            string airlineName = "", lngAndLat = "";
                            if (Regex.IsMatch(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+"))
                            {
                                airlineName = Regex.Match(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+").Value;
                            }
                            if (Regex.IsMatch(item, FileAnalysisReg.lngAndLat))
                            {
                                lngAndLat = Regex.Match(item, FileAnalysisReg.lngAndLat).Value;
                            }
                            airvm.pointList.Add(new PointVM()
                            {
                                Name = airlineName,
                                LatLong = lngAndLat
                            });
                        }
                        if (airvm.pointList.Count > model.airLineMaxCol)
                        {
                            model.airLineMaxCol = airvm.pointList.Count;
                        }
                        //高度
                        if (Regex.IsMatch(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))"))
                        {
                            airvm.FlyHeight = Regex.Match(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))").Value;
                        }
                        model.airlineList.Add(airvm);
                    }
                    catch (Exception ex)
                    { }

                }

            }
            #endregion
            #region 作业区

            if (Regex.IsMatch(fileText, @"(?<=(\uff08\u4e8c\uff09\u4f5c\u4e1a\u533a))[.\s\S]*?(?=(\u4e94\u3001\u5b89\u5168\u8d23\u4efb))"))
            {
                model.WorkText = Regex.Match(fileText, @"(?<=(\uff08\u4e8c\uff09\u4f5c\u4e1a\u533a))[.\s\S]*?(?=(\u4e94\u3001\u5b89\u5168\u8d23\u4efb))").Value.Trim();
            }

            MatchCollection areaMatch1 = Regex.Matches(model.WorkText, @"(?<=[1-9\uff1a][\.\u3001\r])[^\uff1b^\u3002^\;]+");
            if (areaMatch1.Count > 0)
            {
                var i = 0;
                foreach (Match area in areaMatch1)
                {
                    if (i < 1)
                    {
                        i++;
                        continue;
                    }
                    #region 作业区（圆）
                    try
                    {
                        if (area.Value.Contains("为圆心半径") && Regex.IsMatch(area.Value, @"(?<=(\u4e3a\u5706\u5fc3\u534a\u5f84))[.\s\S]*?(?=(\u516c\u91cc\u8303\u56f4\u5185))"))
                        {
                            var areaval = area.Value.Split('-');
                            WorkVM workvm = new WorkVM();
                            foreach (var item in areaval)
                            {
                                string workName = "", lngAndLat = "";
                                if (Regex.IsMatch(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+"))
                                {
                                    workName = Regex.Match(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+").Value.Replace("以", "");
                                }

                                if (Regex.IsMatch(item, FileAnalysisReg.lngAndLat))
                                {
                                    lngAndLat = Regex.Match(item, FileAnalysisReg.lngAndLat).Value;
                                }
                                workvm.pointList.Add(new PointVM()
                                {
                                    Name = workName,
                                    LatLong = lngAndLat
                                });
                            }
                            //半径
                            if (Regex.IsMatch(area.Value, @"(?<=(\u4e3a\u5706\u5fc3\u534a\u5f84))[.\s\S]*?(?=(\u516c\u91cc\u8303\u56f4\u5185))"))
                            {
                                workvm.Raidus = Regex.Match(area.Value, @"(?<=(\u4e3a\u5706\u5fc3\u534a\u5f84))[.\s\S]*?(?=(\u516c\u91cc\u8303\u56f4\u5185))").Value;
                            }
                            //高度
                            if (Regex.IsMatch(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))"))
                            {
                                workvm.FlyHeight = Regex.Match(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))").Value;
                            }
                            model.cworkList.Add(workvm);
                        }
                    }
                    catch
                    { }
                    #endregion
                    #region 作业区（点）
                    try
                    {
                        if (area.Value.Contains("连线范围"))
                        {
                            var areaval = area.Value.Split('-');
                            WorkVM workvm = new WorkVM();
                            foreach (var item in areaval)
                            {
                                string workName = "", lngAndLat = "";
                                if (Regex.IsMatch(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+"))
                                {
                                    workName = Regex.Match(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+").Value;
                                }

                                if (Regex.IsMatch(item, FileAnalysisReg.lngAndLat))
                                {
                                    lngAndLat = Regex.Match(item, FileAnalysisReg.lngAndLat).Value;
                                }
                                workvm.pointList.Add(new PointVM()
                                {
                                    Name = workName,
                                    LatLong = lngAndLat
                                });

                            }
                            if (workvm.pointList.Count > model.pworkMaxCol)
                            {
                                model.pworkMaxCol = workvm.pointList.Count;
                            }

                            //高度
                            if (Regex.IsMatch(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))"))
                            {
                                workvm.FlyHeight = Regex.Match(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))").Value;
                            }
                            model.pworkList.Add(workvm);
                        }
                    }
                    catch
                    { }
                    #endregion
                    #region 作业区（线）
                    try
                    {
                        if (area.Value.Contains("航线左右") && Regex.IsMatch(area.Value, @"(?<=(\u822a\u7ebf\u5de6\u53f3))[.\s\S]*?(?=(\u516c\u91cc\u8303\u56f4\u5185))"))
                        {
                            var areaval = area.Value.Split('-');
                            WorkVM workvm = new WorkVM();
                            foreach (var item in areaval)
                            {
                                string airportName = "", lngAndLat = "";
                                if (Regex.IsMatch(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+"))
                                {
                                    airportName = Regex.Match(item, "^[\u4e00-\u9fa5_a-zA-Z0-9]+").Value;
                                }
                                if (Regex.IsMatch(item, FileAnalysisReg.lngAndLat))
                                {
                                    lngAndLat = Regex.Match(item, FileAnalysisReg.lngAndLat).Value;
                                }
                                workvm.pointList.Add(new PointVM()
                                {
                                    Name = airportName,
                                    LatLong = lngAndLat
                                });

                            }
                            if (workvm.pointList.Count > model.hworkMaxCol)
                            {
                                model.hworkMaxCol = workvm.pointList.Count;
                            }
                            //距离(航线左右--公里范围)
                            if (Regex.IsMatch(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))"))
                            {
                                workvm.Raidus = Regex.Match(area.Value, @"(?<=(\u822a\u7ebf\u5de6\u53f3))[.\s\S]*?(?=(\u516c\u91cc\u8303\u56f4))").Value;
                            }
                            //高度
                            if (Regex.IsMatch(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))"))
                            {
                                workvm.FlyHeight = Regex.Match(area.Value, @"(?<=(\u9ad8\u5ea6))[.\s\S]*?(?=(\u7c73))").Value;
                            }
                            model.hworkList.Add(workvm);
                        }
                    }
                    catch
                    { }
                    #endregion
                };


            }
            #endregion
        }
        Response.Clear();
        Response.Write(JsonConvert.SerializeObject(model));
        Response.ContentType = "application/json";
        Response.End();


    }
    //private void GetPlanCode()
    //{
    //    Response.Clear();
    //    Response.Write(OrderHelper.GenerateId(OrderTypeEnum.RP, User.CompanyCode3));
    //    Response.ContentType = "text/plain";
    //    Response.End();
    //}
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "MyUnSubmitRepetPlanCheck";
        }
    }
    #endregion
}