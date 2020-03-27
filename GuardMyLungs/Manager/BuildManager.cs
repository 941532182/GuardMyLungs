using System;

namespace Manager
{
    public static class BuildManager
    {

        public static BuildState State { get; private set; }
        public static event Action StateChanged;

        public static void Reset()
        {
            State = BuildState.None;
        }

        public static void SelectCell(long cellId)
        {
            if (State == BuildState.None)
            {
                State = BuildState.Building;
                BuildingCellId = cellId;
                StateChanged?.Invoke();
            } else if (State == BuildState.Building)
            {
                State = BuildState.None;
                StateChanged?.Invoke();
            }
        }

        public static void DeselectCell()
        {
            State = BuildState.None;
            StateChanged?.Invoke();
        }

        public static long BuildingCellId { get; set; }

    }

    public enum BuildState
    {
        None,
        Building


    }
}