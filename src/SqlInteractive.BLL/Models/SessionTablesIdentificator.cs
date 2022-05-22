using System;

namespace SqlInteractive.BLL.Models
{
	/// <summary>
	/// Идентифицирует таблицы сессии
	/// </summary>
	public class SessionTablesIdentificator
	{
		public Session Session { get; private set; }

		public SessionTablesIdentificator(Session session)
		{
			if (session is null)
				throw new ArgumentNullException(nameof(session));

			this.Session = session;
		}

		/// <summary>
		/// Имена таблиц в формате Id$TableName
		/// </summary>
		public IEnumerable<string> IdentifiedTableNames => Session.TableNames.Select(t => $"{Session.Id}{ID_TABLE_SEPARATOR}{t}");

		/// <summary>
		/// Разделитель идентификатора сессии и имени таблицы
		/// </summary>
		public const char ID_TABLE_SEPARATOR = '$';
	}
}
