using System;
using UnityEngine;
using Manager;

namespace Controller
{
    [RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
    public class MissileMoveController : ProjectileMoveController
    {

        private long projectileId = -1;

        public int MoveSpeed;
        public override long ProjectileId
        {
            get => projectileId;
            set
            {
                if (projectileId == -1)
                {
                    projectileId = value;
                    MoveSpeed = ProjectileManager.GetMoveSpeed(projectileId);
                }
            }
        }

        private void Start()
        {
            var sub = Target.position - transform.position;
            var dir = sub.normalized;
            transform.up = dir;
        }

        private void Update()
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }
            var sub = Target.position - transform.position;
            var dir = sub.normalized;
            transform.up = dir;
            var dx = MoveSpeed / 50f * Time.deltaTime * dir;
            if (sub.sqrMagnitude > dx.sqrMagnitude)
            {
                transform.position += dx;
            } else
            {
                transform.position = Target.position;
            }
        }

    }
}
