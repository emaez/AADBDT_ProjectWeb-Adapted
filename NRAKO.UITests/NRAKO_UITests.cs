using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace NRAKO.UITests
{
    public class NRAKO_UITests
    {
        private bool isLoggedin = false;
        IWebDriver webDriver;
        [OneTimeSetUp]
        public void StartChrome()
        {
            webDriver = WebDriverSingleton.GetInstance();
        }

        [Test]
        public void Login()
        {
            webDriver.Url = "http://localhost:55076/Identity/Account/Login";
            var input_userName = webDriver.FindElement(By.Id("Input_Username"));
            var input_password = webDriver.FindElement(By.Id("Input_Password"));
            input_userName.SendKeys("Admin");
            input_password.SendKeys("Pa$$w0rd");
            var button = webDriver.FindElement(By.CssSelector("button[type='submit']"));
            button.Click();
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
            isLoggedin = true;
            Assert.Pass();
        }
        [Test]
        public void UploadPhoto()
        {
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30.00));

            // login First
            if (!isLoggedin)
            {
                webDriver.Url = "http://localhost:55076/Identity/Account/Login";
                var input_userName = webDriver.FindElement(By.Id("Input_Username"));
                var input_password = webDriver.FindElement(By.Id("Input_Password"));
                input_userName.SendKeys("Admin");
                input_password.SendKeys("Pa$$w0rd");
                var button = webDriver.FindElement(By.CssSelector("button[type='submit']"));
                button.Click();
                wait.Until(driver1 => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
            }

            webDriver.Url = "http://localhost:55076/Photos/CreateNewPhoto";
            var description = webDriver.FindElement(By.Id("Description"));
            var hashTag = webDriver.FindElement(By.Id("HashtagsString"));
            var inputFile = webDriver.FindElement(By.Id("file"));
            description.SendKeys("This is file uploaded by UI test");
            hashTag.SendKeys("UITest");

            //Get file path
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var pathUrlToDllDirectory = Path.GetDirectoryName(codebase);
            var pathToDllDirectory = new Uri(pathUrlToDllDirectory).LocalPath;
            var rootPath = pathToDllDirectory.Split("\\bin\\")[0];
            var path = rootPath + @"\image.jpg";

            inputFile.SendKeys(path);

            var createButton = webDriver.FindElement(By.CssSelector("input[value='Create']"));
            createButton.Click();
            wait.Until(driver1 => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
            Assert.Pass();
        }
        [Test]
        public void FilterPhotos()
        {
            webDriver.Url = "http://localhost:55076";
            var hashtag = webDriver.FindElement(By.Name("hashtagsFilter"));
            hashtag.SendKeys("UITest");
            var button = webDriver.FindElement(By.CssSelector("input[value='Filter']"));
            button.Click();
            IWait<IWebDriver> wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30.00));
            wait.Until(driver1 => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
            Assert.Pass();
        }

        [OneTimeTearDown]
        public void CloseTest()
        {
            webDriver.Close();
        }
    }
}
