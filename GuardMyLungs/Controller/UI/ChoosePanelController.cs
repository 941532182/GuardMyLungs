using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;
using TMPro;

namespace Controller
{
    [DisallowMultipleComponent]
    public class ChoosePanelController : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI levelText;
        [SerializeField]
        private Transform virusList;
        [SerializeField]
        private Transform selectText;
        [SerializeField]
        private Transform cellList;

        private void Start()
        {
            levelText.text = $"关卡  {LevelManager.Order}";
            foreach (var virus_id in LevelManager.Viruses)
            {
                var name = VirusManager.GetName(virus_id);
                var desc = VirusManager.GetDesc(virus_id);
                var prefab = VirusManager.GetPrefab(virus_id);
                var icon = Instantiate(Resources.Load<Transform>($"Prefabs/Icon/{prefab}"));
                icon.SetParent(virusList);
                icon.localScale = Vector3.one;
                var sid = icon.gameObject.AddComponent<ShowIconDetail>();
                sid.Name = name;
                sid.Desc = desc;
            }
            var cells = PlayerManager.Cells;
            if (cells.Length <= 5)
            {
                cellList.gameObject.SetActive(false);
                selectText.gameObject.SetActive(false);
                foreach (var cell_id in cells)
                {
                    LevelManager.Choose(cell_id);
                }
            } else
            {
                foreach (var cell_id in cells)
                {
                    var name = CellManager.GetName(cell_id);
                    var desc = CellManager.GetDesc(cell_id);
                    var prefab = CellManager.GetPrefab(cell_id);
                    var icon = Instantiate(Resources.Load<Transform>($"Prefabs/Icon/{prefab}"));
                    icon.SetParent(cellList);
                    icon.localScale = Vector3.one;
                    var sid = icon.gameObject.AddComponent<ShowIconDetail>();
                    sid.Name = name;
                    sid.Desc = desc;
                    var cic = icon.gameObject.AddComponent<CellIconController>();
                    cic.CellId = cell_id;
                    icon.GetComponentInChildren<Button>().onClick.AddListener(cic.ChoosePanelClick);
                }
            }
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            gameObject.AddComponent<PanelEnable>();
        }

        public void FadeOut()
        {
            if (PlayerManager.Cells.Length > 5 && LevelManager.ChosenCells.Count < 5)
            {
                AudioController.PlaySE("buzzer");
                return;
            }
            GameObject.Find("Director").GetComponent<Director>().StartGame();
            gameObject.SetActive(true);
            gameObject.AddComponent<PanelDisable>();
        }

        public void OnQuitClicked()
        {
            SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        }

    }
}

