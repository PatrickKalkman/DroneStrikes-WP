using System.Collections.Generic;

namespace DroneStrikes.Model
{
    public class RootObject
    {
        public string status { get; set; }
        public List<Strike> strike { get; set; }
    }
}