namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  public enum WardUnidimensional
    {
        Forward = 0x01,
        Back = 0x02,
        // оба направления
        Both = Forward | Back,
        // ни одного направления
        None = 0x00,
    }
}
