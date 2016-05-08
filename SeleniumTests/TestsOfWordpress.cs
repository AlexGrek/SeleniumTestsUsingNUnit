using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Threading;

namespace SeleniumTests
{
    [TestFixture]
    public class Csharp
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demosite.center/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        //TASK 2 TEST 1
        [Test]
        public void TheCsharpTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/wordpress/wp-login.php");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("admin");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("demo123");
            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.TagName("BODY")).Text, ".*Howdy, admin.*"));
            driver.FindElement(By.LinkText("Posts")).Click();
            driver.FindElement(By.CssSelector("a.add-new-h2")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Selenium");
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Selenium Demo Post");
            driver.FindElement(By.Id("publish")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("#message > p"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#message > p")).Text, "Post published.*"));
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        
        //task 2 test 2
        [Test]
        public void TheTask2test2Test()
        {
            driver.Navigate().GoToUrl(baseURL + "/wordpress/wp-login.php");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys("admin");
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys("demo");
            driver.FindElement(By.Id("wp-submit")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.Id("login_error"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.Id("login_error")).Text, "ERROR: .*The password you entered for the username admin is incorrect.*"));
        }

        public string[] ReadFile()
        {
            //in root directory, because NUnit cannot copy files to testing directory (unlike the MSTest)
            return File.ReadAllLines(@"C:\TextFile.txt");
        }

        //TASK 3
        [Test]
        public void TheCsharpTest2()
        {
            var file = ReadFile();
            driver.Navigate().GoToUrl(baseURL + "/wordpress/wp-login.php");
            driver.FindElement(By.Id("user_login")).Clear();
            driver.FindElement(By.Id("user_login")).SendKeys(file[0]);
            driver.FindElement(By.Id("user_pass")).Clear();
            driver.FindElement(By.Id("user_pass")).SendKeys(file[1]);
            driver.FindElement(By.Id("wp-submit")).Click();
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.TagName("BODY")).Text, ".*Howdy, admin.*"));
            driver.FindElement(By.LinkText("Posts")).Click();
            driver.FindElement(By.CssSelector("a.add-new-h2")).Click();
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Selenium");
            driver.FindElement(By.Id("title")).Clear();
            driver.FindElement(By.Id("title")).SendKeys("Selenium Demo Post");
            driver.FindElement(By.Id("publish")).Click();
            for (int second = 0; ; second++)
            {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("#message > p"))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("#message > p")).Text, "Post published.*"));
        }
    }
}
