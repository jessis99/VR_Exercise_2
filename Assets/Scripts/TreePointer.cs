using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class TreePointer : MonoBehaviour
{
    [SerializeField] private XRNode controllerNode = XRNode.RightHand; // Choose which hand
    [SerializeField] private float maxPointDistance = 200f;
    [SerializeField] private LayerMask treeLayer; // Make sure trees are on a specific layer
    
    [SerializeField] private LineRenderer pointerLine;
    [SerializeField] private BirdMovementH birdPath;
    
    private bool isPointing = false;
    private GameObject pointedTree = null;
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice targetDevice;
    
    void Start()
    {
        if (birdPath == null)
        {
            birdPath = FindObjectOfType<BirdMovementH>();
        }
        
        if (pointerLine == null && GetComponent<LineRenderer>() != null)
        {
            pointerLine = GetComponent<LineRenderer>();
        }
        
        // Initialize line renderer if it exists
        if (pointerLine != null)
        {
            pointerLine.positionCount = 2;
            pointerLine.enabled = false;
        }
        
        // Initialize the target device
        GetDevice();
    }
    
    void Update()
    {
        if (!targetDevice.isValid)
        {
            GetDevice();
            return;
        }
        
        // Check if trigger button is pressed
        if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
            PerformRaycast();
        }
        else
        {
            // Hide line when not pointing
            if (pointerLine != null)
            {
                pointerLine.enabled = false;
            }
            isPointing = false;
        }
    }
    
    private void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(controllerNode, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    
    private void PerformRaycast()
    {
        // Get controller position and forward direction
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;
        
        // Draw line for visual feedback
        if (pointerLine != null)
        {
            pointerLine.enabled = true;
            pointerLine.SetPosition(0, rayOrigin);
            pointerLine.SetPosition(1, rayOrigin + rayDirection * maxPointDistance);
        }
        
        TreeLifecycle treeLifecycle;

        // Perform raycast to detect trees
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxPointDistance, treeLayer))
        {
            // Check if hit object has TreeLifecycle component
            treeLifecycle = hit.collider.GetComponent<TreeLifecycle>();
            if (treeLifecycle != null)
            {
                treeLifecycle.treeSelected();
                // Update line endpoint to hit position for visual feedback
                if (pointerLine != null)
                {
                    pointerLine.SetPosition(1, hit.point);
                }
                
                // If we hit a tree, tell the bird to fly to it
                if (!isPointing || pointedTree != hit.collider.gameObject)
                {
                    isPointing = true;
                    pointedTree = hit.collider.gameObject;
                    birdPath.SetTargetTree(pointedTree);
                }
            }else{
                treeLifecycle.treeUnselected();
            }
        }
    }

    void selectTree(){

    }

    void OnHover(){

    }
}
