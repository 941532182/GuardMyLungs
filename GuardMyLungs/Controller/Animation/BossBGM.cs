using UnityEngine;

namespace Controller
{
    public class BossBGM : MonoBehaviour
    {

        [SerializeField]
        private string bgm;

        void Start()
        {
            AudioController.PlayBGM(bgm);
            Destroy(this);
        }

    }
}