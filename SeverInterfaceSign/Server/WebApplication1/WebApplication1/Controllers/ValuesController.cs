using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public class RecObj
        {
            public int num { get; set; }
            public string name { get; set; }
            public string school { get; set; }
            public string sign { get; set; }
            public string date { get; set; }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("testMD5")]
        public string testMD5([FromBody] RecObj obj)
        {
            Console.WriteLine(obj.num);
            Console.WriteLine(obj.name);
            Console.WriteLine(obj.school);
            Console.WriteLine(obj.sign);
            Console.Write(obj.date);

            //Thread.Sleep(20 * 1000);
            DateTime dt = Convert.ToDateTime(obj.date);
            DateTime now = DateTime.Now;
            Console.WriteLine("now time: {0}", now.ToString("yyyy-MM-dd hh:mm:ss"));
            TimeSpan ts = dt - now;
            
            if(Math.Abs(ts.Seconds) > 20)
            {
                return "out of time";
            }



            SortedDictionary<string, string> paramDict = new SortedDictionary<string, string>();

            // use reflection
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach(PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(obj, null);
                paramDict.Add(name, value.ToString());
            }

            paramDict.Remove("sign");

            bool checkRes = SignatureUtil.SignCheck.checkSign(paramDict, obj.sign, "testauth");
            
            return checkRes ? "check success": "check fail";

        }
    }
}
