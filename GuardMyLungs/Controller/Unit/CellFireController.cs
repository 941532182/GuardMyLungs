using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class CellFireController : MonoBehaviour, IFireable
    {

        private CellController cc;
        private VirusController vc;
        private ProjectileController pc;
        private long weaponId = -1;
        private float cd;

        public Transform Target { get; set; }
        public string FireSE { get; private set; }
        public int Damage { get; private set; }
        public float AttackSpeed { get; private set; }
        public int Range { get; private set; }
        public float SpeedMultiplier { get; private set; }
        public float BuffTime { get; private set; }
        public bool IsFire { get; private set; }
        public long ProjectileId { get; private set; }
        public bool KillSelf { get; private set; }
        public long WeaponId
        {
            get => weaponId;
            set
            {
                weaponId = value;
                FireSE = WeaponManager.GetFireSE(weaponId);
                Damage = WeaponManager.GetDamage(weaponId);
                AttackSpeed = WeaponManager.GetAttackSpeed(weaponId);
                Range = WeaponManager.GetRange(weaponId);
                SpeedMultiplier = WeaponManager.GetSpeedMultiplier(weaponId);
                BuffTime = WeaponManager.GetBuffTime(weaponId);
                IsFire = WeaponManager.IsFire(weaponId);
                ProjectileId = WeaponManager.GetProjectileId(weaponId);
                KillSelf = WeaponManager.KillSelf(weaponId);
            }
        }

        private void Start()
        {
            cc = GameObject.Find("Unit/Cell").GetComponent<CellController>();
            vc = GameObject.Find("Unit/Virus").GetComponent<VirusController>();
            pc = GameObject.Find("Unit/Projectile").GetComponent<ProjectileController>();
        }

        private void Update()
        {
            if (Target != null && (Target.position - transform.position).sqrMagnitude > Range * Range / 2500f)
            {
                Target = null;
            }
            if (cd <= 0)
            {
                if (Target == null)
                {
                    Search();
                }
                if (Target != null)
                {
                    Fire();
                }
            } else
            {
                cd -= Time.deltaTime;
            }
        }

        private void Search()
        {
            foreach (var virus in vc.Viruses)
            {
                if ((virus.transform.position - transform.position).sqrMagnitude <= Range * Range / 2500f)
                {
                    Target = virus.transform;
                    return;
                }
            }
        }

        public void Fire()
        {
            if (FireSE != null)
            {
                AudioController.PlaySE(FireSE);
            }
            pc.CreateProjectile(ProjectileId, transform.position, Target, Damage, SpeedMultiplier, BuffTime, IsFire);
            if (KillSelf)
            {
                cc.DestroyCell(GetComponent<Cell>());
            } else
            {
                cd = 1 / AttackSpeed;
            }
        }

    }
}
