using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace albom_proto_2
{
    class SHIFR
    {
        const string R = "niktwer@ukr.net";
        const string R_vec = "b7doSuDitOz1hZe#";
        const string R_alg = "SHA1";

        //метод шифрования строки
        public static string Shifrovka(string ishText, string pass,
               string sol = R, string cryptographicAlgorithm = R_alg,
               int passIter = 2, string initVec = R_vec,
               int keySize = 256)
        {
            if (string.IsNullOrEmpty(ishText))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
            byte[] solB = Encoding.ASCII.GetBytes(sol);
            byte[] ishTextB = Encoding.UTF8.GetBytes(ishText);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
            byte[] keyBytes = derivPass.GetBytes(keySize / 8);
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

        //replace symbovs
        //private static string verife_kod(string old_kod)
        //{
        //    string new_kod =  "";
        //    char[,] symbols = new char[8, 2] { { '+', '&' }, { '*', ':' }, { '?', ',' }, { '|', '-' }, { '$', '%' }, { '(', '<' }, { ')', '>' }, { '\\', '/' } };

        //    //for (int i = 0; i < old_kod.Length; i++)
        //    //{
        //        foreach (char h in old_kod)
        //        {
        //            switch (h)
        //            {
        //                case '+':
        //                    new_kod = new_kod + '&';
        //                    break;
        //                case '*':
        //                    new_kod = new_kod + ':';
        //                    break;
        //                case '?':
        //                    new_kod = new_kod + ',';
        //                    break;
        //                case '|':
        //                    new_kod = new_kod + '-';
        //                    break;
        //                case '$':
        //                    new_kod = new_kod + '%';
        //                    break;
        //                case '(':
        //                    new_kod = new_kod + '<';
        //                    break;
        //                case ')':
        //                    new_kod = new_kod + '>';
        //                    break;
        //                case '\\':
        //                    new_kod = new_kod + '/';
        //                    break;
        //                default:
        //                    new_kod = new_kod + h;
        //                    break;
        //            }
        //        }
        //    //}
        //    return new_kod;
        //}

        //метод дешифрования строки
        public static string DeShifrovka(string ciphText, string pass,
               string sol = R, string cryptographicAlgorithm = R_alg,
               int passIter = 2, string initVec = R_vec,
               int keySize = 256)
            
        {
            if (string.IsNullOrEmpty(ciphText))
                return "";
            try
            {
                byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
                byte[] solB = Encoding.ASCII.GetBytes(sol);

                //Regex regex = new Regex(@"\w*");
                //Match match = regex.Match(ciphText);
                //MatchCollection matches = regex.Matches(ciphText);
                //string kk="";
                //while (match.Success)
                //{
                //    // Т.к. мы выделили в шаблоне одну группу (одни круглые скобки),
                //    // ссылаемся на найденное значение через свойство Groups класса Match
                //    kk=kk+match.Groups[1].Value;

                //    // Переходим к следующему совпадению
                //    match = match.NextMatch();
                //}

                //string[] rt =ciphText.Regex.("  ");

                byte[] cipherTextBytes = Convert.FromBase64String(ciphText);
            
                PasswordDeriveBytes derivPass = new PasswordDeriveBytes(pass, solB, cryptographicAlgorithm, passIter);
                byte[] keyBytes = derivPass.GetBytes(keySize / 8);

                RijndaelManaged symmK = new RijndaelManaged();
                symmK.Mode = CipherMode.CBC;

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int byteCount = 0;

                using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
                {
                    using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                        {
                            byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            mSt.Close();
                            cryptoStream.Close();
                        }
                    }
                }

            symmK.Clear();
                //проверка
                string hh = Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
            }

            catch(System.FormatException e)
            {
                return""; 
            }

        }
   
    }
}
