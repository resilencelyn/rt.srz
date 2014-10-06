// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserWorkerInteraction.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The UserWorkerInteraction interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Interfaces
{
  /// <summary>
  ///   The UserWorkerInteraction interface.
  /// </summary>
  public interface IUserWorkerInteraction
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The is task cancelled.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    bool IsTaskCancelled();

    /// <summary>
    ///   The is timer tripped.
    /// </summary>
    /// <returns>
    ///   The <see cref="bool" />.
    /// </returns>
    bool IsTimerTripped();

    /// <summary>
    /// The post progress.
    /// </summary>
    /// <param name="ratio">
    /// The ratio.
    /// </param>
    void PostProgress(double ratio);

    /// <summary>
    ///   The restart timer.
    /// </summary>
    void RestartTimer();

    /// <summary>
    ///   The stop timer.
    /// </summary>
    void StopTimer();

    #endregion
  }
}