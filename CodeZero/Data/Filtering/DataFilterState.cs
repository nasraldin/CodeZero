namespace CodeZero.Data.Filtering
{
    public class DataFilterState
    {
        public bool IsEnabled { get; set; }

        public DataFilterState(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public DataFilterState Clone()
        {
            return new DataFilterState(IsEnabled);
        }
    }
}