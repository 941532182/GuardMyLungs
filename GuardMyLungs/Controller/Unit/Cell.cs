using UnityEngine;
using UnityEngine.EventSystems;
using Manager;
using System.Collections.Generic;

namespace Controller
{
    [DisallowMultipleComponent]
    public class Cell : MonoBehaviour, IPointerClickHandler
    {

        private Transform canvas;
        private CellController cc;
        public static UnitPanelController UnitPanel { get; set; }

        public long CellId { get; set; }
        public int CurrentLevel { get; private set; } = 1;

        private void Start()
        {
            canvas = GameObject.Find("Canvas").transform;
            cc = GameObject.Find("Unit/Cell").GetComponent<CellController>();
        }

        private void Update()
        {
            if (UnitPanel == null) return;
            if (Input.GetMouseButtonDown(0))
            {
                var eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, raycastResults);
                var click_on_panel = false;
                foreach (var item in raycastResults)
                {
                    if (item.gameObject.GetComponent<UnitPanelController>() != null)
                    {
                        click_on_panel = true;
                        break;
                    }
                }
                if (!click_on_panel)
                {
                    UnitPanel.DestroyThis();
                    return;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                UnitPanel.DestroyThis();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerId != -1)
            {
                return;
            }
            if (UnitPanel != null)
            {
                if (UnitPanel.GetComponent<UnitPanelController>().Cell == this)
                {
                    return;
                }
                UnitPanel.DestroyThis();
            }
            var screen_pos = Camera.main.WorldToScreenPoint(transform.position);
            var canvas_pos = new Vector3(screen_pos.x / Screen.width * 1920 - 960, screen_pos.y / Screen.height * 1080 - 540);
            var unit_panel = Instantiate(Resources.Load<Transform>("Prefabs/UI/UnitPanel"));
            unit_panel.SetParent(canvas);
            unit_panel.localScale = Vector3.one;
            unit_panel.localPosition = canvas_pos + new Vector3(0, 125);

            UnitPanel = unit_panel.gameObject.GetComponent<UnitPanelController>();
            UnitPanel.Cell = this;

            UnitPanel.gameObject.AddComponent<PanelEnable>();
        }

        public void Enhance()
        {
            var cost = CurrentLevel == 1 ? CellManager.GetEnhanceCost1(CellId) : CellManager.GetEnhanceCost2(CellId);
            LevelManager.AminoAcidAmount -= cost;
            CurrentLevel++;
            if (CurrentLevel == 2)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            } else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
            GetComponent<CellFireController>().WeaponId = CurrentLevel == 2 ? CellManager.GetVeteranWeaponId(CellId) : CellManager.GetEliteWeaponId(CellId);
            var cb = GetComponent<CellBuild>();
            if (cb != null)
            {
                cb.Refresh();
            } else
            {
                gameObject.AddComponent<CellBuild>();
            }
            AudioController.PlaySE("enhance");
        }

        public void Sale()
        {
            var total_cost = 0;
            var cost1 = CellManager.GetCost(CellId);
            var cost2 = CellManager.GetEnhanceCost1(CellId);
            var cost3 = CellManager.GetEnhanceCost2(CellId);
            switch (CurrentLevel)
            {
                case 1:
                    total_cost = cost1;
                    break;
                case 2:
                    total_cost = cost1 + cost2;
                    break;
                case 3:
                    total_cost = cost1 + cost2 + cost3;
                    break;
            }
            LevelManager.AminoAcidAmount += (int) (total_cost * 0.5f);
            cc.DestroyCell(this);
            AudioController.PlaySE("sale");
        }

    }
}
