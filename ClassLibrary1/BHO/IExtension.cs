using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.ServiceModel;

namespace ClassLibrary1.BHO
{
    [
        ComVisible(true),
        Guid("0bda2434-16ae-4af5-be91-b11409482f86"),
        InterfaceType(ComInterfaceType.InterfaceIsDual)
        ]
    public interface IExtension
    {
        [DispId(1)]
        void Foo(string s);
        [DispId(1)]
        void Mobile(string s);
    }
}
