using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public static class PlayerManager
    {

        private static bool isInitialized;

        public static void Initialize()
        {
            if (isInitialized)
            {
                Debug.LogError("禁止重复调用初始化方法!");
                return;
            }
            DatabaseDriver.Setup();
            Player.Load();
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
            isInitialized = true;
        }

        public static void NewLevel()
        {
            var unlock_cell_id = Database.Select<Level>(LevelManager.LevelId).UnlockCellId;
            if (unlock_cell_id != -1)
            {
                Player.Cells.Add(unlock_cell_id);
            }
            Player.CurrentLevel++;
            Player.Save();
        }

#if DEVELOPER
        public static int CurrentLevel => 15;
        public static long[] Cells => new long[]
        {
            1000000,
            1000001,
            1000002,
            1000003,
            1000004,
            1000005,
            1000006,
            1000007,
            1000008,
            1000009
        };
#else
        public static int CurrentLevel => Player.CurrentLevel;
        public static long[] Cells => Player.Cells.ToArray();
#endif

    }
}