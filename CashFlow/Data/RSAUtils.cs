using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Data
{
    public class RSAUtils
    {
        public static string pubKeyStr;
        public static string privKeyStr;

        public static string Encriptar(string str)
        {
            RSAServices rsa = new RSAServices();
            RSAServices rsa2 = new RSAServices();

            pubKeyStr = rsa.GetPublicKey();
            privKeyStr = rsa.GetPrivateKey();

            var pubKey = RSAServices.PublicParametersFromXml(pubKeyStr);

            string resulEncrit = rsa2.Encrypt(str, pubKey);

            return resulEncrit;
        }

        public static string Desencriptar(string privkeyStr, string msjEncriptado)
        {
            RSAServices rsa = new RSAServices();

            var privKey = RSAServices.PublicParametersFromXml(privkeyStr);

            string resulDecript = rsa.Decrypt(msjEncriptado, privKey);

            return resulDecript;
        }
    }
}
