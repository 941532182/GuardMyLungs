using UnityEngine;
using Manager;
using Util;

namespace Controller
{
    [DisallowMultipleComponent]
    public class VirusSpeedAdjuster : MonoBehaviour
    {

        private VirusMoveController vm;
        private SpriteRenderer sr;
        private Jump jump;
        private float t;
        private int moveSpeed;
        private Color color;

        private void Start()
        {
            vm = GetComponent<VirusMoveController>();
            sr = GetComponent<SpriteRenderer>();
            jump = GetComponent<Jump>();
            moveSpeed = vm.MoveSpeed;
            color = sr.color;
        }

        public void SetSpeedMultiplier(float multiplier, float time)
        {
            var target = (int) (moveSpeed * multiplier);
            if (vm.MoveSpeed < target) return;
            vm.MoveSpeed = target;
            sr.color = Color.blue;
            jump.IsPause = true;
            if (multiplier == 0)
            {
                transform.GetChild("Frozen").gameObject.SetActive(true);
            }
            if (time < t) return;
            t = time;
            enabled = true;
        }

        public void Recover()
        {
            vm.MoveSpeed = moveSpeed;
            sr.color = color;
            jump.IsPause = false;
            transform.GetChild("Frozen").gameObject.SetActive(false);
            enabled = false;
        }

        private void Update()
        {
            if (t > 0)
            {
                t -= Time.deltaTime;
            } else
            {
                Recover();
            }
        }

    }
}
