using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Allegro
{
    class Selenium
    {
        public ChromeDriver driver;

        static void Main(string[] args)
        {

            string searchItemTextBox = ".//input[@type='search']";
            string searchButton = ".//button[text()='szukaj']";
            string moveNextButton = ".//button[text()='przejdź dalej']";
            string blackColorCheckBox = ".//span[text()='czarny']";
            string itemName = "Iphone 11";
            string url = "https://allegro.pl/";

            var selenium = new Selenium();

            selenium.SetChromeDriver();
            selenium.NavigateToPage(url);
            selenium.ClickButtonOrChecbox(moveNextButton);
            selenium.SetText(itemName, searchItemTextBox);
            selenium.ClickButtonOrChecbox(searchButton);
            selenium.ClickButtonOrChecbox(blackColorCheckBox);
            selenium.PrintCountItemsOnPage();
            selenium.MaxPrice();
            selenium.AddingValueToMax();
        }

        public void SetChromeDriver()
        {
            driver = new ChromeDriver(@"C:\Users\Alan\source\repos\Allegro");

            
        }

        public void NavigateToPage(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void SetText(string text, string xPath)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var textBox = driver.FindElementByXPath(xPath);
            textBox.Clear();
            textBox.SendKeys(text);
        }

        public void ClickButtonOrChecbox(string xPath)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var control = driver.FindElementByXPath(xPath);
            control.Click();
        }

        public void PrintCountItemsOnPage()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var itemsOnPage = driver.FindElementsByXPath(".//section/article");
            int countItemsOnPage = itemsOnPage.Count;

            Console.WriteLine("Items on page: " + countItemsOnPage);
        }

        public double MaxPrice()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var itemsOnPage = driver.FindElementsByXPath(".//section/article");
            var pricesOfItemsList = new List<double>();

            for (int i = 0; i < itemsOnPage.Count; i++)
            {
                var price = driver.FindElementByXPath($"((.//section/article)[{i+1}]/div/div/div[2]/div[2]//span)[1]");

                string priceText = price.Text;
                priceText = price.Text.Replace(',', '.').Remove(price.Text.Length - 3).Replace(" ", "");
                double priceValue = Convert.ToDouble(priceText);

                pricesOfItemsList.Add(priceValue);
            }

            pricesOfItemsList.Sort();

            double maxPrice = pricesOfItemsList[pricesOfItemsList.Count - 1];

            Console.WriteLine("Max price: " + maxPrice);

            return maxPrice;
        }

        public double AddingValueToMax()
        {
            Console.WriteLine(MaxPrice() * 1.23);
            return MaxPrice() * 1.23;
        }
    }
}
