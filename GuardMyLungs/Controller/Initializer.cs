using UnityEngine;
using Manager;

namespace Controller
{
    [DisallowMultipleComponent]
    public class Initializer : MonoBehaviour
    {

        [SerializeField]
        private GameObject audioGO;

        private void Awake()
        {
            DontDestroyOnLoad(audioGO);
            PlayerManager.Initialize();
        }

    }
}