using UnityEngine;

namespace Controller
{
    public class MakeLensShake : MonoBehaviour
    {

        [SerializeField]
        private float xShake;
        [SerializeField]
        private float yShake;
        [SerializeField]
        private float shakeTime;

        private void Start()
        {
            var shake = Camera.main.gameObject.GetComponent<LensShake>();
            if (shake == null)
            {
                shake = Camera.main.gameObject.AddComponent<LensShake>();
            }
            shake.XShake = xShake;
            shake.YShake = yShake;
            shake.ShakeTime = shakeTime;
            shake.Restart();
            Destroy(this);
        }

    }
}
