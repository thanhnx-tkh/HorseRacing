using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoDataText", order = 1)]
public class SoDataText : ScriptableObject
{
    public List<GameObject> listText3d;
    public GameObject GetText3D(int index){
        return listText3d[index];
    }
}
