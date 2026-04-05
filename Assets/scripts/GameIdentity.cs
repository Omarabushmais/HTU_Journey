using UnityEngine;
using System;

public class GameIdentity : MonoBehaviour
{
    public static string PlayerId { get; private set; }
    private const string Key = "PLAYER_ID";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(Key))
        {
            PlayerId = PlayerPrefs.GetString(Key);
        }
        else
        {
            PlayerId = Guid.NewGuid().ToString();
            PlayerPrefs.SetString(Key, PlayerId);
            PlayerPrefs.Save();
        }
    }
}