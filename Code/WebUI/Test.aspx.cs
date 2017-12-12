using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.FlightPlan;
using Model.EF;
using DAL.SystemManagement;
using System.Linq.Expressions;
using DAL;
using Untity;
using DAL.BasicData;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfoDAL _userInfoDAL = new UserInfoDAL();
        ResourceDAL _resourceDAL = new ResourceDAL();
        RepetitivePlanDAL _repetitivePlanDAL = new RepetitivePlanDAL();

        UserInfo user = new UserInfo()
        {
            ID = 18,
            UserName = "liujunbo2",
            Password = "E10ADC3949BA59ABBE56E057F20F883E",
            Mobile = "bbbbbb"
        };
        //_userInfoDAL.Update(user, new string[] { "Mobile" });

        
        Resource res = new Resource()
        {
            ID = 4,
            Created = DateTime.Now
        };
        //_resourceDAL.Update(res, new string[] { "Created" });

        List<RepetitivePlan> repList = _repetitivePlanDAL.FindList(a => a.RepetPlanID, true);
        //统计
        var grp = repList.GroupBy(a => a.CompanyCode3);
        foreach (var item in grp)
        {
            //item.Key对应的是GroupBy的字段，item.Count（）返回记录数
            //TextBox1.Text += item.Key + "_" + item.Count();
        }


        var resList = _userInfoDAL.GetGroupCount();
        foreach (var item in resList)
        {
            //TextBox1.Text += item.Key + "_" + item.Value;
        }

        List<resultClass> resList1 = _userInfoDAL.GetGroupCount1();
        foreach (var item in resList1)
        {
            TextBox1.Text += item.fieldName + "_" + item.count;
        }

        /*
        测试Find单个记录（不使用AsNoTracking），然后进行更新操作，返回对象将受Context跟踪:  
        【报错】
        Attaching an entity of type 'Model.EF.UserInfo' failed because another entity of the same type already has the same primary key value. 
        This can happen when using the 'Attach' method or setting the state of an entity to 'Unchanged' or 'Modified' 
        if any entities in the graph have conflicting key values. This may be because some entities are new 
        and have not yet received database-generated key values. In this case use the 'Add' method or the 'Added' entity state to track the graph 
        and then set the state of non-new entities to 'Unchanged' or 'Modified' as appropriate.
        
        UserInfo user1 = _userInfoDAL.Find(m => m.ID == 27);
        user1.Mobile = "88888888";
        _userInfoDAL.Update(user1);
        */

        /*
        测试Find单个记录（使用AsNoTracking），然后进行更新操作，返回对象将不受Context跟踪:
        【成功】
        UserInfo user1 = _userInfoDAL.Find(m => m.ID == 27);
        user1.Mobile = "66666";
        _userInfoDAL.Update(user1);
        */

        /*
        测试FindList多个记录（使用AsNoTracking），然后对其中某记录进行更新操作，返回对象将受Context跟踪：
        【成功】:List中的单个实体未跟踪
        List<UserInfo> userInfoList = _userInfoDAL.FindList(m => m.ID, true);
        userInfoList[0].Mobile = "eeeee";
        _userInfoDAL.Update(userInfoList[0]);
        */


        /*
        测试FindList多个记录（使用AsNoTracking），然后对其中某记录进行更新操作，返回对象将受Context跟踪：
        【成功】
        List<UserInfo> userInfoList = _userInfoDAL.FindList(m => m.ID, false);
        userInfoList[0].Mobile = "222222";
        _userInfoDAL.Update(userInfoList[0]);
        */







        //Resource res = new Resource()
        //{
        //    Created = DateTime.Now,
        //    DealUser = "功夫功夫",
        //    Ended = DateTime.Now,
        //    FilePath = "程序.txt",
        //    IsDeleted = 0,
        //    ResourceType = 3,
        //    SenderId = 123,
        //    Started = DateTime.Now,
        //    Status = 1,
        //    Title = "让地方官方",
        //    UsefulTime = "2017年12月13日-2017年12月22日"
        //};

        //ResourceDAL _dal = new ResourceDAL();
        //_dal.Add(res);

        List<UserInfo> userInfoList = _userInfoDAL.FindList(m => m.ID, false);
        // userInfoList.GroupBy(a=>a.CompanyCode3)''

        //userInfoList[0].Mobile = "222222";
        //_userInfoDAL.Update(userInfoList[0]);





        //List<UserInfo> user1 = _userInfoDAL.FindList(m => m.ID, true);

        //UserInfo user2 = new UserInfo() { ID = user1[0].ID, Mobile = "15913147315" };

        //_userInfoDAL.Update(user, new string[] { "Mobile" });

        //_userInfoDAL.Delete(new UserInfo() { ID = 29 });

        //if (_userInfoDAL.Add(user)==1)
        //    TextBox1.Text = "ADD Success";
        //else
        //    TextBox1.Text = "NOT Success";

        int pCount, rCount;

        Expression<Func<UserInfo, bool>> predicate = PredicateBuilder.True<UserInfo>();
        predicate = predicate.And(m => m.CreateTime < DateTime.Now);
        predicate = predicate.And(m => m.ID > 10);
        List<Model.EF.UserInfo> list0 = _userInfoDAL.FindPagedList(1, 5, out pCount, out rCount, predicate, m => m.ID, true);
        //TextBox1.Text = list0.Count.ToString();

        //Expression<Func<UserInfo, bool>> exp1 = m => m.CreateTime < DateTime.Now;
        //Expression<Func<UserInfo, bool>> exp2 = m => m.ID >10;
        //Expression<Func<UserInfo, bool>> total = Expression.Lambda<Func<UserInfo, bool>>(Expression.And(exp1, exp2));
             
        //MenuDAL _dal = new MenuDAL();
        //Model.EF.Menu menu = _dal.Find(m => m.ParentMenuID == null);

        //List<Model.EF.Menu> list1 = _dal.FindList(m=>m.ID,true);
        //List<Model.EF.Menu> list2 = _dal.FindList(m => m.ID > 20, m => m.ID, true);
        //List<Model.EF.Menu> list3 = _dal.FindPagedList(1, 5, out pCount, out rCount, predicate, m => m.ID, true);

        //TextBox1.Text += "MenuID:" + menu.ID.ToString() + "  FindList():" + list1.Count + " FindList(where):" + list2.Count + " FindPagedList:" + list3[0].ID;
    }

    class resultClass
    {
        public string fieldName { get; set; }
        public int count { get; set; }
    }
}