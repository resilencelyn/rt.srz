namespace rt.core.model
{
  using System;

  /// <summary>
  /// The ServiceRegistry interface.
  /// </summary>
  public interface IServiceRegistry
  {
    /// <summary>
    /// The shutdown.
    /// </summary>
    /// <param name="span">
    /// The span. 
    /// </param>
    void Shutdown(TimeSpan span);
  }
}