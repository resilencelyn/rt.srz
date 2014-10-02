namespace rt.srz.database.business.standard.keyscompiler
{
  using rt.srz.database.business.standard.keyscompiler.Fields;

  public class LoadedKey
    {
        public string name;
        public int type;
        public int subtype;
        public FieldTypes[] formula;
        public string idCardDate;
        public string idCardDateExp;
        public string idCardOrg;
        public string idCardNumber;
    }
}
