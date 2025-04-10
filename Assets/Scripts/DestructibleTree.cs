using UnityEngine;

public class DestructibleTree : InteractObject
{

    [SerializeField] int durability = 5;
    [SerializeField] DroppedItem[] itemDrops;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public override void HandleInteract()
    {
        durability -= 1;

        if (durability < 1)
        {

            HandleItemDrop();
        }
        else
        {
            Debug.Log("Reduce Durablity to :" + durability);
        }
    }

    public void HandleItemDrop()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null) 
            collider.enabled = false;

        Collider childCollider = GetComponentInChildren<Collider>();
        if (childCollider != null)
            childCollider.enabled = false;

        for (int i = 0; i < itemDrops.Length; i++)
        {
            int numberDropped = 0;

            for (int j = 0; j < itemDrops[i].maxDrop; j++)
            {
                if (itemDrops[i].minDrop > numberDropped)
                {
                    Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(0.5f, 2), transform.position.y + Random.Range(2, 3), transform.position.z + Random.Range(0.5f, 2));
                    Debug.Log("drop position:" + dropPosition);
                    Instantiate(itemDrops[i].droppedItem, dropPosition, Quaternion.identity);
                } else
                {
                    int diceRoll = Random.Range(0, 100);
                    if (diceRoll <= itemDrops[i].extraDropChance)
                    {
                        Vector3 dropPosition = new Vector3 (transform.position.x + Random.Range(0.5f,2), transform.position.y + Random.Range(2, 3), transform.position.z + Random.Range(0.5f, 2));
                        Debug.Log("drop position:" + dropPosition);
                        Instantiate(itemDrops[i].droppedItem, dropPosition, Quaternion.identity);
                    } else
                    {
                        break;
                    }
                }
                numberDropped++;
            }
            

            
        }

        Destroy(gameObject);

    }
}

[System.Serializable]
public class DroppedItem
{
    public GameObject droppedItem;
    public int maxDrop;
    public int minDrop;
    public float extraDropChance;
}
