using System.Collections;
using System.Collections.Generic;
using TowerDefense.Level;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public GameObject bestTimesParent;
    public EndGameScoreboard endGameScoreboard;
    public WaveManager waveManager; // Reference to your WaveManager script
    public Text elapsedTimeText; // Reference to your UI text element

    private float[] bestTimes = new float[5] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
    public Text[] bestTimesTexts;

    private float startTime; // Variable to store the start time
    private bool hasWaveStarted = false;

    private void Awake()
    {
        // Load the best times from PlayerPrefs
        for (int i = 0; i < bestTimes.Length; i++)
        {
            bestTimes[i] = PlayerPrefs.GetFloat("BestTime" + i, float.MaxValue);
        }
    }

    private void Start()
    {
        // Initialize the start time when the game starts
        startTime = Time.time;
        bestTimesParent.SetActive(false);
    }

    private void Update()
    {
        if (hasWaveStarted)
        {
            // Calculate the elapsed time
            float elapsedTime = Time.time - startTime;

            // Format the elapsed time into minutes and seconds
            string minutes = ((int)elapsedTime / 60).ToString();
            string seconds = (elapsedTime % 60).ToString("f2"); // "f2" formats the value to 2 decimal places

            // Update the UI text element with the elapsed time
            elapsedTimeText.text = "Time Elapsed: " + minutes + ":" + seconds;
        }
    }

    public void StartWave()
    {
        startTime = Time.time;
        hasWaveStarted = true; // Start the timer
    }

    public void SetStartTime(float time)
    {
        startTime = time;
    }

    public void UpdateElapsedTime()
    {
        if (hasWaveStarted)
        {
            float elapsedTime = Time.time - startTime;
            elapsedTimeText.text = "Elapsed Time: " + elapsedTime.ToString("F2");  // F2 to keep only two decimal points
        }
    }

    public void EndGame()
    {
        float finalElapsedTime = Time.time - startTime;
        endGameScoreboard.AddElapsedTime(finalElapsedTime);

        for (int i = 0; i < bestTimes.Length; i++)
        {
            if (finalElapsedTime < bestTimes[i])
            {
                // Shift the rest of the times down
                for (int j = bestTimes.Length - 1; j > i; j--)
                {
                    bestTimes[j] = bestTimes[j - 1];
                }
                // Insert the new best time
                bestTimes[i] = finalElapsedTime;
                break;
            }
        }

        // Save the best times to PlayerPrefs
        for (int i = 0; i < bestTimes.Length; i++)
        {
            PlayerPrefs.SetFloat("BestTime" + i, bestTimes[i]);
        }
    }

    public void ShowBestTimes()
    {
        // Format and display the best times
        for (int i = 0; i < bestTimes.Length; i++)
        {
            if (bestTimes[i] != float.MaxValue)
            {
                string minutes = ((int)bestTimes[i] / 60).ToString("00");
                string seconds = (bestTimes[i] % 60).ToString("00.00");
                bestTimesTexts[i].text = "Best Time " + (i + 1) + ": " + minutes + ":" + seconds;
            }
            else
            {
                bestTimesTexts[i].text = "Best Time " + (i + 1) + ": --:--";
            }
        }
        bestTimesParent.SetActive(true);
    }
}
