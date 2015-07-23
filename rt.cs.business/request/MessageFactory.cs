// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageFactory.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request
{
  using System.Linq;

  using StructureMap;

  /// <summary>
  /// The message factory.
  /// </summary>
  public class MessageFactory : IMessageFactory
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="IMessageExporter"/>.
    /// </returns>
    public IMessageExporter GetExporter(int type)
    {
      var exportBatchTypeds = ObjectFactory.GetAllInstances<IMessageExporter>();
      return exportBatchTypeds.FirstOrDefault(x => x.AppliesTo(type));
    }

    #endregion
  }
}