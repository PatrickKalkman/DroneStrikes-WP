using System.Windows;

using Microsoft.Phone.Maps.Controls;

namespace DroneStrikes.Common
{
    public static class MapHelper
    {
        public static bool IsVisiblePoint(this Map map, Point point)
        {
            return point.X > 0 && point.X < map.ActualWidth && point.Y > 0 && point.Y < map.ActualHeight;
        }
    }
}
