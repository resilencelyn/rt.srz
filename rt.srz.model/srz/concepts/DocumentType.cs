// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   �������� ���
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.srz.model.srz.concepts
{
  /// <summary> �������� ��� </summary>
  public class DocumentType : Concept
  {
    /// <summary> ������� ���������� ���� </summary>
    public const int DocumentType1 = 1;

    /// <summary> ������������� � ����������� ����������� � ��������� ���������� �������� </summary>
    public const int DocumentType10 = 10;

    /// <summary> ��� �� ���������� </summary>
    public const int DocumentType11 = 11;

    /// <summary> ������������� ������� � ���������� ��������� </summary>
    public const int DocumentType12 = 12;

    /// <summary> ��������� ������������� �������� ���������� ���������� ��������� </summary>
    public const int DocumentType13 = 13;

    /// <summary> ������� ���������� ���������� ��������� </summary>
    public const int PassportRf = 14;

    /// <summary> ����������� ������� ���������� ���������� ��������� </summary>
    public const int DocumentType15 = 15;

    /// <summary> ������� ������ </summary>
    public const int DocumentType16 = 16;

    /// <summary> ������� ����� ������� ������ </summary>
    public const int DocumentType17 = 17;

    /// <summary> ���� ��������� </summary>
    public const int DocumentType18 = 18;

    /// <summary> ������������� ���������� ���� </summary>
    public const int DocumentType2 = 2;

    /// <summary> ������������� � �������� ���������� ��������� </summary>
    public const int BirthCertificateRf = 3;

    /// <summary> ������������� �������� ������� </summary>
    public const int DocumentType4 = 4;

    /// <summary> ������� �� ������������ �� ����� ������� ������� </summary>
    public const int DocumentType5 = 5;

    /// <summary> ������� ����������� </summary>
    public const int DocumentType6 = 6;

    /// <summary> ������� ����� </summary>
    public const int DocumentType7 = 7;

    /// <summary> ��������������� ������� ���������� ���������� ��������� </summary>
    public const int DocumentType8 = 8;

    /// <summary> ����������� ������� </summary>
    public const int DocumentType9 = 9;

    /// <summary> ����� ������ �� ������� � ������� ������� ������� � ����������� ������������ ������ � �������� � � ����� � ������������ </summary>
    public const int DocumentType20 = 391;

    /// <summary> �������� ������������ ���������� </summary>
    public const int DocumentType21 = 392;

    /// <summary> �������� ���� ��� ����������� </summary>
    public const int DocumentType22 = 393;

    /// <summary> ���������� �� ��������� ����������  </summary>
    public const int DocumentType23 = 394;

    /// <summary> ������������� � ����������� �� ����� ����������  </summary>
    public const int CertificationRegistration = 429;

    /// <summary> ������������� � ��������, �������� �� � ���������� ��������� </summary>
    public const int DocumentType24 = 629;

    /// <summary> ������������� � �������������� ���������� ������� �� ���������� ���������� ��������� </summary>
    public const int DocumentType25 = 630;

    public static bool IsDocExp(int id)
    {
      if (id == DocumentType10)
      {
        return true;
      }

      if (id == DocumentType11)
      {
        return true;
      }

      if (id == DocumentType12)
      {
        return true;
      }

      if (id == DocumentType13)
      {
        return true;
      }

      if (id == DocumentType23)
      {
        return true;
      }

      if (id == DocumentType25)
      {
        return true;
      }

      return false;
    }
  }
}