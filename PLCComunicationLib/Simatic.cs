using System.Collections.Generic;
using System.Linq;
using PLCComunicationLib.snap7;
namespace PLCComunicationLib
{
    public class Simatic
    {
        Plc plc;
        public bool IsOpen { get;  private set; }
        public string IP;
        public int ID;
        public Simatic(string ip,CpuType type,int id)
        {
            IsOpen = false;
            IP = ip;
            plc = new Plc(type, ip, 0, 0);
            ID = id;
        }
        public void Open_connection()
        {
            plc.Open();
            IsOpen = true;
        }
        public void Close_connection()
        {
           // plc.Dispose();
            plc.Close();
            IsOpen = false;
        }
        public void Write_bite(string variable,bool value)
        {
            Open_connection();
            plc.Write(variable, value);
            Close_connection();
        }
        public void Write_bite(string[] variables,bool[] value)
        {
            if ((variables.Length != value.Length))
                return;
            for (int i = 0; i < variables.Length; i++)
                Write_bite(variables[i], value[i]);
        }
        public void Write_int(string variable, int value)
        {
            Open_connection();
            plc.Write(variable, value);
            Close_connection();
        }
        public void Write_int(string[] variables, int[] value)
        {
            if ((variables.Length != value.Length))
                return;
            for (int i = 0; i < variables.Length; i++)
                Write_int(variables[i], value[i]);
        }
        public void Write_double(string variable, double value)
        {
            Open_connection();
            plc.Write(variable, value);
            Close_connection();
        }
        public void Write_double(string[] variables, double[] value)
        {
            if ((variables.Length != value.Length))
                return;
            for (int i = 0; i < variables.Length; i++)
                Write_double(variables[i], value[i]);
        }
        public bool Read_bite(string variable)
        {
            while (IsOpen) { }

            Open_connection();
            bool result = false;
            
                object res;
                res = plc.Read(variable);
                result = (bool)res;
            
                                 
            Close_connection();
            return result;
        }
        public Dictionary<string,bool> Read_bite(string[] variable)
        {                    
            return variable.Select(x => Read_bite(x)).ToArray().Zip(variable,(val,v) => new { v, val}).ToDictionary(item => item.v.ToString(),item => (bool)item.val);                     
        }
        public int Read_int(string variable)
        {
            Open_connection();
            var result = (int)plc.Read(variable);
            Close_connection();
            return result;
        }
        public Dictionary<string, int> Read_int(string[] variable)
        {
            return variable.Select(x => Read_int(x)).ToArray().Zip(variable, (val, v) => new { v, val }).ToDictionary(item => item.v.ToString(), item => (int)item.val);
        }
        public double Read_Double(string variable)
        {
            Open_connection();
            var result = (double)plc.Read(variable);
            Close_connection();
            return result;
        }
        public Dictionary<string,double> Read_Double(string[] variable)
        {
            return variable.Select(x => Read_Double(x)).ToArray().Zip(variable, (val, v) => new { v, val }).ToDictionary(item => item.v.ToString(), item => (double)item.val);
        }
    }
}