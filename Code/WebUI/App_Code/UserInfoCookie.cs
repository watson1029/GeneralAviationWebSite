using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class UserInfoCookie
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public int ID { set; get; }
    /// 登录名 
    public string UserName { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary> 
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 状态
    /// </summary> 
    public byte Status { get; set; }
    /// <summary>
    /// 是否通航企业
    /// </summary>
    public byte IsGeneralAviation { get; set; }

    /// <summary>
    ///通航企业三字码
    /// </summary>
    public string CompanyCode3 { get; set; }
    /// <summary>
    /// 通航企业
    /// </summary>
    public string CompanyName{ get; set; }
    /// <summary>
    /// 角色名称
    /// </summary>
    public List<string> RoleName { get; set; }
}