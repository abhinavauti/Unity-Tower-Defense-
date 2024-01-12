using System.Collections;
using System.Collections.Generic;
using TowerDefense.Level;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScoreboard : MonoBehaviour
{
    private WaveManager waveManager;
    public List<float> elapsedTimes = new List<float>();  // List to store elapsed times
    public Text bestTimesText;  // Reference to your UI text element

    private void Start()
    {
        waveManager = WaveManager.instance;
        WaveManager.instance.spawningCompleted += UpdateBestTimesText;
    }
        // Method to add a new elapsed time to the list
        public void AddElapsedTime(float elapsedTime)
    {
        elapsedTimes.Add(elapsedTime);
    }

    private void Update()
    {
        UpdateBestTimesText();
    }

    // Method to display the best 5 times
    public void DisplayBestTimes()
    {
        SortElapsedTimes();
        // Sort the list in ascending order

        string bestTimes = "Best Times:\n";
        for (int i = 0; i < Mathf.Min(5, elapsedTimes.Count); i++)
        {
            bestTimes += (i + 1) + ": " + elapsedTimes[i].ToString("F2") + " seconds\n";
        }

        bestTimesText.text = bestTimes;  // Update the UI text element
    }

    public void SortElapsedTimes()
    {
        // Sorts the elapsed times in ascending order
        elapsedTimes.Sort();
    }

    private void UpdateBestTimesText()
    {
        // This method will be called when the spawningCompleted event is triggered.
        // Here you can update the BestTimesText UI.

        // For example, if you are tracking time with Time.time
        float elapsedTime = GameTimer.instance.ElapsedTime;

        // Format the time as you wish.
        // Here it's formatted as minutes:seconds.
        string minutes = ((int)elapsedTime / 60).ToString();
        string seconds = (elapsedTime % 60).ToString("f2"); // f2 formats the value with 2 decimal places.

        bestTimesText.text = "Best Time: " + minutes + ":" + seconds;
    }

    // Don't forget to unsubscribe from the event when the object is destroyed
    private void OnDestroy()
    {
        if (waveManager != null)
            waveManager.spawningCompleted -= UpdateBestTimesText;
    }
}