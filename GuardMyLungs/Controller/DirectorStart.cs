using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class DirectorStart : MonoBehaviour
    {

        void Start()
        {
            AudioController.PlayBGM("start");
        }
    }
}