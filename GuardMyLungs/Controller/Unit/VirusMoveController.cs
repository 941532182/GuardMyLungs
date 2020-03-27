using UnityEngine;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class VirusMoveController : MonoBehaviour
    {

        private LevelController lc;
        private VirusController vc;
        private SpriteRenderer sr;
        private int currentWaypoint;
        private Transform target;
        private long virusId = -1;

        public int MaxHP { get; private set; }
        public int MoveSpeed { get; set; }
        public int AminoAcid { get; private set; }
        public bool CanFly { get; private set; }
        public int Way { get; set; }
        public long VirusId
        {
            get => virusId;
            set
            {
                if (virusId == -1)
                {
                    virusId = value;
                    MaxHP = VirusManager.GetMaxHP(virusId);
                    MoveSpeed = VirusManager.GetMoveSpeed(virusId);
                    AminoAcid = VirusManager.GetAminoAcid(virusId);
                    CanFly = VirusManager.CanFly(virusId);
                }
            }
        }

        void Start()
        {
            lc = GameObject.Find("Level").GetComponentInChildren<LevelController>();
            vc = GameObject.Find("Unit").GetComponentInChildren<VirusController>();
            sr = GetComponent<SpriteRenderer>();
            target = CanFly ? lc.Waypoints[Way][lc.Waypoints[Way].Length - 1] : lc.Waypoints[Way][0];
        }

        void Update()
        {
            if ((target.position - transform.position).sqrMagnitude < 0.01f)
            {
                currentWaypoint++;
                if (currentWaypoint == lc.Waypoints[Way].Length)
                {
                    vc.DestroyVirus(GetComponent<Virus>());
                    return;
                }
                target = lc.Waypoints[Way][currentWaypoint];
            } else
            {
                var sub = target.position - transform.position;
                var dir = sub.normalized;
                var dx = MoveSpeed / 50f * Time.deltaTime * dir;
                if (sub.sqrMagnitude > dx.sqrMagnitude)
                {
                    transform.position += dx;
                } else
                {
                    transform.position = target.position;
                }
                if (dir.x >= 0)
                {
                    sr.flipX = false;
                } else
                {
                    sr.flipX = true;
                }
            }
        }
    }
}
