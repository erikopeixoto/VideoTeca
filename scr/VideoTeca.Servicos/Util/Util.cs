using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace VideoTeca.Servicos.Util
{
    public static class Util
    {
        private const string ZERO = "0";
        public static string FormatarFone(string numTelefone)
        {
            string retorno = "";
            if (numTelefone?.Trim().Length > 0)
            {
                retorno = numTelefone.ToString();
                if (numTelefone.ToString().Length == 10)
                {
                    retorno = string.Format("({0}){1}-{2}", retorno.Substring(0, 2), retorno.Substring(2, 4), retorno.Substring(6, 4));

                }
                else if (numTelefone.ToString().Length == 11)
                {
                    retorno = string.Format("({0}){1}-{2}", retorno.Substring(0, 2), retorno.Substring(2, 5), retorno.Substring(7, 4));
                }
            }
            return retorno;
        }
        public static string FormatarDocumento(string numDocumento, byte tipoPessoa = 1)
        {
            string retorno = "";
            if (numDocumento?.Trim().Length > 0)
            {
                retorno = numDocumento.ToString();
                if (tipoPessoa == 1)
                {
                    retorno = numDocumento.ToString().PadLeft(11, '0');
                    retorno = string.Format("{0}.{1}.{2}-{3}", retorno.Substring(0, 3), retorno.Substring(3, 3), retorno.Substring(6, 3), retorno.Substring(9, 2));
                }
                else
                {
                    retorno = numDocumento.ToString().PadLeft(14, '0');
                    retorno = string.Format("{0}.{1}.{2}/{3}-{4}", retorno.Substring(0, 2), retorno.Substring(2, 3), retorno.Substring(5, 3), retorno.Substring(8, 4),
                        retorno.Substring(12, 2));
                }
            }
            return retorno;
        }
        public static string RetirarCaracteresEspeciais(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";
            var novastring = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return novastring.Replace(str, string.Empty);
        }
        public static string ObterAppSettings(this string _key, bool validarNull = true, object valorDefaultIfNull = null)
        {
            string valorRetorno = ConfigurationManager.AppSettings[_key];

            if (validarNull && string.IsNullOrWhiteSpace(valorRetorno))
            {
                throw new ArgumentException($"Parametro '{_key}' obrigatorio no arquivo Web.config não encontrado:");
            }

            if (string.IsNullOrWhiteSpace(valorRetorno) && valorDefaultIfNull != null)
            {
                valorRetorno = valorDefaultIfNull.ToString();
            }

            return valorRetorno;
        }

        public static bool ValidarCpfCnpj(byte tipoPessoa, string cpfCnpj)
        {
            bool retorno = false;
            if (cpfCnpj.IsNumeric())
            {
                if (tipoPessoa == 1)
                {
                    retorno = ValidarCpf(cpfCnpj);
                }
                else
                {
                    retorno = ValidarCnpj(cpfCnpj);
                }
            }
            return retorno;
        }
        private static bool ValidarCpf(string cpfFormat)
        {
            try
            {
                cpfFormat = cpfFormat.PadLeft(11, '0');
                return cpfFormat.Equals(CalcularDigitoCpf(cpfFormat));
            }
            catch
            {
                return false;
            }
        }
        private static string CalcularDigitoCpf(string numCpf)
        {
            string cpfBase = numCpf.Substring(0, 9);

            string digito1 = CalcularPrimeiroDigitoCpf(cpfBase);
            string digito2 = CalcularSegundoDigitoCpf(cpfBase, digito1.ToString());

            return cpfBase + digito1 + digito2;
        }
        private static string CalcularPrimeiroDigitoCpf(string cpfBase)
        {
            int soma = 0, resto = 0;

            for (int i = 8, m = 2; i >= 0; i--, m++)
            {
                soma += m * Convert.ToInt32(cpfBase.Substring(i, 1));
            }
            resto = soma % 11;
            if (resto < 2)
            {
                return ZERO;
            }
            else
            {
                return (11 - resto).ToString();
            }
        }
        private static string CalcularSegundoDigitoCpf(string cpfBase, string primeiroDigito)
        {
            cpfBase = cpfBase + primeiroDigito;
            int soma = 0, resto = 0;

            for (int i = 9, m = 2; i >= 0; i--, m++)
            {
                soma += m * Convert.ToInt32(cpfBase.Substring(i, 1));
            }
            resto = soma % 11;
            if (resto < 2)
            {
                return ZERO;
            }
            else
            {
                return (11 - resto).ToString();
            }
        }
        public static bool ValidarEmail(string nomEmail)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return System.Text.RegularExpressions.Regex.IsMatch(nomEmail, strModelo);
        }
        public static bool ValidarCnpj(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            string tempCnpj = cnpj.Substring(0, 12);
            tempCnpj += CalcularPrimeiroDigitoCnpj(tempCnpj);
            tempCnpj += CalcularSegundoDigitoCnpj(tempCnpj);

            return cnpj.Equals(tempCnpj);
        }
        private static string CalcularPrimeiroDigitoCnpj(string tempCnpj)
        {
            int soma = 0;
            int resto;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }
            resto = (soma % 11);
            if (resto > 1)
            {
                return (11 - resto).ToString();
            }
            return ZERO;
        }
        private static string CalcularSegundoDigitoCnpj(string cnpjPrimeiroDigito)
        {
            int soma = 0;
            int resto;
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpjPrimeiroDigito[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto > 1)
            {
                return (11 - resto).ToString();
            }
            return ZERO;
        }
        public static bool IsNumeric(this string valor)
        {
            return valor.All(char.IsNumber);
        }
    }
}
