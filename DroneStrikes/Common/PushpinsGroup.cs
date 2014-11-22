using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Microsoft.Phone.Maps.Toolkit;

namespace DroneStrikes.Common
{
    public class PushpinsGroup
    {
        private List<Pushpin> pushpins = new List<Pushpin>();
        public Point MapLocation { get; set; }

        public PushpinsGroup(Pushpin pushpin, Point location)
        {
            pushpins.Add(pushpin);
            MapLocation = location;
        }

        public FrameworkElement GetElement(DataTemplate clusterTemplate)
        {
            if (pushpins.Count == 1)
                return pushpins[0];

            // more pushpins
            return new Pushpin()
            {
                // just need the first coordinate
                GeoCoordinate = pushpins.First().GeoCoordinate,
                Content = pushpins.Select(p => p.DataContext).ToList(),
                ContentTemplate = clusterTemplate,
            };
        }

        public void IncludeGroup(PushpinsGroup group)
        {
            foreach (var pin in group.pushpins)
                pushpins.Add(pin);
        }
    }
}
