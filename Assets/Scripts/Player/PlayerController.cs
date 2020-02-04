using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMotor motor;

    private Interactable focus;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // click with left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                // player movement to point
                motor.MoveToPoint(hit.point);
                // remove the focus on moving
                RemoveFocus();
            }
        }

        // click with right click
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Interactable")))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                    SetFocus(interactable);
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        // if the focus is different from previous one, set as new
        if(focus != newFocus)
        {
            // if the focus is not null, defocus the player, then focus again
            if (focus != null)
                focus.OnDefocused();
            
            // set the new focus
            focus = newFocus;
            // move towards the focus
            motor.FollowTarget(newFocus);
        }

        // set the player focused in the the new focus
        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        // if the the focus is not null, defocus the player
        if (focus != null)
            focus.OnDefocused();
        // remove the focus
        focus = null;
        // stop following any targets
        motor.StopFollowingTarget();
    }
}