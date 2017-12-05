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

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfoDAL _userInfoDAL = new UserInfoDAL();

        UserInfo user = new UserInfo() { ID = 29, UserName = "liujunbo", Password = "E10ADC3949BA59ABBE56E057F20F883E",
            Mobile = "15913147315", Status = 0, CompanyCode3 = "TET", CreateTime = DateTime.Now, IsGeneralAviation = 1 };

        if (_userInfoDAL.Update(user)==1)
            TextBox1.Text = "UpdateSuccess";
        else
            TextBox1.Text = "NOT Exist";        

        int pCount, rCount;

        Expression<Func<UserInfo, bool>> predicate = PredicateBuilder.True<UserInfo>();
        predicate = predicate.And(m => m.CreateTime < DateTime.Now);
        predicate = predicate.And(m => m.ID > 10);
        List<Model.EF.UserInfo> list0 = _userInfoDAL.FindPagedList(1, 5, out pCount, out rCount, predicate, m => m.ID, true);
        //TextBox1.Text = list0.Count.ToString();

        //Expression<Func<UserInfo, bool>> exp1 = m => m.CreateTime < DateTime.Now;
        //Expression<Func<UserInfo, bool>> exp2 = m => m.ID >10;
        //Expression<Func<UserInfo, bool>> total = Expression.Lambda<Func<UserInfo, bool>>(Expression.And(exp1, exp2));
             
        MenuDAL _dal = new MenuDAL();
        Model.EF.Menu menu = _dal.Find(m => m.ParentMenuID == null);

        List<Model.EF.Menu> list1 = _dal.FindList(m=>m.ID,true);
        List<Model.EF.Menu> list2 = _dal.FindList(m => m.ID > 20, m => m.ID, true);
        //List<Model.EF.Menu> list3 = _dal.FindPagedList(1, 5, out pCount, out rCount, predicate, m => m.ID, true);

        //TextBox1.Text += "MenuID:" + menu.ID.ToString() + "  FindList():" + list1.Count + " FindList(where):" + list2.Count + " FindPagedList:" + list3[0].ID;
    }
}