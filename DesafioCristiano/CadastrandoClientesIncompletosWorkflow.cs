using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class CadastrandoClientesIncompletosWorkflow
    {
        private IWebDriver driver;
        private LoginPage fazLogin;
        private FormularioPage mostraFormulario;
        private InserirClienteIncompletoPage preencheDadosPessoa;
        private SalvandoCadastroPage clicaSalvar;

        public CadastrandoClientesIncompletosWorkflow(IWebDriver driver)
        {
            this.fazLogin = new LoginPage(driver);
            this.mostraFormulario = new FormularioPage(driver);
            this.preencheDadosPessoa = new InserirClienteIncompletoPage(driver);
            this.clicaSalvar = new SalvandoCadastroPage(driver);
        }

        public void FazLogin(string name, string password)
        {
            fazLogin.DigitaNome(name);
            fazLogin.DigitaSenha(password);
            fazLogin.ClicaLogin();
        }

        public void CadastroPessoaCpfIncompleto(string nome, string email, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaCpfIncompleto(nome, email, nascimento);
        }

        public void CadastroPessoaCpfInvalido(string cpf, string nome, string email, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaCpfInvalido(cpf, nome, email, nascimento);
        }

        public void CadastroPessoaNomeIncompleto(string cpf, string email, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaNomeIncompleto(cpf, email, nascimento);
        }

        public void CadastroPessoaNascimentoIncompleto(string cpf, string nome, string email)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaNascimentoIncompleto(cpf, nome, email);
        }

        public void CadastroPessoaEmailIncompleto(string cpf, string nome, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaEmailIncompleto(cpf, nome, nascimento);
        }

        public void CadastroPessoaEmailInvalido(string cpf, string nome, string email, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaEmailInvalido(cpf, nome, email, nascimento);
        }
        public void CadastroPessoaGeneroIncompleto(string cpf, string nome, string email, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaGeneroIncompleto(cpf, nome, email, nascimento);
        }
        public void CadastroPessoaEstCivilIncompleto(string cpf, string nome, string email, string nascimento)
        {
            mostraFormulario.AbrindoFormulario();
            preencheDadosPessoa.PreencheIdentPessoaEstCivilIncompleto(cpf, nome, email, nascimento);
        }

        public void SalvaCadastro()
        {
            clicaSalvar.SalvaEnderecos();
        }

        
    }
}
