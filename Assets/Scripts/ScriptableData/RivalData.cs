using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RivalData", menuName = "Data/RivalData", order = 2)]
public class RivalData : ScriptableObject 
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool center;

    public int RivalHealth;
    public int TempHealth;
    public int index;
    
    public string RivalsName;

}
