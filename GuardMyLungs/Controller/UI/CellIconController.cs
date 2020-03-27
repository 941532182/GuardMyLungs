using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Util;

namespace Controller
{
    [DisallowMultipleComponent]
    public class CellIconController : MonoBehaviour
    {

        public long CellId { get; set; }

        public void ChoosePanelClick()
        {
            var succeed = LevelManager.Choose(CellId);
            if (succeed)
            {
                var selected = transform.GetChild("Selected").gameObject.activeSelf;
                transform.GetChild("Selected").gameObject.SetActive(!selected);
            } else
            {
                AudioController.PlaySE("buzzer");
            }
        }

        public void GamePanelClick()
        {
            BuildManager.SelectCell(CellId);
        }

    }
}