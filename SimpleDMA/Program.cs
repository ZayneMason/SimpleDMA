using KMBox.NET;
using SimpleDMA.SimpleDMA;
using System.Net;
using Vmmsharp;

namespace SimpleDMABase {
    public class Program
    {
        static void Main(string[] args)
        {
            /*=======================================================================================================
               PLEASE PLACE THE DLLs PROVIDED BY YOUR VENDOR INTO THE "libs" FOLDER AND REFERENCE THEM IN YOUR PROJECT
             ========================================================================================================*/


            // INPUTS
            KmBoxClient kmBoxClient = new KmBoxClient(IPAddress.Parse("YOUR KMBOX IP"), /* YOUR KMBOX PORT */ 0, "YOUR KMBOX UUID");
            SimpleInput simpleInput = new SimpleInput(kmBoxClient);

            // MEMORY
            SimpleMem simpleMem = new SimpleMem("YOUR VMM DEVICE");
            VmmProcess process = simpleMem.ReadProcess("YOUR PROCESS");
            ulong moduleBase = simpleMem.GetModuleBase(process, "YOUR MODULE");
        }
    }
}
