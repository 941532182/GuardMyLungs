using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    public class VirusExplodeController : ExplodeController
    {

        private VirusController vc;
        private long virusId = -1;

        public int Damage { get; set; }
        public long VirusId
        {
            get => virusId;
            set
            {
                if (virusId == -1)
                {
                    virusId = value;
                    Damage = VirusManager.GetDamage(virusId);
                }
            }
        }

        private void Start()
        {
            vc = GameObject.Find("Unit/Virus").GetComponent<VirusController>();
        }

        protected override void Explode(Collider2D collision)
        {
            collision.GetComponent<LungsHPController>().TakeDamage(Damage);
            vc.DestroyVirus(GetComponent<Virus>());
        }

    }
}
