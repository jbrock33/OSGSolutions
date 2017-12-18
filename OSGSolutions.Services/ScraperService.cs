using HtmlAgilityPack;
using OSGSolutions.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGSolutions.Services
{
    public class ScraperService
    {
        public ScraperList GetAll()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            ScraperList model = new ScraperList();
            model.Items = new List<ScraperItem>();

            string url = "https://www.reddit.com/r/Nootropics/search?q=flair%3A%22Scientific+Study%22+OR+site%3Ancbi.nlm.nih.gov&restrict_sr=on&sort=new&t=all";

            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url);

            var anchorTags = document.DocumentNode.Descendants("a")
               .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("search-title"));

            foreach (var node in anchorTags)
            {
                ScraperItem item = new ScraperItem();
                item.PostTitle = node.InnerText;
                item.PostUrl = node.GetAttributeValue("href", null);
                //item is pushed to model list
                model.Items.Add(item);
            }

            return model;
        }
    }
}
