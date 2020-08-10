using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIO.Business.Models.Validations.Documentos
{
    public class CpfValidacao
    {
        public const int ValorMaxCpf = 11;

        private static long CpfLimpo(string cpf)
        {
            cpf = Utils.GetNumeros(cpf);

            if (string.IsNullOrEmpty(cpf))
                return 0;

            while (cpf.StartsWith("0"))
                cpf = cpf.Substring(1);

            return long.Parse(cpf);
        }

        private static string GetCpfCompleto(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return "";

            cpf = CpfLimpo(cpf).ToString();

            while (cpf.Length < ValorMaxCpf)
                cpf = "0" + cpf;

            return cpf;
        }

        public static bool Validar(string cpf)
        {
            cpf = GetCpfCompleto(cpf);

            if (!TamanhoValido(cpf)) return false;

            if (cpf == "11111111111") return false;

            if (cpf == "22222222222") return false;

            if (cpf == "33333333333") return false;

            if (cpf == "44444444444") return false;

            if (cpf == "55555555555") return false;

            if (cpf == "66666666666") return false;

            if (cpf == "77777777777") return false;

            if (cpf == "88888888888") return false;

            if (cpf == "99999999999") return false;

            if (cpf == "00000000000") return false;

            if (cpf.Length > ValorMaxCpf)
                return false;

            while (cpf.Length != ValorMaxCpf)
                cpf = '0' + cpf;

            var igual = true;
            for (var i = 1; i < ValorMaxCpf && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            var numeros = new int[ValorMaxCpf];

            for (var i = 0; i < ValorMaxCpf; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            var resultado = soma % ValorMaxCpf;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != ValorMaxCpf - resultado)
                return false;

            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % ValorMaxCpf;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else if (numeros[10] != ValorMaxCpf - resultado)
                return false;

            return true;
        }

        private static bool TamanhoValido(string cpf)
        {
            return cpf.Length == ValorMaxCpf;
        }
    }

    public class CnpjValidacao
    {
        public const int ValorMaxCnpj = 14;

        private static long CnpjLimpo(string cnpj)
        {
            cnpj = Utils.GetNumeros(cnpj);

            if (string.IsNullOrEmpty(cnpj))
                return 0;

            while (cnpj.StartsWith("0"))
                cnpj = cnpj.Substring(1);

            return long.Parse(cnpj);
        }

        private static string GetCnpjCompleto(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return "";

            cnpj = CnpjLimpo(cnpj).ToString();

            while (cnpj.Length < ValorMaxCnpj)
                cnpj = "0" + cnpj;

            return cnpj;
        }

        public static bool Validar(string cnpj)
        {
            cnpj = GetCnpjCompleto(cnpj);

            if (!TamanhoValido(cnpj)) return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            cnpj = cnpj.Trim();

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        private static bool TamanhoValido(string cnpj)
        {
            return cnpj.Length == ValorMaxCnpj;
        }
    }


    public class Utils
    {
        public static string GetNumeros(string texto)
        {
            return string.IsNullOrEmpty(texto) ? "" : new String(texto.Where(Char.IsDigit).ToArray());
        }
    }
}
