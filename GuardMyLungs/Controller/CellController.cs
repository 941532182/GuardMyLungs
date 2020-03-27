using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Manager;
using Util;

namespace Controller
{
    [DisallowMultipleComponent]
    public class CellController : MonoBehaviour
    {

        private Tilemap ground;
        public Dictionary<Vector3Int, Cell> Cells { get; private set; } = new Dictionary<Vector3Int, Cell>();

        public Cell CreateCell(long cellId, Vector3Int cellPos)
        {
            if (ground == null)
            {
                ground = GameObject.Find("Level").transform.GetChild("Ground").GetComponent<Tilemap>();
            }
            var real_pos = ground.CellToWorld(cellPos) + new Vector3(1, 1);
            var cell = Instantiate(Resources.Load<Transform>($"Prefabs/Unit/{CellManager.GetPrefab(cellId)}"));
            cell.position = real_pos;
            cell.SetParent(transform);

            var cell_comp = cell.gameObject.AddComponent<Cell>();
            cell_comp.CellId = cellId;
            var cell_fire_ctrl = cell.gameObject.AddComponent<CellFireController>();
            cell_fire_ctrl.WeaponId = CellManager.GetRookieWeaponId(cellId);

            Cells[cellPos] = cell_comp;

            LevelManager.AminoAcidAmount -= CellManager.GetCost(BuildManager.BuildingCellId);
            BuildManager.DeselectCell();

            return cell_comp;
        }

        public void DestroyCell(Cell cell)
        {
            Vector3Int key = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            foreach (var pair in Cells)
            {
                if (pair.Value == cell)
                {
                    key = pair.Key;
                    break;
                }
            }
            Cells.Remove(key);
            Destroy(cell.gameObject);
        }

    }
}
