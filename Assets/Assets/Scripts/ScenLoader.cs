using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FUBAR
{
    public class ScenLoader : MonoBehaviour
    {
        private bool Init = false;
        // Start is called before the first frame update
        void Start()
        {
            if (!Init)
            {
                DontDestroyOnLoad(this);
                SceneManager.LoadScene(1);
                Init = true;
            }
        }

        public void OnSceneSelection()
        {
            SceneManager.LoadScene(2);
        }
    }
}
