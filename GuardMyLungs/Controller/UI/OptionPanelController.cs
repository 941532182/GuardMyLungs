using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Manager;
using TMPro;

namespace Controller
{
    [DisallowMultipleComponent]
    public class OptionPanelController : MonoBehaviour
    {

        [SerializeField]
        private Transform gamePanel;

        private float timeScale;
        private bool buttonLock;

        public void FadeIn()
        {
            gamePanel.gameObject.SetActive(false);
            gameObject.SetActive(true);
            gameObject.AddComponent<PanelEnable>();
        }

        public void FadeOut()
        {
            gamePanel.gameObject.SetActive(true);
            gameObject.SetActive(true);
            gameObject.AddComponent<PanelDisable>();
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

        private void OnEnable()
        {
            buttonLock = false;
            timeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioController.BGM.Pause();
        }

        private void OnDisable()
        {
            Time.timeScale = timeScale;
            AudioController.BGM.Play();
        }

    }
}

