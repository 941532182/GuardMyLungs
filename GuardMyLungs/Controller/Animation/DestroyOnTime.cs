using UnityEngine;

namespace Controller
{
    public class DestroyOnTime : MonoBehaviour
    {

        [SerializeField]
        private float lifeTime;
        [SerializeField]
        private bool useFrameTime;

        void Update()
        {
            lifeTime -= useFrameTime ? Time.timeScale : Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
