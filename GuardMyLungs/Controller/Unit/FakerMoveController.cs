using System;
using UnityEngine;
using Manager;

namespace Controller
{
    [RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
    public class FakerMoveController : ProjectileMoveController
    {

        private long projectileId = -1;

        public bool ExplodeAtSelf;
        public override long ProjectileId
        {
            get => projectileId;
            set
            {
                if (projectileId == -1)
                {
                    projectileId = value;
                    ExplodeAtSelf = ProjectileManager.ExplodeAtSelf(projectileId);
                }
            }
        }

        private void Update()
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }
            if (!ExplodeAtSelf)
            {
                transform.position = Target.position;
            }
        }

    }
}
