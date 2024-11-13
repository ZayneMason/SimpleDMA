using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Vmmsharp;

namespace SimpleDMA.SimpleDMA
{
    [SupportedOSPlatform("windows")]
    internal class SimpleMem
    {
        private string Device { get; set; }
        private Vmm Vmm { get; set; }
        public SimpleMem(string device)
        {
            this.Device = device;
            this.Vmm = new Vmm("-device", device);
        }

        public VmmProcess ReadProcess(string process)
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

        public byte[] ReadBytes(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return vmmProcess.MemRead(address, offset);
        }

        public int ReadInt(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToInt32(ReadBytes(vmmProcess, address, offset), 0);
        }

        public float ReadFloat(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToSingle(ReadBytes(vmmProcess, address, offset), 0);
        }

        public double ReadDouble(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToDouble(ReadBytes(vmmProcess, address, offset), 0);
        }

        public string ReadString(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return Encoding.UTF8.GetString(ReadBytes(vmmProcess, address, offset));
        }

        public ulong ReadLong(VmmProcess vmmProcess, ulong address, uint offset)
        {
            return BitConverter.ToUInt64(ReadBytes(vmmProcess, address, offset), 0);
        }
    }
}
