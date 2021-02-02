namespace CodeZero.Data
{
    public class CodeZeroDbConnectionOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public CodeZeroDbConnectionOptions()
        {
            ConnectionStrings = new ConnectionStrings();
        }
    }
}