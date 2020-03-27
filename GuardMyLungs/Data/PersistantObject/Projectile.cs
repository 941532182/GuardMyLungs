namespace Data
{
    public class Projectile : PersistantObject
    {

        public string Prefab { get; private set; }
        public ProjectileBehaviour Behaviour { get; private set; }
        public bool IsAOE { get; private set; }
        public int Radius { get; private set; }
        public int Attenuation { get; private set; } = 100;
        public string ExplodeEffect { get; private set; }
        public int MoveSpeed { get; private set; }
        public bool IsPenestrative { get; private set; }
        public bool ExplodeAtSelf { get; private set; }

    }

    public enum ProjectileBehaviour
    {
        Missile,
        Laser,
        Faker,
    }
}
