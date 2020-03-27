using UnityEngine;

namespace Controller
{
    [DisallowMultipleComponent]
    public class PanelEnable : MonoBehaviour
    {

        private void Start()
        {
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            if (transform.localScale.x < 0.99)
            {
                transform.localScale += 10 * Time.unscaledDeltaTime * Vector3.one;
            } else
            {
                transform.localScale = Vector3.one;
                Destroy(this);
            }
        }
    }

}
