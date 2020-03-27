using UnityEngine;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class Virus : MonoBehaviour
    {

        private long virusId;

        public int AminoAcid { get; private set; }
        public long VirusId
        {
            get => virusId;
            set
            {
                virusId = value;
                AminoAcid = VirusManager.GetAminoAcid(virusId);
            }
        }

        private void Start()
        {
            GetComponent<VirusHPController>().Die += OnDie;
        }

        private void OnDie()
        {
            var mul = LevelManager.AminoAcidMultiplier[LevelManager.CurrentWave - 1];
            LevelManager.AminoAcidAmount += (int) (mul * AminoAcid);
        }

    }
}