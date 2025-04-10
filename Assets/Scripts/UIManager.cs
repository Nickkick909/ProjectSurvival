using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject eventLogLine;
    [SerializeField] GameObject eventLog;
    [SerializeField] List<string> eventList;
    [SerializeField] List<GameObject> eventLineList;

    const int EVENT_LOG_OFFSET = 32;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Inventory.pickUpItem += AddItemToInventory;
    }

    private void OnDisable()
    {
        Inventory.pickUpItem -= AddItemToInventory;
    }

    void AddItemToInventory(InventoryObject itemToAdd)
    {
        eventList.Add("+" + itemToAdd.quantity + " " + itemToAdd.type );
        GameObject eventText = Instantiate(eventLogLine, eventLog.transform);
        eventText.transform.position = new Vector3(0, EVENT_LOG_OFFSET*eventList.Count, 0) + eventText.transform.position; 
        eventText.GetComponent<TMP_Text>().text = "+" + itemToAdd.quantity + " " + itemToAdd.type;
        eventLineList.Add(eventText);

        StartCoroutine(RemoveEventListItem(eventText));
    }

    IEnumerator RemoveEventListItem(GameObject eventListItem)
    {
        yield return new WaitForSeconds(5);
        Destroy(eventListItem);
        eventList.RemoveAt(0);
        eventLineList.RemoveAt(0);

        for (int i = 0; i < eventLineList.Count; i++)
        {
            eventLineList[i].transform.position -= new Vector3(0, 32, 0);
        }

    }
}
