using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CashFlow.Data
{
    public class RSAServices
    {
        private RSACryptoServiceProvider csp;
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public RSAServices()
        {
            csp = new RSACryptoServiceProvider(512);
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }

        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _publicKey);
            return sw.ToString();
        }

        public string GetPrivateKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, _privateKey);
            return sw.ToString();
        }

        public static RSAParameters PublicParametersFromXml(string data)
        {
            XmlSerializer xml = new XmlSerializer(typeof(RSAParameters));
            object result;
            using (TextReader reader = new StringReader(data))
            {
                result = xml.Deserialize(reader);
            }
            return (RSAParameters)result;
        }

        public string Encrypt(string plainText, RSAParameters publicKey)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(publicKey);
            var data = Encoding.Unicode.GetBytes(plainText);
            var cypher = csp.Encrypt(data, false);
            return Convert.ToBase64String(cypher);
        }

        public string Decrypt(string cypherText, RSAParameters privateKey)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(privateKey);
            var plainText = csp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainText); ;
        }
    }
}
