using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIScript : MonoBehaviour
{

    public TextMeshProUGUI percentText;
    public TextMeshProUGUI winText;

    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnWin += EnableWinText;
    }
    
    void Update()
    {
        SetPercentText();
    }

    public void SetPercentText()
    {
        percentText.SetText("" + gameManager.percentComplete*100 + "%");
    }

    public void EnableWinText()
    {
        winText.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        gameManager.OnWin -= EnableWinText;
    }
}
