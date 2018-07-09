using DAL.FlightPlan;
using Model.FlightPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Untity;

public partial class FlightPlan_SubmitDetail : BasePage
{

    protected List<WorkflowNodeInstance> auditList = new List<WorkflowNodeInstance>();
    WorkflowNodeInstanceDAL insdal = new WorkflowNodeInstanceDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"]!=null)
            {
                GetAuditRecord();
            }
        }
    }
    private void GetAuditRecord()
    {
        var planid=Guid.Parse(Request.QueryString["id"]);
        auditList = insdal.GetAllNodeInstance(planid, (int)TWFTypeEnum.RepetitivePlan).Where(u => u.ActorID != User.ID&&u.State!=WorkflowNodeInstance.StepStateType.NoValid && u.State != WorkflowNodeInstance.StepStateType.Initialized).ToList();
    }
}