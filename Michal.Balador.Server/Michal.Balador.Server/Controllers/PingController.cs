
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web;

namespace Michal.Balador.Server.Controllers
{
    [RoutePrefix("api/Ping")]
    public class PingController: ApiController
    {
        [Route("")]
        //[AcceptVerbs("GET")]
        public IHttpActionResult Get(string s)
        {
            var defaultLogFolder = HttpContext.Current.Server.MapPath("~/CollectLogs");
            var pat = Path.Combine(defaultLogFolder, ("ping_Get" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".txt"));
            File.WriteAllText(pat, s);
          
            return Ok("test="+s);
        }
        [Route("")]
       // [AcceptVerbs("GET")]
        public IHttpActionResult Get()
        {
            var defaultLogFolder = HttpContext.Current.Server.MapPath("~/CollectLogs");
            var pat = Path.Combine(defaultLogFolder, ("ping_Get_Empty" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".txt"));
            File.WriteAllText(pat, "ok");
            return Ok("test");
        }
        [Route("")]
       [AcceptVerbs("Post")]
        public IHttpActionResult Post(string s)
        {
            var defaultLogFolder = HttpContext.Current.Server.MapPath("~/CollectLogs");
            var pat = Path.Combine(defaultLogFolder, ("pingPost" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + ".txt"));
            File.WriteAllText(pat, "ok");
            return Ok("test=" + s);
        }

        [Route("complex")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Complex()
        {
            return Ok(new { key = "1", value = "ss" });
        }
        [Route("complexv")]
        [AcceptVerbs("GET")]
        public IHttpActionResult ComplexValue(string v)
        {
            return Ok(new { key = "1", value = v });
        }

        [Route("f")]
        [AcceptVerbs("GET")]
        public IHttpActionResult FindPerson(string t)
        {
            System.Threading.Thread.Sleep(500);
            if(t.Trim()== "+972 (66)-6666666")
                return  Ok(new { tel = t, name = "ליאור גרוסמן !!!",cityCode= "6100", cityName="בני ברק",streetCode="",streetName= "הרב מצליח", num="10",extra= "קומה א" });
            return Ok( new { tel = t, name = "no found!!!", cityCode = "", cityName = "", streetCode = "", streetName = "", num = "", extra = ""  });
        }

        

    }
}