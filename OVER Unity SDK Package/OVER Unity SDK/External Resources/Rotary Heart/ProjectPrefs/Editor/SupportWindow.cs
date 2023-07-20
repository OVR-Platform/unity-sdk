using UnityEditor;

namespace RotaryHeart.Lib.ProjectPreferences
{
    public class SupportWindow : BaseSupportWindow
    {
        const string SUPPORT_FORUM = "https://forum.unity.com/threads/released-project-prefs-preferences-saved-on-projectsettings.522208/";
        const string STORE_LINK = "https://assetstore.unity.com/packages/tools/utilities/project-prefs-112798";
        const string ASSET_NAME = "Project Preferences";
        const string VERSION = "1.2.1";

        protected override string SupportForum
        {
            get { return SUPPORT_FORUM; }
        }
        protected override string StoreLink
        {
            get { return STORE_LINK; }
        }
        protected override string AssetName
        {
            get { return ASSET_NAME; }
        }
        protected override string Version
        {
            get { return VERSION; }
        }
        
        [MenuItem("Tools/Rotary Heart/Project Preferences/About")]
        public static void ShowWindow()
        {
            ShowWindow<SupportWindow>();
        }
    }
}