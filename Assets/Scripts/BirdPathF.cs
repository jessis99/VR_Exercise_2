using UnityEngine;

public class BirdPathF : MonoBehaviour
{
    [HideInInspector] public bool birdHasTree;
    bool closeToTree;

    // Bird rotation
    [Range(-180f, 180f)] public float rotationSpeed = 90f; // Grad pro Sekunde
    Vector3 rotationAxis;

    [SerializeField] float birdSpeed = 2f;

    Vector3 biggestTreePosition;

    TreeSpawner treeSpawner;

    void Start()
    {
        if (treeSpawner == null)
        {
            treeSpawner = FindObjectOfType<TreeSpawner>();
        }

        rotationAxis = new Vector3(0, 1, 0);
    }

    void Update()
    {
        if(birdHasTree)
        {
            if(closeToTree)
            {
                // Baum umkreisen
                RotateBird();
            }
            else
            {
                // zu groesstem Baum hinfliegen
                MoveBirdToTree();
            }
        }
        else
        {
            biggestTreePosition = treeSpawner.BiggestTreePosition();
            biggestTreePosition.y = 3;
            birdHasTree = true;
            closeToTree = false;
        }
    }

    void MoveBirdToTree()
    {
        if(Vector3.Distance(transform.position, biggestTreePosition) >= 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, biggestTreePosition, birdSpeed*Time.deltaTime);
            transform.LookAt(biggestTreePosition);
        }
        else
        {
            closeToTree = true;
        }
    }

    void RotateBird()
    {
        // Rotiert das Objekt um den Baum (y-Achse) 
        transform.RotateAround(biggestTreePosition, rotationAxis, rotationSpeed * Time.deltaTime);
        transform.LookAt(biggestTreePosition);
    }
}