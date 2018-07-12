using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary;

public partial class Account_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register";
        OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

        var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (!String.IsNullOrEmpty(returnUrl))
        {
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }

        LogHelper.LogWriter("方法名称", "日志消息正文a");
        LogHelper.LogWriter("方法名称", "日志消息正文b", "Path1");
        LogHelper.LogWriter("方法名称", "日志消息正文c", "Path2");

        LogHelper.LogWriterFolder("方法名称", "日志消息正文d", "folder1", "Path2");
        LogHelper.LogWriterFolder("方法名称", "日志消息正文e", "folder2", "Path2");
        LogHelper.LogWriterFolder("方法名称", "日志消息正文f", "folder2", "Path1");
        LogHelper.LogWriterFolder("方法名称", "日志消息正文g", "folder2", "Path3");
    }
}