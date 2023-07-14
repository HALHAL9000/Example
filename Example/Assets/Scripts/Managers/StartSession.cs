using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartSession : MonoBehaviour
{
    void Start()
    {
        if (SaveManager.Instance.Level != 1)
        {
            SceneManager.LoadScene(SaveManager.Instance.Level-1);
        }
    }

}
