using System;
using UnityEngine;

namespace Controller
{
    public abstract class HPController : MonoBehaviour
    {
        protected int hp;

        public virtual int MaxHP { get; protected set; }
        public virtual event Action<int> HPChanged;
        public virtual event Action Die;
        public virtual void TakeDamage(int damage)
        {
            hp = Mathf.Clamp(hp - damage, 0, MaxHP);
            if (hp > 0)
            {
                HPChanged?.Invoke(hp);
            } else
            {
                Die?.Invoke();
            }
        }
    }
}
