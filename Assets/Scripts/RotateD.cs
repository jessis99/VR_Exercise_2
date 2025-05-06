using UnityEngine;

public class RotateD : MonoBehaviour
{
    [Range(-180f, 180f)]
    public float rotationSpeed = 90f; // Grad pro Sekunde
    public GameObject vogel;
    private bool lookingRight = true;

    void Update()
    {
        
        //vogel.transform.rotation = Quaternion.Euler(0f, -50f, 0f);

        // Rotiert das Objekt um die y-Achse 
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        if(rotationSpeed < 0 && lookingRight){
            FlipVogel(false);
        }else if(rotationSpeed > 0 && !lookingRight){
            FlipVogel(true);
        }

    }

    void FlipVogel(bool right)
    {
        lookingRight = right;
        vogel.transform.Rotate(0f, -180f, 0f);
/*
        Vector3 scale = vogel.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (right ? 1 : -1);
        vogel.transform.localScale = scale;*/
    }
}
