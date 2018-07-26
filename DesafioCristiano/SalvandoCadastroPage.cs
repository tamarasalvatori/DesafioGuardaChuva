using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class SalvandoCadastroPage
    {
        private IWebDriver driver;

        public SalvandoCadastroPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SalvaEnderecos()
        {
            IWebElement buttonSave = driver.FindElement(By.Id("save"));
            buttonSave.Click();
        }
    }
}
