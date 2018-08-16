using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TreinamentoAutomacao.Automation.Workflows;
using TreinamentoAutomacao.Automation.Pages;
using TreinamentoAutomacao.Automation.Components;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using TreinamentoAutomacao.Automation;
using System.Linq;
using System.Text;

namespace TreinamentoAutomacao
{
    [TestClass]
    public class CadastroClientesTest
    {
        public static SqlConnection UmbrellasFactory { get; private set; }

        private IWebDriver driver;
        static IAlert alert;

        private const string EvidencesDirectory = "C:\\Evidences2\\";
        private Log log;

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
            const string EvidencesDirectory = "C:\\Evidences2\\";
            string logFileName = "Test_" + GetTimestamp();
            log = new Log(EvidencesDirectory, logFileName);

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=C:/Users/tamaras/AppData/Local/Google/Chrome/User Data");
            driver = new ChromeDriver(options);
            pagina = new HomePage(driver);

            log.Write("Acessando to Umbrellas Factory's page");

            pagina.VisitaPagina("file:///C:/Users/tamaras/Desktop/Version%202/index.html");

            log.Write("Screenshot da página inicial");

            Screenshot umbrellaInitial = ((ITakesScreenshot)driver).GetScreenshot();
            umbrellaInitial.SaveAsFile(EvidencesDirectory + GetTimestamp() + ".jpeg", ScreenshotImageFormat.Jpeg);
        }

        [TestMethod]
        public void CadastrandoPessoas()
        {
            List<ClientePf2> clientesPf2 = ObterClientesPf2();
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados pessoais");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), clientesPf2.First().Nome, "Tamara@gsd.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            log.Write("Preenchendo campos de endereço");
            cadastrando.CadastroEnderecoPrinc("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");
            cadastrando.CadastroEnderecoCob("55555", "Maestro Mendanha", "84", "Porto Alegre", "5555555555", "7777777777");

            cadastrando.SalvaCadastro();
            Thread.Sleep(500);

            log.Write("Screenshot sucesso - Company client");
            Screenshot umbrellaPersonalClientSuccess = ((ITakesScreenshot)driver).GetScreenshot();
            umbrellaPersonalClientSuccess.SaveAsFile(EvidencesDirectory + GetTimestamp() + ".jpeg", ScreenshotImageFormat.Jpeg);

            log.Save();

            bool cadastroRealizado = driver.PageSource.Contains("Client inserted with success");
            Assert.IsTrue(cadastroRealizado);
        }

        private List<ClientePf2> ObterClientesPf2()
        {
            SqlConnection UmbrellasFactory = null;
            var clientesPf2 = new List<ClientePf2>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";
                builder.IntegratedSecurity = true;
                builder.ConnectTimeout = 30;
                builder.Encrypt = false;
                builder.TrustServerCertificate = false;
                builder.ApplicationIntent = ApplicationIntent.ReadWrite;
                builder.MultiSubnetFailover = false;
                builder.InitialCatalog = "UmbrellasFactory";
                Console.WriteLine(builder.ToString());
                using (UmbrellasFactory = new SqlConnection(builder.ToString()))
                {
                    UmbrellasFactory.Open();
                    SqlDataReader myReader = null;

                    var sb = new StringBuilder();

                    sb.AppendLine("select C.Nome [Nome Cliente], C.[Email], D. Id as [Id], D.Tipo as [Documento], D.Numero as [Numero]");
                    sb.AppendLine("from ClientePf as C");
                    sb.AppendLine("left join ClientePfDocumento as D on C.Id = D.ClientePF_Id");

                    using (SqlCommand myCommand = new SqlCommand(sb.ToString(), UmbrellasFactory))
                    {
                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            var nomeAux = myReader["Nome Cliente"] ?? null;
                            var idAux = myReader["Id"] ?? null;
                            var emailAux = myReader["Email"] ?? null;
                            var documentoAux = myReader["Numero"] ?? null;

                            var cliente2 = new ClientePf2
                            {
                                Nome = nomeAux == DBNull.Value ? string.Empty : nomeAux.ToString(),
                                Id = idAux == DBNull.Value ? Guid.Empty : Guid.Parse(idAux.ToString()),
                                Email = emailAux == DBNull.Value ? string.Empty : emailAux.ToString(),
                                Documento = documentoAux == DBNull.Value ? 0L : long.Parse(documentoAux.ToString())
                            };

                            clientesPf2.Add(cliente2);
                        }
                    }
                }
                //clientesPf2.First().Nome;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                UmbrellasFactory.Close();
            }
            return clientesPf2;
        }

        private string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyMMdd_hhmmss");
        }

       /* [TestMethod]
        public void CadastrandoEmpresas()
        {
            List<AccountJuridica> accountsJuridica = DeserializeAccountJuridica();

            
            List<AccountJuridica> accountListJuridica = accountsJuridica.Where(x => x.Nome == "Leonardo").ToList();
            AccountJuridica accountJuridica = accountListJuridica.First();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados da empresa");
            cadastrando.CadastroEmpresa((GeradorPage.GerandoCnpj()), accountJuridica.Nome, accountJuridica.Email);

            log.Write("Preenchendo campos de endereço");
            cadastrando.CadastroEnderecoPrinc(accountJuridica.Cep.ToString(), accountJuridica.Endereco, accountJuridica.Numero.ToString(), accountJuridica.Cidade, accountJuridica.Telefone, accountJuridica.Celular);
            cadastrando.CadastroEnderecoCob(accountJuridica.Cep.ToString(), accountJuridica.Endereco, accountJuridica.Numero.ToString(), accountJuridica.Cidade, accountJuridica.Telefone, accountJuridica.Celular);

            cadastrando.SalvaCadastro();
            Thread.Sleep(500);

            log.Write("Screenshot sucesso - Personal client");
            Screenshot umbrellaCompanyClientSuccess = ((ITakesScreenshot)driver).GetScreenshot();
            umbrellaCompanyClientSuccess.SaveAsFile(EvidencesDirectory + GetTimestamp() + ".jpeg", ScreenshotImageFormat.Jpeg);

            log.Save();

            bool cadastroRealizado = driver.PageSource.Contains("Client inserted with success");
            Assert.IsTrue(cadastroRealizado);
        }
        */

        [TestMethod]
        public void CadastrandoPessoasCpfIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados do cliente - sem CPF");
            cadastrando.CadastroPessoa("", "Tamara Salvatori", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");
            
            log.Write("Screenshot falha CPF - Personal client");
            Screenshot umbrellaCompanyClientFailCpf = ((ITakesScreenshot)driver).GetScreenshot();
            umbrellaCompanyClientFailCpf.SaveAsFile(EvidencesDirectory + GetTimestamp() + ".jpeg", ScreenshotImageFormat.Jpeg);

            IAlert alert = driver.SwitchTo().Alert();
            
            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nPromo-code \r\n");
            log.Save();
        }

        [TestMethod]
        public void CadastrandoPessoasNomeIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados do cliente - sem Nome");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            //
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(printscreen as Image))
            {
                graphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.GetBounds(Point.Empty).Size);
            }

            byte[] img = (byte[])new ImageConverter().ConvertTo(printscreen, typeof(byte[]));
            File.WriteAllBytes(@"C:\Evidences2\PrintSemNome.jpg", img);
            //

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nName/Company Name \r\n");
            log.Save();

        }

        [TestMethod]
        public void CadastrandoPessoasEmailIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados do cliente - sem Email");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            //
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(printscreen as Image))
            {
                graphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.GetBounds(Point.Empty).Size);
            }

            byte[] img = (byte[])new ImageConverter().ConvertTo(printscreen, typeof(byte[]));
            File.WriteAllBytes("PrintSemEmail.jpg", img);
            //

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nEmail \r\n");
            log.Save();

        }

        [TestMethod]
        public void CadastrandoPessoasNascimentoIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados do cliente - sem Nascimento");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com", "", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(2)");

            //
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(printscreen as Image))
            {
                graphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.GetBounds(Point.Empty).Size);
            }

            byte[] img = (byte[])new ImageConverter().ConvertTo(printscreen, typeof(byte[]));
            File.WriteAllBytes("PrintSemNascimento.jpg", img);
            //

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nBirthday Date \r\n");
            log.Save();

        }

        [TestMethod]
        public void CadastrandoPessoasGeneroIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados do cliente - sem Gênero");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(1)", "#marital_status > option:nth-child(2)");

            //
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(printscreen as Image))
            {
                graphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.GetBounds(Point.Empty).Size);
            }

            byte[] img = (byte[])new ImageConverter().ConvertTo(printscreen, typeof(byte[]));
            File.WriteAllBytes("PrintSemGenero.jpg", img);
            //

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nGender \r\n");
            log.Save();

        }

        [TestMethod]
        public void CadastrandoPessoasEstCivilIncompleto()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            CadastrandoClientesWorkflow cadastrando = new CadastrandoClientesWorkflow(driver);

            log.Write("Preenchendo campos de login");
            cadastrando.FazLogin("paul", "paul");

            log.Write("Preenchendo campos de dados do cliente - sem Estado Civil");
            cadastrando.CadastroPessoa((GeradorPage.GerandoCpf()), "Tamara", "tamarasalvatori@gmail.com", "17071990", "#gender > option:nth-child(3)", "#marital_status > option:nth-child(1)");

            //
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            using (Graphics graphics = Graphics.FromImage(printscreen as Image))
            {
                graphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.GetBounds(Point.Empty).Size);
            }

            byte[] img = (byte[])new ImageConverter().ConvertTo(printscreen, typeof(byte[]));
            File.WriteAllBytes("PrintSemEstCivil.jpg", img);
            //

            VerificacaoDeErro verificaErro = new VerificacaoDeErro(driver);
            verificaErro.VerificaMensagemErro("Mandatory field(s) not informed: \r\n \r\nMarital Status \r\n");
            log.Save();
        }

        [TestCleanup]
         public void Cleanup()
         {
             Thread.Sleep(1000);

            if (driver != null)
            {
                driver.Close();
                driver.Dispose();
            }
         }

    }
}
