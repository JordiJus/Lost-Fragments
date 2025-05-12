using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PickaxeTrigger : MonoBehaviour
{
    public TerrainTerraforming terrainDig;
    public Rigidbody pickaxeRb;
    public XRBaseController controller;
    public float hapticIntensity = 0.5f;
    public float hapticDuration = 0.1f;

    public float speedThreshold = 0.5f; // Minimum velocity to trigger dig
    public string terrainTag = "Terrain";

    private Vector3 lastPosition;
    private Vector3 velocity;
    void Start()
    {
        lastPosition = pickaxeRb.position;
    }
    void Update()
    {
        Vector3 currentPosition = pickaxeRb.position;
        velocity = (currentPosition - lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(terrainTag)) return;

        float speed = velocity.magnitude;
        
        Debug.Log(speed);
        if (speed >= speedThreshold)
        {
            terrainDig.Terraform(transform.position); // Use tip's position
            TriggerFeedback();
        }
    }

    private void TriggerFeedback()
    {
        // Optional: Add particle FX or sound here

        // Haptics (if controller is assigned)
        if (controller != null)
        {
            controller.SendHapticImpulse(hapticIntensity, hapticDuration);
        }
    }
}
