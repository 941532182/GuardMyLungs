using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class VirusController : MonoBehaviour
    {

        private LevelController lc;
        private Queue<Queue<(long, int, int)>> troops;
        private bool isSpawning;
        private bool isWinning;

        public List<Virus> Viruses { get; private set; } = new List<Virus>();

        private void Start()
        {
            troops = LevelManager.Troops;
        }

        private void Update()
        {
            if (Viruses.Count == 0 && !isSpawning && troops.Count > 0)
            {
                LevelManager.CurrentWave++;
                StartCoroutine(SpawnVirus());
            } else if (Viruses.Count == 0 && !isSpawning && troops.Count == 0 && !isWinning)
            {
                isWinning = true;
                StartCoroutine(Win());
            }
        }

        private IEnumerator Win()
        {
            yield return new WaitForSeconds(5);
            var director = GameObject.Find("Director").GetComponent<Director>();
            director.Win();
        }

        private IEnumerator SpawnVirus()
        {
            isSpawning = true;
            if (LevelManager.CurrentWave == 1)
            {
                yield return new WaitForSeconds(7);
            }
            var wave = troops.Dequeue();
            var sequence = new Queue<(long, int)>();
            while (wave.Count > 0)
            {
                var troop = wave.Dequeue();
                for (int i = 0; i < troop.Item2; i++)
                {
                    sequence.Enqueue((troop.Item1, troop.Item3));
                }
            }
            yield return new WaitForSeconds(3);
            while (sequence.Count > 0)
            {
                var seq = sequence.Dequeue();
                CreateVirus(seq.Item1, seq.Item2, VirusManager.IsBoss(seq.Item1));
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            }
            isSpawning = false;
        }

        public Virus CreateVirus(long virusId, params object[] parameters)
        {
            if (lc == null)
            {
                lc = GameObject.Find("Level").GetComponentInChildren<LevelController>();
            }
            var virus = Instantiate(Resources.Load<Transform>($"Prefabs/Unit/{VirusManager.GetPrefab(virusId)}"));
            virus.position = lc.Waypoints[(int) parameters[0]][0].position;
            virus.SetParent(transform);

            var virus_comp = virus.gameObject.AddComponent<Virus>();
            virus_comp.VirusId = virusId;
            var virus_move_ctrl = virus.gameObject.AddComponent<VirusMoveController>();
            virus_move_ctrl.VirusId = virusId;
            virus_move_ctrl.Way = (int) parameters[0];
            virus.gameObject.AddComponent<VirusSpeedAdjuster>();
            var virus_hp_ctrl = virus.gameObject.AddComponent<VirusHPController>();
            virus_hp_ctrl.VirusId = virusId;
            if ((bool) parameters[1])
            {
                virus.gameObject.AddComponent<LongHPBarController>();
            } else
            {
                virus.gameObject.AddComponent<HPBarController>();
            }
            var virus_exp_ctrl = virus.gameObject.AddComponent<VirusExplodeController>();
            virus_exp_ctrl.VirusId = virusId;
            virus_exp_ctrl.TargetLayer = LayerMask.NameToLayer("Lungs");

            Viruses.Add(virus_comp);

            return virus_comp;
        }

        public void DestroyVirus(Virus virus)
        {
            Viruses.Remove(virus);
            Destroy(virus.gameObject);
        }

    }
}

