using UnityEngine;

namespace Controller
{
    [DisallowMultipleComponent]
    public class PanelDisable : MonoBehaviour
    {

        private void Awake()
        {
            transform.localScale = Vector3.one;
        }

        private void Update()
        {
            if (transform.localScale.x > 0.01)
            {
                transform.localScale -= 10 * Time.unscaledDeltaTime * Vector3.one;
            } else
            {
                transform.localScale = Vector3.zero;
                gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }

}
