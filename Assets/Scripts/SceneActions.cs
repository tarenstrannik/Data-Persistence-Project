using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameManager.Instance.SavePrevSceneName(SceneManager.GetActiveScene().name);
        if (GameManager.Instance.GetCurPlayer() is null || GameManager.Instance.GetCurPlayer() == "")
        {

            SceneManager.LoadScene("enteryourname");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadPrevScene()
    {
       
        SceneManager.LoadScene(GameManager.Instance.GetPrevSceneName());
    }

    public void Quit()
    {
        GameManager.Instance.SavePlayerData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
