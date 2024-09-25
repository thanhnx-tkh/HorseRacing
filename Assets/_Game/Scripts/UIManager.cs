using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] HolderHorse holderHorse;
    [SerializeField] FinishPoint finishPoint;
    [SerializeField] HorseControllerPlayer horsePlayer;
    public Text textNumberPlayer;
    public Text textListHorseResult;
    public Text textOrdinalIndicators;
    public Text textDisToFinish;
    public Text textCountDown;
    public Button buttonPlayGame;
    public Button buttonRetryGame;

    public GameObject[] arrayUI;

    public static UIManager Instance { get; private set; }

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
    private void Start()
    {
        buttonPlayGame.onClick.AddListener(BtnPlayGame);
        buttonRetryGame.onClick.AddListener(BtnRetryGame);
        StartCoroutine(UpdateTextNumberPlayer());
    }

    public void ActiveUI(int indexUi)
    {
        for (int i = 0; i < arrayUI.Length; i++)
        {
            arrayUI[i].SetActive(false);
        }
        arrayUI[indexUi].SetActive(true);
    }

    private IEnumerator UpdateTextNumberPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            int number = holderHorse.GetIndexNumberPlayer() + 1;
            textNumberPlayer.text = (number).ToString();
            textOrdinalIndicators.text = GetOrdinal(number);
            int disToFinish = (int)horsePlayer.DisPlayerToFinish();
            textDisToFinish.text =  disToFinish.ToString() + "m";
        }
    }
    public void UpdateTextListHorseResult(string result)
    {
        textListHorseResult.text = result;
    }

    public void BtnPlayGame(){
        ActiveUI(3);
        StartCoroutine(StartCountdown());
    }

    public void BtnRetryGame(){
        holderHorse.SpawnRetry();
        finishPoint.horses.Clear();
        ActiveUI(3);
        StartCoroutine(StartCountdown());
    }
    public IEnumerator StartCountdown()
    {
        textCountDown.text = "3";
        yield return new WaitForSeconds(1f);

        textCountDown.text = "2";
        yield return new WaitForSeconds(1f);

        textCountDown.text = "1";
        yield return new WaitForSeconds(1f);

        textCountDown.text = "Go!";
        yield return new WaitForSeconds(1f);
        GameManager.Instance.State = GameState.GamePlay;
        ActiveUI(1);
    }
    private string GetOrdinal(int number)
    {
        if (number <= 0) return number.ToString();

        switch (number % 100)
        {
            case 11:
            case 12:
            case 13:
                return "th";
        }

        switch (number % 10)
        {
            case 1: return "st";
            case 2: return "nd";
            case 3: return "rd";
            default: return "th";
        }
    }

}
