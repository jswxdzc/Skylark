﻿using System;
using System.Linq;
using System.Web.UI;

namespace Mygod.Skylark.Offline
{
    public partial class New : Page
    {
        protected string Path;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.GetUser().OperateTasks)
            {
                Response.StatusCode = 401;
                return;
            }
            Path = RouteData.GetRelativePath();
        }

        protected void Submit(object sender, EventArgs e)
        {
            foreach (var link in LinkBox.Text.Split(new[] { '\r', '\n' }).Where(link => !string.IsNullOrWhiteSpace(link)))
                OfflineDownloadTask.Create(link, Path);
            Response.Redirect("/Browse/" + Path + '/');
        }

        protected void MediaFire(object sender, EventArgs e)
        {
            OfflineDownloadTask.CreateMediaFire(MediaFireBox.Text, Path);
            Response.Redirect("/Browse/" + Path + '/');
        }
    }
}