using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RandomColor", order = 1)]

public class RandomColor : ScriptableObject
{
    public List<Color> colors;

    public Color GetRandomColor(){
        return colors[Random.Range(0,colors.Count)];
    }
}
