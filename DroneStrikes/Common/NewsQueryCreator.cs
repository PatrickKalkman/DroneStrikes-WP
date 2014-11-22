using System.Text;

namespace DroneStrikes.Common
{
    public class NewsQueryCreator
    {
        public string CreateDroneNewsQuery()
        {
            var query = new StringBuilder();

            query.Append("Drones");

            return query.ToString();
        }
    }
}
