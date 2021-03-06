﻿using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
 
namespace Example
{
    public class Account
    {
        [JsonProperty(PropertyName = "NomePessoaFisica")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Nascimento")]
        public int Nascimento { get; set; }

        [JsonProperty(PropertyName = "Cep")]
        public int Cep { get; set; }

        [JsonProperty(PropertyName = "Endereco")]
        public string Endereco { get; set; }

        [JsonProperty(PropertyName = "Numero")]
        public int Numero { get; set; }

        [JsonProperty(PropertyName = "Cidade")]
        public string Cidade { get; set; }

        [JsonProperty(PropertyName = "Telefone")]
        public string Telefone { get; set; }

        [JsonProperty(PropertyName = "Celular")]
        public string Celular { get; set; }

        public Account()
        {
            Nome = "";
            Email = "";
            Nascimento = 0;
            Cep = 0;
            Endereco = "";
            Numero = 0;
            Cidade = "";
            Telefone = "";
            Celular = "";
        }
    }
}