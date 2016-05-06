using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using PVOID     = System.IntPtr;
using HANDLE    = System.IntPtr;
using SIZE_T    = System.Int32;
using HINSTANCE = System.IntPtr;
using DWORD     = System.UInt32;
using PCHAR     = System.String;

//https://www.youtube.com/watch?v=C2OtYr0EyOg

namespace Dll_Injector
{
    class Program
    {

        const uint PROCESS_ALL_ACCESS = 0x1f0fff;
        const uint MEM_COMMIT = 0x00001000;
        const uint PAGE_READWRITE = 0x04;
        static IntPtr NULL = IntPtr.Zero;

        static void Main(string[] args)
        {
            DWORD pid = Get_Pid("Unturned File Modifier");
            if (pid != 0)
            {
                Inject_Dll(pid, @"C:\Users\Bon\Desktop\Win32Project1.dll");
            }
        }

        static DWORD Get_Pid(PCHAR processName)
        {
            bool isRunning = Process.GetProcesses().Any((Process pname) => pname.ProcessName.Contains(processName));
            if (!isRunning)
                return 0;
            Process[] procNames = Process.GetProcessesByName(processName);
            return (DWORD)procNames[0].Id;
        }


        static void Inject_Dll(DWORD Pid, PCHAR dllName)
        {
         
            HANDLE processHandle;
            PVOID Alloc;
            SIZE_T dllSize;
            HINSTANCE Kernal32Base;
            PVOID LoadLibAddress;


            if (Pid != 0 && dllName != null)
            {
                dllSize = dllName.Length;

                Kernal32Base = pInvokes.GetModuleHandleA("Kernel32.dll");
                if (Kernal32Base == null)
                    goto ExitPoint;

                LoadLibAddress = pInvokes.GetProcAddress(Kernal32Base,"LoadLibraryA");
                processHandle = pInvokes.OpenProcess(PROCESS_ALL_ACCESS, false, Pid);
                if (processHandle == null)
                    goto ExitPoint;

                Alloc = pInvokes.VirtualAllocEx(processHandle, IntPtr.Zero, dllSize, MEM_COMMIT, PAGE_READWRITE);
                if (Alloc == null)
                    goto ExitPoint;

                if (!pInvokes.WriteProcessMemory(processHandle,Alloc,dllName,dllSize,NULL))
                    goto ExitPoint;

                pInvokes.CreateRemoteThread(processHandle,NULL, 0, LoadLibAddress, Alloc, 0, NULL);

            }

            ExitPoint:
                return;
                
        }
    }
}
