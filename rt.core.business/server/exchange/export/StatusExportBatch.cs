//-----------------------------------------------------------------------
// <copyright file="StatusExportBatch.cs" company="Rintech" author="Syurov">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
	/// <summary>
	/// Статус экспортера пакетов
	/// </summary>
	public enum StatusExportBatch
	{
		/// <summary>
		/// Не инициализированый
		/// </summary>
		NotInit = 0,

		/// <summary>
		/// Открыт
		/// </summary>
		Opened = 1,

		/// <summary>
		/// Подтвержден
		/// </summary>
		Commited
	}
}
