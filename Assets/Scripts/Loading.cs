using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    public GameObject loading;
    void OnEnable()
    {
        Invoke("HomeScreen", 5);
    }

    void HomeScreen()
    {
        //SceneManager.LoadScene(1);
        loading.SetActive(false);
    }
}
