using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWinLoseText : MonoBehaviour
{
    public GameObject winObj;
    public GameObject loseObj;

    public void SetText(bool win)
    {
        this.winObj.SetActive(win);
        this.loseObj.SetActive(!win);
    }
}
