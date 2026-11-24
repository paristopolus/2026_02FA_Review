using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject currentTarget;
    public GameObject InteractiveRootObject;
    [SerializeField] private string targetTag = "Interactable";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("GameManager has started.");
        Debug.Log($"The current scene is: {this.gameObject.scene.name}");
        SetLayerRecursive(InteractiveRootObject,targetTag);
    }

    private void SetLayerRecursive(GameObject obj, string tag)
    {
        if(obj == null) {
            Debug.LogWarning("SetLayerRecursive was called with a null object.");
            return;
        }
        // Set the object and it's children layer to the supplied int.
        obj.tag = tag;
        foreach(Transform child in obj.transform)
        {
            SetLayerRecursive(child.gameObject, tag);
        }
    }

    public void OnQuit()
    {
        Debug.Log("The game is quitting...");
        Application.Quit();
    }

    public void OnInteract()
    {
        /*
        LookCheck();
        Debug.Log($"You seem to be interacting with {currentTarget.gameObject.name}");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(
            ray.origin,
            ray.direction * 100f,
            Color.red
        );
        */
    }

    private void Update()
    {
        // LookCheck();
    }

    private void LookCheck()
    {
        Camera cam = Camera.main;

        if (cam == null)
            return;

        // Create a ray from the center of the screen
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Looking at: " + hit.collider.gameObject.name);
            // currentTarget = hit.collider.gameObject;
            currentTarget = hit.collider.gameObject.CompareTag(targetTag) ? hit.collider.gameObject : null;

        }
    }
}
