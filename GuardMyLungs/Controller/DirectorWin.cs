using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.SceneManagement;

namespace Controller
{
    [DisallowMultipleComponent]
    public class DirectorWin : MonoBehaviour
    {

        private bool winLock = true;

        private void Start()
        {
            StartCoroutine(Win());
        }

        private IEnumerator Win()
        {
            AudioController.PlaySE("win2");
            yield return new WaitForSecondsRealtime(3);
            winLock = false;
        }

        private void Update()
        {
            if (!winLock && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
            }
        }

    }
}