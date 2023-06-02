using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardToShow;
    [SerializeField] private Transform Hand;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("RewardNo"))
        {
            var multiplier=other.gameObject.name;
            rewardToShow.text=(500*float.Parse(multiplier)).ToString();
            PlayerPrefs.SetFloat("reward",float.Parse(rewardToShow.text));
        }
        //https://www.youtube.com/watch?v=TRUOqUGAfLM
        //yukaridaki link ile dotween baglantisini saglayabilirsin
    }
}
