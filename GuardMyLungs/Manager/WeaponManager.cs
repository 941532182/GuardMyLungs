using Data;

namespace Manager
{
    public static class WeaponManager
    {

        public static string GetFireSE(long weaponId) => Database.Select<Weapon>(weaponId).FireSE;
        public static int GetDamage(long weaponId) => Database.Select<Weapon>(weaponId).Damage;
        public static float GetAttackSpeed(long weaponId) => (float) Database.Select<Weapon>(weaponId).AttackSpeed;
        public static int GetRange(long weaponId) => Database.Select<Weapon>(weaponId).Range;
        public static float GetSpeedMultiplier(long weaponId) => (float) Database.Select<Weapon>(weaponId).SpeedMultiplier;
        public static float GetBuffTime(long weaponId) => (float) Database.Select<Weapon>(weaponId).BuffTime;
        public static bool IsFire(long weaponId) => Database.Select<Weapon>(weaponId).IsFire;
        public static long GetProjectileId(long weaponId) => Database.Select<Weapon>(weaponId).ProjectileId;
        public static bool KillSelf(long weaponId) => Database.Select<Weapon>(weaponId).KillSelf;

    }
}