using System;
using UnityEngine;

namespace Controller
{
    public abstract class ProjectileMoveController : MonoBehaviour
    {
        public virtual long ProjectileId { get; set; }
        public virtual Transform Target { get; set; }
    }
}
