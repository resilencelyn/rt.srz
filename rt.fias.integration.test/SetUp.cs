// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetUp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.integration.test
{
  using System;
  using System.Diagnostics;
  using System.IO;

  using NUnit.Framework;

  /// <summary>
  ///   The set up.
  /// </summary>
  [SetUpFixture]
  public class SetUp
  {
    #region Constants

    /// <summary>
    ///   The application name.
    /// </summary>
    private const string ApplicationName = "rt.fias.services";

    #endregion

    #region Fields

    /// <summary>
    ///   The _iis process.
    /// </summary>
    private Process iisProcess;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The run after any tests.
    /// </summary>
    [TearDown]
    public void RunAfterAnyTests()
    {
      if (iisProcess.HasExited == false)
      {
        iisProcess.Kill();
      }
    }

    /// <summary>
    ///   The run before any tests.
    /// </summary>
    [SetUp]
    public void RunBeforeAnyTests()
    {
      var applicationPath = GetApplicationPath(ApplicationName);
      var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

      iisProcess = new Process
                    {
                      StartInfo =
                      {
                        FileName = string.Format("{0}/IIS Express/iisexpress.exe", programFiles),
                        Arguments =
                          string.Format("/path:\"{0}\" /port:{1}", applicationPath, 9060)
                      }
                    };
      iisProcess.Start();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get application path.
    /// </summary>
    /// <param name="applicationName">
    /// The application name.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetApplicationPath(string applicationName)
    {
      var tmpDirName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
      var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(tmpDirName)));
      var result = Path.Combine(solutionFolder, applicationName);
      return result;
    }

    #endregion
  }
}