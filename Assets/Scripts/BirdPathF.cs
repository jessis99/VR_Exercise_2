using UnityEngine;

public class BirdPathF : MonoBehaviour
{
    [HideInInspector] public bool birdHasTree;
    bool closeToTree;

    // Bird rotation
    [Range(-180f, 180f)] public float rotationSpeed = 90f; // Grad pro Sekunde
    Vector3 rotationAxis;

    [SerializeField] float birdSpeed = 2f;

    Vector3 targetTreePosition;
    GameObject targetTree;

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
                // zu Baum hinfliegen
                MoveBirdToTree();
            }
        }
        else
        {
            // Default behavior when no tree is selected - find biggest tree
            FindBiggestTree();
        }
    }

    void MoveBirdToTree()
    {
        if(Vector3.Distance(transform.position, targetTreePosition) >= 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTreePosition, birdSpeed*Time.deltaTime);
            transform.LookAt(targetTreePosition);
        }
        else
        {
            closeToTree = true;
        }
    }

    void RotateBird()
    {
        // Rotiert das Objekt um den Baum (y-Achse) 
        transform.RotateAround(targetTreePosition, rotationAxis, rotationSpeed * Time.deltaTime);
        transform.LookAt(targetTreePosition);
    }
    
    // Find the biggest tree when no tree is selected
    private void FindBiggestTree()
    {
        targetTreePosition = treeSpawner.BiggestTreePosition();
        targetTreePosition.y = 3;
        birdHasTree = true;
        closeToTree = false;
    }
    
    // Called when the current tree is destroyed
    public void OnTreeDestroyed()
    {
        birdHasTree = false;
        closeToTree = false;
        targetTree = null;
    }
}
