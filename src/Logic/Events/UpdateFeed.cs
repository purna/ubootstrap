﻿using System.Web;
using Bootstrap.Logic.Utils;
using umbraco.businesslogic;
using umbraco.cms.businesslogic.web;

namespace Bootstrap.Logic.Events
{
    public class UpdateFeed : ApplicationStartupHandler
    {
        public UpdateFeed()
        {
            Document.AfterPublish += (doc, e) => ClearFeedCache(doc);
            Document.AfterUnPublish += (doc, e) => ClearFeedCache(doc);
        }

        private void ClearFeedCache(Document sender)
        {
            int rootId;
            if (sender.Level <= 1) return;

            if (sender.ContentType.Alias == "Newslist")
                rootId = sender.Id;
            else if (new Document(sender.ParentId).ContentType.Alias == "Newslist")
                rootId = sender.ParentId;
            else
                return;

            var cacheName = string.Format(MainHelper.FeedCache, rootId);
            var cache = HttpRuntime.Cache[cacheName];
            if (cache == null) return;

            HttpRuntime.Cache.Remove(cacheName);
        }
    }
}
