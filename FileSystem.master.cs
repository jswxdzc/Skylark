﻿using System;
using System.Web.UI;

namespace Mygod.Skylark
{
    public partial class FileSystem : MasterPage
    {
        protected void WritePath()
        {
            var relativePath = Page.RouteData.GetRelativePath();
            var dirs = relativePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var tempPath = string.Empty;
            foreach (var dir in dirs)
            {
                tempPath += '/' + dir;
                Response.Write(" &gt; <a href=\"/Browse" + tempPath + "/\">" + dir + "</a>");
            }
            if (FileHelper.IsFileExtended(FileHelper.GetFilePath(relativePath)) == false) Response.Write(" &gt;");
        }
    }
}