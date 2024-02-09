using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTT_ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject cross0;
    [SerializeField] private GameObject cross1;
    [SerializeField] private GameObject circle0;

    private bool wasUsed = false;
    public bool WasUsed { get => wasUsed; }

    private int value = 0;
    public int Value { get => value; }

    [HideInInspector] public TTT_Bot bot;

    void Start()
    {
        ResetSymble();
    }

    private void OnMouseDown()
    {
        if (wasUsed) return;
        if(!GameManager.instance.isPlayerTurn) return;

        InvokeCross();
    }

    public void ResetSymble()
    {
        value = 0;
        wasUsed = false;
        cross0.SetActive(false);
        cross1.SetActive(false);
        circle0.SetActive(false);
    }

    /// <summary>
    /// Bot Click
    /// </summary>
    public void InvokeCircle()
    {
        wasUsed = true;
        circle0.SetActive(true);
        value = -1;
    }

    /// <summary>
    /// Player Click
    /// </summary>
    private void InvokeCross() 
    {
        wasUsed = true;
        cross0.SetActive(true);
        cross1.SetActive(true);
        value = 1;

        bot.PlayerClick(this);
    }
}