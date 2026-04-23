using UnityEngine;

public class MobileUiManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileUI;

    private void Start()
    {
        if (Application.isMobilePlatform)
        {
            mobileUI.SetActive(true);
        }
        else
        {
            mobileUI.SetActive(false);
        }
    }
}