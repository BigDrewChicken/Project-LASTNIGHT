using UnityEngine;

public class BobbingItem : MonoBehaviour
{
    public float amplitude = 0.25f; 
    public float frequency = 1f;   

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency * Mathf.PI * 2) * amplitude;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}
