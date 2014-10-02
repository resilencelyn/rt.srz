// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchTypedPfr.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export.pfr
{
  #region

  using System.Collections.Generic;
  using System.IO;

  using rt.core.business.server.exchange.export;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.model.HL7.pfr;

  #endregion

  /// <summary>
  /// The export batch pfr.
  /// </summary>
  public class ExportBatchPfr : ExportBatchTyped<SnilsZlListAtr, string>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportBatchPfr"/> class.
    /// </summary>
    public ExportBatchPfr()
      : base(ExportBatchType.Pfr)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add node.
    /// </summary>
    /// <param name="node">
    /// The node.
    /// </param>
    public override void AddNode(string node)
    {
      base.AddNode(node);
      if (!string.IsNullOrEmpty(node))
      {
        SerializeObject.Snilses.Add(node);
      }
    }

    public override void BeginBatch()
    {
      base.BeginBatch();
      SerializeObject = new SnilsZlListAtr
        {
          Snilses = new List<string>(), 
          Zglv = new ZglvAtr { Version = "1.0" }
        };
    }

    /// <summary>
    ///   Сериализует текущий объект пакета
    /// </summary>
    protected override void SerializePersonCurrent()
    {
      BuildZglv();
      XmlSerializationHelper.SerializeToFile(SerializeObject, GetFileNameFull(), "snils_zl_list");
      base.SerializePersonCurrent();
    }

    private void BuildZglv()
    {
      SerializeObject.Zglv.Nrec = Count.ToString();
      SerializeObject.Zglv.Filename = Path.GetFileNameWithoutExtension(FileName);
      SerializeObject.Zglv.Nfile = FileName.Substring(10, 3);
      SerializeObject.Zglv.CodPfr = FileName.Substring(0, 3);
    }


    /// <summary>
    ///   Количество выгруженных сообщений за текущий сеанс
    /// </summary>
    public override int Count
    {
      get
      {
        if (SerializeObject != null)
        {
          if (SerializeObject.Snilses != null)
          {
            return SerializeObject.Snilses.Count;
          }
        }

        return 0;
      }
    }

    #endregion
  }
}