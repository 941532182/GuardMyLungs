#define DEVELOPER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.SceneManagement;

namespace Controller
{
    [DisallowMultipleComponent]
    public class Director : MonoBehaviour
    {

        [SerializeField]
        private Transform level;
        [SerializeField]
        private Transform choosePanel;
        [SerializeField]
        private Transform gamePanel;
        [SerializeField]
        private Transform winPanel;

        private void Start()
        {
            var map = Instantiate(Resources.Load<Transform>($"Prefabs/Level/{LevelManager.LevelId}"));
            map.SetParent(level);
            map.localScale = Vector3.one;
            Time.timeScale = 0;
            StartCoroutine(DisplayLevel());
        }

        private IEnumerator DisplayLevel()
        {
            AudioController.PlayBGM("choose");
            yield return new WaitForSecondsRealtime(3);
            choosePanel.GetComponent<ChoosePanelController>().FadeIn();
        }

        private IEnumerator DoStartGame()
        {
            AudioController.BGM.Stop();
            yield return new WaitForSecondsRealtime(1);
            AudioController.PlayBGM(LevelManager.BGM);
            Time.timeScale = 1;
            gamePanel.gameObject.SetActive(true);
        }

        public void StartGame()
        {
            StartCoroutine(DoStartGame());
        }

        public void Win()
        {
            GameObject.Find("Canvas/GamePanel").SetActive(false);
            if (PlayerManager.CurrentLevel <= LevelManager.Order && !LevelManager.IsFinalLevel)
            {
                PlayerManager.NewLevel();
            }
            if (LevelManager.IsFinalLevel)
            {
                Camera.main.gameObject.AddComponent<LensOut>().Post = ()
                => SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
            } else
            {
                AudioController.BGM.Stop();
                AudioController.PlaySE(LevelManager.WinSE);
                winPanel.GetComponent<WinPanelController>().FadeIn();
            }
        }

        public void Fail()
        {
            GameObject.Find("Canvas/GamePanel").SetActive(false);
            var lo = Camera.main.gameObject.AddComponent<LensOut>();
            lo.Color = Color.red;
            lo.Post = () => SceneManager.LoadScene("FailScene", LoadSceneMode.Single);
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
            AudioController.BGM.Stop();
            BuildManager.Reset();
        }

        private void Update()
        {
#if DEVELOPER
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Win();
            }
#endif
        }

    }
}