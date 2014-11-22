using Microsoft.Phone.Controls;

using Telerik.Windows.Controls;

namespace DroneStrikes.Views
{
    public partial class PivotItem2View : PhoneApplicationPage
    {
        public PivotItem2View()
        {
            InitializeComponent();
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));
        }
    }
}