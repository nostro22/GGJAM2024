using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSelector : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.StopEffects();
        AudioManager.Instance.StopMusic();
    }

    public void SelectBoom()
    {
        LoadSceneManager.Instance.LoadNewScene(ScenesIndexes.BOOM);
    }
    
    public void SelectFootball()
    {
        LoadSceneManager.Instance.LoadNewScene(ScenesIndexes.FOOTBALL);
    }
}
