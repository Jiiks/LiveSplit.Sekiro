using System;
using System.Diagnostics;

namespace SekiroIGT {
    class SekiroPointer {

        private long _address;
        private IntPtr _handle = IntPtr.Zero;
        private Process _process;

        public long GetAddress() => _address;

        public SekiroPointer() {
            GetProcess(Config.Module);
            FindAddress();
        }

        private void FindAddress() {
            if (_process == null) return;
            _handle = _process.Id.GetHandle();

            _address = Config.BasePointer.GetAddress(Config.Offsets[0], _handle, 8);
        }

        private void GetProcess(string name) {
            var processes = Process.GetProcessesByName(name);

            if (processes.Length <= 0) return;
            _process = processes[0];
            _process.EnableRaisingEvents = true;
            _process.Exited += (sender, args) => DoReset();
        }

        private void DoReset() {
            _process = null;
            _handle = IntPtr.Zero;
            _address = 0;
        }

        public int GetIgt() {
            if (_process == null) {
                GetProcess(Config.Module);
                return 0;
            }

            if(_handle == IntPtr.Zero) FindAddress();
            if(_address == 0) FindAddress();

            var millis = Native.GetGameTimeMilliseconds(_address, _handle, 8);

            if(millis < 200) FindAddress();

            return Native.GetGameTimeMilliseconds(_address, _handle, 8);
        }

    }
}
