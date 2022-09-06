using ProtoBuf;
using ProtoBuf.Meta;
using ProtoBufExample.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ProtoBufExample.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 輸出 proto octet-stream
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProto()
        {
            List<TestContact> dateMatchs = new List<TestContact>()
        {
            new TestContact
            {
                ID = 1,
                Name = "xiao ming",
                Address = "Cheng Du",
                Time = DateTime.Now,
                Datas = new List<TestContact2>{ },
            },
            new TestContact
            {
                ID = 2,
                Name = "xiao ming2",
                Address = "Cheng Du2",
                Time = DateTime.Now.AddHours(1),
                Datas = new List<TestContact2>{ },
            },
            new TestContact
            {
                ID = 3,
                Name = "xiao ming3",
                Address = "Cheng Du3",
                Time = DateTime.Now.AddHours(2),
                Datas = new List<TestContact2>{ },
            }
        };
            TestResponse response = new TestResponse
            {
                DateMatchs = dateMatchs,
            };
            byte[] data = null;
            using (var memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, response);
                data = memoryStream.ToArray();
            }

            return File(data, "application/octet-stream");
        }
        /// <summary>
        /// 測試
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestProto()
        {
            var client = new RestClient("https://localhost:44381/");
            
            var request = new RestRequest("Home/GetProto", Method.Get);
            request.Timeout = -1;
            RestResponse response = client.Execute(request);

            byte[] byteArray = response.RawBytes;

            TestResponse testResponse = null;
            using (MemoryStream fileStream = new MemoryStream(byteArray))
            {
                testResponse = Serializer.Deserialize<TestResponse>(fileStream);
            }

            return Json(testResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 輸出 Proto 文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetProtoFile()
        {
            return Serializer.GetProto<TestResponse>(ProtoSyntax.Proto3);
        }
    }
}