namespace rt.srz.barcode.activex
{
  using System;
  using System.Runtime.InteropServices;

  [Guid("4794D615-BE51-4a1e-B1BA-453F6E9337C4")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ComSourceInterfaces(typeof(IComEvents))]
  public class MyComObject : IComObject
  {
    [ComVisible(false)]
    public delegate void MyFirstEventHandler(string args);

    public event MyFirstEventHandler MyFirstEvent;

    public int MyFirstComCommand(string arg)
    {
      if (MyFirstEvent != null)
        MyFirstEvent(arg);
      return (int)DateTime.Now.Ticks;
    }

    public void Dispose()
    {
    }
  }

  [Guid("4B3AE7D8-FB6A-4558-8A96-BF82B54F329C")]
  [ComVisible(true)]
  public interface IComObject
  {
    [DispId(0x10000001)]
    int MyFirstComCommand(string arg);

    [DispId(0x10000002)]
    void Dispose();
  }

  [Guid("ECA5DD1D-096E-440c-BA6A-0118D351650B")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  public interface IComEvents
  {
    [DispId(0x00000001)]
    void MyFirstEvent(string args);
  }
}
