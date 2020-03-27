using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    [DisallowMultipleComponent, RequireComponent(typeof(LineRenderer))]
    public class LaserEffect : MonoBehaviour
    {

        [SerializeField]
        private LineRenderer lr;
        private Material mate;
        private float t;

        void Start()
        {
            mate = lr.material;
            var c = mate.GetColor("_Emission");
            c.a = 0;
            mate.SetColor("_Emission", c);
        }

        void Update()
        {
            var c = mate.GetColor("_Emission");
            c.a = 64 * t * (0.25f - t);
            mate.SetColor("_Emission", c);
            t += Time.deltaTime;
            if (t > 0.25f)
            {
                Destroy(gameObject);
            }
        }
    }
}

