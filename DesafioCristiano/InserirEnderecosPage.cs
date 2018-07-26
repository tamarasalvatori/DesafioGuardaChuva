using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class InserirEnderecosPage
    {
        private IWebDriver driver;

        public InserirEnderecosPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void PreencheEnderecoPrincipal(string cep, string endereco, string numero, string cidade, string telefone, string celular)
        {
            IWebElement digitaCEP = driver.FindElement(By.Id("addm_zipcode"));
            digitaCEP.SendKeys(cep);

            IWebElement digitaEndereco = driver.FindElement(By.Id("addm_address"));
            digitaEndereco.SendKeys(endereco);

            IWebElement digitaNumero = driver.FindElement(By.Id("addm_number"));
            digitaNumero.SendKeys(numero);

            IWebElement digitaCidade = driver.FindElement(By.Id("addm_city"));
            digitaCidade.SendKeys(cidade);

            IWebElement escolheEstado = driver.FindElement(By.Id("addm_state"));
            escolheEstado.Click();
            IWebElement estado = driver.FindElement(By.CssSelector("#addm_state > option:nth-child(3)"));
            estado.Click();

            IWebElement digitaTelefone = driver.FindElement(By.Id("addm_phone"));
            digitaTelefone.SendKeys(telefone);

            IWebElement digitaCelular = driver.FindElement(By.Id("addm_mobile"));
            digitaCelular.SendKeys(celular);

            IWebElement buttonSave = driver.FindElement(By.Id("save"));
        }

        public void PreencheEnderecoCobranca(string cep, string endereco, string numero, string cidade, string telefone, string celular)
        {
            IWebElement digitaCEP = driver.FindElement(By.Id("addb_zipcode"));
            digitaCEP.SendKeys(cep);

            IWebElement digitaEndereco = driver.FindElement(By.Id("addb_address"));
            digitaEndereco.SendKeys(endereco);

            IWebElement digitaNumero = driver.FindElement(By.Id("addb_number"));
            digitaNumero.SendKeys(numero);

            IWebElement digitaCidade = driver.FindElement(By.Id("addb_city"));
            digitaCidade.SendKeys(cidade);

            IWebElement escolheEstado = driver.FindElement(By.Id("addb_state"));
            escolheEstado.Click();
            IWebElement estado = driver.FindElement(By.CssSelector("#addb_state > option:nth-child(3)"));
            estado.Click();

            IWebElement digitaTelefone = driver.FindElement(By.Id("addb_phone"));
            digitaTelefone.SendKeys(telefone);

            IWebElement digitaCelular = driver.FindElement(By.Id("addb_mobile"));
            digitaCelular.SendKeys(celular);
        }
    }
}
