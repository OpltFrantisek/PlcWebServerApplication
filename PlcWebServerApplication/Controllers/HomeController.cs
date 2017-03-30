using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PLCComunicationLib;
using PLCComunicationLib.snap7;
namespace PlcWebServerApplication.Controllers
{
    public class HomeController : Controller
    {

        static string[] outputs = new string[] { "DB1.DBX0.0",
                                          "DB1.DBX0.1",
                                          "DB1.DBX0.2",
                                          "DB1.DBX0.3",
                                          "DB1.DBX0.4",
                                          "DB1.DBX0.5",
                                          "DB1.DBX0.6",
                                          "DB1.DBX0.7"};


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Plc()
        {
            ViewBag.OutputsCount = outputs.Length;
            var status = Plc_magazine.GetPlc(0, out Simatic plc1);
            if (status)
            {
                return View(Plc_magazine.GetList());
            }
            else
            {
                Plc_magazine.CreatePlc("192.168.1.6", CpuType.S71200);
                return Plc();
            }
            
        }
        public void Write2PLC(int? id, int? pin, bool? value)
        {
            
            if (id != null && pin != null && value != null)
            {
                if (Plc_magazine.Exist((int)id))
                {
                    ThreadColector.DoAction(ActionType.Write, id, pin, value, outputs);                
                }
            }
        }
        public string ReadFromPlc(int? id, int? pin)
        {
            if(id != null&&pin != null&& pin <= 7)
            {
                if (Plc_magazine.Exist((int)id))
                {
                  return  ThreadColector.DoAction(ActionType.Read, id, pin,null,outputs).ToString();               
                }
            }
            return "NULL";
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
