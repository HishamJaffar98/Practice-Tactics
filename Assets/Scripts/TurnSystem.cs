using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    public event Action OnTurnChanged;
    private int turnNumber = 1;
    private bool isPlayerTurn = true;
    public int TurnNumber
	{
        get
		{
            return turnNumber;
		}
	}

    public bool IsPlayerTurn
	{
		get
		{
            return isPlayerTurn;
		}
	}

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged?.Invoke();
    }
}
