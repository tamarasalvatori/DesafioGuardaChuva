using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DesafioCristiano
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        
        [TestInitialize]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=C:/Users/tamaras/AppData/Local/Google/Chrome/User Data");
            driver = new ChromeDriver(options);
            HomePage pagina = new HomePage(driver);
            pagina.VisitaPagina("file:///C:/Users/tamaras/Desktop/Version%202/index.html");

           // Gerador geraPromCode = new Gerador(driver);
            //geraPromCode.GerandoCnpj();
        }

    [TestMethod]
        public void CadastrandoPessoas()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            //Gerador geraPromCode = new Gerador();
            //geraPromCode.GerandoCpf();

            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara Salvatori", "tamarasalvatori@gmail.com", "17071990");
            cadastrando.CadastroEnderecoPrinc("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.CadastroEnderecoCob("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.SalvaCadastro();

            Thread.Sleep(500);

            bool cadastroRealizado = driver.PageSource.Contains("Client inserted with success");
            Assert.IsTrue(cadastroRealizado);
        }

    [TestMethod]
        public void CadastrandoEmpresas()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            //Gerador geraPromCode = new Gerador();
            //geraPromCode.GerandoCnpj();

            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroEmpresa((GeradorPage.GerandoCnpj()), "Tamara Salvatori", "tamarasalvatori@gmail.com");
            cadastrando.CadastroEnderecoPrinc("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.CadastroEnderecoCob("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.SalvaCadastro();
        }

       /* [TestCleanup]
        public void Cleanup()
        {
            Thread.Sleep(1000);
            driver.Close();
        }*/

    }
}
