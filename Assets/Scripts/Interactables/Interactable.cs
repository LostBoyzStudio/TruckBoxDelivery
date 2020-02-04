using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;

    private bool isFocus = false;
    private Transform player;

    [SerializeField] private GameObject panelMessage;

    void Update()
    {
        if (isFocus)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            if(distance < radius)
            {
                panelMessage.SetActive(true);

                if (Input.GetMouseButtonDown(1))
                    Interact();
            }
        }
        else
        {
            panelMessage.SetActive(false);
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
    }

    // base class for interactions
    public virtual void Interact()
    {
        Debug.Log("Interacting with: " + transform.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}