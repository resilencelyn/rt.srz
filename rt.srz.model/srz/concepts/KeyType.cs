﻿//-------------------------------------------------------------------------------------
// <copyright file="KeyType.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------- 

namespace rt.srz.model.srz.concepts
{
  /// <summary> Код причины совпадения </summary>
  public class KeyType : Concept
  {
    /// <summary> {фамилия}, {имя}, {отчество}, {место рождения}, {код типа документа, удостоверяющего личность}, {номер или серия и номер документа, удостоверяющего личность} </summary>
    public const int H01 = 441;

    /// <summary> {фамилия}, {имя}, {отчество}, {дата рождения}, {код типа документа, удостоверяющего личность}, {номер или серия и номер документа, удостоверяющего личность} </summary>
    public const int H02 = 442;

    /// <summary> {фамилия}, {имя}, {отчество}, {дата рождения}, {СНИЛС} </summary>
    public const int H03 = 443;

    /// <summary> {фамилия}, {имя}, {отчество}, {дата рождения}, {код территории, выдавшей документ, подтверждающий факт страхования по ОМС }, {код типа документа, подтверждающего факт страхования по ОМС }, {серия и номер документа, подтверждающего факт страхования по ОМС } </summary>
    public const int H04 = 444;

    /// <summary> {имя}, {отчество}, {дата рождения}, {место рождения}, {СНИЛС} </summary>
    public const int H05 = 445;

    /// <summary> {фамилия}, {имя}, {отчество}, {место рождения}, {код типа документа, удостоверяющего личность}, {номер или серия и номер документа, удостоверяющего личность} </summary>
    public const int P01 = 446;

    /// <summary> {фамилия}, {имя}, {отчество}, {дата рождения}, {код типа документа, удостоверяющего личность}, {номер или серия и номер документа, удостоверяющего личность} </summary>
    public const int P02 = 447;

    /// <summary> {фамилия}, {имя}, {отчество}, {дата рождения}, {СНИЛС} </summary>
    public const int P03 = 448;

    /// <summary> {фамилия}, {имя}, {отчество}, {дата рождения}, {код территории, выдавшей документ, подтверждающий факт страхования по ОМС }, {код типа документа, подтверждающего факт страхования по ОМС }, {серия и номер документа, подтверждающего факт страхования по ОМС } </summary>
    public const int P04 = 449;

    /// <summary> {имя}, {отчество}, {дата рождения}, {место рождения}, {СНИЛС} </summary>
    public const int P05 = 450;
  }
}
