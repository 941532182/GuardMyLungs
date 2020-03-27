namespace Data
{
    public class Level : PersistantObject
    {

        public string Prefab { get; private set; }
        public int Order { get; private set; }
        public long NextLevelId { get; private set; }
        public int AminoAcidAmount { get; private set; }
        public string BGM { get; private set; }
        public string WinSE { get; private set; }
        public long UnlockCellId { get; private set; } = -1;
        public int Wave { get; private set; }
        public Troop[][] Troops { get; private set; }

        [Autowired(nameof(NextLevelId))]
        public Level NextLevel { get; private set; }

        public struct Troop
        {
            public long id;
            public int count;
            public int way;
        }

    }
}
