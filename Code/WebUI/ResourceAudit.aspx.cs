using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResourceAudit : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region 权限编码
    public override string PageRightCode
    {
        get
        {
            return "ResourceAuditCheck";
        }
    }
    #endregion
}