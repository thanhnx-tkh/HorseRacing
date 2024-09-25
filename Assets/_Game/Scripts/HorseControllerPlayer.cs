using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseControllerPlayer : HorseController
{
    [SerializeField] Text textVelocity;
    private void Start()
    {
        StartCoroutine(CoUpdateUISpeed());
    }
    public float DisPlayerToFinish()
    {
        return Mathf.Abs(transform.position.z - 2000f);
    }
    private IEnumerator CoUpdateUISpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (textVelocity != null)
            {
                textVelocity.text = ((int)speed).ToString() + " KM/H";
            }
        }
    }
}
