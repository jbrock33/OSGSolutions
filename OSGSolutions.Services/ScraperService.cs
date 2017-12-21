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
            
            string url = "https://www.indeed.com/jobs?q=full+stack+developer&l=austin%2C+tx&sort=date";

            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            document = htmlWeb.Load(url);

            var anchorTags = document.DocumentNode.Descendants("div")
               .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("result") && d.Attributes["class"].Value.Contains("row"));

            foreach (var node in anchorTags)
            {
                ScraperItem item = new ScraperItem();
                item.PostTitle = node.InnerText;
                item.PostUrl = node.GetAttributeValue("href", null);
                item.PostUrl = node.InnerHtml;
                model.Items.Add(item);
            }
            

            htmlWeb = new HtmlWeb();
            document = htmlWeb.Load(url + "&start=10");

            anchorTags = document.DocumentNode.Descendants("div")
               .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("result") && d.Attributes["class"].Value.Contains("row"));

            foreach (var node in anchorTags)
            {
                ScraperItem item = new ScraperItem();
                item.PostTitle = node.InnerText;
                item.PostUrl = node.GetAttributeValue("href", null);
                item.PostUrl = node.InnerHtml;
                model.Items.Add(item);
            }

            htmlWeb = new HtmlWeb();
            document = htmlWeb.Load(url + "&start=20");

            anchorTags = document.DocumentNode.Descendants("div")
               .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("result") && d.Attributes["class"].Value.Contains("row"));

            foreach (var node in anchorTags)
            {
                ScraperItem item = new ScraperItem();
                item.PostTitle = node.InnerText;
                item.PostUrl = node.GetAttributeValue("href", null);
                item.PostUrl = node.InnerHtml;
                model.Items.Add(item);
            }

            return model;
        }
    }
}
