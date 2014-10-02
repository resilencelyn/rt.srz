// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The ContentManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Text;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The ContentManager.
  /// </summary>
  public partial class ContentManager
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
    public byte[] Base64ToByte(string str)
    {
      if (str == null)
        return null;

      return Convert.FromBase64String(str);
    }

    /// <summary>
    /// The byte to base 64.
    /// </summary>
    /// <param name="content">
    /// The content.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string ByteToBase64(byte[] content)
    {
      if (content == null)
        return null;

      return Convert.ToBase64String(content);
    }

    /// <summary>
    /// Генерация пустой подписи
    /// </summary>
    /// <returns></returns>
    public byte[] CreateEmptySign()
    {
      return InternalCreateImage(736, 160);
    }

    /// <summary>
    /// Генерация пустого фото
    /// </summary>
    /// <returns></returns>
    public byte[] CreateEmptyPhoto()
    {
      return InternalCreateImage(320, 400);
    }

    private byte[] InternalCreateImage(int width, int height)
    {
      using (var result = new Bitmap(width, height, PixelFormat.Format24bppRgb))
      {
        using (var stream = new MemoryStream())
        {
          using (var g = Graphics.FromImage(result))
          {
            g.FillRectangle(Brushes.White, 0, 0, width, height);
          }
          result.Save(stream, ImageFormat.Jpeg);
          return stream.GetBuffer();
        }
      }
    }


    /// <summary>
    /// Конвертация в тона серого
    /// </summary>
    /// <param name="image">
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public byte[] ConvertToGrayScale(byte[] image)
    {
      using (var originalImageStream = new MemoryStream(image))
      {
        using (var original = (Bitmap)Image.FromStream(originalImageStream))
        {
          using (var newBitmap = new Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb))
          {
            newBitmap.SetResolution(original.HorizontalResolution, original.VerticalResolution);
            using (var g = Graphics.FromImage(newBitmap))
            {
              // create the grayscale ColorMatrix
              var colorMatrix =
                new ColorMatrix(
                  new[]
                    {
                      new[] { .3f, .3f, .3f, 0, 0 }, new[] { .59f, .59f, .59f, 0, 0 }, new[] { .11f, .11f, .11f, 0, 0 }, 
                      new float[] { 0, 0, 0, 1, 0 }, new float[] { 0, 0, 0, 0, 1 }
                    });

              var attributes = new ImageAttributes();
              attributes.SetColorMatrix(colorMatrix);

              // draw the original image on the new image using the grayscale color matrix
              g.DrawImage(
                original,
                new Rectangle(0, 0, original.Width, original.Height),
                0,
                0,
                original.Width,
                original.Height,
                GraphicsUnit.Pixel,
                attributes);
            }

            using (var newImageStream = new MemoryStream())
            {
              newBitmap.Save(newImageStream, original.RawFormat);
              return newImageStream.GetBuffer();
            }
          }
        }
      }
    }

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
    public Content GetContent(Guid personDataId, int contentTypeId)
    {
      return SingleOrDefault(x => x.InsuredPersonData.Id == personDataId && x.ContentType.Id == contentTypeId);
    }

    /// <summary>
    /// The get foto.
    /// </summary>
    /// <param name="personDataId">
    ///   The person data id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetFoto(Guid personDataId)
    {
      return GetContentBase64(personDataId, TypeContent.Foto);
    }

    /// <summary>
    /// The get signature.
    /// </summary>
    /// <param name="personDataId">
    ///   The person data id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetSignature(Guid personDataId)
    {
      return GetContentBase64(personDataId, TypeContent.Signature);
    }

    /// <summary>
    /// The save content record.
    /// </summary>
    /// <param name="contentType">
    /// The content type.
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
    public Content SaveContentRecord(int contentType, byte[] content, string fileName = null)
    {
      var contentRecord = new Content
                            {
                              ContentType =
                                ObjectFactory.GetInstance<IConceptCacheManager>().GetById(contentType),
                              DocumentContent = content,
                              FileName = fileName,
                              ChangeDate = DateTime.Now
                            };

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      ITransaction transaction = null;
      try
      {
        transaction = session.BeginTransaction();
        session.SaveOrUpdate(contentRecord);
        transaction.Commit();
      }
      catch (Exception ex)
      {
        transaction.Dispose();
        throw;
      }

      return contentRecord;
    }

    #endregion

    #region Methods

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
    public string GetContentBase64(Guid personDataId, int contentTypeId)
    {
      var content = GetContent(personDataId, contentTypeId);
      return content != null ? ByteToBase64(content.ContentInterior) : string.Empty;
    }

    #endregion
  }
}