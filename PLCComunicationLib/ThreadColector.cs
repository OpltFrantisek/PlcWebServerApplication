using System;
using System.Collections.Generic;
using System.Text;

namespace PLCComunicationLib
{

    public enum ActionType
    {
        Read =0,
        Write = 1
    }
    /// <summary>
    /// Tato třída slouží k serializaci vláken, protože dotazy na čtení nebo zápis z plc chodí pokaždé v jiném vlákně tak může docházet ke kolizím (například pokud čteme a zároveň zapisujeme)
    /// a proto musíme vlákna serializovat aby dotazy prováděli postupně 
    /// </summary>
    public static class ThreadColector
    {
        static object locker = new object();

        /// <summary>
        /// Ta metoda slouží ke čtení nebo zápis do plc. "lock" nám zajišťuje že pokud voláme tuto metodu z různých vláken tak čtení i zápis provádí vždy jen jedno vlákno a ostatní čekají
        /// </summary>
        /// <param name="action"> Tato proměná nám říká jestli chceme zapisovat nebo číst</param>
        /// <param name="id">identifikační číslo PLC</param>
        /// <param name="pin">číslo bitu</param>
        /// <param name="value">hodnotak kterou chceme zapisovat, pokud čteme můžebýt být parametr libovolný</param>
        /// <param name="outputs">seznam názvů proměných</param>
        /// <returns> metoda vrací object bool pokud provádíme čtení nebo null pokud zapisujeme</returns>
        public static object DoAction(ActionType action,int? id, int? pin, bool? value,string[] outputs)
        {
            lock(locker){
                if (action == ActionType.Read)
                {
                    Plc_magazine.GetPlc((int)id, out Simatic plc);
                    return plc.Read_bite(outputs[(int)pin]);
                }
                else
                {
                    Plc_magazine.GetPlc((int)id, out Simatic plc);
                    plc.Write_bite(outputs[(int)pin], (bool)value);
                }               
            }
            return null;
        }
    }
}
