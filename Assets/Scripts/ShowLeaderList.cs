using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowLeaderList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI  leadersList;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.GetLeaders() != null && GameManager.Instance.GetLeaders().Count > 0)
        {
            int i = 1;
            foreach (GameManager.BestRecord bestRecord in GameManager.Instance.GetLeaders())
            {
                leadersList.text += $"{i}. {bestRecord.playerName} : {bestRecord.playerScore} \n";
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
