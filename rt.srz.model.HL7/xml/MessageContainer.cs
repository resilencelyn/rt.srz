// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageContainer.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The message container.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.foms.HL7.xml
{
  #region references

  using System;
  using System.IO;
  using System.Xml.Linq;

  using rt.foms.HL7.commons;
  using rt.foms.HL7.person;
  using rt.foms.HL7.person.messages;
  using rt.foms.HL7.person.requests;

  #endregion

  /// <summary>
  ///   The message container.
  /// </summary>
  internal sealed class MessageContainer
  {
    #region Fields

    /// <summary>
    ///   The file path.
    /// </summary>
    private readonly string filePath;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageContainer"/> class.
    /// </summary>
    /// <param name="filePath">
    /// The file path.
    /// </param>
    internal MessageContainer(string filePath)
    {
      this.filePath = filePath;
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The deserialize.
    /// </summary>
    /// <returns>
    ///   The <see cref="BaseAnswerMessageTemplate" />.
    /// </returns>
    internal BaseAnswerMessageTemplate Deserialize()
    {
      try
      {
        if (FileSystemPhysical.FileExists(filePath))
        {
          switch (Path.GetExtension(filePath).ToLower())
          {
            case ".rsp_zk1":
              return XmlStreamer.Deserialize<RSP_ZK1>(filePath);

            case ".rsp_zk2":
              return XmlStreamer.Deserialize<RSP_ZK2>(filePath);

            case ".rsp_zk4":
              return XmlStreamer.Deserialize<RSP_ZK4>(filePath);

            case ".rsp_zk5":
              return XmlStreamer.Deserialize<RSP_ZK5>(filePath);
          }

          return XmlStreamer.Deserialize<Ack>(filePath);
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
      }

      return null;
    }

    /// <summary>
    ///   The load.
    /// </summary>
    /// <returns>
    ///   The <see cref="XElement" />.
    /// </returns>
    internal XElement Load()
    {
      if (FileSystemPhysical.FileExists(filePath))
      {
        return XElement.Load(filePath);
      }

      return null;
    }

    /// <summary>
    /// The serialize.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    internal bool Serialize(object message)
    {
      try
      {
        if (message != null)
        {
          XmlStreamer.Serialize(message, filePath, null);
          return true;
        }
      }
      catch (Exception exception)
      {
        FomsLogger.WriteError(exception, null);
      }

      return false;
    }

    #endregion
  }
}