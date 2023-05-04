using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Data
{
    public class RSAUtils
    {
        static string pubKeyStr;
        static string privKeyStr;

        static string Encriptar(string str)
        {
            RSAServices rsa = new RSAServices();
            RSAServices rsa2 = new RSAServices();

            pubKeyStr = rsa.GetPublicKey();
            privKeyStr = rsa.GetPrivateKey();

            var pubKey = RSAServices.PublicParametersFromXml(pubKeyStr);

            string resulEncrit = rsa2.Encrypt(str, pubKey);

            return resulEncrit;
        }

        static string Desencriptar(string privkeyStr, string msjEncriptado)
        {
            RSAServices rsa = new RSAServices();

            var privKey = RSAServices.PublicParametersFromXml(privkeyStr);

            string resulDecript = rsa.Decrypt(msjEncriptado, privKey);

            return resulDecript;
        }
    }
}
