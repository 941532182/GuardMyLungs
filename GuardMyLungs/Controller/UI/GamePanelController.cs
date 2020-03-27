using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Manager;
using Util;

namespace Controller
{
    [DisallowMultipleComponent]
    public class GamePanelController : MonoBehaviour
    {

        [SerializeField]
        private OptionPanelController opc;
        [SerializeField]
        private TextMeshProUGUI aminoAcidAmount;
        [SerializeField]
        private Transform units;
        [SerializeField]
        private TextMeshProUGUI currentWave;
        [SerializeField]
        private TextMeshProUGUI totalWave;
        [SerializeField]
        private Image timeRate;
        [SerializeField]
        private Image options;

        private void Start()
        {
            LevelManager.AminoAcidAmountChanged += OnAminoAcidAmountChanged;
            LevelManager.CurrentWaveChanged += OnCurrentWaveChanged;
            BuildManager.StateChanged += OnBuildStateChanged;
            aminoAcidAmount.text = LevelManager.AminoAcidAmount.ToString();
            currentWave.text = LevelManager.CurrentWave.ToString();
            totalWave.text = LevelManager.TotalWave.ToString();
            SetupUnitList();
        }

        private void OnDestroy()
        {
            LevelManager.AminoAcidAmountChanged -= OnAminoAcidAmountChanged;
            LevelManager.CurrentWaveChanged -= OnCurrentWaveChanged;
            BuildManager.StateChanged -= OnBuildStateChanged;
        }

        private void OnAminoAcidAmountChanged(int amount)
        {
            aminoAcidAmount.text = amount.ToString();
            CalcUnitListInteractable();
        }

        private void SetupUnitList()
        {
            foreach (var cell_id in LevelManager.ChosenCells)
            {
                var name = CellManager.GetName(cell_id);
                var desc = CellManager.GetDesc(cell_id);
                var prefab = CellManager.GetPrefab(cell_id);
                var cost = CellManager.GetCost(cell_id);
                var icon = Instantiate(Resources.Load<Transform>($"Prefabs/Icon/{prefab}"));
                icon.SetParent(units);
                icon.localScale = Vector3.one;
                var sid = icon.gameObject.AddComponent<ShowIconDetail>();
                sid.Name = name;
                sid.Desc = desc;
                sid.Cost = cost;
                var cic = icon.gameObject.AddComponent<CellIconController>();
                cic.CellId = cell_id;
                icon.GetComponentInChildren<Button>().onClick.AddListener(cic.GamePanelClick);
            }
        }

        private void CalcUnitListInteractable()
        {
            switch (BuildManager.State)
            {
                case BuildState.None:
                    foreach (var icon in units.GetChildren())
                    {
                        var cell_id = icon.GetComponent<CellIconController>().CellId;
                        if (LevelManager.AminoAcidAmount >= CellManager.GetCost(cell_id))
                        {
                            icon.GetComponentInChildren<Button>().interactable = true;
                        } else
                        {
                            icon.GetComponentInChildren<Button>().interactable = false;
                        }
                    }
                    break;
                case BuildState.Building:
                    foreach (var icon in units.GetChildren())
                    {
                        if (icon.GetComponent<CellIconController>().CellId == BuildManager.BuildingCellId)
                        {
                            icon.GetComponentInChildren<Button>().interactable = true;
                        } else
                        {
                            icon.GetComponentInChildren<Button>().interactable = false;
                        }
                    }
                    break;
            }
        }

        private void OnCurrentWaveChanged(int wave)
        {
            currentWave.text = wave.ToString();
        }

        public void OnTimeRateClicked()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 2;
                timeRate.sprite = Instantiate(Resources.Load<Sprite>("Textures/double_speed"));
            } else if (Time.timeScale == 2)
            {
                Time.timeScale = 1;
                timeRate.sprite = Instantiate(Resources.Load<Sprite>("Textures/normal_speed"));
            }
        }

        private void OnBuildStateChanged()
        {
            CalcUnitListInteractable();
        }

        public void OnOptionsClicked()
        {
            AudioController.PlaySE("pause");
            opc.FadeIn();
        }

    }
}
