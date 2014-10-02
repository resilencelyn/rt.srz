using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UECLibTest1
{
  class Program
  {
    private const int UECMaxStringLength = 1024;

    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint OpenCard(
      [MarshalAs(UnmanagedType.LPStr)] string uekServiceToken);
    
    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadOMSData(
      [MarshalAs(UnmanagedType.LPArray)] [In] [Out] OMSData[] omsData,
      [In] [Out] ref ulong dataSize);

    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint Authorise(
      [MarshalAs(UnmanagedType.LPStr)] string pinCode,
      [In] [Out] ref byte pinRestTriesOut);

    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadPrivateDataInString(
     [MarshalAs(UnmanagedType.LPWStr)] StringBuilder destinationString,
     ulong stringSize);

    [DllImport("UECLib.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ReadMainOMSDataInString(
     [MarshalAs(UnmanagedType.LPWStr)] StringBuilder destinationString,
     ulong stringSize);

    static void Main(string[] args)
    {
      uint n = OpenCard(string.Empty);
      ulong size = 11;
      byte b = 0;
      n = Authorise("1234", ref b);
      
      ulong stringSize = 10000;
      StringBuilder destinationString = new StringBuilder((int)stringSize);
      ReadPrivateDataInString(destinationString, stringSize);

      StringBuilder destinationString1 = new StringBuilder((int)stringSize);
      ReadMainOMSDataInString(destinationString, stringSize);
    }
  }
}
