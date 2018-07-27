using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioCristiano
{
    class GeradorPage
    {
        //public static string GerandoCpf()
        public static string GerandoCpf()
        {
            string iniciais = "";// contém os 9 primeiros números do cpf
            string num = ""; // receberá o valor contido em iniciais
            int numero;// número gerado randomicamente



            int primDig, segDig;// recebem o primeiro e o segundo digitos calculados
            int soma = 0, peso = 10; // utilizados nos calculos dos digitos verificadores

            Random random = new Random();

            for (int i = 0; i < 9; i++)
            {

                //Thread.Sleep(50);

                //gera numero aleatorio entre 0 e 10
                numero = random.Next(0, 9);

                iniciais += Convert.ToString(numero);
            }

            // armazena em "num" o valor de iniciais
            num = iniciais;

            //algoritmo para cpf
            for (int i = 0; i < iniciais.Length; i++)
            {
                soma += Convert.ToInt32(num.Substring(i, 1)) * peso--;
            }

            if (soma % 11 == 0 | soma %11 == 1)
            {
                primDig = 0;
            }
            else
            {
                primDig = 11 - (soma % 11);
            }

            soma = 0;
            peso = 11;

            for (int i = 0; i < num.Length; i++)
            {
                soma += Convert.ToInt32(num.Substring(i, 1)) * peso--;
            }

            soma += primDig * 2;

            if (soma % 11 == 0 | soma % 11 == 1)
            {
                segDig = 0;
            }
            else
            {
                segDig = 11 - (soma % 11);
            }

            string cpfGerado = iniciais + primDig + segDig;

            Thread.Sleep(50);

            //retorna o cpf gerado
            return cpfGerado;

        }

        public static string GerandoCnpj()
        //public static string GerandoCnpj()
        {
            string iniciais = "";// contém os 8 primeiros números do cnpj
            int numero;// número gerado randomicamente
            int primDig, segDig;// recebem o primeiro e o segundo digitos calculados
            String num; // receberá o valor contido em iniciais
            int intRestoDivisao; // recebe o resto de uma divisão

            Random random = new Random();


            for (int i = 0; i < 8; i++)
            {
               // Thread.Sleep(50);

                //gera numero aleatorio entre 0 e 10
                numero = random.Next(0, 9);

                iniciais += Convert.ToString(numero);
            }
                iniciais += "0001";
                // armazena em num o valor de iniciais
                num = iniciais;
                
                // 5 4 3 2 9 8 7 6 5 4 3 2
                // calculando o primeiro dígito:
                int soma = 0;
                soma += 5 * (Convert.ToInt32(num.Substring(0, 1)));
                soma += 4 * (Convert.ToInt32(num.Substring(1, 1)));
                soma += 3 * (Convert.ToInt32(num.Substring(2, 1)));
                soma += 2 * (Convert.ToInt32(num.Substring(3, 1)));
                soma += 9 * (Convert.ToInt32(num.Substring(4, 1)));
                soma += 8 * (Convert.ToInt32(num.Substring(5, 1)));
                soma += 7 * (Convert.ToInt32(num.Substring(6, 1)));
                soma += 6 * (Convert.ToInt32(num.Substring(7, 1)));
                soma += 5 * (Convert.ToInt32(num.Substring(8, 1)));
                soma += 4 * (Convert.ToInt32(num.Substring(9, 1)));
                soma += 3 * (Convert.ToInt32(num.Substring(10, 1)));
                soma += 2 * (Convert.ToInt32(num.Substring(11, 1)));

                intRestoDivisao = soma % 11;
                // Caso o resto da divisão seja menor que 2,
                // o nosso primeiro dígito verificador se torna 0 (zero),
                // caso contrário subtrai-se o valor obtido de 11
                if (intRestoDivisao < 2)
                {
                    primDig = 0;
                }
                else
                {
                    primDig = 11 - intRestoDivisao;
                }

                // 6 5 4 3 2 9 8 7 6 5 4 3 2
                // calculando o segundo dígito:
                soma = 0;

                soma += 6 * (Convert.ToInt32(num.Substring(0, 1)));
                soma += 5 * (Convert.ToInt32(num.Substring(1, 1)));
                soma += 4 * (Convert.ToInt32(num.Substring(2, 1)));
                soma += 3 * (Convert.ToInt32(num.Substring(3, 1)));
                soma += 2 * (Convert.ToInt32(num.Substring(4, 1)));
                soma += 9 * (Convert.ToInt32(num.Substring(5, 1)));
                soma += 8 * (Convert.ToInt32(num.Substring(6, 1)));
                soma += 7 * (Convert.ToInt32(num.Substring(7, 1)));
                soma += 6 * (Convert.ToInt32(num.Substring(8, 1)));
                soma += 5 * (Convert.ToInt32(num.Substring(9, 1)));
                soma += 4 * (Convert.ToInt32(num.Substring(10, 1)));
                soma += 3 * (Convert.ToInt32(num.Substring(11, 1)));
                soma += 2 * primDig;

                intRestoDivisao = soma % 11;
                // Caso o resto da divisão seja menor que 2,
                // o nosso primeiro dígito verificador se torna 0 (zero),
                // caso contrário subtrai-se o valor obtido de 11
                if (intRestoDivisao < 2)
                {
                    segDig = 0;
                }
                else
                {
                    segDig = 11 - intRestoDivisao;
                }

                string cnpjGerado = iniciais + primDig + segDig;

               // Thread.Sleep(50);

                return cnpjGerado;
        }
    }
}
