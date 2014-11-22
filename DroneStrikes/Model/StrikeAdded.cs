namespace DroneStrikes.Model
{
    public partial class Strike
    {
        public string Year 
        {
            get { return date.Substring(0, 4); }
        }

        public string DateClean
        {
            get { return date.Substring(0, 10); }
        }
    }
}
