using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Controller
{
    [RequireComponent(typeof(HPController))]
    public class HPBarController : MonoBehaviour
    {

        private HPController hpc;
        private Transform barCanvas;
        private Transform bar;
        private Image bound;
        private Image content;

        private void Start()
        {
            hpc = GetComponent<HPController>();
            hpc.HPChanged += OnHPChanged;
            barCanvas = GameObject.Find("Canvas/Bar").transform;
            bar = Instantiate(Resources.Load<Transform>("Prefabs/UI/HPBar"));
            bar.SetParent(barCanvas);
            bound = bar.GetChild("Bound").GetComponent<Image>();
            content = bar.GetChild("Content").GetComponent<Image>();
            UpdateBar();
        }

        private void Update()
        {
            UpdateBar();
        }

        private void OnDestroy()
        {
            hpc.HPChanged -= OnHPChanged;
            if (bar != null)
            {
                Destroy(bar.gameObject);
            }
        }

        private void OnEnable()
        {
            if (bar != null)
            {
                bar.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            if (bar != null)
            {
                bar.gameObject.SetActive(false);
            }
        }

        private void UpdateBar()
        {
            var screen_pos = Camera.main.WorldToScreenPoint(transform.position);
            var canvas_pos = new Vector3(screen_pos.x / Screen.width * 1920 - 960, screen_pos.y / Screen.height * 1080 - 540);
            bar.localScale = Vector3.one;
            bar.localPosition = canvas_pos + new Vector3(0, 50, 0);
            var c = bound.color;
            c.a -= 0.01f;
            bound.color = c;
            c = content.color;
            c.a -= 0.01f;
            content.color = c;
        }

        private void OnHPChanged(int hp)
        {
            content.fillAmount = (float) hp / hpc.MaxHP;
            var c = bound.color;
            c.a = 1;
            bound.color = c;
            content.color = Color.Lerp(new Color(2, 0, 0), new Color(0, 2, 0), content.fillAmount);
        }

    }
}
