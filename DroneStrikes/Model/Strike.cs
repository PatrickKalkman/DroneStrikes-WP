using System.Collections.Generic;

namespace DroneStrikes.Model
{
    public partial class Strike
    {
        public string _id { get; set; }
        public int number { get; set; }
        public string country { get; set; }
        public string date { get; set; }
        public string town { get; set; }
        public string location { get; set; }
        public string deaths { get; set; }
        public string deaths_min { get; set; }
        public string deaths_max { get; set; }
        public string civilians { get; set; }
        public string injuries { get; set; }
        public string children { get; set; }
        public string tweet_id { get; set; }
        public string bureau_id { get; set; }
        public string bij_summary_short { get; set; }
        public string bij_link { get; set; }
        public string target { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public List<object> articles { get; set; }
        public List<string> names { get; set; }
    }
}
