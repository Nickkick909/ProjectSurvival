using UnityEngine;

public class Weather : MonoBehaviour
{

    [SerializeField] float[] todaysTempsPerHour;
    [SerializeField] float currentTemp;

    public static Weather instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Make this an event trigger so we aren't always checking for no reason
        SetCurrentTemp();
    }

    public float GetCurrentTemp()
    {
        return currentTemp;
    }

    void SetCurrentTemp()
    {
        currentTemp = todaysTempsPerHour[TimeManager.instance.GetHour()];
    }
}
