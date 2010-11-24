﻿<%@ Control Language="C#" AutoEventWireup="true" %>

<script runat="server">
    public string RemoveComments(string text)
    {
        // Remove comments in /**/ style
        var regex = new System.Text.RegularExpressions.Regex(@"(/)(\*)(.*)(\*)(/)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        text = regex.Replace(text, "");
        
        // Remove comments in // style
        regex = new System.Text.RegularExpressions.Regex(@"(\/)(\/)(.*)", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        text = regex.Replace(text, "");
        
        return text.Replace("\n", "");
    }
    
    public string GetFileContentInCache(string path)
    {
        var text = System.IO.File.ReadAllText(Server.MapPath(path));
        text = RemoveComments(text);

        return Convert.ToString(Cache[path] ?? Cache.Add(path, text, new CacheDependency(Server.MapPath(path)),
                                                        System.Web.Caching.Cache.NoAbsoluteExpiration,
                                                        System.Web.Caching.Cache.NoSlidingExpiration,
                                                        CacheItemPriority.Normal,
                                                        (string key, object value, CacheItemRemovedReason reason) => { GetFileContentInCache(key); }));
    }

    public void Include(string path)
    {
        Response.Write(GetFileContentInCache(path));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = Request["type"] ?? "text/javascript";

        Include("jquery.js");

        Include("jquery.cookies.js");
        Include("jquery.dimensions.js");
        Include("jquery.template.js");

        Include("jquery.jGrowl.js");
        Include("jquery.meioMask.js");
        Include("jquery.validate.js");
        Include("jquery.tooltip.js");
        Include("jquery.serializer.js");
        Include("jquery.template.js");

        Include("jquery.UI.core.js");
        Include("jquery.UI.widget.js");
        Include("jquery.UI.autocomplete.js");
        Include("jquery.UI.duallistbox.js");
        Include("jquery.ui.datepicker.js");

        Include("smartclient/jquery.smartclient.js");

    }
</script>

