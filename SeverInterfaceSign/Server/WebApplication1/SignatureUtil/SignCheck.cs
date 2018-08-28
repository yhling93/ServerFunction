using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SignatureUtil
{
    public class SignCheck
    {
        public static bool checkSign(SortedDictionary<string, string> paramDict, string sign, string key)
        {
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<string, string> pair in paramDict)
            {
                sb.Append(pair.Key);
                sb.Append('=');
                sb.Append(pair.Value);
                sb.Append('&');
            }
            
            sb.Remove(sb.Length - 1, 1);

            sb.Insert(0, key);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            sb.Clear();
            for(int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i].ToString("x2"));
            }
;
            return sign == sb.ToString();

        }

        public static string encrypt(SortedDictionary<string, string> paramDict, string key)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in paramDict)
            {
                sb.Append(pair.Key);
                sb.Append('=');
                sb.Append(pair.Value);
                sb.Append('&');
            }

            sb.Remove(sb.Length - 1, 1);

            sb.Insert(0, key);
            var md5 = new MD5CryptoServiceProvider();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            sb.Clear();
            Console.WriteLine(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                sb.Append(s[i].ToString("x2"));
            }
            return sb.ToString();
            }
        }
}
