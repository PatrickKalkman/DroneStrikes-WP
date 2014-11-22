using System;
using System.Windows.Input;

using Microsoft.Phone.Tasks;

namespace DroneStrikes.Commands
{
    public class SendAnEmailCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var emailTask = new EmailComposeTask();
            emailTask.To = "info@company.com";
            emailTask.Show();
        }
    }
}
