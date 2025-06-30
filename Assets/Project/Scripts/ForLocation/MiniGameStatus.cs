using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MiniGameStatus
{
    public string NameMiniGameScene { get; set; }
    public QuestStatus status = QuestStatus.isClosed;

     public MiniGameStatus() { }

    public MiniGameStatus(string nm, QuestStatus st)
    {
        NameMiniGameScene = nm;
        status = st;
    }

    public MiniGameStatus(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep);
        if (ar.Length >= 2)
        {
            NameMiniGameScene = ar[0];
            switch(ar[1])
            {
                case "isClosed": status = QuestStatus.isClosed; break;
                case "isAccessible": status = QuestStatus.isAccessible; break;
                case "isSuccess": status = QuestStatus.isSuccess; break;
                case "isFailed": status = QuestStatus.isFailed; break;
            }
        }
    }

    public string ToCsvString(char sep = '=')
    {
        return $"{NameMiniGameScene}{sep}{status}{sep}";
    }
}

[Serializable]
public class ListMiniGames
{
    private List<MiniGameStatus> miniGames = new List<MiniGameStatus>();

    public ListMiniGames() { }
    public ListMiniGames(string csv, char sep = '#')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length > 0)
        {
            for(int i = 0; i < ar.Length; i++) miniGames.Add(new MiniGameStatus(ar[i], '='));
        }
    }

    public string ToCsvString(char sep = '#')
    {
        StringBuilder sb = new StringBuilder();
        foreach (MiniGameStatus mgs in miniGames) sb.Append(mgs.ToCsvString('='));
        return sb.ToString();
    }

    public void AddMiniGame(MiniGameStatus miniGame)
    {
        foreach (MiniGameStatus mgs in miniGames)
        {
            if (mgs.NameMiniGameScene == miniGame.NameMiniGameScene)
            {
                mgs.status = miniGame.status;
                return;
            }
        }
        miniGames.Add(new MiniGameStatus(miniGame.NameMiniGameScene, miniGame.status));
    }

    public QuestStatus GetMiniGamesStatus(string nameGame)
    {
        foreach(MiniGameStatus mgs in miniGames)
        {
            if (mgs.NameMiniGameScene == nameGame) return mgs.status;
        }
        return QuestStatus.isClosed;
    }
}
