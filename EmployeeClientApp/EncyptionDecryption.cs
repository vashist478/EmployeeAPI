using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeClientApp
{
    public class EncyptionDecryption
    {
        static readonly string keyData = "su@@@###su&BLSKF";
        public static string Encrypt(string inputData)
        {
            try
            {
                return Convert.ToBase64String(EncryptStringToBytes(inputData, Encoding.Default.GetBytes(keyData)));
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="plainText"></param>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static byte[] EncryptStringToBytes(string plainText, byte[] key)
        {
            return EncryptStringToBytes(plainText, key, null);
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="plainText"></param>  
        /// <param name="key"></param>  
        /// <param name="IV"></param>  
        /// <returns></returns>  
        public static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] IV)
        {
            if ((plainText == null) || (plainText.Length <= 0))
            {
                throw (new ArgumentNullException("PlainText"));
            }
            if ((key == null) || (key.Length <= 0))
            {
                throw (new ArgumentNullException("PlainText"));
            }
            RijndaelManaged rijManaged = new RijndaelManaged();
            rijManaged.Key = key;
            if (!(IV == null))
            {
                if (IV.Length > 0)
                {
                    rijManaged.IV = IV;
                }
                else
                {
                    rijManaged.Mode = CipherMode.ECB;
                }
            }
            else
            {
                rijManaged.Mode = CipherMode.ECB;
            }

            byte[] encryptedData = null;
            ICryptoTransform iCryptoTransform = rijManaged.CreateEncryptor();
            encryptedData = iCryptoTransform.TransformFinalBlock(Encoding.Default.GetBytes(plainText), 0, plainText.Length);

            return encryptedData;

        }





        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="input"></param>  
        /// <param name="pass"></param>  
        /// <returns></returns>  
        public static string Decrypt(string inputData)
        {
            try
            {
                return DecryptStringFromBytes(Convert.FromBase64String(inputData), Encoding.Default.GetBytes(keyData));
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="cipherText"></param>  
        /// <param name="key"></param>  
        /// <returns></returns>  
        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key)
        {
            return DecryptStringFromBytes(cipherText, key, null);
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="cipherText"></param>  
        /// <param name="key"></param>  
        /// <param name="IV"></param>  
        /// <returns></returns>  
        public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] IV)
        {
            if ((cipherText == null) || (cipherText.Length <= 0))
            {
                throw (new ArgumentNullException("cipherText"));
            }
            if ((key == null) || (key.Length <= 0))
            {
                throw (new ArgumentNullException("key"));
            }
            RijndaelManaged rijManaged = new RijndaelManaged();
            rijManaged.Key = key;
            rijManaged.Mode = CipherMode.CBC;
            if (!(IV == null))
            {
                if (IV.Length > 0)
                {
                    rijManaged.IV = IV;
                }
                else
                {
                    rijManaged.Mode = CipherMode.ECB;
                }
            }
            else
            {
                rijManaged.Mode = CipherMode.ECB;
            }

            string PlainData = null;
            ICryptoTransform iCryptoTransform = rijManaged.CreateDecryptor(rijManaged.Key, rijManaged.IV);
            MemoryStream msDecrypt = new MemoryStream(cipherText);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, iCryptoTransform, CryptoStreamMode.Read);
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            PlainData = srDecrypt.ReadToEnd();

            return PlainData;

        }

    }
}
