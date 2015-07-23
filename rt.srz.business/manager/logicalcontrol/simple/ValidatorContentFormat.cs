// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorContentFormat.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator content format.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System.Drawing;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step5;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator content format.
  /// </summary>
  public class ValidatorContentFormat : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorContentFormat"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorContentFormat(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.InsuredPersonData.Contents)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorContentFormat;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public override void CheckObject(Statement statement)
    {
      // проверяем только для уэк и электронного полиса
      if (
        !(statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value
          && (statement.FormManufacturing == null || statement.FormManufacturing.Id == PolisType.Э)))
      {
        return;
      }

      // проверяем формат фотографии
      if (statement.InsuredPersonData != null)
      {
        if (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization
            && statement.InsuredPersonData.Contents != null)
        {
          var list = statement.InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.Foto);
          if (list.Count() == 0)
          {
            throw new FaultEmptyPhotoException();
          }

          foreach (var content in list)
          {
            using (var stream = new MemoryStream(content.ContentInterior))
            {
              var image = Image.FromStream(stream);
              if (image.Width != 320 || image.Height != 400 || !ImageFormat.Jpeg.Equals(image.RawFormat))
              {
                throw new FaultPhotoFormatException();
              }
            }
          }

          // проверяем формат подписи
          list = statement.InsuredPersonData.Contents.Where(x => x.ContentType.Id == TypeContent.Signature);
          if (list.Count() == 0)
          {
            throw new FaultEmptySignatureException();
          }

          foreach (var content in list)
          {
            using (var stream = new MemoryStream(content.ContentInterior))
            {
              var image = Image.FromStream(stream);
              if (image.Width != 736 || image.Height != 160 || !ImageFormat.Jpeg.Equals(image.RawFormat))
              {
                throw new FaultSignatureFormatException();
              }
            }
          }
        }
      }
    }

    #endregion
  }
}