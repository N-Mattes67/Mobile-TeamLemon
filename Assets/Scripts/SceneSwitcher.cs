using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        StartCoroutine(SwitchScenDelay(scene));
    }
    IEnumerator SwitchScenDelay(string scene)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(scene);
    }

}