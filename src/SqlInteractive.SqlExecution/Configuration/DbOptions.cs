using System.Collections;
using System.Data.Common;

namespace SqlInteractive.SqlExecution.Configuration
{
	public record DbOptions
	{
		public string ConnectionString { get; set; }

		public DbConnectionStringBuilder ParseConnectionString() => new DbConnectionStringBuilder
		{
			ConnectionString = ConnectionString
		};
	}
}
