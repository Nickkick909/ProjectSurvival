using Unity.VisualScripting;
using UnityEngine;

public class HeatSource : MonoBehaviour
{
    [SerializeField] float tempDropOff = 20;
    [SerializeField] float temp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTempAffectOnPlayer(Vector3 playerPosition)
    {
        float ambientTemp = Weather.instance.GetCurrentTemp();

        Vector3 positionDiff = playerPosition - transform.position;

        float straightDifference = Mathf.Sqrt(Mathf.Pow(positionDiff.x, 2) + Mathf.Pow(positionDiff.z, 2));

        float tempEffect = -(tempDropOff * straightDifference) + temp;

        if (tempEffect < ambientTemp)
        {
            tempEffect = ambientTemp;
        }

        return tempEffect;
    }

}
