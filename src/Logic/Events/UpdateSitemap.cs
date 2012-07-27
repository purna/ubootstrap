﻿using System.Web;
using Bootstrap.Logic.Utils;
using umbraco.businesslogic;
using umbraco.cms.businesslogic;
using umbraco.cms.businesslogic.web;

namespace Bootstrap.Logic.Events
{
    public class UpdateSitemap : ApplicationStartupHandler
    {
        public UpdateSitemap()
        {
            Document.AfterPublish += OnAfterPublish;
            Document.AfterUnPublish += OnAfterUnPublish;
        }

        private void OnAfterPublish(Document sender, PublishEventArgs e)
        {
            ClearSitemapCache(sender);
        }

        private void OnAfterUnPublish(Document sender, UnPublishEventArgs e)
        {
            ClearSitemapCache(sender);
        }

        private void ClearSitemapCache(Document sender)
        {
            var ids = sender.Path.Split(',');
            if (ids.Length < 1)
            {
                return;
            }

            var rootId = ids[1];
            var cacheName = string.Format(MainHelper.XmlSitemapCache, rootId);
            var cache = HttpRuntime.Cache[cacheName];
            if (cache == null)
            {
                return;
            }

            HttpRuntime.Cache.Remove(cacheName);
        }
    }
}
