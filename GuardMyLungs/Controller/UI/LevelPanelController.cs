using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class LevelPanelController : MonoBehaviour
    {

        [SerializeField]
        private Transform levelsLayout;

        private void Awake()
        {
            for (int i = 0; i < levelsLayout.childCount; i++)
            {
                if (i + 1 > PlayerManager.CurrentLevel)
                {
                    levelsLayout.GetChild(i).gameObject.SetActive(false);
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
            gameObject.SetActive(true);
            gameObject.AddComponent<PanelDisable>();
        }

        public void LoadLevel(string levelId)
        {
            Camera.main.gameObject.AddComponent<LensOut>().Post = () =>
            {
                LevelManager.SetLevel(Convert.ToInt64(levelId));
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            };
        }

    }
}
