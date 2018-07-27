using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class InserirClientePage
    {
        private IWebDriver driver;
        private IWebElement selecionaTipo;
        private IWebElement digitaCpfCnpj;
        private IWebElement digitaNome;
        private IWebElement digitaEmail;
        private IWebElement digitaNascimento;
        private IWebElement escolheGenero;
        private IWebElement genero;
        private IWebElement escolheEstadoCivil;
        private IWebElement estadoCivil;
        private IWebElement buttonNext;
        private string idTipoPessoa = "person";
        private string idTipoEmpresa = "company";
        private string idCpfCnpj = "promo_code";
        private string idNome = "name_companyname";
        private string idEmail = "email";
        private string idNascimento = "birthday_date";
        private string idGenero = "gender";
        //private string generoFem = "#gender > option:nth-child(3)";
        private string idEstadoCivil = "marital_status";
        //private string estadoCasadx = "#marital_status > option:nth-child(2)";
        private string idNext = "next";

        public InserirClientePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PreencheIdentificacaoPessoa(string cpf, string nome, string email, string nascimento, string generoEscolhido, string estCivil)
        {
            selecionaTipo = driver.FindElement(By.Id(idTipoPessoa));
            selecionaTipo.Click();

            digitaCpfCnpj = driver.FindElement(By.Id(idCpfCnpj));
            digitaCpfCnpj.SendKeys(cpf);

            digitaNome = driver.FindElement(By.Id(idNome));
            digitaNome.SendKeys(nome);

            digitaEmail = driver.FindElement(By.Id(idEmail));
            digitaEmail.SendKeys(email);

            digitaNascimento = driver.FindElement(By.Id(idNascimento));
            digitaNascimento.SendKeys(nascimento);

            escolheGenero = driver.FindElement(By.Id(idGenero));
            escolheGenero.Click();
            genero = driver.FindElement(By.CssSelector(generoEscolhido));
            genero.Click();

            escolheEstadoCivil = driver.FindElement(By.Id(idEstadoCivil));
            escolheEstadoCivil.Click();
            estadoCivil = driver.FindElement(By.CssSelector(estCivil));
            estadoCivil.Click();

            buttonNext = driver.FindElement(By.Id(idNext));
            buttonNext.Click();
        }

        public void PreencheIdentificacaoEmpresa(string cnpj, string nome, string email)
        {
            selecionaTipo = driver.FindElement(By.Id(idTipoEmpresa));
            selecionaTipo.Click();

            digitaCpfCnpj = driver.FindElement(By.Id(idCpfCnpj)); 
            digitaCpfCnpj.SendKeys(cnpj);

            digitaNome = driver.FindElement(By.Id(idNome));
            digitaNome.SendKeys(nome);

            digitaEmail = driver.FindElement(By.Id(idEmail));
            digitaEmail.SendKeys(email);

            buttonNext = driver.FindElement(By.Id(idNext));
            buttonNext.Click();
        }
    }
}
