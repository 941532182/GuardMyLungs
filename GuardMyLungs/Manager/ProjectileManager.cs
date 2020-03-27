using Data;

namespace Manager
{
    public static class ProjectileManager
    {

        /// <summary>
        /// 抛射体的预制体
        /// </summary>
        public static string GetPrefab(long projectileId)
        {
            var proj = Database.Select<Projectile>(projectileId);
            return proj.Prefab == null ? proj.Id.ToString() : proj.Prefab;
        }
        /// <summary>
        /// 抛射体的行为
        /// </summary>
        public static int GetBehaviour(long projectileId) => (int) Database.Select<Projectile>(projectileId).Behaviour;
        /// <summary>
        /// 是否造成范围效果
        /// </summary>
        public static bool IsAOE(long projectileId) => Database.Select<Projectile>(projectileId).IsAOE;
        /// <summary>
        /// 作用半径，仅当IsAOE=true时生效，单位: 像素
        /// </summary>
        public static int GetRadius(long projectileId) => Database.Select<Projectile>(projectileId).Radius;
        /// <summary>
        /// 衰减率，边缘和中心的伤害比，仅当IsAOE=true时生效
        /// </summary>
        public static int GetAttenuation(long projectileId) => Database.Select<Projectile>(projectileId).Attenuation;
        public static string GetExplodeEffect(long projectileId) => Database.Select<Projectile>(projectileId).ExplodeEffect;
        /// <summary>
        /// 移动速度，单位: 像素/秒，只对Missile有影响
        /// </summary>
        public static int GetMoveSpeed(long projectileId) => Database.Select<Projectile>(projectileId).MoveSpeed;
        /// <summary>
        /// 是否会穿透目标(攻击目标后面的单位)，只对Laser有影响
        /// </summary>
        public static bool IsPenestrative(long projectileId) => Database.Select<Projectile>(projectileId).IsPenestrative;
        /// <summary>
        /// 是否在自身的坐标点爆炸，只对Faker有影响
        /// </summary>
        public static bool ExplodeAtSelf(long projectileId) => Database.Select<Projectile>(projectileId).ExplodeAtSelf;

    }
}
