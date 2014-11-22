using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DroneStrikes.Common
{
    public class BackgroundImageBrush
    {
        public ImageBrush GetBackground()
        {
            ImageBrush imageBrush = null;

            if ((Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] == Visibility.Visible)
            { 
                imageBrush = new ImageBrush
                                 {
                                     ImageSource = new BitmapImage(new Uri("/Assets/Background.jpg", UriKind.Relative))
                                 };
            }
            else
            {
                imageBrush = new ImageBrush
                                 {
                                     ImageSource = new BitmapImage(new Uri("/Assets/Background.jpg", UriKind.Relative))
                                 };
            }

            return imageBrush;
        }
    }
}