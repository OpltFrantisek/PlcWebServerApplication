using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLCComunicationLib.snap7;
namespace PLCComunicationLib

{
    public static class Plc_magazine
    {
        static List<Simatic> plcList = new List<Simatic>();
        public static bool IsEmpty {
            get {
                if (plcList.Count == 0)
                    return true;
                else
                    return false;
            }
        }
        public static List<Simatic> GetList()
        {
            return plcList;
        }
        public static bool Exist(int id)
        {
            if (id >= plcList.Count)
                return false;
            return plcList[id] != null;
        }
        public static bool GetPlc(int id, out Simatic plc) {
            if (id != plcList.Count && plcList[id] != null)
            {
                plc = plcList[id];
                return true;
            }
            plc = null;
            return false;
        }
        public static void CreatePlc(string ip, CpuType type)
        {
            plcList.Add(new Simatic(ip, type,plcList.Count));
        }
    }
    public static class HelperClass
    {
        public static void Neco()
        {
            int a = 10;
            int b = 20;
            int c = a + b;
        }
    }
    
    
    
}
