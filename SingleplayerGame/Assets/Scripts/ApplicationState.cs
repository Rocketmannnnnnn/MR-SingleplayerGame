﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationState : MonoBehaviour
{
    public bool allowRestart = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }

        if(this.allowRestart && Input.GetKeyDown(KeyCode.R))
        {
            GameObject managerObj = GameObject.FindWithTag("GameController");
            GameManager gm = managerObj.GetComponent<GameManager>();
            gm.loadingScene = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
