using UnityEngine;

namespace Controller
{
    [DisallowMultipleComponent]
    public class LungsController : MonoBehaviour
    {

        public Vector3 LungsPos { get; private set; }

        public Lungs CreateLungs(Vector3 position)
        {
            var lungs = Instantiate(Resources.Load<Transform>($"Prefabs/Unit/Lungs"));
            lungs.position = position;
            lungs.SetParent(transform);

            LungsPos = lungs.position;

            return lungs.gameObject.GetComponent<Lungs>();
        }

    }
}
