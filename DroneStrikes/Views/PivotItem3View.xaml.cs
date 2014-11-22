using Microsoft.Phone.Controls;

using Telerik.Windows.Controls;

namespace DroneStrikes.Views
{
    public partial class PivotItem3View : PhoneApplicationPage
    {
        public PivotItem3View()
        {
            InitializeComponent();
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));
        }
    }
}