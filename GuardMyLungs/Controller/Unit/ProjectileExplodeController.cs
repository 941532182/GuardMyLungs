using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    public class ProjectileExplodeController : ExplodeController
    {

        private VirusController vc;
        private long projectileId = -1;

        public int Damage { get; set; }
        public float SpeedMultiplier { get; set; }
        public float BuffTime { get; set; }
        public bool IsFire { get; set; }
        public bool IsAOE { get; private set; }
        public int Radius { get; private set; }
        public int Attenuation { get; private set; }
        public string ExplodeEffect { get; private set; }
        public bool ExplodeAtSelf { get; private set; }
        public long ProjectileId
        {
            get => projectileId;
            set
            {
                if (projectileId == -1)
                {
                    projectileId = value;
                    IsAOE = ProjectileManager.IsAOE(projectileId);
                    Radius = ProjectileManager.GetRadius(projectileId);
                    Attenuation = ProjectileManager.GetAttenuation(projectileId);
                    ExplodeEffect = ProjectileManager.GetExplodeEffect(projectileId);
                    ExplodeAtSelf = ProjectileManager.ExplodeAtSelf(projectileId);
                }
            }
        }

        private void Start()
        {
            vc = GameObject.Find("Unit/Virus").GetComponentInChildren<VirusController>();
        }

        private void Update()
        {
            if (ExplodeAtSelf)
            {
                Explode(null);
            }
        }

        protected override void Explode(Collider2D collision)
        {
            if (ExplodeEffect != null)
            {
                EffectFactory.CreateEffect(ExplodeEffect, transform.position);
            }
            if (IsAOE)
            {
                List<Virus> targets = new List<Virus>();
                foreach (var virus in vc.Viruses)
                {
                    targets.Add(virus);
                }
                foreach (var virus in targets)
                {
                    var dist = (virus.transform.position - transform.position).magnitude;
                    if (dist <= Radius * 0.02f)
                    {
                        if (!IsFire && SpeedMultiplier < 1)
                        {
                            virus.GetComponent<VirusSpeedAdjuster>().SetSpeedMultiplier(SpeedMultiplier, BuffTime);
                        } else if (IsFire)
                        {
                            virus.GetComponent<VirusSpeedAdjuster>().Recover();
                        }
                        virus.GetComponent<VirusHPController>().TakeDamage((int) (Damage * Mathf.Lerp(1, Attenuation * 0.01f, dist * 50f / Radius)));
                    }
                }
            } else if (collision != null)
            {
                if (!IsFire && SpeedMultiplier < 1)
                {
                    collision.GetComponent<VirusSpeedAdjuster>().SetSpeedMultiplier(SpeedMultiplier, BuffTime);
                } else if (IsFire)
                {
                    collision.GetComponent<VirusSpeedAdjuster>().Recover();
                }
                collision.GetComponent<VirusHPController>().TakeDamage(Damage);
            }
            Destroy(gameObject);
        }

    }
}
