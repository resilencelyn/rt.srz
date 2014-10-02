using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.Threading;
using Microsoft.Data.ConnectionUI;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace rt.srz.setup.CA
{
	public class CustomActions
	{
		private const string INITIAL_CATALOG = "Initial Catalog";
		private const string SQL_VERSION_QUERY = "SELECT SERVERPROPERTY('productversion')";
		private const string SQL_SET_SINGLE_USER = "IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
		private const string SQL_DROP_DATABASE = "IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') DROP DATABASE [{0}]";
    private const string SQL_CREATE_DATABASE = "CREATE DATABASE [{0}] ";
    private const string SQL_RESTORE = "RESTORE DATABASE [{0}] FROM  DISK = N'{1}' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10";

		[CustomAction]
		public static ActionResult BrowseSqlConnection(Session session)
		{
			try
			{
				var connectionString = "";
				var title = string.Empty;
				switch (session["CURRENT_CONNECTION"])
				{
					case "1":
						connectionString = session["RTSRZ_CONNECTION_STRING"];
						title = session["RTSRZ_TTITLE"];
						break;
					case "2":
						connectionString = session["RTQUARTZ_CONNECTION_STRING"];
						title = session["RTQUARTZ_TTITLE"];
						break;
					case "3":
						connectionString = session["ATLANTIC_CONNECTION_STRING"];
						title = session["ATLANTIC_TTITLE"];
						break;
					default:
						throw new NotSupportedException();
				}
				var thread = new Thread(() =>
				{
					DataConnectionDialog dialog = new DataConnectionDialog()
					{
						ShowInTaskbar = false,
						TopMost = true,
						Title = title,
					};
					DataSource sqlDataSource = new DataSource("MicrosoftSqlServer", "Microsoft SQL Server");
					sqlDataSource.Providers.Add(DataProvider.SqlDataProvider);
					dialog.DataSources.Add(sqlDataSource);
					if (!string.IsNullOrEmpty(connectionString))
						dialog.ConnectionString = connectionString;
					connectionString = null;
					if (DataConnectionDialog.Show(dialog) == DialogResult.OK)
						connectionString = dialog.ConnectionString;
				});
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				thread.Join();
				if (connectionString != null)
					switch (session["CURRENT_CONNECTION"])
					{
						case "1":
							session["RTSRZ_CONNECTION_STRING"] = connectionString;
							break;
						case "2":
							session["RTQUARTZ_CONNECTION_STRING"] = connectionString;
							break;
						case "3":
							session["ATLANTIC_CONNECTION_STRING"] = connectionString;
							break;
						default:
							throw new NotSupportedException();
					}
			}
			catch (Exception ex)
			{
				session["LOG_MESSAGE"] = ex.ToString();
				return ActionResult.Failure;
			}
			return ActionResult.Success;
		}

		[CustomAction]
		public static ActionResult VerifySqlConnection(Session session)
		{
			try
			{
				if (CheckCatalog(session, session["RTSRZ_TTITLE"], session["RTSRZ_CONNECTION_STRING"]))
					if (CheckCatalog(session, session["RTQUARTZ_TTITLE"], session["RTQUARTZ_CONNECTION_STRING"]))
						if (CheckCatalog(session, session["ATLANTIC_TTITLE"], session["ATLANTIC_CONNECTION_STRING"]))
							if (VerifySqlConnection(session, session["RTSRZ_TTITLE"], session["RTSRZ_CONNECTION_STRING"]))
								if (VerifySqlConnection(session, session["RTQUARTZ_TTITLE"], session["RTQUARTZ_CONNECTION_STRING"]))
									if (VerifySqlConnection(session, session["ATLANTIC_TTITLE"], session["ATLANTIC_CONNECTION_STRING"]))
									{
										//
									}
			}
			catch (Exception ex)
			{
				session["LOG_MESSAGE"] = ex.ToString();
				return ActionResult.Failure;
			}
			return ActionResult.Success;
		}
		[CustomAction]
		public static ActionResult VerifySqlConnection2(Session session)
		{
			try
			{
				if (!string.IsNullOrEmpty(session["RTSRZ_DEPLOY"]))
					if (CheckCatalog(session, session["RTSRZ_TTITLE"], session["RTSRZ_CONNECTION_STRING"]))
						if (CheckLocal(session, session["RTSRZ_TTITLE"], session["RTSRZ_CONNECTION_STRING"]))
							if (VerifySqlConnection(session, session["RTSRZ_TTITLE"], TrimCatalog(session["RTSRZ_CONNECTION_STRING"])))
							{
								//
							}
				if (!string.IsNullOrEmpty(session["RTQUARTZ_DEPLOY"]))
					if (CheckCatalog(session, session["RTQUARTZ_TTITLE"], session["RTQUARTZ_CONNECTION_STRING"]))
						if (CheckLocal(session, session["RTQUARTZ_TTITLE"], session["RTQUARTZ_CONNECTION_STRING"]))
							if (VerifySqlConnection(session, session["RTQUARTZ_TTITLE"], TrimCatalog(session["RTQUARTZ_CONNECTION_STRING"])))
							{
								//
							}
			}
			catch (Exception ex)
			{
				session["LOG_MESSAGE"] = ex.ToString();
				return ActionResult.Failure;
			}
			return ActionResult.Success;
		}

	  /// <summary>
	  /// The restore database.
	  /// </summary>
	  /// <param name="session">
	  /// The session.
	  /// </param>
	  /// <returns>
	  /// The <see cref="ActionResult"/>.
	  /// </returns>
	  [CustomAction]
		public static ActionResult RestoreDatabase(Session session)
		{
			session.Log("Begin RestoreDatabase Custom Action");
			try
			{
				var data = session.CustomActionData.ToString().Split('*');
				var connectionString = data[0];
				var backupPath = data[1];

				var database = GetCatalog(connectionString);
				var folder = Path.GetDirectoryName(backupPath);
				SetFolderPermission(folder, true);
				using (var connection = new SqlConnection(TrimCatalog(connectionString)))
				{
					SqlCommand command;
					connection.Open();
					
          // Переводим базу в режим "SINLE_USER"
          using (command = connection.CreateCommand())
					{
						command.CommandText = string.Format(SQL_SET_SINGLE_USER, database);
						command.ExecuteNonQuery();
					}
					
          // Удаляем базу, если уже существует такая
          using (command = connection.CreateCommand())
					{
						command.CommandText = string.Format(SQL_DROP_DATABASE, database);
						command.ExecuteNonQuery();
					}
					
          // Создаем пустую БД, чтобы сохранить пути к базе и логу по умолчанию
          using (command = connection.CreateCommand())
          {
            command.CommandText = string.Format(SQL_CREATE_DATABASE, database);
            command.ExecuteNonQuery();
          }

				  // Восстанавливаем Backup
				  using (command = connection.CreateCommand())
					{
						command.CommandText = string.Format(SQL_RESTORE, database, backupPath);
						command.ExecuteNonQuery();
					}

					connection.Close();
				}
				
        // Удаляем файл Backup
        File.Delete(backupPath);
				SetFolderPermission(folder, false);
			}
			catch (Exception ex)
			{
				session.Log("ERROR in custom action RestoreDatabase:  {0}", ex.ToString());
				return ActionResult.Failure;
			}
			session.Log("End RestoreDatabase Custom Action");
			return ActionResult.Success;
		}

		private static bool VerifySqlConnection(Session session, string dbTitle, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				try
				{
					connection.Open();
				}
				catch (Exception ex)
				{
					session["LOG_MESSAGE"] = ex.ToString();
					session["DIALOG_ERROR_MESSAGE"] = string.Format("Не удается подключиться к {0}\n{1}", dbTitle, ex.Message);
					return false;
				}
				var command = connection.CreateCommand();
				command.CommandText = SQL_VERSION_QUERY;
				var version = command.ExecuteScalar().ToString();
				connection.Close();
				session["LOG_MESSAGE"] = string.Format("Версия SQL сервера: {0}", version);
				if (!version.StartsWith("10.") && !version.StartsWith("11."))
					session["DIALOG_ERROR_MESSAGE"] = string.Format("Установка возможна только с использованием SQL Server 2008 (R2) или SQL Server 2012. Версия {0} не соответствует.", dbTitle);
			}
			return true;
		}
		
    private static bool CheckCatalog(Session session, string dbTitle, string connectionString)
		{
			var result = false;
			if (!string.IsNullOrEmpty(connectionString))
			{
				var builder = new SqlConnectionStringBuilder(connectionString);
				result = builder.ContainsKey(INITIAL_CATALOG) && !string.IsNullOrEmpty(builder.InitialCatalog);
				if (!result)
					session["DIALOG_ERROR_MESSAGE"] = string.Format("В строке подключения к {0} отсутствует имя БД", dbTitle);
			}
			else
				session["DIALOG_ERROR_MESSAGE"] = string.Format("Не указана строка подлкючения к {0}", dbTitle);
			return result;
		}
		
    private static bool CheckLocal(Session session, string dbTitle, string connectionString)
		{
			return true;
		}
		
    private static string TrimCatalog(string connectionString)
		{
			var builder = new SqlConnectionStringBuilder(connectionString);
			builder.Remove(INITIAL_CATALOG);
			return builder.ToString();
		}
		
    private static string GetCatalog(string connectionString)
		{
			var builder = new SqlConnectionStringBuilder(connectionString);
			return builder.InitialCatalog;
		}
		
    private static void SetFolderPermission(string path, bool allowWrite)
		{
			var dirSecurity = Directory.GetAccessControl(path);
			var everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			var rule = new FileSystemAccessRule(everyone, FileSystemRights.Write, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
			if (allowWrite)
				dirSecurity.AddAccessRule(rule);
			else
				dirSecurity.RemoveAccessRule(rule);
			Directory.SetAccessControl(path, dirSecurity);
		}
	}
}
