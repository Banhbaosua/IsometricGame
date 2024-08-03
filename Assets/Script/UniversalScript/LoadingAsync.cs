using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingAsync : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] GameObject menu;

    public void LoadScene(object scene)
    {
        string[] split = scene.ToString().Split(' ');
        StartCoroutine(LoadSceneAsync(split[0]));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        if(menu != null)
            menu.SetActive(false);
        
        loadingScreen.SetActive(true);

        while(!operation.isDone) 
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            slider.value = progressValue;

            yield return null;
        }
    }
}
