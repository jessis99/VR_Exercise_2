using UnityEngine;

public class PulsierenA : MonoBehaviour
{
    private float frequency = 1f;     // Frequenz in Hz
    private float amplitude = 0.5f;   // Stärke des Pulsierens
    private Vector3 baseScale = new Vector3(1,1,1); // Ausgangsgröße

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.Sin(2 * Mathf.PI * frequency * Time.time) * amplitude;
        Vector3 newScale = baseScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);
        transform.localScale = newScale;
    }
}