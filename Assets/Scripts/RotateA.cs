using UnityEngine;

public class RotateA : MonoBehaviour
{
    private float rotationSpeed = 90f; // Grad pro Sekunde

    void Update()
    {
        // Rotiert das Objekt um die y-Achse
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
