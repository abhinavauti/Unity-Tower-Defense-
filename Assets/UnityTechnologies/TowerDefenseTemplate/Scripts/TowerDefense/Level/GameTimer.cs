using System.Collections;
using System.Collections.Generic;
using TowerDefense.Level;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    private float startTime;

    public bool isWaveStarted { get; private set; } = false;
    public float ElapsedTime => isWaveStarted ? Time.time - startTime : 0f;



    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("Multiple instances of GameTimer!");
            return;
        }
        instance = this;
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isWaveStarted = true;
    }

    public void StopTimer()
    {
        isWaveStarted = false;
    }
}