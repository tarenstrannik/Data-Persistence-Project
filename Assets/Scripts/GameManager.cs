using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using static GameManager;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update

    private string curPlayer;

    private string prevSceneName;
    private List<BestRecord> curBestRecords;
    [SerializeField] private int maxBestListCount = 10;
    private float curSpeedCoef;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance);
        LoadPlayerData();
    }
    public void UpdateBestList(int score)
    {
        BestRecord record = new BestRecord();
        record.playerName = curPlayer;
        record.playerScore = score;
        curBestRecords.Insert(0,record);
        if(curBestRecords.Count>maxBestListCount)
        {
            curBestRecords.RemoveRange(maxBestListCount, curBestRecords.Count- maxBestListCount);

        }
    }

    public void SavePrevSceneName(string sceneName)
    {
        prevSceneName = sceneName;
    }

    public string GetPrevSceneName()
    {
       return prevSceneName;
    }

    public void SetCurPlayer(string cPlayer)
    {
        curPlayer = cPlayer;
    }
    public string GetCurPlayer()
    {
        return curPlayer;
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public BestRecordList records;
        public GameParamsList gameParams;
    }
    [System.Serializable]
    public class BestRecord
    {
        public string playerName;
        public int playerScore;
    }
    [System.Serializable]
    
    public class GameParamsList
    {
        public List<GameParam> gameParam;
    }
    [System.Serializable]
    public enum paramTypes
    {
        String,
        Float,
        Int
    };
    [System.Serializable] 
    public class GameParam
    {
        public string paramName;
        public paramTypes paramType;
        public float paramFloatValue;
        public string paramStringValue;
        public int paramIntValue;
    }
    
    [System.Serializable]
    public class BestRecordList
    {
        public List<BestRecord> bestRecordList;
    }

    public void SavePlayerData()
    {
        GameParam speedParam = new GameParam
        {
            paramName = "speed",
            paramType = paramTypes.Float,
            paramFloatValue = curSpeedCoef

        };
        SaveData data = new SaveData
        {
            playerName = curPlayer,
            records = new BestRecordList
            {
                bestRecordList = curBestRecords
            },
            gameParams = new GameParamsList
            {
                gameParam = new List<GameParam>
                {
                    speedParam
                }
            }
        };


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data.playerName != null)
                curPlayer = data.playerName;
            else curPlayer = "";
            if (data.records != null)
                curBestRecords = data.records.bestRecordList;
            else curBestRecords = new List<BestRecord> { };

            if (data.gameParams != null && data.gameParams.gameParam != null)
            {
                GameParam targetGameParam = data.gameParams.gameParam.FirstOrDefault(param => param.paramName == "speed");

                // Проверяем, был ли найден объект GameParam с заданным paramName
                if (targetGameParam != null)
                {
                    curSpeedCoef = targetGameParam.paramFloatValue;


                    // Делаем с объектом GameParam то, что вам нужно
                }
                else
                {
                    curSpeedCoef = 1;
                }
            }
            else
            {
                curSpeedCoef = 1;
            }


        }
    }

    public int GetBestScore()
    {
        if (curBestRecords.Count > 0)
            return curBestRecords[0].playerScore;
        else return 0;
    }
    public BestRecord GetBestPlayer()
    {
        if (curBestRecords.Count > 0)
            return curBestRecords[0];
        else return new BestRecord { playerName="", playerScore= 0 };
    }

    public List<BestRecord> GetLeaders()
    {

        if (curBestRecords.Count > 0)
            return curBestRecords;
        else return null;
    }
    public float GetSpeedCoef()
    {
        return curSpeedCoef;
    }
    public void SetSpeedCoef(float coef)
    {
        curSpeedCoef = coef;
    }
}
