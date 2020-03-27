using UnityEngine;
using Manager;

namespace Controller
{
    public class VirusHPController : HPController
    {

        private VirusController vc;
        private long virusId = -1;
        private float t;

        public int HPRegen { get; private set; }
        public string ExplodeEffect { get; private set; }
        public long VirusId
        {
            get => virusId;
            set
            {
                if (virusId == -1)
                {
                    virusId = value;
                    MaxHP = VirusManager.GetMaxHP(virusId);
                    hp = MaxHP;
                    HPRegen = VirusManager.GetHPRegen(virusId);
                    ExplodeEffect = VirusManager.GetExplodeEffect(virusId);
                }
            }
        }

        private void Start()
        {
            Die += OnDie;
            vc = GameObject.Find("Unit/Virus").GetComponent<VirusController>();
        }

        private void Update()
        {
            if (t < 1f)
            {
                t += Time.deltaTime;
            } else
            {
                TakeDamage(-HPRegen);
                t = 0;
            }
        }

        private void OnDie()
        {
            if (ExplodeEffect != null)
            {
                EffectFactory.CreateEffect(ExplodeEffect, transform.position);
            }
            vc.DestroyVirus(GetComponent<Virus>());
        }

    }
}
