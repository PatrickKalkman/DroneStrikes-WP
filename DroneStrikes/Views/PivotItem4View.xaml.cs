using Microsoft.Phone.Controls;

using Telerik.Windows.Controls;

namespace DroneStrikes.Views
{
    public partial class PivotItem4View : PhoneApplicationPage
    {
        public PivotItem4View()
        {
            InitializeComponent();
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));
        }
    }
}