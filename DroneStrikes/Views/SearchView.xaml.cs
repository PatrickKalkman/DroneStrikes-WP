using Microsoft.Phone.Controls;

using Telerik.Windows.Controls;

namespace DroneStrikes.Views
{
    public partial class SearchView : PhoneApplicationPage
    {
        public SearchView()
        {
            InitializeComponent();
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));
        }
    }
}