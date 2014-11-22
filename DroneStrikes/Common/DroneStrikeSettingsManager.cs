using FavoriteArtist.Common;

namespace DroneStrikes.Common
{
    public class DroneStrikeSettingsManager
    {
        private readonly SettingsHelper settingsHelper;

        public DroneStrikeSettingsManager(SettingsHelper settingsHelper)
        {
            this.settingsHelper = settingsHelper;
        }

        //private const string NumberOfTopArtistsKey = "NumberOfTopArtists";

        //public int NumberOfTopArtists
        //{
        //    get
        //    {
        //        return this.settingsHelper.GetSetting(NumberOfTopArtistsKey, 10);
        //    }
        //    set
        //    {
        //        int valueToSet = value;
        //        if (valueToSet > 50)
        //        {
        //            valueToSet = 50;
        //        }
        //        this.settingsHelper.UpdateSetting(NumberOfTopArtistsKey, valueToSet);
        //    }
        //}

    }
}