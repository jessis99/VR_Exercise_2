using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform[] cameraAnchors; // 0: First, 1: Third, 2: Top-down
    private int currentView = 0;
    private Transform currentAnchor;

    void Start()
    {
        // Initially set the camera to the first anchor (first-person view)
        currentAnchor = cameraAnchors[currentView];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentView = (currentView + 1) % cameraAnchors.Length;
            currentAnchor = cameraAnchors[currentView];
        }

        // Update the camera's position and rotation to follow the anchor
        if (currentAnchor != null)
        {
            transform.position = currentAnchor.position;
            transform.rotation = currentAnchor.rotation;
        }
    }
}
