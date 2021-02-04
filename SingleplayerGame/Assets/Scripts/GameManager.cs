using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    public string userInterfaceTag = "UI";
    public float winCheckDelay = 0.5f;

    private void Start()
    {
        StartCoroutine("Check");
    }

    IEnumerator Check()
    {
        while (true)
        {
            GameObject[] tanks = GameObject.FindGameObjectsWithTag(this.playerTag);
            if (tanks.Length <= 0)
                GameOver(false);
            tanks = GameObject.FindGameObjectsWithTag(this.enemyTag);
            if (tanks.Length <= 0)
                GameOver(true);
            yield return new WaitForSeconds(this.winCheckDelay);
        }
    }

    public void GameOver(bool win)
    {
        GameObject userInterface = GameObject.FindWithTag(this.userInterfaceTag);
        userInterface.GetComponentInChildren<SetWinLoseText>().SetText(win);
        ApplicationState state = GetComponent<ApplicationState>();
        state.allowRestart = true;
    }
}
