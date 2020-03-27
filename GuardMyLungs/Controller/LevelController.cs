using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Manager;
using Util;

namespace Controller
{
    [DisallowMultipleComponent]
    public class LevelController : MonoBehaviour
    {

        private CellController cc;
        private LungsController lc;
        [SerializeField]
        private Tilemap ground;
        [SerializeField]
        private Tilemap path;
        private Tile currentTile;
        private Vector3Int curPos;
        private Layer layer;
        public Transform[][] Waypoints { get; private set; }

        private void Start()
        {
            cc = GameObject.Find("Unit/Cell").GetComponent<CellController>();
            lc = GameObject.Find("Unit/Lungs").GetComponent<LungsController>();
            var paths = transform.GetChild("Waypoints").GetChildren();
            Waypoints = new Transform[paths.Length][];
            for (int i = 0; i < paths.Length; i++)
            {
                Waypoints[i] = paths[i].GetChildren();
            }
            var lungs_pos = Waypoints[0][Waypoints[0].Length - 1].position;
            GameObject.Find("Unit/Lungs").GetComponent<LungsController>().CreateLungs(lungs_pos);
            BuildManager.StateChanged += OnBuildStateChanged;
        }

        private void OnDestroy()
        {
            BuildManager.StateChanged -= OnBuildStateChanged;
        }

        private void Update()
        {
            if (BuildManager.State == BuildState.Building)
            {
                var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse_pos.z = 0;
                var cell_pos = path.WorldToCell(mouse_pos);
                if (cell_pos != curPos)
                {
                    var temp = path.GetTile<Tile>(cell_pos);
                    layer = Layer.Path;
                    if (temp == null)
                    {
                        temp = ground.GetTile<Tile>(cell_pos);
                        layer = Layer.Ground;
                    }
                    if (temp == null)
                    {
                        layer = Layer.Top;
                    }

                    if (currentTile != null)
                    {
                        currentTile.color = Color.white;
                        path.RefreshTile(curPos);
                        ground.RefreshTile(curPos);
                    }

                    currentTile = temp;
                    curPos = cell_pos;

                    if (currentTile != null)
                    {
                        var sub = mouse_pos - lc.LungsPos;
                        if (cc.Cells.ContainsKey(curPos) || Mathf.Abs(sub.x) < 3 && Mathf.Abs(sub.y) < 1)
                        {
                            currentTile.color = Color.red;
                        } else if (CellManager.IsBarrier(BuildManager.BuildingCellId))
                        {
                            if (layer == Layer.Path)
                            {
                                currentTile.color = Color.green;
                            } else if (layer == Layer.Ground)
                            {
                                currentTile.color = Color.red;
                            }
                        } else
                        {
                            if (layer == Layer.Path)
                            {
                                currentTile.color = Color.red;
                            } else if (layer == Layer.Ground)
                            {
                                currentTile.color = Color.green;
                            }
                        }
                        path.RefreshTile(curPos);
                        ground.RefreshTile(curPos);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (currentTile == null)
                    {
                        return;
                    } else if (currentTile.color == Color.green)
                    {
                        AudioController.PlaySE("build");
                        cc.CreateCell(BuildManager.BuildingCellId, curPos);
                    } else if (currentTile.color == Color.red)
                    {
                        AudioController.PlaySE("buzzer");
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    BuildManager.DeselectCell();
                }
            }
        }

        private void OnBuildStateChanged()
        {
            if (BuildManager.State == BuildState.None)
            {
                var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse_pos.z = 0;
                var cell_pos = path.WorldToCell(mouse_pos);
                var temp = path.GetTile<Tile>(cell_pos);
                if (temp == null)
                {
                    temp = ground.GetTile<Tile>(cell_pos);
                }
                if (temp != null)
                {
                    temp.color = Color.white;
                    path.RefreshTile(curPos);
                    ground.RefreshTile(curPos);
                }
            }
        }

        private enum Layer
        {
            Top,
            Ground,
            Path
        }

    }
}