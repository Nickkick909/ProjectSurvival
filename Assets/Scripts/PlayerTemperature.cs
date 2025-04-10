using System.Collections.Generic;
using UnityEngine;

public class PlayerTemperature : MonoBehaviour
{
    [SerializeField] int comfortTempMax = 75;
    [SerializeField] int comfortTempMin = 65;

    // TODO: Add in ranges for slightly hot, really hot, overheating, slightly cold, really cold, hypothermic

    [SerializeField] bool isOutside = true;

    [SerializeField] float currentTemp = 70;

    [SerializeField] List<HeatSource> heatSourcesInRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTemp = comfortTempMax;   
    }

    // Update is called once per frame
    void Update()
    {
        ManageBodyTemp();
    }

    void ManageBodyTemp()
    {
        float averageTemp = 0;
        int tempSources = 1; // 1 is for the ambient weather;

        float ambientTemp = Weather.instance.GetCurrentTemp();

        averageTemp += ambientTemp;

        if (heatSourcesInRange.Count > 0)
        {
            // Find distance to heat source
            // Find temp of heat source ??
            // Use those to find how much heat we receive

            foreach (HeatSource hs in  heatSourcesInRange)
            {
                averageTemp += hs.GetTempAffectOnPlayer(transform.position);
            }
            
        }

       

        // Average all temps together ?
        tempSources += heatSourcesInRange.Count;
        averageTemp /= tempSources;

        //Debug.Log("Ambient temp: " + ambientTemp + "Average Temp: " + averageTemp);

        //currentTemp = averageTemp;


        float tempDiff = averageTemp - currentTemp;

        Debug.Log("temp diff: " +  tempDiff + " current tem: " + currentTemp);

        currentTemp += tempDiff;

        //if (currentTemp > averageTemp)
        //{
        //    currentTemp = averageTemp;
        //}


    }

    private void OnTriggerEnter(Collider other)
    {
        HeatSource heatSource = null;
        if (other.TryGetComponent<HeatSource>(out heatSource))
        {
            heatSourcesInRange.Add(heatSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HeatSource heatSource = null;
        if (other.TryGetComponent<HeatSource>(out heatSource))
        {
            heatSourcesInRange.Remove(heatSource);
        }
    }
}
