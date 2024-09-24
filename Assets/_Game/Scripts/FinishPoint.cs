using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public List<HorseController> horses;
    private void OnTriggerEnter(Collider other) {
        HorseController horse = Cache<HorseController>.GetCollider(other);
        if (horse != null && horse != other)
        {
            Time.timeScale = 0.6f;
            horses.Add(horse);
            horse.IsCheckRandomSpeed = false;
            StartCoroutine(horse.SlowDownSpeed());

            if(horses.Count == 12) {
                Time.timeScale = 1f;
                GameManager.Instance.State = GameState.Finish;
                UIManager.Instance.ActiveUI(2);                
                UIManager.Instance.UpdateTextListHorseResult(ShowHorseListNumber());
            }
        }
    }
    public string ShowHorseListNumber(){
        if(horses.Count != 12) return null;
        string text = "";
        for (int i = 0; i < horses.Count; i++)
        {
            text += (i+1).ToString() + " " + horses[i].Name + "\n";
        }
        return text;
    }
}
