using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;
using System.Linq;
using ConsoleApp1;

namespace sahibinden
{
    class Program
    {
        public static List<Product> products = new List<Product>();
        //created product list globally at program class to make reachable from other methods
        static void Main(string[] args)
        {
            string url = "https://www.sahibinden.com";
            HtmlWeb web = new HtmlWeb();
            //used HAP package to get source code
            HtmlDocument document = web.Load(url);
            TextWriter writer = new StreamWriter("D:\\Products.txt");
            var Nodes = document.DocumentNode.SelectNodes("/html/body/div[5]/div[3]/div/div[3]/div[3]/ul/li/a");
            //reached products links from uibox-showcase sahibinden homepage with full xpath
            foreach (var node in Nodes)
            {
                //Creating product from each node
                CreateProduct(url + node.GetAttributeValue("href", "").ToString());
            }
            foreach (var product in products)
            {
                Console.WriteLine(product.Title + "         " + product.Price);
                writer.WriteLine(product.Title + "         " + product.Price);
            }
        }
        public static void CreateProduct(string url)
        {
            // Get data from product page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument productDocument = web.Load(url);
            try
            {
                // Get title and price of product
                string title = productDocument.DocumentNode.SelectNodes("//div[contains(@class, 'classifiedDetailTitle')]/h1").FirstOrDefault().InnerText;
                string price = productDocument.DocumentNode.SelectNodes("//div[contains(@class, 'classifiedInfo ')]/h3").FirstOrDefault().InnerText.Trim().Split(" ")[0];
                
                Product product = new Product()
                {
                    Url = url,
                    Price = price,
                    Title = title,
                };
                // Product Added to product list
                products.Add(product);
            }
            catch
            {
                return;
            }
        }
    }
}