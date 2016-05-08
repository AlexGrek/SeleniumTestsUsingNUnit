using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace SeleniumTests
{
    [TestFixture]
    public class Exportedfilecs
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://uk.wikipedia.org/";
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
        
        [Test]
        public void TheExportedfilecsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "wiki/%D0%93%D0%BE%D0%BB%D0%BE%D0%B2%D0%BD%D0%B0_%D1%81%D1%82%D0%BE%D1%80%D1%96%D0%BD%D0%BA%D0%B0");
            
            driver.FindElement(By.LinkText("Війна у Сирії")).Click();
            TakeScreenshot("1.png");
            
            driver.FindElement(By.CssSelector("a.mw-wiki-logo")).Click();
            TakeScreenshot("2.png");
            driver.FindElement(By.LinkText("English")).Click();
            TakeScreenshot("3.png");
            driver.FindElement(By.CssSelector("a[title=\"German\"]")).Click();
            driver.FindElement(By.LinkText("Українська")).Click();
            // ERROR: Caught exception [unknown command []]
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

        public void TakeScreenshot(string filename)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                using (MemoryStream ms = new MemoryStream(ss.AsByteArray))
                using (Image screenShotImage = Image.FromStream(ms))
                {
                    Bitmap cp = new Bitmap(screenShotImage);
                    cp.Save(@"C:\Users\AlexG\Pictures\" + filename, ImageFormat.Png);
                    cp.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
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
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
