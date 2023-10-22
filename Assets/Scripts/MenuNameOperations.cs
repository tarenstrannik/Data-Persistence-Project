using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuNameOperations : MonoBehaviour
{
    [SerializeField] private TMP_InputField playersName;
    [SerializeField] private TextMeshProUGUI bestScore;
    private void Awake()
    {
        if(GameManager.Instance.GetCurPlayer()!=null)
        {
            playersName.text = GameManager.Instance.GetCurPlayer();
        }
        if (GameManager.Instance.GetBestPlayer().playerName != "")
            bestScore.text = $"Best Score: {GameManager.Instance.GetBestPlayer().playerName} : {GameManager.Instance.GetBestPlayer().playerScore}";
        else bestScore.text = $"Best Score: None";

    }
    public void SetPlayersName()
    {
        GameManager.Instance.SetCurPlayer(playersName.text);
    }

}
