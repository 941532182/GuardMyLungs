using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    [DisallowMultipleComponent]
    public class Jump : MonoBehaviour
    {

        [SerializeField]
        private float amplitude = 0.5f;
        [SerializeField]
        private float speed = 5;
        private float lastDy;
        private float t;

        public bool IsPause { get; set; }

        private void Update()
        {
            float dy = 0;
            if (!IsPause)
            {
                dy = amplitude * Mathf.Sin(speed * t) * Mathf.Sin(speed * t);
            }
            var pos = transform.position;
            pos.y -= lastDy;
            pos.y += dy;
            transform.position = pos;
            lastDy = dy;
            t += Time.deltaTime;
        }

    }
}
