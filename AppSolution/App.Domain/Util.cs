using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Domain
{
    public static class Util
    {
        

        public static string CryptoGrafiaSenha(string senha)
        {
            // Calcular o Hash
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);

            // Converter byte array para string hexafloat
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

            //// generate a 128-bit salt using a secure PRNG
            //byte[] salt = new byte[128 / 8];

            //string senhaCrypto = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //password: senha,
            //salt: salt,
            //prf: KeyDerivationPrf.HMACSHA256,
            //iterationCount: 10000,
            //numBytesRequested: 256 / 8));

            //return senhaCrypto;
        }

        public static bool ValidarCartao(string numeroCartao, string bandeira)
        {
            string nrCartao = numeroCartao.Replace("-", "");
            switch (bandeira.ToUpper())
            {
                case "VISA":
                    if (Regex.IsMatch(nrCartao, "^(4)"))
                        return nrCartao.Length == 13 || nrCartao.Length == 16;
                    break;
                case "MASTERCARD":
                    if (Regex.IsMatch(nrCartao, "^(51|52|53|54|55)"))
                        return nrCartao.Length == 16;
                    break;
                case "AMEX":
                    if (Regex.IsMatch(nrCartao, "^(34|37)"))
                        return nrCartao.Length == 15;
                    break;
                case "DINERS":
                    if (Regex.IsMatch(nrCartao, "^(300|301|302|303|304|305|36|38)"))
                        return nrCartao.Length == 14;
                    break;
            }

            return false;
        }


        public static bool checkLuhn(string value)
        {
            // remove all non digit characters
            //var value = value.replace(/\D / g, '');
            var apenasDigitos = new Regex(@"[^\d]");
            value = apenasDigitos.Replace(value, "");

            var sum = 0;
            var shouldDouble = false;
            // loop through values starting at the rightmost side
            for (var i = 0; i < value.Length; i++)
            {
                var digit = Convert.ToInt32(value.Substring(i, 1));

                if (shouldDouble)
                {
                    if ((digit *= 2) > 9) digit -= 9;
                }

                sum += digit;
                shouldDouble = !shouldDouble;
            }
            return (sum % 10) == 0;
        }


        public static void ValidarSqlInject(string texto)
        {
            string[] lixo = new string[12];

            lixo.Append("eval()");
            lixo.Append("setTimeout()");
            lixo.Append("setInterval()");
            lixo.Append("new Function()");
            lixo.Append("if");
            lixo.Append("'return'");
            lixo.Append("+ object");
            lixo.Append("console.log");
            lixo.Append("require(");
            lixo.Append("select");
            lixo.Append("insert");
            lixo.Append("update");
            lixo.Append("delete");
            lixo.Append("drop");
            lixo.Append("truncate");
            lixo.Append("--");
            lixo.Append("'");
            lixo.Append("1=1");
            lixo.Append("xp_");
            lixo.Append("'  having 1=1--");
            lixo.Append("' OR \" = '");


            if (lixo.Contains(texto.ToLower()))
                throw new Exception("Texto contem palavras que não não são permitido!!!");

        }

        public static bool ValidarSenha(string Senha1, string Senha2)
        {
            if (Senha1 == Senha2)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidarCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        public static bool ValidarCNPJ(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt;
            bool[] CNPJOk;

            ftmt = "6543298765432";
            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(
                        CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidEmail(this string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }


        public static double dias() //Pede uma data como parâmetro
        {
            DateTime dataInicial = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime dataFinal = new DateTime(DateTime.Now.AddYears(1).Year, 1, 1);
            var dias = (dataFinal - dataInicial).TotalDays; //Retorna os dias. É preciso fazer o cast porque vem em double

            return dias / 12;
        }

        public static string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }


        public static void SepararCaminhoArquivo(string path, string raiz, string nomeDiretorio, string nomeArquivo, string extensaoArquivo, string nomeArquivoSemExtensao)
        {
            raiz = Path.GetPathRoot(path);
            nomeDiretorio = Path.GetDirectoryName(path);
            nomeArquivo = Path.GetFileName(path);
            extensaoArquivo = Path.GetExtension(path);
            nomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(path);
        }

        public static IList<TipoItemList> ListTipoLayout()
        {
            IList<TipoItemList> list = new List<TipoItemList>();
            list.Add(new TipoItemList { Codigo = Convert.ToInt32(EnumTipo.TipoLayout.TelaCheia), Nome = "Tela Cheia" });


            return list;
        }

        public static bool ValidarExtensaoArquivo(string sFileName)
        {
            bool bValido = false;
            //Verifica se é imagem
            string fileExtension = System.IO.Path.GetExtension(sFileName).ToLower();
            foreach (string ext in new string[] { ".gif", ".jpeg", ".jpg", ".png" })
            {
                if (fileExtension == ext)
                    bValido = true;
            }
            return bValido;

        }

        public static string ConverterNumeroCartao(string numero)
        {
            StringBuilder sbcartao = new StringBuilder();

            for (int i = 0; i < numero.Length; i++)
            {
                if (i < 12)
                    sbcartao.Append("*");
                else
                    sbcartao.Append(numero.Substring(i - 1, 1));

            }
            return sbcartao.ToString();
        }


        static readonly string PasswordHash = "VOjA@Orh@xB$Ft7dD$KlCL04Jb#f^0frTWLoOF^U"; //"P@@Sw0rd";
        static readonly string SaltKey = "P&$KgGDR72!luc&x6!dbeWqdhKt9AcP*@HajnGo8";//"S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";


        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }


        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }


    }
}
