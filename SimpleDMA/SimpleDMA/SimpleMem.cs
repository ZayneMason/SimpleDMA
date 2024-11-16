using KMBox.NET;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Versioning;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vmmsharp;

namespace SimpleDMA.SimpleDMA
{
    internal class DMAInfo {
        public string DeviceOrFile { get; set; }
    }

    [SupportedOSPlatform("windows")]
    internal class SimpleMem
    {
        private Vmm Vmm { get; set; }

        public SimpleMem()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\DMASettings.json";

            try
            {
                string jsonString = File.ReadAllText(filePath);

                DMAInfo dmaInfo = JsonSerializer.Deserialize<DMAInfo>(jsonString);

                Vmm.LoadNativeLibrary("C:\\MemProcFS");
                string device = dmaInfo.DeviceOrFile;
                string[] init_args = { "-printf", "-v", "-vm", "-waitinitialize", "-device", device };

                this.Vmm = new Vmm(init_args);
                Console.WriteLine(this.Vmm);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file {filePath} was not found.");
            }            
        }

        public VmmProcess[] Processes()
        {
            return Vmm.Processes;
        }

        public VmmProcess GetProcess(string process)
        {
            return Vmm.Process(process);
        }

        public void LoadLibrary(string library)
        {
            Vmm.LoadNativeLibrary(library);
        }

        public ulong GetModuleBase(VmmProcess vmmProcess, string module)
        {
            return vmmProcess.GetModuleBase(module);
        }

        public byte[] Read(VmmProcess vmmProcess, ulong address, uint offset, uint flags = 0u)
        {
            return vmmProcess.MemRead(address, offset, flags);
        }

        public bool Read(VmmProcess vmmProcess, ulong address, nint pb, uint offset, out uint cbRead, uint flags = 0u)
        {
            return vmmProcess.MemRead(address, pb, offset, out cbRead, flags);
        }

        public int ReadInt(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToInt32(Read(vmmProcess, address, offset), 0);
        }

        public float ReadFloat(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToSingle(Read(vmmProcess, address, offset), 0);
        }

        public double ReadDouble(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToDouble(Read(vmmProcess, address, offset), 0);
        }

        public string ReadString(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return Encoding.UTF8.GetString(Read(vmmProcess, address, offset));
        }

        public ulong ReadLong(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToUInt64(Read(vmmProcess, address, offset), 0);
        }

        public SimpleScatter InitializeScatter(uint flags = 0u)
        {
            return new SimpleScatter(Vmm.Scatter_Initialize(flags));
        }

        public SimpleScatter InitializeScatter(VmmProcess vmmProcess, uint flags = 0u)
        {
            return new SimpleScatter(vmmProcess.Scatter_Initialize(flags));
        }
    }
}
