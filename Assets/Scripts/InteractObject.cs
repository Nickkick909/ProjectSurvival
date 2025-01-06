using UnityEngine;

public class InteractObject : MonoBehaviour
{
    Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        InteractWithObject.canInteractWithObject += HighLightObject;
    }

    private void OnDisable()
    {
        InteractWithObject.canInteractWithObject -= HighLightObject;
    }

    public void HighLightObject()
    {
        outline.enabled = true;
    }

    public void RemoveHighLight()
    {
        outline.enabled = false;
    }
}
