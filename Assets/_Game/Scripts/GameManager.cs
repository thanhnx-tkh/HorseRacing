using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    Start,
    GamePlay,
    Finish,
}
public class GameManager : MonoBehaviour
{
    public GameState State = GameState.Start;
    public RandomColor randomColor;
    public SoDataText soDataText;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start() {
        UIManager.Instance.ActiveUI(0);
    }
}
