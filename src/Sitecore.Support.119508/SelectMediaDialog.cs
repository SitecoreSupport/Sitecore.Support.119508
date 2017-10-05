using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web;
using Sitecore.Web.PageCodes;
using System;
using System.Collections.Specialized;
using System.Web;

namespace Sitecore.Support.Speak.Applications
{
    public class SelectMediaDialog : Sitecore.Speak.Applications.SelectMediaDialog
    {
        public override void Initialize()
        {
            SelectMediaDialog.RedirectOnItembucketsDisabled(ClientHost.Items.GetItem("{16227E67-F9CB-4FB7-9928-7FF6A529708E}"));
            string queryString = Sitecore.Web.WebUtil.GetQueryString("ro");
            string queryString2 = Sitecore.Web.WebUtil.GetQueryString("fo");
            bool showFullPath = SelectMediaDialog.GetShowFullPath(queryString2);
            string queryString3 = Sitecore.Web.WebUtil.GetQueryString("hasUploaded");
            if (!string.IsNullOrEmpty(queryString3) && queryString3 == "1")
            {
                this.DataSource.Parameters["SearchConfigItemId"] = "{1E723604-BFE0-47F6-B7C5-3E2FA6DD70BD}";
                this.Menu.Parameters["DefaultSelectedItemId"] = "{BE8CD31C-2A01-4ED6-9C83-E84C2275E429}";
            }
            Sitecore.Data.Items.Item item = SelectMediaDialog.GetMediaItemFromQueryString(queryString) ?? ClientHost.Items.GetItem("/sitecore/media library");
            Sitecore.Data.Items.Item mediaItemFromQueryString = SelectMediaDialog.GetMediaItemFromQueryString(queryString2);
            string folder = (mediaItemFromQueryString == null) ? null : mediaItemFromQueryString.Paths.Path;
            if (item != null)
            {
                this.MediaResultsListControl.Parameters["ContentLanguage"] = item.Language.ToString();
                this.MediaResultsListControl.Parameters["DefaultSelectedItemId"] = ((mediaItemFromQueryString == null) ? item.ID.ToString() : mediaItemFromQueryString.ID.ToString());
                this.DataSource.Parameters["RootItemId"] = item.ID.ToString();
                this.MediaFolderValueText.Parameters["Text"] = SelectMediaDialog.GetDisplayPath(item.Paths.Path, folder, showFullPath);
            }
            this.TreeViewToggleButton.Parameters["Click"] = string.Format(this.TreeViewToggleButton.Parameters["Click"], HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(queryString2), showFullPath);
            string navigateUrl;
            string text;
            string text2;
            string text3;
            SelectMediaDialog.FillCommandParts(this.UploadButton.Parameters["Click"], out navigateUrl, out text, out text2, out text3);
            string text4 = SelectMediaDialog.SetUrlContentDatabase(navigateUrl, Sitecore.Web.WebUtil.GetQueryString("sc_content"));
            string format = string.Concat(new string[]
            {
                text,
                text3,
                text4,
                text3,
                text2
            });
            this.UploadButton.Parameters["Click"] = string.Format(format, HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(queryString2), showFullPath);
        }
    }
}
