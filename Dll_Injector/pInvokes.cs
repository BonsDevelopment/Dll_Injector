using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dll_Injector
{
    static class pInvokes
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
            uint dwDesiredAccess,
            bool bInheritHandle,
            uint dwProcessId
        );

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(
            IntPtr hProcess, 
            IntPtr lpAddress,
            int dwSize, 
            uint flAllocationType, 
            uint flProtect);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            string lpBuffer,
            int nSize,
            IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandleA(
            string lpModuleName);

        [DllImport("kernel32")]
        public static extern IntPtr GetProcAddress(
            IntPtr hModule, 
            string lpProcName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(
           IntPtr hProcess,
           IntPtr lpThreadAttributes, 
           uint dwStackSize, 
           IntPtr lpStartAddress,
           IntPtr lpParameter,
           uint dwCreationFlags, 
           IntPtr lpThreadId);

    }
}
