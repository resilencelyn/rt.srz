// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IContentManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface ContentManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface ContentManager.
  /// </summary>
  public partial interface IContentManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The base 64 to byte.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    byte[] Base64ToByte(string str);

    /// <summary>
    /// The byte to base 64.
    /// </summary>
    /// <param name="content">
    /// The content.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string ByteToBase64(byte[] content);

    /// <summary>
    /// The convert to gray scale.
    /// </summary>
    /// <param name="image">
    /// The image.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    byte[] ConvertToGrayScale(byte[] image);

    /// <summary>
    /// The get content.
    /// </summary>
    /// <param name="personDataId">
    ///   The person data id.
    /// </param>
    /// <param name="contentTypeId">
    ///   The content type id.
    /// </param>
    /// <returns>
    /// The <see cref="Content"/>.
    /// </returns>
    Content GetContent(Guid personDataId, int contentTypeId);

    /// <summary>
    /// The get foto.
    /// </summary>
    /// <param name="personDataId">
    ///   The person data id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetFoto(Guid personDataId);

    /// <summary>
    /// The get signature.
    /// </summary>
    /// <param name="personDataId">
    ///   The person data id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetSignature(Guid personDataId);

    /// <summary>
    /// The save content record.
    /// </summary>
    /// <param name="typeContent">
    /// The type content.
    /// </param>
    /// <param name="content">
    /// The content.
    /// </param>
    /// <param name="fileName">
    /// The file Name.
    /// </param>
    /// <returns>
    /// The <see cref="Content"/>.
    /// </returns>
    Content SaveContentRecord(int typeContent, byte[] content, string fileName = null);

    /// <summary>
    /// Генерация пустой подписи
    /// </summary>
    /// <returns></returns>
    byte[] CreateEmptySign();

    /// <summary>
    /// Генерация пустого фото
    /// </summary>
    /// <returns></returns>
    byte[] CreateEmptyPhoto();

    #endregion

    /// <summary>
    /// The get content base 64.
    /// </summary>
    /// <param name="personDataId">
    ///   The person data id.
    /// </param>
    /// <param name="contentTypeId">
    ///   The content type id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetContentBase64(Guid personDataId, int contentTypeId);
  }
}