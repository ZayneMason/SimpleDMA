using KMBox.NET;
using KMBox.NET.Structures;
using SimpleDMA.SimpleDMA;
using System.Net;
using Vmmsharp;

namespace SimpleDMA {
    public class Program
    {
        static async Task Main(string[] args)
        {
            /*=======================================================================================================
               PLEASE PLACE THE DLLs PROVIDED BY YOUR VENDOR INTO THE "libs" FOLDER AND REFERENCE THEM IN YOUR PROJECT

               EDIT KMBOX SETTINGS JSON TO SETUP YOUR KMBOX CONNECTION
             ========================================================================================================*/


            // KMBox
            SimpleInput simpleInput = new SimpleInput();

            // DMA Read
            SimpleMem simpleMem = new SimpleMem();
        }
    }
}
