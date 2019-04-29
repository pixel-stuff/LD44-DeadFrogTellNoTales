using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSartButtonScript : MonoBehaviour
{
    public void NextLevel()
    {
        LoadScene("TutoScene");
    }

    void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
