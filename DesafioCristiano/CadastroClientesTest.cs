using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TreinamentoAutomacao.Automation.Workflows;
using TreinamentoAutomacao.Automation.Pages;
using TreinamentoAutomacao.Automation.Components;

namespace TreinamentoAutomacao
{
    [TestClass]
    public class CadastroClientesTest
    {
        private IWebDriver driver;
        static IAlert alert;

        private HomePage pagina;

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get {return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=C:/Users/tamaras/AppData/Local/Google/Chrome/User Data");
            driver = new ChromeDriver(options);
            pagina = new HomePage(driver);
            pagina.VisitaPagina("file:///C:/Users/tamaras/Desktop/Version%202/index.html");
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV","dataSource/dados.csv","dados#csv",DataAccessMethod.Sequential)]
        [TestMethod]
        public void CadastrandoPessoas()
        {
            //conteudo da linha e da coluna
            string nome = Convert.ToString(TestContext.DataRow["Nome"]);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), nome, "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");
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

            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroEmpresa((GeradorPage.GerandoCnpj()), "Tamara Salvatori", "tamarasalvatori@gmail.com");
            cadastrando.CadastroEnderecoPrinc("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.CadastroEnderecoCob("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.SalvaCadastro();
        }

        [TestMethod]
        public void CadastrandoPessoasCpfIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa("", "Tamara Salvatori", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");
            
            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nPromo-code \r\n");
        }

        [TestMethod]
        public void CadastrandoPessoasNomeIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nName/Company Name \r\n");
        }

        [TestMethod]
        public void CadastrandoPessoasEmailIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nEmail \r\n");
        }
        
        [TestMethod]
        public void CadastrandoPessoasNascimentoIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com", "", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nBirthday Date \r\n");
        }

        [TestMethod]
        public void CadastrandoPessoasGeneroIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(1)", "#marital_status > option:nth-child(2)");

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nGender \r\n");
        }

        [TestMethod]
        public void CadastrandoPessoasEstCivilIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(1)");

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nMarital Status \r\n");
        }

        [TestCleanup]
         public void Cleanup()
         {
             Thread.Sleep(1000);
             driver.Close();
         }

    }
}
