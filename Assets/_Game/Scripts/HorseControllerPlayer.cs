using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseControllerPlayer : HorseController
{
    public float DisPlayerToFinish(){
        return Mathf.Abs(transform.position.z - 2000f);
    }
}
