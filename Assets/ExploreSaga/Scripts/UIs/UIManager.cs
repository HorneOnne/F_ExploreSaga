using UnityEngine;

namespace ExploreSaga
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu uiMainMenu;
        public UISettings uiSettings;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplaySettingsMenu(false);
        }

        public void DisplayMainMenu(bool isActive)
        {
            uiMainMenu.DisplayCanvas(isActive);
        }

        public void DisplaySettingsMenu(bool isActive)
        {
            uiSettings.DisplayCanvas(isActive);
        }

 
    }
}
