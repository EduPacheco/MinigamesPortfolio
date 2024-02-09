using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager instance;

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

    [HideInInspector] public bool isPlayerTurn = true;

    [SerializeField] private TTT_Bot bot;

    public void ScoreFeedback(EndConditions ec)
    {
        UIManager.instance.InputScore(ec);

        StartCoroutine(TTT_GameReset());
    }

    public void Reset_TTT()
    {
        bot.BotReset();
        isPlayerTurn = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            Reset_TTT();
        }
    }

    private IEnumerator TTT_GameReset()
    {
        yield return new WaitForSeconds(1f);

        Reset_TTT();
    }
}