namespace CodeZero.Configuration;

public partial class DefaultApiVersion
{
    public int Major { get; set; } = 1;
    public int Minor { get; set; } = 0;
    public string Status { get; set; } = default!;
}