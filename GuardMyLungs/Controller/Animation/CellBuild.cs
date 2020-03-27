using UnityEngine;

[RequireComponent(typeof(SpriteRenderer)), DisallowMultipleComponent]
public class CellBuild : MonoBehaviour
{

    private SpriteRenderer sr;
    [SerializeField]
    private float lifeTime = 1;
    [SerializeField, Range(1, 10)]
    private float power = 2;
    [SerializeField]
    private bool isUnscaled;
    private float t;
    private Color originColor;
    private Material originMate;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        originMate = sr.material;
        sr.material = Instantiate(Resources.Load<Material>("Material/CellBuildMaterial"));
    }

    private void Update()
    {
        if (t >= lifeTime)
        {
            Destroy(this);
        }
        var weight = Mathf.Pow(t / lifeTime, power);
        sr.color = Color.Lerp(Color.white, originColor, weight);
        sr.material.SetFloat("_Weight", weight);
        t += isUnscaled ? Time.unscaledDeltaTime : Time.deltaTime;
    }

    private void OnDestroy()
    {
        sr.material = originMate;
    }

    public void Refresh()
    {
        t = 0;
    }

}
