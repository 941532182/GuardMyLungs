namespace Data
{
    public class Cell : PersistantObject
    {

        public string Name { get; private set; } = "无名细胞";
        public string Desc { get; private set; } = "这个细胞很懒，什么也没有留下";
        public string Prefab { get; private set; }
        public int Cost { get; private set; }
        public int EnhanceCost1 { get; private set; }
        public int EnhanceCost2 { get; private set; }
        public long RookieWeaponId { get; private set; }
        public long VeteranWeaponId { get; private set; }
        public long EliteWeaponId { get; private set; }
        public bool IsBarrier { get; private set; }
        public bool IsEnhanceable { get; private set; } = true;

        [Autowired(nameof(RookieWeaponId))]
        public Weapon RookieWeapon { get; private set; }
        [Autowired(nameof(VeteranWeaponId))]
        public Weapon VeteranWeapon { get; private set; }
        [Autowired(nameof(EliteWeaponId))]
        public Weapon EliteWeapon { get; private set; }

    }
}
