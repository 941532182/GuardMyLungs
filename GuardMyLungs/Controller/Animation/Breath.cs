using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Breath : MonoBehaviour
{

    [SerializeField]
    private float intensity = 0.1f;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private bool isUnscaled;
    [SerializeField]
    private bool xBreath = false;
    [SerializeField]
    private bool yBreath = true;
    private float t;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        var scale = originalScale;
        scale.y *= 1 + intensity * Mathf.Sin(speed * t) * (yBreath ? 1 : 0);
        scale.x *= 1 + intensity * Mathf.Sin(speed * t) * (xBreath ? 1 : 0);
        transform.localScale = scale;
        t += isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime;
    }

    private void OnEnable()
    {
        t = 0;
    }

    private void OnDisable()
    {
        transform.localScale = originalScale;
    }

}
