using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class FormularioPage
    {

        private IWebDriver driver;

        public FormularioPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void AbrindoFormulario()
        {
            IWebElement clicaClients = driver.FindElement(By.XPath("//a[contains(text(), 'Clients')]"));
            clicaClients.Click();

            IWebElement clicaInsert = driver.FindElement(By.CssSelector("[href='insertclient_identification.html']"));
            clicaInsert.Click();
        }
    }
}
