using Rockying.Models;
using Rockying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

public partial class ConvertBookCover : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (RockyingDataClassesDataContext dc = new RockyingDataClassesDataContext(Utility.ConnectionString))
        {
            var books = dc.Books.Where(t => t.CoverPage.StartsWith("data:image/"));
            foreach (var b in books)
            {
                if (!string.IsNullOrEmpty(b.GoogleData))
                {
                    dynamic result = JObject.Parse(b.GoogleData);
                    var vi = result.volumeInfo;
                    if (vi.imageLinks != null)
                        if (vi.imageLinks.thumbnail != null)
                            b.CoverPage = vi.imageLinks.thumbnail;
                }
            }

            dc.SubmitChanges();
        }
    }
}