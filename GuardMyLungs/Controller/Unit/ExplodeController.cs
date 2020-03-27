using UnityEngine;

namespace Controller
{
    public abstract class ExplodeController : MonoBehaviour
    {
        public int TargetLayer { get; set; }
        private bool isExploded;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == TargetLayer && !isExploded)
            {
                Explode(collision);
                isExploded = true;
            }
        }
        protected abstract void Explode(Collider2D collision);
    }
}
