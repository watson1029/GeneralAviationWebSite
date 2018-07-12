using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

[HubName("noticeHub")]
public class NoticeHub : Hub
{
    private BLL.SystemManagement.MenuBLL menuBll = new BLL.SystemManagement.MenuBLL();
    private static Dictionary<int, List<string>> _connection = new Dictionary<int, List<string>>();
    private static object locker = new object();

    public static Action<int, string, string> SendNoticeAction = null;
    public static Action<int, string> SendChangeAction = null;

    public NoticeHub()
    {
        SendNoticeAction = new Action<int, string, string>(SendNotice);
        SendChangeAction = new Action<int, string>(SendChange);
    }

    [HubMethodName("initHub")]
    public void InitHub()
    {
        Clients.All.init_callback("666");
    }

    [HubMethodName("noticeLogin")]
    public void NoticeLogin()
    {
        var user = UserLoginService.Instance.GetUser();
        lock (locker)
        {
            if (!_connection.ContainsKey(user.ID))
                _connection.Add(user.ID, new List<string>());
            if (!_connection[user.ID].Contains(Context.ConnectionId))
                _connection[user.ID].Add(Context.ConnectionId);
        }
    }

    [HubMethodName("noticeLogout")]
    public void NoticeLogout()
    {
        var user = UserLoginService.Instance.GetUser();
        if (_connection.ContainsKey(user.ID))
            _connection.Remove(user.ID);
    }

    /// <summary>
    /// 消息提醒
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="menuCode"></param>
    /// <param name="notice"></param>
    [HubMethodName("sendNotice")]
    public void SendNotice(int userId, string menuCode, string notice)
    {
        if (menuBll.JudgeMenuRole(userId, menuCode))
        {
            if (_connection.ContainsKey(userId))
            {
                Clients.Clients(_connection[userId]).sendNotice_callback(menuCode, notice);
            }
        }
    }

    /// <summary>
    /// 数据改变
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="menuCode"></param>
    [HubMethodName("sendChange")]
    public void SendChange(int userId, string menuCode)
    {
        if (menuBll.JudgeMenuRole(userId, menuCode))
        {
            if (_connection.ContainsKey(userId))
            {
                Clients.Clients(_connection[userId]).sendChange_callback(menuCode);
            }
        }
    }
}
