namespace rt.srz.business.exchange.import.zags
{
  using rt.srz.model.HL7.zags;

  public interface IZagsImportFactory
  {
    Zags_VNov GetImportData(string xmlFilePath);
  }
}
