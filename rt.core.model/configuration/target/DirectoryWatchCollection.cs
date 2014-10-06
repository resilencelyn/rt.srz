namespace rt.core.model.configuration.target
{
  using System.Configuration;

  /// <summary>
  ///   The file name element colection.
  /// </summary>
  public class DirectoryWatchCollection : ConfigurationElementCollection
  {
    /// <summary>
    ///   The create new element.
    /// </summary>
    /// <returns>
    ///   The <see cref="ConfigurationElement" />.
    /// </returns>
    protected override ConfigurationElement CreateNewElement()
    {
      return new DirectoryWatchElement();
    }

    /// <summary>
    /// The get element key.
    /// </summary>
    /// <param name="element">
    /// The element.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object GetElementKey(ConfigurationElement element)
    {
      var filenameElement = element as DirectoryWatchElement;
      return filenameElement != null ? filenameElement.Path : element.ToString();
    }
  }
}