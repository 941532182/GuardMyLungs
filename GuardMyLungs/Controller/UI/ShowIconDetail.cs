using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class ShowIconDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        private Transform canvas;
        private Transform detail;

        public string Name { get; set; }
        public string Desc { get; set; }
        public int Cost { get; set; } = -1;

        private void Start()
        {
            canvas = GameObject.Find("Canvas").transform;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            detail = Instantiate(Resources.Load<Transform>("Prefabs/UI/IconDetail"));
            detail.SetParent(canvas);
            detail.localScale = Vector3.one;
            detail.position = transform.position;
            detail.localPosition += new Vector3(350, -200);
            var cost = LevelManager.AminoAcidAmount >= Cost ? $"价格：{Cost}" : $"<color=red>价格：{Cost}</color>";
            var text = $"{Name}\n{Desc}\n{(Cost < 0 ? "" : cost)}";
            detail.GetChild(0).GetComponent<Text>().text = text;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Destroy(detail.gameObject);
        }

    }
}
