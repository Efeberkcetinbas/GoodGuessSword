using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject 
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public bool center;

    public int MaxDamageAmount=10;
    public int Health=20;
    public int TempHealth=20;
}
