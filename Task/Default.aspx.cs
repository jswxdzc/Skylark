﻿using System;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace Mygod.Skylark.Task
{
    public partial class Default : Page
    {
        protected string LogSize;
        private string logPath;
        protected int TaskCount;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.GetUser().Browse)
            {
                Response.StatusCode = 401;
                return;
            }
            var info = new FileInfo(logPath = FileHelper.GetDataPath("error.log"));
            LogSize = Helper.GetSize(info.Exists ? info.Length : 0);
            var tasks = Directory.EnumerateFiles(Server.MapPath("~/Data"), "*.task")
                .Select(path => GeneralTask.Create(Path.GetFileNameWithoutExtension(path)))
                .OrderBy(task => task.Status).ThenByDescending(task => task.StartTime).ToList();
            TaskList.DataSource = tasks;
            TaskList.DataBind();
            TaskCount = tasks.Count;
        }

        protected void Delete(object sender, EventArgs e)
        {
            if (!Request.GetUser().OperateTasks)
            {
                Response.StatusCode = 401;
                return;
            }
            foreach (var id in TaskList.Items.GetSelectedItemsID())
            {
                var pid = GeneralTask.Create(id).PID;
                if (pid != 0) CloudTask.KillProcess(pid);
                FileHelper.DeleteWithRetries(Server.MapPath("~/Data/" + id + ".task"));
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void DestroyLog(object sender, EventArgs e)
        {
            FileHelper.DeleteWithRetries(logPath);
        }

        protected static string AddSpace(string coolHax)
        {
            return ' ' + coolHax;
        }
    }
}