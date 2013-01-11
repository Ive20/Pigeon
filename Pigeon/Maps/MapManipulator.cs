using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Services;    // for RouteQuery class

using System.Threading;

namespace Pigeon.Maps
{
    class MapManipulator
    {
        Map _map;


        public MapManipulator(Map map)
        {
            _map = map;
        }

        /// <summary>
        /// Jump to the specified location with animation. 
        /// </summary>
        /// <param name="coordinate"> Your desired location. </param>
        /// <param name="scale"> Your desired zooming scale when the navigation animation finishes. </param>
        /// <param name="animationKind"> The kind of animation for navigation. Find more information in MapAnimationKind </param>
        public void JumpTo(GeoCoordinate coordinate, int scale = 20, MapAnimationKind animationKind = MapAnimationKind.Parabolic)
        {
            _map.SetView(coordinate, scale, animationKind);
        }

        /// <summary>
        /// Move to the specified location without animation.  
        /// </summary>
        /// <param name="coordinate"> Your desired location. </param>
        public void MoveTo(GeoCoordinate coordinate, int scale)
        {
            _map.Center = coordinate;
            _map.ZoomLevel = scale;
        }


        List<GeoCoordinate> _coordinates = new List<GeoCoordinate>();

        /// <summary>
        /// Mark the user's current location. 
        /// </summary>
        /// <param name="brush"> The brush to fill the mark. </param>
        public async void PinpointCurrentUserLocation(Brush brush)
        {
            Geolocator locator = new Geolocator();
            Geoposition position = await locator.GetGeopositionAsync();
            Geocoordinate oldStyleCoordinate = position.Coordinate;
            GeoCoordinate coordinate = CoordinateConverter.ConvertGeocoordinate(oldStyleCoordinate);
            string temp = coordinate.ToString();
            JumpTo(coordinate);
            Pinpoint(coordinate, brush);
        }

        /// <summary>
        /// Mark the specified location
        /// </summary>
        /// <param name="coordinate"> Your desired location. </param>
        /// <param name="brush"> The brush to fill the mark. </param>
        public void Pinpoint(GeoCoordinate coordinate, Brush brush)
        {
            // Create a circle
            Ellipse circle = new Ellipse();
            circle.Fill = brush;
            circle.Height = 20;
            circle.Width = 20;
            circle.Opacity = 50;

            // Create a MapOverlay that contains the circle
            MapOverlay locationOverlay = new MapOverlay();
            locationOverlay.Content = circle;
            locationOverlay.PositionOrigin = new Point(0.5, 0.5);
            locationOverlay.GeoCoordinate = coordinate;

            // Create a MapLayer to contain the MapOverlay
            MapLayer locationLayer = new MapLayer();
            locationLayer.Add(locationOverlay);

            // Add the Overlay to the map
            _map.Layers.Add(locationLayer);
        }


        public void PutText(string text, GeoCoordinate coordinate)
        {
            // Create a TextBlock
            TextBlock textBlock = new TextBlock();
            textBlock.Width = 150;
            textBlock.Height = 60;
            textBlock.Opacity = 50;
            textBlock.Text = text;
            textBlock.FontSize = 40;

            textBlock.Foreground = new SolidColorBrush(Colors.White);

            Rectangle rect = new Rectangle();
            rect.Width = 150;
            rect.Height = 60;
            rect.Opacity = 50;
            rect.Fill = new SolidColorBrush(Colors.Black);

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Resources/head.png", UriKind.Relative));
            img.Stretch = Stretch.Uniform;
            img.Height = 60;

            MapOverlay textOverlay = new MapOverlay();
            textOverlay.Content = textBlock;
            textOverlay.PositionOrigin = new Point(0.3, 0.5);
            textOverlay.GeoCoordinate = coordinate;

            MapOverlay textBgOverlay = new MapOverlay();
            textBgOverlay.Content = rect;
            textBgOverlay.PositionOrigin = new Point(0.0, 0.5);
            textBgOverlay.GeoCoordinate = coordinate;

            MapOverlay imgOverlay = new MapOverlay();
            imgOverlay.Content = img;
            imgOverlay.PositionOrigin = new Point(0.9, 0.5);
            imgOverlay.GeoCoordinate = coordinate;

            MapLayer layer = new MapLayer();
            layer.Add(textBgOverlay);
            layer.Add(textOverlay);
            layer.Add(imgOverlay);

            _map.Layers.Add(layer);
        }
    }
}