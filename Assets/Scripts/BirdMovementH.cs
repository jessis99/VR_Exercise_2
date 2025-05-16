using UnityEngine;

public class BirdMovementH : MonoBehaviour
{

    [SerializeField] float birdSpeed = 2f;

    Vector3 targetTreePosition;
    GameObject targetTree;

    TreeSpawner treeSpawner;

    private bool selectedTree;
    private bool flyAroudTree;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (treeSpawner == null)
        {
            treeSpawner = FindObjectOfType<TreeSpawner>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // New method to set the target tree from the pointer
    public void SetTargetTree(GameObject tree)
    {
        if (tree != null)
        {
            targetTree = tree;
            targetTreePosition = tree.transform.position;
            targetTreePosition.y = 3; // Adjust height for bird flight
            
            // Mark the tree as chosen by the bird
            TreeLifecycle treeLifecycle = tree.GetComponent<TreeLifecycle>();
            if (treeLifecycle != null)
            {
                treeLifecycle.birdChoseThisTree = true;
                MoveBirdToTree();
            }
            
            //birdHasTree = true;
            //closeToTree = false;
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
            //closeToTree = true;
        }
    }
}
