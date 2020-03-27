using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Controller
{
    [DisallowMultipleComponent, RequireComponent(typeof(HPController))]
    public class LongHPBarController : MonoBehaviour
    {

        private HPController hpc;
        private Transform barCanvas;
        private Transform bar;
        private Image content;

        private void Start()
        {
            hpc = GetComponent<HPController>();
            hpc.HPChanged += OnHPChanged;
            barCanvas = GameObject.Find("Canvas/Bar").transform;
            bar = Instantiate(Resources.Load<Transform>("Prefabs/UI/LongHPBar"));
            bar.SetParent(barCanvas);
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
            bar.localPosition = canvas_pos + new Vector3(0, 125, 0);
        }

        private void OnHPChanged(int hp)
        {
            content.fillAmount = (float) hp / hpc.MaxHP;
            content.color = Color.Lerp(new Color(2, 0, 0), new Color(0, 2, 0), content.fillAmount);
        }

    }
}
