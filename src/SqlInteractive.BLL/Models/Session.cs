namespace SqlInteractive.BLL.Models;

public record Session(Guid Id)
{
	public IList<string>? TableNames { get; set; }
}
