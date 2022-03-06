using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SumoDemo
{
    public class ReloadScene : MonoBehaviour
    {
        public void Button_ReloadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
