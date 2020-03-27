using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public static class VirusManager
    {

        public static string GetName(long virusId) => Database.Select<Virus>(virusId).Name;
        public static string GetDesc(long virusId) => Database.Select<Virus>(virusId).Desc;
        public static string GetPrefab(long virusId)
        {
            var virus = Database.Select<Virus>(virusId);
            return virus.Prefab == null ? virus.Id.ToString() : virus.Prefab;
        }
        public static int GetMaxHP(long virusId) => Database.Select<Virus>(virusId).MaxHP;
        public static int GetMoveSpeed(long virusId) => Database.Select<Virus>(virusId).MoveSpeed;
        public static int GetDamage(long virusId) => Database.Select<Virus>(virusId).Damage;
        public static int GetHPRegen(long virusId) => Database.Select<Virus>(virusId).HPRegen;
        public static int GetAminoAcid(long virusId) => Database.Select<Virus>(virusId).AminoAcid;
        public static bool CanFly(long virusId) => Database.Select<Virus>(virusId).CanFly;
        public static string GetExplodeEffect(long virusId) => Database.Select<Virus>(virusId).ExplodeEffect;
        public static bool IsBoss(long virusId) => Database.Select<Virus>(virusId).IsBoss;

    }
}