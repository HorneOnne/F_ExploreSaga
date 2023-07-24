using UnityEngine.SceneManagement;

namespace ExploreSaga
{
    public static class Loader
    {
        public enum Scene
        {
            MainMenu,
            Level_01,
            Level_02,
            Level_03,
            Level_04,
            Level_05,
            Level_06,
            Level_07,
            Level_08,
            Level_09,
            Level_10,
        }

        private static Scene targetScene;

        public static void Load(Scene targetScene, System.Action afterLoadScene = null)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Loader.targetScene.ToString());
        } 
    }
}
