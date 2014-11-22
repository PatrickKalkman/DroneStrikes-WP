using System;
using System.Windows.Input;

using Microsoft.Phone.Tasks;

namespace DroneStrikes.Commands
{
    public class RateThisAppCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var reviewTask = new MarketplaceReviewTask();
            reviewTask.Show();
        }
    }
}
