namespace rt.srz.business.exchange.import.zags.Implementation
{
  using rt.srz.model.HL7.zags;

  public class Zags_VImporter : ZagsImporter<Zags_VNov>
  {
    protected override string XsdResourceName { get { return "Zags_V.xsd"; } }

    protected override Zags_VNov Convert(Zags_VNov data)
    {
      return data;
    }
  }
}
