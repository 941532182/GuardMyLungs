namespace Data
{
    public class Virus : PersistantObject
    {

        public string Name { get; private set; } = "无名病毒";
        public string Desc { get; private set; } = "这个病毒很懒，什么也没有留下";
        public string Prefab { get; private set; }
        public int MaxHP { get; private set; }
        public int MoveSpeed { get; private set; }
        public int Damage { get; private set; }
        public int HPRegen { get; private set; }
        public int AminoAcid { get; private set; }
        public bool CanFly { get; private set; }
        public string ExplodeEffect { get; private set; }
        public bool IsBoss { get; private set; }

    }
}