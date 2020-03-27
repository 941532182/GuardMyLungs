using UnityEngine;
using UnityEditor;

namespace Controller
{
    [DisallowMultipleComponent]
    public class Quit : MonoBehaviour
    {

        public void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

    }
}
