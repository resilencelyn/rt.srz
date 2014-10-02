// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PortReader.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Класс для считывания байт с СОМ порта. ВАЖНО!!! - возможно в считанную последоватлеьность вкрадывается 1 или 2 байта
//   лишние, х.з. откуда
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Timers;
using rt.srz.barcode.converter;
using rt.srz.barcode.reader.delegates;
using rt.srz.barcode.reader.delegates.args;
using rt.srz.model.barcode;
using Timer = System.Timers.Timer;

#endregion

namespace rt.srz.barcode.reader
{
  /// <summary>
  ///   Класс для считывания байт с СОМ порта. ВАЖНО!!! - возможно в считанную последоватлеьность вкрадывается 1 или 2 байта
  ///   лишние, х.з. откуда
  /// </summary>
  public class PortReader
  {
    #region Fields

    /// <summary>
    ///   The reading timer.
    /// </summary>
    private readonly Timer readingTimer;

    /// <summary>
    ///   The serial port.
    /// </summary>
    private readonly SerialPort serialPort;

    /// <summary>
    ///   The buffer.
    /// </summary>
    private string buffer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="PortReader" /> class.
    /// </summary>
    public PortReader()
    {
      serialPort = new SerialPort();
      SetPortDefaults();
      readingTimer = new Timer(ReadTimeout);
      readingTimer.Elapsed += ReadingTimerElapsed;
    }

    #endregion

    #region Public Events

    /// <summary>
    ///   The beep event.
    /// </summary>
    public event StringBarcodeEventHandler BeepEvent;

    /// <summary>
    ///   The data recieved.
    /// </summary>
    public event DataRecievedEventHandler DataRecieved;

    /// <summary>
    ///   The error occurred.
    /// </summary>
    public event ErrorOccurredEventHandler ErrorOccurred;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Скорость передачи данных
    /// </summary>
    public int BaudRate
    {
      get { return serialPort.BaudRate; }

      set { serialPort.BaudRate = value; }
    }

    /// <summary>
    ///   Имя порта, например COM5
    /// </summary>
    public string PortName
    {
      get { return serialPort.PortName; }

      set
      {
        var readindStarted = readingTimer.Enabled;
        var serialPortOpened = serialPort.IsOpen;
        if (readindStarted)
        {
          readingTimer.Stop();
        }

        if (serialPortOpened)
        {
          serialPort.Close();
        }

        serialPort.PortName = value;
        if (serialPortOpened)
        {
          serialPort.Open();
        }

        if (readindStarted)
        {
          readingTimer.Start();
        }
      }
    }

    /// <summary>
    ///   Таймаут опроса порта
    /// </summary>
    public int ReadTimeout
    {
      get { return serialPort.ReadTimeout; }

      set { serialPort.ReadTimeout = value; }
    }

    /// <summary>
    ///   Интервал времени, втечение которого ожидается приход остальной
    ///   части данных с порта, в случае
    ///   если все данные не удалось считать за одну операию чтения
    /// </summary>
    public int ReadToEndTimeout { get; set; }

    /// <summary>
    ///   Длинна ожидаемого сообщения
    /// </summary>
    public int WaitingMessageLength { get; private set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The set port defaults.
    /// </summary>
    public void SetPortDefaults()
    {
      serialPort.PortName = "COM5";
      serialPort.BaudRate = 9600;
      serialPort.Parity = Parity.None;
      serialPort.DataBits = 8;
      serialPort.StopBits = StopBits.One;
      serialPort.Handshake = Handshake.None;
      serialPort.ParityReplace = 0xff;
      serialPort.Encoding = Encoding.GetEncoding(28591);
      serialPort.ReadTimeout = 50;
      serialPort.WriteTimeout = 100;
      serialPort.DtrEnable = true;
      WaitingMessageLength = 130;
      ReadToEndTimeout = 300;
    }

    /// <summary>
    ///   The start.
    /// </summary>
    public void Start()
    {
      serialPort.Open();
      readingTimer.Start();
    }

    /// <summary>
    ///   The stop.
    /// </summary>
    public void Stop()
    {
      readingTimer.Stop();
      serialPort.Close();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The on beep.
    /// </summary>
    /// <param name="args">
    /// The args. 
    /// </param>
    protected virtual void OnBeep(string args)
    {
      if (BeepEvent != null)
      {
        BeepEvent(args);
      }
    }

    /// <summary>
    /// The on data recieved.
    /// </summary>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected virtual void OnDataRecieved(PolicyData args)
    {
      if (DataRecieved != null)
      {
        var dt = DateTime.Parse(args.BirthDate);
        args.BirthDate = dt.ToString("dd.MM.yyyy");
        DataRecieved(args);
      }
    }

    /// <summary>
    /// The on error occurred.
    /// </summary>
    /// <param name="e">
    /// The e. 
    /// </param>
    protected virtual void OnErrorOccurred(ErrorEventArgs e)
    {
      if (ErrorOccurred != null)
      {
        ErrorOccurred(this, e);
      }
    }

    /// <summary>
    /// The reading timer elapsed.
    /// </summary>
    /// <param name="sender">
    /// The sender. 
    /// </param>
    /// <param name="e">
    /// The e. 
    /// </param>
    private void ReadingTimerElapsed(object sender, ElapsedEventArgs e)
    {
      lock (this)
      {
        try
        {
          var readedStr = serialPort.ReadExisting();

          if (!string.IsNullOrEmpty(readedStr))
          {
            buffer += readedStr;
            Thread.Sleep(ReadToEndTimeout);
            if (serialPort.BytesToRead == 0)
            {
              OnBeep(buffer);
              var tempBytes = Encoding.GetEncoding(28591).GetBytes(buffer);
              buffer = string.Empty;
              var manager = new BarcodeManager();
              var strXml = manager.DecomposeBarcode(tempBytes);
              var drea = new DataRecievedEventArgs(strXml);
              OnDataRecieved(drea.PolicyData);
            }
          }
        }
        catch (TimeoutException)
        {
        }
        catch (BarcodeConverterException exc)
        {
          OnErrorOccurred(new ErrorEventArgs(exc));
        }
        catch (BarcodeServiceException exc)
        {
          OnErrorOccurred(new ErrorEventArgs(exc));
        }
        catch (InvalidOperationException exc)
        {
        }
        catch (IOException)
        {
        }
      }
    }

    #endregion
  }
}