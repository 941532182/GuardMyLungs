using UnityEngine;

namespace Controller
{
    public static class EffectFactory
    {

        public static Transform CreateEffect(string prefab, Vector3 position)
        {
            var effect = Object.Instantiate(Resources.Load<Transform>($"Prefabs/Effect/{prefab}"));
            effect.position = position;
            return effect;
        }

        public static Transform CreateLaser(string prefab, Vector3 src, Vector3 dst)
        {
            var laser = Object.Instantiate(Resources.Load<Transform>($"Prefabs/Effect/{prefab}"));
            var line = laser.GetComponent<LineRenderer>();
            line.SetPositions(new Vector3[] { src, dst });
            return laser;
        }

    }
}