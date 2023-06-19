using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public int MaxDamageAmount=1;
    
    public string RivalsName;


}
