using System;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TimeManager : MonoBehaviour
{
    const int MINUTUES_IN_DAY = 1440;
    const float START_OF_FIRST_DAY = 420;
    [SerializeField] Light sunLight;
    [SerializeField] Gradient sunGradient;
    [SerializeField] Gradient ambientGradient;
    [SerializeField, Range(0, MINUTUES_IN_DAY)] float timeOfDay = START_OF_FIRST_DAY;


    
    [SerializeField, Tooltip("How many real time seconds = 1 minute")] float timeSpeedFactor = 5.0f;

    [SerializeField] TMP_Text timeDisplay;


    private void Start()
    {
        timeOfDay = PlayerPrefs.GetFloat("timeOfDay", START_OF_FIRST_DAY);
    }
    private void Update()
    {
        if (Application.isPlaying)
        {
            // Delta time is in seconds in seconds
            // We want each day to be 24 minutes (instead of 24 hours)
            timeOfDay += Time.deltaTime / timeSpeedFactor; 
            timeOfDay %= MINUTUES_IN_DAY;
        }

        UpdateLighting(timeOfDay / MINUTUES_IN_DAY);
        UpdateWatchTime(Mathf.FloorToInt(timeOfDay));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("timeOfDay", timeOfDay);
    }

    private void UpdateLighting(float time)
    {
        RenderSettings.ambientLight = ambientGradient.Evaluate(time);
        sunLight.color = sunGradient.Evaluate(time);
        sunLight.transform.rotation = Quaternion.Euler(new Vector3((time * 360) - 90, 180f, 0));
    }

    private void UpdateWatchTime(int time)
    {
        int hour = time / 60;
        int minute = time % 60;

        string amOrPm = "";
        if (hour < 12)
        {
            amOrPm = "AM";
            
        }
        else
        {
            amOrPm = "PM";
            hour -= 12;
        }

        if (hour == 0)
        {
            hour = 12;
        }

        string watchDisplayString = String.Format("{0:00}:{1:00}", hour, minute);

        timeDisplay.text = watchDisplayString + " " + amOrPm;
    }
}
