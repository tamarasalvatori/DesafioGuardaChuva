using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    public class LoginPage
    {
        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void DigitaNome(string name)
        {
            IWebElement campoNome = driver.FindElement(By.XPath("//input[@id='login']"));
            campoNome.SendKeys(name);
        }

        public void DigitaSenha(string password)
        {
            IWebElement campoSenha = driver.FindElement(By.XPath("//input[@id='password']"));
            campoSenha.SendKeys(password);

        }

        public void ClicaLogin()
        {
            IWebElement buttonLogin = driver.FindElement(By.XPath("//input[@id='btnLogin']"));
            buttonLogin.Click();
        }
        

    }
}
