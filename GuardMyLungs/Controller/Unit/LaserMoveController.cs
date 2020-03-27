using System;
using UnityEngine;
using Manager;

namespace Controller
{
    [RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
    public class LaserMoveController : ProjectileMoveController
    {

        private long projectileId = -1;

        public bool IsPenestrative;
        public override long ProjectileId
        {
            get => projectileId;
            set
            {
                if (projectileId == -1)
                {
                    projectileId = value;
                    IsPenestrative = ProjectileManager.IsPenestrative(projectileId);
                }
            }
        }

        private void Start()
        {
            if (Target == null)
            {
                Destroy(gameObject);
            }
            var src = transform.position;
            var dir = (Target.position - transform.position).normalized;
            var dist = IsPenestrative ? 100 : ((Target.position - transform.position).magnitude + 5);
            var layer = 1 << LayerMask.NameToLayer("Virus");
            var hits = Physics2D.BoxCastAll(src, new Vector2(0.2f, 0.2f), 0, dir, dist, layer);
            var pec = GetComponent<ProjectileExplodeController>();
            var damage = pec.Damage;
            var speedMultiplier = pec.SpeedMultiplier;
            var buffTime = pec.BuffTime;
            var isFire = pec.IsFire;
            foreach (var hit in hits)
            {
                if (!isFire && speedMultiplier < 1)
                {
                    hit.collider.GetComponent<VirusSpeedAdjuster>().SetSpeedMultiplier(speedMultiplier, buffTime);
                } else if (isFire)
                {
                    hit.collider.GetComponent<VirusSpeedAdjuster>().Recover();
                }
                hit.collider.GetComponent<VirusHPController>().TakeDamage(damage);
            }
            EffectFactory.CreateLaser("laser", src, src + dir * dist);
            Destroy(gameObject);
        }

    }
}
