using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Vmmsharp;

namespace SimpleDMA.SimpleDMA
{
    [SupportedOSPlatform("windows")]
    internal class SimpleScatter
    {
        private VmmScatterMemory Scatter { get; set; }
        private bool Disposed = false;
        public SimpleScatter(VmmScatterMemory vmmScatter)
        {
            this.Scatter = vmmScatter;
        }

        public bool Prepare(ulong address, uint offset, uint flags = 0u)
        {
            return Scatter.Prepare(address, offset);
        }

        public unsafe byte[] Read(ulong address, uint offset)
        {
            return Scatter.Read(address, offset);
        }

        public unsafe string ReadString(Encoding encoding, ulong address, uint offset, bool endOnNull = true)
        {
            return Scatter.ReadString(encoding, address, offset, endOnNull);
        }

        public unsafe T[] ReadArray<T>(ulong address, uint count) where T : unmanaged
        {
            return Scatter.ReadArray<T>(address, count);
        }

        public bool ReadAs<T>(ulong address, uint offset, out T value) where T : unmanaged
        {
            return Scatter.ReadAs<T>(address, out value);
        }

        public bool PrepareWrite(ulong address, byte[] data)
        {
           return Scatter.PrepareWrite(address, data);
        }

        public bool PrepareWrite<T>(ulong address, T value) where T : unmanaged
        {
            return Scatter.PrepareWriteStruct(address, value);
        }

        public bool Execute()
        {
            return Scatter.Execute();
        }

        public bool Clear(uint flags)
        {
            return Scatter.Clear(flags);
        }
        public override string ToString()
        {
            return Scatter.ToString();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    Scatter.Dispose();
                }
                Disposed = true;
            }
        }
    }
}
