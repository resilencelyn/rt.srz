namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  public enum TEmptyStringTypes
    {
        Empty,     // пустая строка (string.Empty)
        Null,      // null на месте объекта строки
        Signature, // строка, содержащая текст "null"
    }
}
