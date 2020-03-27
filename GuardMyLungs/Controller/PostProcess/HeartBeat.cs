using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Controller
{
    [DisallowMultipleComponent]
    public class HeartBeat : MonoBehaviour
    {

        private ChromaticAberration ca;
        private Bloom bl;
        private float t;

        private void Start()
        {
            var volume = GetComponentInChildren<Volume>();
            volume.profile.TryGet(out ca);
            volume.profile.TryGet(out bl);
        }

        private void Update()
        {
            ca.intensity.value = Mathf.Sin(Mathf.PI * t) * Mathf.Sin(Mathf.PI * t);
            bl.intensity.value = 3 + Mathf.Sin(Mathf.PI * t) * Mathf.Sin(Mathf.PI * t);
            bl.tint.value = new Color(1 + Mathf.Sin(Mathf.PI * t) * Mathf.Sin(Mathf.PI * t), 0.5f, 0.5f);
            t += Time.unscaledDeltaTime;
        }

        private void OnEnable()
        {
            t = 0;
            AudioController.PlayBGS("beat_slowly");
        }

        private void OnDisable()
        {
            ca.intensity.value = 0;
            bl.intensity.value = 3;
            bl.tint.value = new Color(1, 0.5f, 0.5f);
            AudioController.BGS.Stop();
        }

    }
}

