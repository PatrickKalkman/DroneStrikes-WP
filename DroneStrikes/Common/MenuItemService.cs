using DroneStrikes.Model;

namespace DroneStrikes.Common
{
    public class MenuItemService
    {
        public MenuItemCollection GetAll()
        {
            var fixedMenu = new MenuItemCollection();

            var menuItem1 = new MenuItem();
            menuItem1.Title = "News";
            menuItem1.Icon = "../Assets/Menu/news.png";

            fixedMenu.Add(menuItem1);

            var menuItem3 = new MenuItem();
            menuItem3.Title = "Help";
            menuItem3.Icon = "../Assets/Menu/help.png";

            fixedMenu.Add(menuItem3);

            var menuItem4 = new MenuItem();
            menuItem4.Title = "About";
            menuItem4.Icon = "../Assets/Menu/about.png";

            fixedMenu.Add(menuItem4);

            var menuItem5 = new MenuItem();
            menuItem5.Title = "Settings";
            menuItem5.Icon = "../Assets/Menu/settings.png";

            fixedMenu.Add(menuItem5);

            var menuItem2 = new MenuItem();
            menuItem2.Title = "Privacy";
            menuItem2.Icon = "../Assets/Menu/privacy.png";

            fixedMenu.Add(menuItem2);

            return fixedMenu;
        }
    }

}
