namespace rt.srz.business.exchange.import.zags
{
  using System;

  using rt.srz.model.HL7.zags;

  public class ZagsImportFactory : IZagsImportFactory
  {
    private readonly IZagsImporter[] _importerZags;

    public ZagsImportFactory(IZagsImporter[] importerZags)
    {
      _importerZags = importerZags;
    }

    public Zags_VNov GetImportData(string xmlFilePath)
    {
      Zags_VNov result = null;
      foreach (var importer in _importerZags)
      {
        try
        {
          result = importer.GetImportData(xmlFilePath);
          if (result != null)
          {
            return result;
          }
        }
        catch
        {
        }
      }
      throw new ApplicationException("Не найдены данные, соответствующие какой либо их схем xsd");
    }

  }
}
