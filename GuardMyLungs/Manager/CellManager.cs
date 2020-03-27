using Data;

namespace Manager
{
    public static class CellManager
    {

        public static string GetName(long cellId) => Database.Select<Cell>(cellId).Name;
        public static string GetDesc(long cellId) => Database.Select<Cell>(cellId).Desc;
        public static string GetPrefab(long cellId)
        {
            var cell = Database.Select<Cell>(cellId);
            return cell.Prefab == null ? cell.Id.ToString() : cell.Prefab;
        }
        public static int GetCost(long cellId) => Database.Select<Cell>(cellId).Cost;
        public static int GetEnhanceCost1(long cellId) => Database.Select<Cell>(cellId).EnhanceCost1;
        public static int GetEnhanceCost2(long cellId) => Database.Select<Cell>(cellId).EnhanceCost2;
        public static long GetRookieWeaponId(long cellId) => Database.Select<Cell>(cellId).RookieWeapon.Id;
        public static long GetVeteranWeaponId(long cellId) => Database.Select<Cell>(cellId).VeteranWeapon.Id;
        public static long GetEliteWeaponId(long cellId) => Database.Select<Cell>(cellId).EliteWeapon.Id;
        public static bool IsBarrier(long cellId) => Database.Select<Cell>(cellId).IsBarrier;
        public static bool IsEnhanceable(long cellId) => Database.Select<Cell>(cellId).IsEnhanceable;

    }
}