using UnityEngine.Events;

namespace Events
{
    public class MainMenuEvents
    {
        public static UnityAction SettingsBTN;
        public static UnityAction SettingsExitBTN;
        public static UnityAction NewGameBTN;
        public static UnityAction<float> SoundValueChanged;
        public static UnityAction<float> MusicValueChanged;
    }
}