using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class CadastrandoClientesWorkflow
    {
        private IWebDriver driver;
        private LoginPage fazLogin;
        private FormularioPage mostraFormulario;
        private InserirClientePage preencheDadosPessoa;
        private InserirClientePage preencheDadosEmpresa;
        private InserirEnderecosPage preencheEnderecoPrincipal;
        private InserirEnderecosPage preencheEnderecoCobranca;
        private SalvandoCadastroPage clicaSalvar;
        //private Gerador geraCpf;
        //private Gerador geraCnpj;

        public CadastrandoClientesWorkflow(IWebDriver driver)
        {
            this.fazLogin = new LoginPage(driver);
            this.mostraFormulario = new FormularioPage(driver);
            this.preencheDadosPessoa = new InserirClientePage(driver);
            this.preencheDadosEmpresa = new InserirClientePage(driver);
            this.preencheEnderecoPrincipal = new InserirEnderecosPage(driver);
            this.preencheEnderecoCobranca = new InserirEnderecosPage(driver);
            this.clicaSalvar = new SalvandoCadastroPage(driver);
            //this.geraCpf = new Gerador(driver);
            //this.geraCnpj = new Gerador(driver);
        }

        public void FazLogin(string name, string password)
        {
            fazLogin.DigitaNome(name);
            fazLogin.DigitaSenha(password);
            fazLogin.ClicaLogin();
        }

        public void CadastroPessoa(string cpf, string nome, string email, string nascimento)
        {
            //geraCpf.GerandoCpf();
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentificacaoPessoa(cpf, nome, email, nascimento);
        }

        public void CadastroEmpresa(string cnpj, string nome, string email)
        {
            //geraCnpj.GerandoCnpj();
            mostraFormulario.AbrindoFormulario();
            preencheDadosEmpresa.PreencheIdentificacaoEmpresa(cnpj, nome, email);
        }

        public void CadastroEnderecoPrinc(string cep, string endereco, string numero, string cidade, string telefone, string celular)
        {
            preencheEnderecoPrincipal.PreencheEnderecoPrincipal(cep, endereco, numero, cidade, telefone, celular);
        }

        public void CadastroEnderecoCob(string cep, string endereco, string numero, string cidade, string telefone, string celular)
        {
            preencheEnderecoCobranca.PreencheEnderecoCobranca(cep, endereco, numero, cidade, telefone, celular);
        }

        public void SalvaCadastro()
        {
            clicaSalvar.SalvaEnderecos();
        }

    }


    
}
