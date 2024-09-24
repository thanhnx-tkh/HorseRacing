using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderHorse : MonoBehaviour
{
    [SerializeField] private List<Transform> pointSpawns;
    [SerializeField] private HorseController horsePrefab;
    [SerializeField] public HorseController horsePlayer;

    public List<HorseController> horses;
    private int countSpawnHorse = 0;
    private List<string> namesList = new List<string>
        {
            "Milan Harris",
            "Fern Gilmore",
            "Chantelle Charles",
            "Salma Chapman",
            "Louie Woods",
            "Roman Mcbride",
            "Warren Lowery",
            "Rhiannon Cain",
            "Dawid Glover",
            "Caitlin Davis",
            "Safwan Johnson"
        };
    private void Start()
    {
        SpawnHolderHorse();
        StartCoroutine(HorseNumberSort());
    }
    public void SpawnHolderHorse()
    {
        int index = Random.Range(0, pointSpawns.Count);
        horsePlayer.transform.localPosition = pointSpawns[index].transform.position;
        horsePlayer.transform.localScale = Vector3.one * 0.6f;
        horsePlayer.Name = "Player";
        horsePlayer.OnInit(countSpawnHorse);

        pointSpawns.RemoveAt(index);
        horses.Add(horsePlayer);

        for (int i = 0; i < pointSpawns.Count; i++)
        {
            countSpawnHorse++;
            HorseController newHorse = Instantiate(horsePrefab, transform);
            newHorse.transform.localPosition = pointSpawns[i].transform.position;
            newHorse.transform.localScale = Vector3.one * 0.6f;
            newHorse.Name = namesList[i];
            newHorse.OnInit(countSpawnHorse);
            horses.Add(newHorse);
        }
    }
    public void SpawnRetry()
    {
        horsePlayer.OnReset();
        for (int i = 0; i < horses.Count; i++)
        {
            horses[i].OnReset();
        }
    }
    // sắp xếp thứ tự của ngựa khi đua 
    private IEnumerator HorseNumberSort()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            horses.Sort((horse1, horse2) =>
            horse1.GetDistanceFinish().CompareTo(horse2.GetDistanceFinish()));
        }
    }
    public int GetIndexNumberPlayer()
    {
        if (horses.Count != 12) return 0;
        return horses.IndexOf(horsePlayer);
    }
    // ShowHorseListNumber
    public string ShowHorseListNumber()
    {
        if (horses.Count != 12) return null;
        string text = "";
        for (int i = 0; i < horses.Count; i++)
        {
            text += i.ToString() + " " + horses[i].Name + "\n";
        }
        return text;
    }
}
