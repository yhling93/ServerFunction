using System;
using System.Collections.Generic;

namespace SignatureUtil
{
    class Program
    {
        static void Main(string[] args)
        {


            string key = "testauth";

            SortedDictionary<string, string> paramDict = new SortedDictionary<string, string>
            {
                { "name", "testname" },
                { "school", "testschool" },
                { "num", "1" }
            };

            string signstr = SignCheck.encrypt(paramDict, key);

            Console.WriteLine("signstr:{0}, len:{1}", signstr, signstr.Length);

            bool checkRes = SignCheck.checkSign(paramDict, signstr, key);

            Console.WriteLine("check result: {0}", checkRes);


            
        }
    }
}
