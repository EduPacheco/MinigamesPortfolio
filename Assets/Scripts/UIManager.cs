using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public enum EndConditions
{
    DRAW,
    WIN,
    LOSS
}

public class UIManager : MonoBehaviour
{
    #region SINGLETON
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI textPrefab;
    
    public void InputScore(EndConditions ec)
    {
        TextMeshProUGUI t = Instantiate(textPrefab, scorePanel.transform);

        switch (ec)
        {
            case EndConditions.DRAW:
                t.text = "DRAW";
                t.color = new Color32(255, 153, 51, 255);
                break;
            case EndConditions.WIN:
                t.text = "WIN";
                t.color = new Color32(0, 204, 0, 255);
                break;
            case EndConditions.LOSS:
                t.text = "LOSS";
                t.color = new Color32(153, 0, 0, 255);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InputScore(EndConditions.DRAW);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            InputScore(EndConditions.WIN);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InputScore(EndConditions.LOSS);
        }
    }
}