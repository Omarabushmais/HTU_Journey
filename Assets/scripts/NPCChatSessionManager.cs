using UnityEngine;
using System;
using System.Collections.Generic;

public class NPCChatSessionManager : MonoBehaviour
{
    public static NPCChatSessionManager Instance;

    private Dictionary<string, string> npcChatIds = new Dictionary<string, string>();
    private string playerId;

    private const string PlayerIdKey = "PLAYER_ID";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadOrCreatePlayerId();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadOrCreatePlayerId()
    {
        if (PlayerPrefs.HasKey(PlayerIdKey))
        {
            playerId = PlayerPrefs.GetString(PlayerIdKey);
        }
        else
        {
            playerId = Guid.NewGuid().ToString();
            PlayerPrefs.SetString(PlayerIdKey, playerId);
            PlayerPrefs.Save();
        }
    }

    public string GetPlayerId()
    {
        return playerId;
    }

    public string GetOrCreateChatId(string npcId)
    {
        if (npcChatIds.ContainsKey(npcId))
            return npcChatIds[npcId];

        string newChatId = Guid.NewGuid().ToString();
        npcChatIds[npcId] = newChatId;
        return newChatId;
    }

    public string ClearChatAndCreateNew(string npcId)
    {
        string newChatId = Guid.NewGuid().ToString();
        npcChatIds[npcId] = newChatId;
        return newChatId;
    }

    public string GetCurrentChatId(string npcId)
    {
        if (npcChatIds.ContainsKey(npcId))
            return npcChatIds[npcId];

        return null;
    }
}