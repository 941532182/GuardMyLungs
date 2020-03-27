using System;
using System.Collections.Generic;
using Data;

namespace Manager
{
    public static class LevelManager
    {

        private static Level levelPO;
        private static Level nextLevelPO;
        private static int aminoAcidAmount;
        private static int currentWave;
        private static List<long> chosenCells;
        public static float[] AminoAcidMultiplier => new float[]
        {
            5,
            3,
            2,
            1,
            0.8f,
            0.6f,
            0.5f,
            0.3f,
            0.2f,
            0.1f,
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
        };

        public static LevelState LevelState { get; set; }

        public static long LevelId => levelPO.Id;
        public static string Prefab => levelPO.Prefab == null ? levelPO.Id.ToString() : levelPO.Prefab;
        public static int Order => levelPO.Order;
        public static string BGM => levelPO.BGM;
        public static string WinSE => levelPO.WinSE;
        public static long NextLevelId => levelPO.NextLevelId;
        public static int TotalWave => levelPO.Wave;
        public static Queue<Queue<(long, int, int)>> Troops
        {
            get
            {
                var troops = levelPO.Troops;
                var result = new Queue<Queue<(long, int, int)>>();
                for (int i = 0; i < troops.Length; i++)
                {
                    var wave = new Queue<(long, int, int)>();
                    result.Enqueue(wave);
                    for (int j = 0; j < troops[i].Length; j++)
                    {
                        wave.Enqueue((troops[i][j].id, troops[i][j].count, troops[i][j].way));
                    }
                }
                return result;
            }
        }

        public static event Action<int> AminoAcidAmountChanged;
        public static event Action<int> CurrentWaveChanged;

        public static void SetLevel(long levelId)
        {
            levelPO = Database.Select<Level>(levelId);
            nextLevelPO = levelPO.NextLevel;
            AminoAcidAmount = levelPO.AminoAcidAmount;
            CurrentWave = 0;
            chosenCells = new List<long>();
            LevelState = LevelState.Init;
        }

        public static void NextLevel()
        {
            levelPO = nextLevelPO;
            nextLevelPO = levelPO.NextLevel;
            AminoAcidAmount = levelPO.AminoAcidAmount;
            CurrentWave = 0;
            chosenCells = new List<long>();
            LevelState = LevelState.Init;
        }

        public static bool Choose(long cellId)
        {
            if (chosenCells.Contains(cellId))
            {
                chosenCells.Remove(cellId);
                return true;
            } else if (chosenCells.Count >= 5)
            {
                return false;
            } else
            {
                chosenCells.Add(cellId);
                ChosenCells.Sort();
                return true;
            }
        }

        public static bool IsFinalLevel => levelPO == nextLevelPO;

        public static List<long> ChosenCells => chosenCells;

        public static long[] Viruses
        {
            get
            {
                var types = new List<long>();
                foreach (var wave in levelPO.Troops)
                {
                    foreach (var troop in wave)
                    {
                        if (!types.Contains(troop.id))
                        {
                            types.Add(troop.id);
                        }
                    }
                }
                types.Sort();
                return types.ToArray();
            }
        }

        public static int AminoAcidAmount
        {
            get => aminoAcidAmount;
            set
            {
                aminoAcidAmount = value;
                AminoAcidAmountChanged?.Invoke(aminoAcidAmount);
            }
        }

        public static int CurrentWave
        {
            get => currentWave;
            set
            {
                currentWave = value;
                CurrentWaveChanged?.Invoke(currentWave);
            }
        }

    }

    public enum LevelState
    {
        Init,
        Run
    }
}
