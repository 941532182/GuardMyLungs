using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Controller
{
    [DisallowMultipleComponent]
    public class LensOut : MonoBehaviour
    {

        private ColorAdjustments ca;
        private Action post;
        public Color Color = Color.white;

        void Start()
        {
            GetComponentInChildren<Volume>().profile.TryGet(out ca);
            ca.postExposure.value = 0;
            ca.colorFilter.value = Color;
        }

        void Update()
        {
            if (ca.postExposure.value < 9.99)
            {
                ca.postExposure.value += 10 * Time.unscaledDeltaTime;
            } else
            {
                ca.postExposure.value = 10;
                post?.Invoke();
                Destroy(this);
            }
        }

        public Action Post
        {
            set
            {
                if (post == null)
                {
                    post = value;
                }
            }
        }
    }
}
