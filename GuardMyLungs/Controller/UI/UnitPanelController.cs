using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Manager;

namespace Controller
{
    public class UnitPanelController : MonoBehaviour
    {

        [SerializeField]
        private Button EnhanceButton;
        [SerializeField]
        private GameObject EnhanceButtonText;
        [SerializeField]
        private GameObject NotEnoughButtonText;
        [SerializeField]
        private TextMeshProUGUI EnhanceCostText;
        [SerializeField]
        private TextMeshProUGUI SaleAmountText;

        public Cell Cell { get; set; }

        private void Start()
        {
            LevelManager.AminoAcidAmountChanged += OnAminoAcidAmountChanged;
            UpdateEnhance(LevelManager.AminoAcidAmount);
            UpdateSale();
        }

        private void OnDestroy()
        {
            LevelManager.AminoAcidAmountChanged -= OnAminoAcidAmountChanged;
        }

        private void OnAminoAcidAmountChanged(int amount)
        {
            UpdateEnhance(amount);
        }

        public void OnEnhanceClicked()
        {
            DestroyThis();
            Cell.Enhance();
        }

        public void OnSaleClicked()
        {
            DestroyThis();
            Cell.Sale();
        }

        public void DestroyThis()
        {
            gameObject.AddComponent<PanelDisable>();
            Destroy(gameObject, 1);
            Cell.UnitPanel = null;
        }

        private void UpdateEnhance(int amount)
        {
            var enhanceable = CellManager.IsEnhanceable(Cell.CellId);
            var levelupable = Cell.CurrentLevel < 3;

            Action<bool> setButtonState = delegate (bool state)
            {
                EnhanceButton.interactable = state;
                EnhanceButtonText.SetActive(state);
                NotEnoughButtonText.SetActive(!state);
                EnhanceCostText.color = state ? Color.green : Color.red;
            };

            if (enhanceable && levelupable)
            {
                var cost = Cell.CurrentLevel == 1 ? CellManager.GetEnhanceCost1(Cell.CellId) : CellManager.GetEnhanceCost2(Cell.CellId);
                EnhanceCostText.text = cost.ToString();
                var enough = amount >= cost;
                if (enough)
                {
                    setButtonState.Invoke(true);
                } else
                {
                    setButtonState.Invoke(false);
                }
            } else
            {
                EnhanceCostText.text = "----";
                setButtonState.Invoke(false);
            }
        }

        private void UpdateSale()
        {
            var total_cost = 0;
            var cost1 = CellManager.GetCost(Cell.CellId);
            var cost2 = CellManager.GetEnhanceCost1(Cell.CellId);
            var cost3 = CellManager.GetEnhanceCost2(Cell.CellId);
            switch (Cell.CurrentLevel)
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
            total_cost = (int) (total_cost * 0.5f);
            SaleAmountText.text = total_cost.ToString();
        }

    }
}
