namespace rt.srz.database.business.standard.hasher
{
  using System.IO;
  using System.Text;
  using System.Security.Cryptography;

  public class Hasher
  {
    public static byte[] CalculateHashFromString(string value, IHashAlgorithm algorithm)
    {
      byte[] buf = Encoding.GetEncoding("Windows-1251").GetBytes(value);
      Stream stream = new MemoryStream(buf);
      return algorithm.ComputeHash(stream);
    }

  }
}
