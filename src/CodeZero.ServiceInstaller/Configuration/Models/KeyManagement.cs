namespace CodeZero.Configuration.Models;

public partial class KeyManagement
{
    public int NewKeyLifetime { get; set; }
    public bool AutoGenerateKeys { get; set; }
}