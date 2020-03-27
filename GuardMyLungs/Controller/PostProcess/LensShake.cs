using UnityEngine;

namespace Controller
{
    [DisallowMultipleComponent]
	public class LensShake : MonoBehaviour
	{

        public float XShake { get; set; }
        public float YShake { get; set; }
        public float ShakeTime { get; set; }
        private float t = 0.01f;
        private Vector3 originPosition;

        private void Start()
        {
            originPosition = transform.position;
        }

        private void Update()
        {
            float x = XShake * Mathf.Sin(10 * Mathf.PI * t) / t / 10;
            float y = YShake * Mathf.Sin(6 * Mathf.PI * t) / t / 10;
            if (t > ShakeTime)
            {
                transform.position = originPosition;
                Destroy(this);
                return;
            }
            transform.position = new Vector3(x, y, originPosition.z);
            t += Time.deltaTime;
        }

        public void Restart()
        {
            t = 0.01f;
        }

    }
}
