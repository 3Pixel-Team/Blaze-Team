using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[GameManager] There is more than one GM Instance");
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        GameManager.Instance.LoadUIGameplay();
    }
}
