using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using Caliburn.Micro;

using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;

namespace DroneStrikes.Common
{
    public class ClustersGenerator : MapChildControl
    {
        const double MAXDISTANCE = 40;

        private Map _map;
        private List<Pushpin> _pushpins;
        private readonly IEventAggregator eventAggregator;
        public DataTemplate ClusterTemplate { get; set; }

        public ClustersGenerator(Map map, List<Pushpin> pushpins, DataTemplate clusterTemplate, IEventAggregator eventAggregator)
        {
            _map = map;
            _pushpins = pushpins;
            this.eventAggregator = eventAggregator;
            this.ClusterTemplate = clusterTemplate;

            // maps event
            _map.ResolveCompleted += (s, e) => GeneratePushpins();
            //_map.ViewChanged += (s, e) => GeneratePushpins();
            _map.ZoomLevelChanged += (s, e) => GeneratePushpins();
            //_map.CenterChanged += (s, e) => GeneratePushpins();

            // first generate
            GeneratePushpins();
        }

        private void GeneratePushpins()
        {
            try
            {

                eventAggregator.Publish(new StartPinGroupingMessage());
                var pushpinsToAdd = new List<PushpinsGroup>();
                foreach (var pushpin in _pushpins)
                {
                    bool addGroup = true;
                    var newGroup = new PushpinsGroup(pushpin, _map.ConvertGeoCoordinateToViewportPoint(pushpin.GeoCoordinate));

                    foreach (var pushpinToAdd in pushpinsToAdd)
                    {
                        double distance = pushpinToAdd.MapLocation.GetDistanceTo(newGroup.MapLocation);

                        if (distance < MAXDISTANCE)
                        {
                            pushpinToAdd.IncludeGroup(newGroup);
                            addGroup = false;
                            break;
                        }
                    }

                    if (addGroup)
                        pushpinsToAdd.Add(newGroup);
                }

                _map.Dispatcher.BeginInvoke(() =>
                {
                    _map.Layers.Clear();
                    MapLayer layer = new MapLayer();
                    foreach (var visibleGroup in pushpinsToAdd.Where(p => _map.IsVisiblePoint(p.MapLocation)))
                    {
                        var cluster = visibleGroup.GetElement(this.ClusterTemplate) as Pushpin;
                        if (cluster != null)
                        {
                            layer.Add(new MapOverlay() { GeoCoordinate = cluster.GeoCoordinate, Content = cluster.Content, ContentTemplate = cluster.ContentTemplate });

                        }
                    }
                    if (layer.Count > 0)
                        _map.Layers.Add(layer);
                    eventAggregator.Publish(new StopPinGroupingMessage());
                });
            }
            catch (Exception error)
            {
                throw;
            }

        }
    }
}
