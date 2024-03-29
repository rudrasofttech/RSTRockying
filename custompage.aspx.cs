﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rockying;
using Rockying.Models;
using System.Text;
using System.IO;

public partial class custompage : BasePage
{
    public CPage CP { get; set; }
    public HomePageModel HPM
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HPM = null;
            string pname = "home";
            if (Page.RouteData.Values["pagename"] != null)
            {
                pname = Page.RouteData.Values["pagename"].ToString();
            }
            if (pname != "home")
            {
                using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
                {
                    int pid = (from u in dc.CustomPages where u.Name == pname && u.Status == (byte)PostStatusType.Publish select u.ID).SingleOrDefault();
                    CP = Utility.Deserialize<CPage>(System.IO.File.ReadAllText(Server.MapPath(string.Format("{1}/cpagexml-{0}.txt", pid, Utility.CustomPageFolder))));

                    //Master.NoTemplate = CP.NoTemplate;

                    #region Replace Custom Data Source
                    DataSourceManager dsm = new DataSourceManager();
                    CP.Body = dsm.ParseAndPopulate(CP.Body);
                    #endregion
                }
            }
            else
            {
                HPM = new HomePageModel();
                using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
                {
                    var items = (from t in dc.Posts
                                 where t.Status == (byte)PostStatusType.Publish
                                 orderby t.DateCreated descending
                                 select t).Take(9);
                    foreach (var i in items)
                    {
                        Article a = new Article
                        {
                            Category = i.Category,
                            CategoryName = i.Category1.Name,
                            CreatedBy = i.CreatedBy,
                            CategoryUrlName = i.Category1.UrlName,
                            CreatedByName = i.Member.MemberName,
                            CreatedByUserName = i.Member.UserName,
                            DateCreated = i.DateCreated,
                            DateModified = i.DateModified,
                            Description = i.Description,
                            ID = i.ID,
                            ModifiedBy = i.ModifiedBy,
                            Status = (PostStatusType)i.Status,
                            Tag = i.Tag,
                            Text = string.Empty,
                            Title = i.Title,
                            OGImage = Utility.TrimStartHTTP(i.OGImage),
                            OGDescription = i.OGDescription,
                            WriterEmail = i.WriterEmail,
                            WriterName = i.WriterName,
                            Viewed = i.Viewed,
                            URL = i.URL
                        };
                        HPM.LatestList.Add(a);
                    }
                    var top = from t in dc.TopStories orderby t.DateCreated descending select t;
                    foreach (var i in top)
                    {
                        Article a = new Article
                        {
                            Category = i.Post.Category,
                            CategoryName = i.Post.Category1.Name,
                            CategoryUrlName = i.Post.Category1.UrlName,
                            CreatedBy = i.Post.CreatedBy,
                            CreatedByName = i.Post.Member.MemberName,
                            CreatedByUserName = i.Post.Member.UserName,
                            DateCreated = i.Post.DateCreated,
                            DateModified = i.Post.DateModified,
                            Description = i.Post.Description,
                            ID = i.Post.ID,
                            ModifiedBy = i.Post.ModifiedBy,
                            Status = (PostStatusType)i.Post.Status,
                            Tag = i.Post.Tag,
                            Text = string.Empty,
                            Title = i.Post.Title,
                            OGImage = Utility.TrimStartHTTP(i.Post.OGImage),
                            OGDescription = i.Post.OGDescription,
                            WriterEmail = i.Post.WriterEmail,
                            WriterName = i.Post.WriterName,
                            URL = i.Post.URL
                        };
                        HPM.HeroList.Add(a);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CP = new CPage
            {
                Body = Resources.Resource._404
            };
            Trace.Write("Invalid pagename");
            Trace.Write(ex.Message);
            Trace.Write(ex.StackTrace);
        }
    }
}