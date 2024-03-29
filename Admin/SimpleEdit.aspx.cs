﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Rockying;
using Rockying.Models;

public partial class Admin_SimpleEdit : AdminPage
{
    Article a = new Article();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Mode == "edit")
        {
            //redirect if user is not admin or dont own the article
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                Post p = (from t in dc.Posts where t.ID == ID && (t.CreatedBy == CurrentUser.ID || CurrentUser.UserType == (byte)MemberTypeType.Admin) select t).SingleOrDefault();
                if (p == null)
                {
                    Response.Redirect("default.aspx");
                }
            }

            HeadingLit.Text = "Edit";
            //PopulateArticle();
        }

        if (!Page.IsCallback && !Page.IsPostBack)
        {
            if (Mode == "edit")
            {
                PopulateForm();
            }
        }
    }

    private void PopulateArticle()
    {
        using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
        {
            try
            {
                Post p = (from t in dc.Posts where t.ID == ID select t).SingleOrDefault();
                if (p != null)
                {
                    a = Utility.Deserialize<Article>(p.Article); //Utility.Deserialize<Article>(System.IO.File.ReadAllText(Server.MapPath(string.Format("{1}/articlexml-{0}.txt", ID, Utility.ArticleFolder))));
                    if (string.IsNullOrEmpty(a.MetaTitle))
                    {
                        a.MetaTitle = a.Title;
                    }


                    a.URL = p.URL;
                }
            }
            catch (Exception ex)
            {
                a = new Article();
                Trace.Write("Unable to read xml file of article.");
                Trace.Write(ex.Message);
                Trace.Write(ex.StackTrace);
                Post p = (from t in dc.Posts where t.ID == ID select t).SingleOrDefault();
                if (p != null)
                {
                    a.Category = p.Category;
                    a.CreatedBy = p.CreatedBy;
                    a.DateCreated = p.DateCreated;
                    a.DateModified = p.DateModified;
                    a.Description = p.Description;
                    a.ID = p.ID;
                    a.ModifiedBy = p.ModifiedBy;
                    a.Status = (PostStatusType)Enum.Parse(typeof(PostStatusType), p.Status.ToString());
                    a.Tag = p.Tag;
                    a.Title = p.Title;
                    a.WriterEmail = p.WriterEmail;
                    a.WriterName = p.WriterName;
                    a.Viewed = p.Viewed;
                    a.URL = p.URL;
                    try
                    {
                        a.Text = System.IO.File.ReadAllText(Server.MapPath(string.Format("{1}/article-{0}.txt", p.ID, Utility.ArticleFolder)));
                    }
                    catch (Exception ex2)
                    {
                        Trace.Write(ex2.Message);
                        Trace.Write(ex2.StackTrace);
                    }
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
        }
    }

    private void PopulateForm()
    {
        using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
        {


            Post p = (from t in dc.Posts where t.ID == ID select t).SingleOrDefault();
            URLTextBox.Text = p.URL;
            TitleTextBox.Text = p.Title;
            //MetaTitleTextBox.Text = p.MetaTitle;
            TagTextBox.Text = p.Tag;
            WriterTextBox.Text = p.WriterName;
            //WriterEmailTextBox.Text = p.WriterEmail;
            CategoryDropDown.SelectedValue = p.Category.ToString();
            FacebookImageTextBox.Text = p.OGImage.Replace("http://rockying.com/", "//rockying.com/").Replace("http://rudrasofttech.com/rockying/", "//rockying.com/").Replace("http://www.rudrasofttech.com/rockying/", "//rockying.com/").Replace("http://www.rockying.com/", "//rockying.com/"); ;
            FacebookDescTextBox.Text = p.OGDescription;
            StatusDropDown.SelectedValue = ((byte)p.Status).ToString();
            TextTextBox.Text = p.Article.Replace("http://rockying.com/", "//rockying.com/").Replace("http://rudrasofttech.com/rockying/", "//rockying.com/").Replace("http://www.rudrasofttech.com/rockying/", "//rockying.com/").Replace("http://www.rockying.com/", "//rockying.com/");
            //TemplateDropDown.SelectedValue = p.TemplateName;
            //previewLink.NavigateUrl = string.Format("{0}/a/{1}{2}", Utility.SiteURL, p.URL, p.Status == PostStatusType.Draft ? "?preview=true" : "");
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        Page.Validate("VideoGrp");
        if (!Page.IsValid) return;
        string notifyEmail = string.Empty;
        try
        {
            a.Category = int.Parse(CategoryDropDown.SelectedValue);
            a.OGDescription = FacebookDescTextBox.Text.Trim();
            a.OGImage = FacebookImageTextBox.Text.Trim();
            a.Description = string.Format("<img src='{0}' alt='' /><p>{1}</p>", a.OGImage, a.OGDescription);
            a.Status = (PostStatusType)Enum.Parse(typeof(PostStatusType), StatusDropDown.SelectedValue);
            a.Tag = TagTextBox.Text.Trim();
            a.Text = TextTextBox.Text.Trim();
            a.Title = TitleTextBox.Text.Trim();
            a.WriterEmail = CurrentUser.Email;
            a.WriterName = WriterTextBox.Text.Trim(); //CurrentUser.MemberName.Trim();
            a.TemplateName = "LeftColumnBlogTemplate";
            a.URL = URLTextBox.Text;
            a.MetaTitle = a.Title;
            using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
            {
                if (Mode == "edit")
                {
                    string str = Utility.Serialize<Article>(a);
                    Post p = (from t in dc.Posts where t.ID == ID select t).SingleOrDefault();
                    p.Category = int.Parse(CategoryDropDown.SelectedValue);
                    p.DateModified = DateTime.Now;
                    p.ID = ID;
                    p.ModifiedBy = CurrentUser.ID;
                    p.Status = byte.Parse(StatusDropDown.SelectedValue);
                    p.Tag = TagTextBox.Text.Trim();
                    p.Title = TitleTextBox.Text.Trim();
                    p.WriterEmail = CurrentUser.Email;
                    p.WriterName = WriterTextBox.Text.Trim();
                    p.OGDescription = FacebookDescTextBox.Text.Trim();
                    p.OGImage = FacebookImageTextBox.Text.Trim();
                    p.Description = string.Format("<img src='{0}' alt='' /><p>{1}</p>", p.OGImage, p.OGDescription);
                    p.URL = URLTextBox.Text;
                    p.Article = TextTextBox.Text.Trim();
                    if ((byte)a.Status != p.Status)
                    {
                        notifyEmail = string.Format("Dear {0},<br/><br/> Your article is in {3} mode. Check it here <a href='{1}'>{1}</a>. <br/><br/>Thanks,<br/>{2}",
                        a.WriterName, string.Format("{0}/a/{1}", Utility.SiteURL, a.URL), Utility.AdminName, a.Status.ToString());
                    }

                    dc.SubmitChanges();

                    //System.IO.File.WriteAllText(Server.MapPath(string.Format("{1}/articlexml-{0}.txt", p.ID, Utility.ArticleFolder)), str);

                    if (!string.IsNullOrEmpty(notifyEmail))
                    {
                        EmailManager.SendMail(Utility.ContactEmail, a.WriterEmail, Utility.AdminName, a.WriterName, notifyEmail,
                            string.Format("Article Edited - '{0}'", a.Title), EmailMessageType.Notification, "Article Edited");

                    }
                }
                else
                {

                    string str = Utility.Serialize<Article>(a);
                    Post p = new Post();
                    p.Category = int.Parse(CategoryDropDown.SelectedValue);
                    p.DateCreated = DateTime.Now;
                    p.CreatedBy = CurrentUser.ID;
                    p.Status =  byte.Parse(StatusDropDown.SelectedValue);
                    p.Tag = TagTextBox.Text.Trim();
                    p.Title = TitleTextBox.Text.Trim();
                    p.WriterEmail = CurrentUser.Email;
                    p.WriterName = WriterTextBox.Text.Trim();
                    p.OGDescription = FacebookDescTextBox.Text.Trim();
                    p.OGImage = FacebookImageTextBox.Text.Trim();
                    p.Description = string.Format("<img src='{0}' alt='' /><p>{1}</p>", p.OGImage, p.OGDescription);
                    p.URL = URLTextBox.Text;
                    p.Article = TextTextBox.Text.Trim();
                    dc.Posts.InsertOnSubmit(p);
                    dc.SubmitChanges();

                    //System.IO.File.WriteAllText(Server.MapPath(string.Format("{1}/articlexml-{0}.txt", p.ID, Utility.ArticleFolder)), str);

                    notifyEmail = string.Format("Dear {0},<br/> Your article is posted in {2} mode. Check it here <a href='{1}'>{1}</a>. <br/>",
                    a.WriterName, string.Format("{0}/a/{1}", Utility.SiteURL, a.URL), a.Status.ToString());


                    EmailManager.SendMail(Utility.ContactEmail, a.WriterEmail, Utility.AdminName, a.WriterName, notifyEmail,
                        string.Format("Article Posted - '{0}'", a.Title), EmailMessageType.Notification, "Article Posted");
                }
            }
            if ((sender as Button).Text.ToLower() == "save")
            {
                Response.Redirect("default.aspx");
            }

            //previewLink.NavigateUrl = string.Format("{0}/a/{1}{2}", Utility.SiteURL, a.URL, a.Status == PostStatusType.Draft ? "?preview=true" : "");
        }
        catch (Exception ex)
        {
            message1.Text = string.Format("Unable to save article. {0}", ex.Message);
            message1.Visible = true;
            message1.Indicate = AlertType.Error;
            Trace.Write("Unable to save article.");
            Trace.Write(ex.Message);
            Trace.Write(ex.StackTrace);
        }
    }

    protected void URLTextBox_TextChanged(object sender, EventArgs e)
    {
        URLTextBox.Text = Utility.Slugify(URLTextBox.Text.Trim());
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
        {
            if (Mode == "edit")
            {
                Post p = (from t in dc.Posts where t.ID != ID && t.URL == URLTextBox.Text.Trim() select t).SingleOrDefault();
                if (p != null)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                Post p = (from t in dc.Posts where t.URL == URLTextBox.Text.Trim() select t).SingleOrDefault();
                if (p != null)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }
    }

    protected void TitleTextBox_TextChanged(object sender, EventArgs e)
    {
        URLTextBox.Text = Utility.Slugify(TitleTextBox.Text.Trim());
    }
}