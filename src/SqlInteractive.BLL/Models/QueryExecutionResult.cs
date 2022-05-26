namespace SqlInteractive.BLL.Models
{
	public class QueryExecutionResult
	{
		public ICollection<Table>? Tables { get; set; }
		public string? Exception { get; set; }
	}
}
