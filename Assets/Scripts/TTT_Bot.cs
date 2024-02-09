using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTT_Bot : MonoBehaviour
{
    [SerializeField] private List<TTT_ButtonBehaviour> buttons = new List<TTT_ButtonBehaviour>();

    void Start()
    {
        BotReset();

        foreach (TTT_ButtonBehaviour b in buttons) 
        {
            b.bot = this;
        }
    }

    public void BotReset()
    {
        foreach (TTT_ButtonBehaviour b in buttons)
        {
            b.ResetSymble();
        }
    }


    public void TakeTurn()
    {
        TTT_ButtonBehaviour b = null;
        do
        {
            int r = Random.Range(0, buttons.Count);
            if (!buttons[r].WasUsed) b = buttons[r];
        } while(b == null);

        b.InvokeCircle();

        GameManager.instance.isPlayerTurn = CheckEndGame();
    }

    public void PlayerClick(TTT_ButtonBehaviour b)
    {
        GameManager.instance.isPlayerTurn = false;

        if (!CheckEndGame())
        {

            return;
        }

        StartCoroutine(TTT_Turn());
    }

    /// <summary>
    /// Coroutine just to wait x secs for bot play
    /// </summary>
    /// <returns></returns>
    private IEnumerator TTT_Turn()
    {
        yield return new WaitForSeconds(.8f);

        TakeTurn();
    }

    private IEnumerator TTT_GameReset()
    {
        yield return new WaitForSeconds(1f);

        GameManager.instance.Reset_TTT();
    }

    private void ShowValues()
    {
        int i = 0;
        foreach(TTT_ButtonBehaviour b in buttons)
        {

            Debug.Log(i + ", " + b.Value);

            i++;
        }
    }

    /// <summary>
    /// Rudimentary way to check each end condition, calculates sum of each solution
    /// 1+1+1 = player wins // -1-1-1 = bot wins
    /// </summary>
    /// <returns>Returns True if the game hasn't ended, Returns False when a solution has been met</returns>
    private bool CheckEndGame()
    {
        //ShowValues();

        int s1 = buttons[0].Value + buttons[1].Value + buttons[2].Value;
        int s2 = buttons[3].Value + buttons[4].Value + buttons[5].Value;
        int s3 = buttons[6].Value + buttons[7].Value + buttons[8].Value;

        int s4 = buttons[0].Value + buttons[3].Value + buttons[6].Value;
        int s5 = buttons[1].Value + buttons[4].Value + buttons[7].Value;
        int s6 = buttons[2].Value + buttons[5].Value + buttons[8].Value;

        int s7 = buttons[0].Value + buttons[4].Value + buttons[8].Value;
        int s8 = buttons[2].Value + buttons[4].Value + buttons[6].Value;

        int[] solutions = new int[] { s1, s2, s3, s4, s5, s6, s7 , s8};

        foreach (int s in solutions)
        {
            if(s == 3)
            {
                GameManager.instance.ScoreFeedback(EndConditions.WIN);
                return false;
            }

            if(s == -3)
            {
                GameManager.instance.ScoreFeedback(EndConditions.LOSS);
                return false;
            }
        }

        bool draw = true;

        foreach (TTT_ButtonBehaviour b in buttons)
        {
            if(b.Value == 0)
            { 
                draw = false; 
                break;
            }
        }

        if (draw)
        {
            GameManager.instance.ScoreFeedback(EndConditions.DRAW);
            return false;
        }

        return true;
    }
}