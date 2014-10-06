// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SoundExTransliteratedRus.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   SoundEx ��� ��������� �� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard
{
  using System.Collections.Generic;
  using System.Text;
  using System.Text.RegularExpressions;

  using rt.srz.database.business.standard.helpers;

  /// <summary>
  ///   SoundEx ��� ��������� �� �������
  /// </summary>
  public static class SoundExTransliteratedRus
  {
    #region Static Fields

    /// <summary>
    ///   ������� �����
    /// </summary>
    private static readonly CodesDictionary codesDictionary = new CodesDictionary
                                                              {
                                                                symbols =
                                                                  new Dictionary
                                                                  <char, CodesDictionary>
                                                                  {
                                                                    {
                                                                      'A', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        0, 
                                                                        -1, 
                                                                        -1
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'I', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'J', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'Y', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'U', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                7, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'B', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        7, 7, 
                                                                        7
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'C', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        5, 5, 
                                                                        5
                                                                      }, 
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        4, 4, 
                                                                        4
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'Z', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'S', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'K', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                5, 
                                                                                5, 
                                                                                5
                                                                              }, 
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                45, 
                                                                                45, 
                                                                                45
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'H', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                5, 
                                                                                5, 
                                                                                5
                                                                              }, 
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        5, 
                                                                                        54, 
                                                                                        54
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'D', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        3, 3, 
                                                                        3
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'T', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                3, 
                                                                                3, 
                                                                                3
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'Z', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'H', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'S', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'H', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'R', 
                                                                              new CodesDictionary
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'E', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        0, 
                                                                        -1, 
                                                                        -1
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'I', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'J', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'Y', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'U', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                1, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'F', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        7, 7, 
                                                                        7
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'B', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                7, 
                                                                                7, 
                                                                                7
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'G', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        5, 5, 
                                                                        5
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'H', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        5, 5, 
                                                                        -1
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'I', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        0, 
                                                                        -1, 
                                                                        -1
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'A', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                1, 
                                                                                -1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'E', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                1, 
                                                                                -1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'O', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                1, 
                                                                                -1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'U', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                1, 
                                                                                -1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'J', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        4, 4, 
                                                                        4
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'K', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        5, 5, 
                                                                        5
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'H', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                5, 
                                                                                5, 
                                                                                5
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'S', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                5, 
                                                                                54, 
                                                                                54
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'L', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        8, 8, 
                                                                        8
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'M', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        6, 6, 
                                                                        6
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'N', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                66, 
                                                                                66, 
                                                                                66
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'N', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        6, 6, 
                                                                        6
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'M', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                66, 
                                                                                66, 
                                                                                66
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'O', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        0, 
                                                                        -1, 
                                                                        -1
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'I', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'J', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'Y', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'P', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        7, 7, 
                                                                        7
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'F', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                7, 
                                                                                7, 
                                                                                7
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'H', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                7, 
                                                                                7, 
                                                                                7
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'Q', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        5, 5, 
                                                                        5
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'R', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        9, 9, 
                                                                        9
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'Z', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                94, 
                                                                                94, 
                                                                                94
                                                                              }, 
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                94, 
                                                                                94, 
                                                                                94
                                                                              })
                                                                                              
                                                                              // special case
                                                                            }, 
                                                                            {
                                                                              'S', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                94, 
                                                                                94, 
                                                                                94
                                                                              }, 
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                94, 
                                                                                94, 
                                                                                94
                                                                              })
                                                                                              
                                                                              // special case
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'S', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        4, 4, 
                                                                        4
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'Z', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'T', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        2, 
                                                                                        43, 
                                                                                        43
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'C', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'Z', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                            {
                                                                                              'S', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'D', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        2, 
                                                                                        43, 
                                                                                        43
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'D', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                2, 
                                                                                43, 
                                                                                43
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'T', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                2, 
                                                                                43, 
                                                                                43
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'R', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'Z', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                            {
                                                                                              'S', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'C', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                            {
                                                                                              'C', 
                                                                                              new CodesDictionary
                                                                                              {
                                                                                                symbols
                                                                                                  =
                                                                                                  new Dictionary
                                                                                                  <
                                                                                                  char, 
                                                                                                  CodesDictionary
                                                                                                  >
                                                                                                  {
                                                                                                    {
                                                                                                      'H', 
                                                                                                      new CodesDictionary
                                                                                                      (
                                                                                                      new sbyte
                                                                                                        [
                                                                                                        ]
                                                                                                      {
                                                                                                        2, 
                                                                                                        4, 
                                                                                                        4
                                                                                                      })
                                                                                                    }, 
                                                                                                  }
                                                                                              }
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'C', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                2, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'H', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'T', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                43, 
                                                                                                43
                                                                                              })
                                                                                              {
                                                                                                symbols
                                                                                                  =
                                                                                                  new Dictionary
                                                                                                  <
                                                                                                  char, 
                                                                                                  CodesDictionary
                                                                                                  >
                                                                                                  {
                                                                                                    {
                                                                                                      'S', 
                                                                                                      new CodesDictionary
                                                                                                      {
                                                                                                        symbols
                                                                                                          =
                                                                                                          new Dictionary
                                                                                                          <
                                                                                                          char, 
                                                                                                          CodesDictionary
                                                                                                          >
                                                                                                          {
                                                                                                            {
                                                                                                              'C', 
                                                                                                              new CodesDictionary
                                                                                                              {
                                                                                                                symbols
                                                                                                                  =
                                                                                                                  new Dictionary
                                                                                                                  <
                                                                                                                  char, 
                                                                                                                  CodesDictionary
                                                                                                                  >
                                                                                                                  {
                                                                                                                    {
                                                                                                                      'H', 
                                                                                                                      new CodesDictionary
                                                                                                                      (
                                                                                                                      new sbyte
                                                                                                                        [
                                                                                                                        ]
                                                                                                                      {
                                                                                                                        2, 
                                                                                                                        4, 
                                                                                                                        4
                                                                                                                      })
                                                                                                                    }, 
                                                                                                                  }
                                                                                                              }
                                                                                                            }, 
                                                                                                            {
                                                                                                              'H', 
                                                                                                              new CodesDictionary
                                                                                                              (
                                                                                                              new sbyte
                                                                                                                [
                                                                                                                ]
                                                                                                              {
                                                                                                                2, 
                                                                                                                4, 
                                                                                                                4
                                                                                                              })
                                                                                                            }, 
                                                                                                          }
                                                                                                      }
                                                                                                    }, 
                                                                                                    {
                                                                                                      'C', 
                                                                                                      new CodesDictionary
                                                                                                      {
                                                                                                        symbols
                                                                                                          =
                                                                                                          new Dictionary
                                                                                                          <
                                                                                                          char, 
                                                                                                          CodesDictionary
                                                                                                          >
                                                                                                          {
                                                                                                            {
                                                                                                              'H', 
                                                                                                              new CodesDictionary
                                                                                                              (
                                                                                                              new sbyte
                                                                                                                [
                                                                                                                ]
                                                                                                              {
                                                                                                                2, 
                                                                                                                4, 
                                                                                                                4
                                                                                                              })
                                                                                                            }, 
                                                                                                          }
                                                                                                      }
                                                                                                    }, 
                                                                                                  }
                                                                                              }
                                                                                            }, 
                                                                                            {
                                                                                              'D', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                43, 
                                                                                                43
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'H', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'T', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        2, 
                                                                                        43, 
                                                                                        43
                                                                                      })
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'C', 
                                                                                              new CodesDictionary
                                                                                              {
                                                                                                symbols
                                                                                                  =
                                                                                                  new Dictionary
                                                                                                  <
                                                                                                  char, 
                                                                                                  CodesDictionary
                                                                                                  >
                                                                                                  {
                                                                                                    {
                                                                                                      'H', 
                                                                                                      new CodesDictionary
                                                                                                      (
                                                                                                      new sbyte
                                                                                                        [
                                                                                                        ]
                                                                                                      {
                                                                                                        2, 
                                                                                                        4, 
                                                                                                        4
                                                                                                      })
                                                                                                    }, 
                                                                                                  }
                                                                                              }
                                                                                            }, 
                                                                                            {
                                                                                              'S', 
                                                                                              new CodesDictionary
                                                                                              {
                                                                                                symbols
                                                                                                  =
                                                                                                  new Dictionary
                                                                                                  <
                                                                                                  char, 
                                                                                                  CodesDictionary
                                                                                                  >
                                                                                                  {
                                                                                                    {
                                                                                                      'H', 
                                                                                                      new CodesDictionary
                                                                                                      (
                                                                                                      new sbyte
                                                                                                        [
                                                                                                        ]
                                                                                                      {
                                                                                                        2, 
                                                                                                        4, 
                                                                                                        4
                                                                                                      })
                                                                                                    }, 
                                                                                                  }
                                                                                              }
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'C', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'D', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        2, 
                                                                                        43, 
                                                                                        43
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'T', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        3, 3, 
                                                                        3
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'C', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'H', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'Z', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'S', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'H', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'C', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                      })
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                4, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'T', 
                                                                              new CodesDictionary
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'Z', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                4, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                            {
                                                                                              'C', 
                                                                                              new CodesDictionary
                                                                                              {
                                                                                                symbols
                                                                                                  =
                                                                                                  new Dictionary
                                                                                                  <
                                                                                                  char, 
                                                                                                  CodesDictionary
                                                                                                  >
                                                                                                  {
                                                                                                    {
                                                                                                      'H', 
                                                                                                      new CodesDictionary
                                                                                                      (
                                                                                                      new sbyte
                                                                                                        [
                                                                                                        ]
                                                                                                      {
                                                                                                        4, 
                                                                                                        4, 
                                                                                                        4
                                                                                                      })
                                                                                                    }, 
                                                                                                  }
                                                                                              }
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'C', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                4, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'H', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                3, 
                                                                                3, 
                                                                                3
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'R', 
                                                                              new CodesDictionary
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'S', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'U', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        0, 
                                                                        -1, 
                                                                        -1
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'E', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                -1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'I', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'J', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                            {
                                                                              'Y', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                0, 
                                                                                1, 
                                                                                -1
                                                                              })
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                    {
                                                                      'V', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        7, 7, 
                                                                        7
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'W', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        7, 7, 
                                                                        7
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'X', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        5, 
                                                                        54, 
                                                                        54
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'Y', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        1, 
                                                                        -1, 
                                                                        -1
                                                                      })
                                                                    }, 
                                                                    {
                                                                      'Z', 
                                                                      new CodesDictionary
                                                                      (
                                                                      new sbyte
                                                                        []
                                                                      {
                                                                        4, 4, 
                                                                        4
                                                                      })
                                                                      {
                                                                        symbols
                                                                          =
                                                                          new Dictionary
                                                                          <
                                                                          char, 
                                                                          CodesDictionary
                                                                          >
                                                                          {
                                                                            {
                                                                              'D', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                2, 
                                                                                43, 
                                                                                43
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'Z', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        2, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                2, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'H', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                2, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'D', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        2, 
                                                                                        43, 
                                                                                        43
                                                                                      })
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'Z', 
                                                                                              new CodesDictionary
                                                                                              {
                                                                                                symbols
                                                                                                  =
                                                                                                  new Dictionary
                                                                                                  <
                                                                                                  char, 
                                                                                                  CodesDictionary
                                                                                                  >
                                                                                                  {
                                                                                                    {
                                                                                                      'H', 
                                                                                                      new CodesDictionary
                                                                                                      (
                                                                                                      new sbyte
                                                                                                        [
                                                                                                        ]
                                                                                                      {
                                                                                                        2, 
                                                                                                        4, 
                                                                                                        4
                                                                                                      })
                                                                                                    }, 
                                                                                                  }
                                                                                              }
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                            {
                                                                              'S', 
                                                                              new CodesDictionary
                                                                              (
                                                                              new sbyte
                                                                                [
                                                                                ]
                                                                              {
                                                                                4, 
                                                                                4, 
                                                                                4
                                                                              })
                                                                              {
                                                                                symbols
                                                                                  =
                                                                                  new Dictionary
                                                                                  <
                                                                                  char, 
                                                                                  CodesDictionary
                                                                                  >
                                                                                  {
                                                                                    {
                                                                                      'H', 
                                                                                      new CodesDictionary
                                                                                      (
                                                                                      new sbyte
                                                                                        [
                                                                                        ]
                                                                                      {
                                                                                        4, 
                                                                                        4, 
                                                                                        4
                                                                                      })
                                                                                    }, 
                                                                                    {
                                                                                      'C', 
                                                                                      new CodesDictionary
                                                                                      {
                                                                                        symbols
                                                                                          =
                                                                                          new Dictionary
                                                                                          <
                                                                                          char, 
                                                                                          CodesDictionary
                                                                                          >
                                                                                          {
                                                                                            {
                                                                                              'H', 
                                                                                              new CodesDictionary
                                                                                              (
                                                                                              new sbyte
                                                                                                [
                                                                                                ]
                                                                                              {
                                                                                                4, 
                                                                                                4, 
                                                                                                4
                                                                                              })
                                                                                            }, 
                                                                                          }
                                                                                      }
                                                                                    }, 
                                                                                  }
                                                                              }
                                                                            }, 
                                                                          }
                                                                      }
                                                                    }, 
                                                                  }
                                                              };

    /// <summary>
    ///   The regex alt chars.
    /// </summary>
    private static readonly Regex regexAltChars = new Regex(@"[^\w\s]|\d", RegexOptions.Compiled);

    // ���, ��� �� ����� � �� ������

    // �������� �����

    /// <summary>
    ///   The regex multiple spaces.
    /// </summary>
    private static readonly Regex regexMultipleSpaces = new Regex(@"\s{2,}", RegexOptions.Compiled);

    /// <summary>
    ///   The regex short words.
    /// </summary>
    private static readonly Regex regexShortWords = new Regex(@"\b[^\s]{1,3}\b", RegexOptions.Compiled);

    /// <summary>
    ///   ������� ��������������
    /// </summary>
    private static readonly Dictionary<char, string> transliterationTable = new Dictionary<char, string>
                                                                            {
                                                                              { '�', "A" }, 
                                                                              { '�', "a" }, 
                                                                              { '�', "B" }, 
                                                                              { '�', "b" }, 
                                                                              { '�', "V" }, 
                                                                              { '�', "v" }, 
                                                                              { '�', "G" }, 
                                                                              { '�', "g" }, 
                                                                              { '�', "D" }, 
                                                                              { '�', "d" }, 
                                                                              { '�', "E" }, 
                                                                              { '�', "e" }, 
                                                                              { '�', "E" }, 
                                                                              { '�', "e" }, 
                                                                              { '�', "Zh" }, 
                                                                              { '�', "zh" }, 
                                                                              { '�', "Z" }, 
                                                                              { '�', "z" }, 
                                                                              { '�', "I" }, 
                                                                              { '�', "i" }, 
                                                                              { '�', "J" }, 
                                                                              { '�', "j" }, 
                                                                              { '�', "K" }, 
                                                                              { '�', "k" }, 
                                                                              { '�', "L" }, 
                                                                              { '�', "l" }, 
                                                                              { '�', "M" }, 
                                                                              { '�', "m" }, 
                                                                              { '�', "N" }, 
                                                                              { '�', "n" }, 
                                                                              { '�', "O" }, 
                                                                              { '�', "o" }, 
                                                                              { '�', "P" }, 
                                                                              { '�', "p" }, 
                                                                              { '�', "R" }, 
                                                                              { '�', "r" }, 
                                                                              { '�', "S" }, 
                                                                              { '�', "s" }, 
                                                                              { '�', "T" }, 
                                                                              { '�', "t" }, 
                                                                              { '�', "U" }, 
                                                                              { '�', "u" }, 
                                                                              { '�', "F" }, 
                                                                              { '�', "f" }, 
                                                                              { '�', "H" }, 
                                                                              { '�', "h" }, 
                                                                              { '�', "C" }, 
                                                                              { '�', "c" }, 
                                                                              { '�', "Ch" }, 
                                                                              { '�', "ch" }, 
                                                                              { '�', "Sh" }, 
                                                                              { '�', "sh" }, 
                                                                              {
                                                                                '�', "Sch"
                                                                              }, 
                                                                              {
                                                                                '�', "sch"
                                                                              }, 
                                                                              { '�', "\'" }, 
                                                                              { '�', "\'" }, 
                                                                              { '�', "Y" }, 
                                                                              { '�', "y" }, 
                                                                              { '�', "\'" }, 
                                                                              { '�', "\'" }, 
                                                                              { '�', "E" }, 
                                                                              { '�', "e" }, 
                                                                              { '�', "Ju" }, 
                                                                              { '�', "ju" }, 
                                                                              { '�', "Ja" }, 
                                                                              { '�', "ja" }, 
                                                                            };

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// ��������� ������ ��� ��������� ���������
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string GenerateLooseString(string str)
    {
      str = TStringHelper.StringToEmpty(str, true);
      if (!string.IsNullOrEmpty(str))
      {
        // ��������������� ������ � �������� �� � �������� ��������
        bool cyrillic;
        str = Transliterate(str, out cyrillic).ToUpper();

        // �������� � ���������������� ����
        str = regexAltChars.Replace(str, string.Empty); // ������� ���, ��� �� ����� � �� ������
        str = regexShortWords.Replace(str, string.Empty); // ������� �������� �����
        str = regexMultipleSpaces.Replace(str, " ").Trim();

        // ��������� ������������� �������; ������ �������/�������� �������

        // ��������� �� ������ � ������������
        var len = str.Length;
        if (len > 0)
        {
          var result = new StringBuilder();

          // ���� �����
          // !! ����� � ����, ��� ������ �� ����� ���������� ��� ��������� ���������; ����� ��� ������������� ������� ���������
          int i = 0, start;
          do
          {
            // ���� ������ ��� ����� ������
            start = i++;
            while (i < len && str[i] != ' ')
            {
              ++i;
            }

            // ��������� �����������
            if (start > 0)
            {
              result.Append(' ');
            }

            // ������������ �����
            result.Append(WordSoundEx(str.Substring(start, i - start), cyrillic));
          }
          while (++i < len);

          // ������
          return result.ToString();
        }
      }

      // ���������� ��������������� ������ (�� ��������� ��������������)
      return str;
    }

    #endregion

    // !! ������ �� ����� �� ������ ���� �����
    // !! ����������� � ������� � ��������� �����, ����� �� �������� �� ��������
    #region Methods

    /// <summary>
    /// The transliterate.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <param name="cyrillic">
    /// The cyrillic.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string Transliterate(string str, out bool cyrillic)
    {
      var len = str.Length;
      var untouched = 0;

      StringBuilder transliterated = null;
      for (var i = 0; i < len; ++i)
      {
        string translit;
        if (transliterationTable.TryGetValue(str[i], out translit))
        {
          if (transliterated == null)
          {
            transliterated = new StringBuilder();
          }

          var untouchedLength = i - untouched;
          if (untouchedLength > 0)
          {
            transliterated.Append(str.Substring(untouched, untouchedLength));
          }

          transliterated.Append(translit);
          untouched = i + 1;
        }
      }

      if (transliterated != null)
      {
        var untouchedLength = len - untouched;
        if (untouchedLength > 0)
        {
          transliterated.Append(str.Substring(untouched, untouchedLength));
        }

        cyrillic = true;
        return transliterated.ToString();
      }

      cyrillic = false;
      return str;
    }

    // !! ������� �������� ������ �� ������� (��� ��������)
    // !! ������ �� ����� �� ������ ���� �����
    // !! ����� ������ ���� ������ � ������� ��������
    /// <summary>
    /// The word sound ex.
    /// </summary>
    /// <param name="_string">
    /// The _string.
    /// </param>
    /// <param name="_cyrillic">
    /// The _cyrillic.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private static string WordSoundEx(string _string, bool _cyrillic)
    {
      var _output = new StringBuilder();

      int _i = 0, _length = _string.Length, _previous = -1;
      while (_i < _length)
      {
        CodesDictionary _current, _last;
        _current = _last = codesDictionary[_string[_i]];

        int _j, _k;
        for (_j = _k = 1; _k < 7; _k++)
        {
          var _pos = _i + _k;
          if (_pos >= _length)
          {
            break;
          }

          var ch = _string[_pos];
          if (!_current.IsSet(ch))
          {
            break;
          }

          _current = _current[ch];

          if (_current.IsSet(0))
          {
            _last = _current;
            _j = _k + 1;
          }
        }

        int _code;
        var _nxt = _i + _j;
        if (_i == 0)
        {
          _code = _last[0][0];
        }
        else if (_nxt >= _length || codesDictionary[_string[_nxt]][0][0] != 0)
        {
          _code = _cyrillic ? (_last.IsSet(1) ? _last[1][2] : _last[0][2]) : _last[0][2];
        }
        else
        {
          _code = _cyrillic ? (_last.IsSet(1) ? _last[1][1] : _last[0][1]) : _last[0][1];
        }

        if (_code != -1 && _code != _previous)
        {
          _output.Append(_code);
        }

        _previous = _code;

        _i = _nxt;
      }

      if (_output.Length < 6)
      {
        do
        {
          _output.Append('0');
        }
        while (_output.Length < 6);
      }
      else if (_output.Length > 6)
      {
        _output.Length = 6;
      }

      return _output.ToString();
    }

    #endregion

    // ������������� �������

    /// <summary>
    ///   �������� mixed-������� ��� �������
    /// </summary>
    private class CodesDictionary
    {
      #region Fields

      /// <summary>
      ///   The values.
      /// </summary>
      internal readonly sbyte[][] values;

      /// <summary>
      ///   The symbols.
      /// </summary>
      internal Dictionary<char, CodesDictionary> symbols;

      #endregion

      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="CodesDictionary"/> class.
      /// </summary>
      /// <param name="values">
      /// The values.
      /// </param>
      internal CodesDictionary(params sbyte[][] values)
      {
        this.values = values;
      }

      #endregion

      #region Indexers

      /// <summary>
      /// The this.
      /// </summary>
      /// <param name="index">
      /// The index.
      /// </param>
      /// <returns>
      /// The <see cref="sbyte[]"/>.
      /// </returns>
      internal sbyte[] this[int index]
      {
        get
        {
          return values[index];
        }
      }

      /// <summary>
      /// The this.
      /// </summary>
      /// <param name="symbol">
      /// The symbol.
      /// </param>
      /// <returns>
      /// The <see cref="CodesDictionary"/>.
      /// </returns>
      internal CodesDictionary this[char symbol]
      {
        get
        {
          return symbols[symbol];
        }
      }

      #endregion

      #region Methods

      /// <summary>
      /// The is set.
      /// </summary>
      /// <param name="index">
      /// The index.
      /// </param>
      /// <returns>
      /// The <see cref="bool"/>.
      /// </returns>
      internal bool IsSet(int index)
      {
        return values != null && index >= 0 && index < values.Length;
      }

      /// <summary>
      /// The is set.
      /// </summary>
      /// <param name="symbol">
      /// The symbol.
      /// </param>
      /// <returns>
      /// The <see cref="bool"/>.
      /// </returns>
      internal bool IsSet(char symbol)
      {
        return symbols != null && symbols.ContainsKey(symbol);
      }

      #endregion
    }

    /*
         * ���������� � PHP � ������ ��������������
         * -ils-, 2010
         * 
         * (c) provocateur, 2008
         * http://habrahabr.ru/blogs/php/28752/
         * 
         * (c) Randy Daitch, Gary Mokotoff, 1985
         * http://en.wikipedia.org/wiki/Daitch%E2%80%93Mokotoff_Soundex
         * 
         * (c) Robert Russel, Margaret Obell, 1918-1922
         * http://ru.wikipedia.org/wiki/Soundex
         * 
         */

    // ------------------------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------------------------
  }
}