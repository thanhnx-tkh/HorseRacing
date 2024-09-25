using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private CameraFollow camerafl;
    public List<HorseController> horses;
    private void OnTriggerEnter(Collider other)
    {
        HorseController horse = Cache<HorseController>.GetCollider(other);
        if (horse != null && horse != other)
        {
            if(horse as HorseControllerPlayer){
                UIManager.Instance.arrayUI[1].SetActive(false);
            }
            camerafl.IsFinish = true;
            Time.timeScale = 0.6f;
            horses.Add(horse);
            horse.IsCheckRandomSpeed = false;
            StartCoroutine(horse.SlowDownSpeed());

            if (horses.Count == 12)
            {
                StartCoroutine(CoActiveFinish());
            }
        }
    }
    private IEnumerator CoActiveFinish()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        GameManager.Instance.State = GameState.Finish;
        UIManager.Instance.ActiveUI(2);
        UIManager.Instance.UpdateTextListHorseResult(ShowHorseListNumber());
        camerafl.IsFinish = false;
    }
    public string ShowHorseListNumber()
    {
        if (horses.Count != 12) return null;
        string text = "";
        for (int i = 0; i < horses.Count; i++)
        {
            text += (i + 1).ToString() + " " + horses[i].Name + "\n";
        }
        return text;
    }
}
