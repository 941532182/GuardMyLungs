namespace Data
{
    public class Weapon : PersistantObject
    {

        public string FireSE { get; private set; }
        public int Damage { get; private set; }
        public double AttackSpeed { get; private set; }
        public int Range { get; private set; }
        public double SpeedMultiplier { get; private set; } = 1;
        public double BuffTime { get; private set; }
        public bool IsFire { get; private set; }
        public long ProjectileId { get; private set; }
        public bool KillSelf { get; private set; }

        [Autowired(nameof(ProjectileId))]
        public Projectile Projectile { get; private set; }

    }
}
