using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;

namespace Controller
{
    public class WinPanelController : MonoBehaviour
    {

        [SerializeField]
        private Button nextLevelButton;
        private bool buttonLock;

        private void Start()
        {
            buttonLock = false;
            if (LevelManager.IsFinalLevel)
            {
                nextLevelButton.interactable = false;
            }
        }

        public void OnNextLevelClicked()
        {
            if (!buttonLock)
            {
                Camera.main.gameObject.AddComponent<LensOut>().Post = () =>
                {
                    LevelManager.NextLevel();
                    SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
                };
                buttonLock = true;
            }
        }

        public void OnTitleClicked()
        {
            if (!buttonLock)
            {
                Camera.main.gameObject.AddComponent<LensOut>().Post = ()
                => SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
                buttonLock = true;
            }
        }

        public void FadeIn()
        {
            gameObject.SetActive(true);
            gameObject.AddComponent<PanelEnable>();
        }

    }
}
