using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinamentoAutomacao.Automation.Components
{
    public class VerificacaoDeErro
    {
        private IWebDriver driver;
        static IAlert alert;

        public VerificacaoDeErro(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void VerificaMensagemErro(string erro)
        {
            alert = driver.SwitchTo().Alert();
            string mensagemErro = alert.Text;
            string erroEsperado = erro;

            Assert.IsTrue(mensagemErro.Equals(erroEsperado));
        }
    }
}
