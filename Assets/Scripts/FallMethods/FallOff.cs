using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

// Tried making that the cube would be the respawn but was getting nowhere so made it old fashion way.
// This script is left to remind me of my suffering.

public class FallOff : MonoBehaviour
{
    [Header("Respawn Settings")]
    [Tooltip("Drag the object where the player should respawn into this slot.")]
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell off! Starting respawn timer...");
            StartCoroutine(Respawn(other.gameObject));
        }
    }

    private IEnumerator Respawn(GameObject player)
    {
        yield return new WaitForSeconds(respawnDelay);

        if (respawnPoint != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            
            if (cc != null)
            {
                cc.enabled = false;
                player.transform.position = respawnPoint.position;
                
                PlayerControls movementScript = player.GetComponent<PlayerControls>();
                if (movementScript != null)
                {
                    movementScript.ResetVelocity();
                }

                cc.enabled = true;
            }
            else
            {
                player.transform.position = respawnPoint.position;

                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }

            Debug.Log("Player respawned!");
        }
        else
        {
            Debug.LogError("Respawn Point is not assigned in the FallOff script!");
        }
    }
}