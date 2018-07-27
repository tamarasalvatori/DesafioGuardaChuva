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
        static IAlert alert;

        [TestInitialize]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=C:/Users/tamaras/AppData/Local/Google/Chrome/User Data");
            driver = new ChromeDriver(options);
            HomePage pagina = new HomePage(driver);
            pagina.VisitaPagina("file:///C:/Users/tamaras/Desktop/Version%202/index.html");
        }

        [TestMethod]
        public void CadastrandoPessoas()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

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

            cadastrando.FazLogin("paul", "paul");
            cadastrando.CadastroEmpresa((GeradorPage.GerandoCnpj()), "Tamara Salvatori", "tamarasalvatori@gmail.com");
            cadastrando.CadastroEnderecoPrinc("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.CadastroEnderecoCob("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.SalvaCadastro();
        }

        [TestMethod]
        public void TentativaCadastroPessoasSemCpf()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaCpfIncompleto("Tamara", "tamarasalvatori@gmail.com", "17071990");

            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroNome = "Mandatory field(s) not informed: \r\n \r\nPromo-code \r\n";

            Assert.IsTrue(mensagem.Equals(erroNome));
        }

        [TestMethod]
        public void TentativaCadastroPessoasCpfInvalido()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaCpfInvalido("00000000000", "Tamara", "tamarasalvatori@gmail.com", "17071990");

            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroNome = "Invalid Promotional Code";

            Assert.IsTrue(mensagem.Equals(erroNome));
        }

        [TestMethod]
        public void TentativaCadastroPessoasSemNome()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaNomeIncompleto((GeradorPage.GerandoCpf()), "tamarasalvatori@gmail.com", "17071990");

            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroNome = "Mandatory field(s) not informed: \r\n \r\nName/Company Name \r\n";

            Assert.IsTrue(mensagem.Equals(erroNome));
        }

        [TestMethod]
        public void TentativaCadastroPessoasSemNascimento()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaNascimentoIncompleto((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com");

            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroNascimento = "Mandatory field(s) not informed: \r\n \r\nBirthday Date \r\n";

            Assert.IsTrue(mensagem.Equals(erroNascimento));
        }

        [TestMethod]
        public void CadastroEmailInvalidoPessoa()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaEmailInvalido((GeradorPage.GerandoCpf()), "Tamara Salvatori", "tamarasalvatori@.com", "17071990");

            alert = driver.SwitchTo().Alert();
            string mensagemEmailInvalido = alert.Text;
            string emailInvalido = "Invalid E-mail!";

            Assert.IsTrue(mensagemEmailInvalido.Equals(emailInvalido));
        }

        [TestMethod]
        public void TentativaCadastroPessoasSemEmail()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaEmailIncompleto((GeradorPage.GerandoCpf()), "Tamara Salvatori", "17071990");
           
            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroEmail = "Mandatory field(s) not informed: \r\n \r\nEmail \r\n";

            Assert.IsTrue(mensagem.Equals(erroEmail));
        }

        [TestMethod]
        public void CadastroGeneroInvalidoPessoa()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaGeneroIncompleto((GeradorPage.GerandoCpf()), "Tamara Salvatori", "tamarasalvatori@gmail.com", "17071990");

            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroGenero = "Mandatory field(s) not informed: \r\n \r\nGender \r\n";

            Assert.IsTrue(mensagem.Equals(erroGenero));
        }

        [TestMethod]
        public void CadastroEstCivilInvalidoPessoa()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesIncompletosWorkflow cadastrando = new CadastrandoClientesIncompletosWorkflow(driver);
            cadastrando.FazLogin("paul", "paul");

            cadastrando.CadastroPessoaEstCivilIncompleto((GeradorPage.GerandoCpf()), "Tamara Salvatori", "tamarasalvatori@gmail.com", "17071990");

            alert = driver.SwitchTo().Alert();
            string mensagem = alert.Text;
            string erroGenero = "Mandatory field(s) not informed: \r\n \r\nMarital Status \r\n";

            Assert.IsTrue(mensagem.Equals(erroGenero));
        }

        /*[TestCleanup]
         public void Cleanup()
         {
             Thread.Sleep(1000);
             driver.Close();
         }*/

    }
}
