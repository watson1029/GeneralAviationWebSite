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
}