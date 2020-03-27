using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.SceneManagement;

namespace Controller
{
    [DisallowMultipleComponent]
    public class DirectorFail : MonoBehaviour
    {

        private bool gameOverLock = true;

        private void Start()
        {
            StartCoroutine(Fail());
        }

        private IEnumerator Fail()
        {
            AudioController.PlaySE("fail");
            yield return new WaitForSecondsRealtime(3);
            gameOverLock = false;
        }

        private void Update()
        {
            if (!gameOverLock && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
            }
        }

    }
}