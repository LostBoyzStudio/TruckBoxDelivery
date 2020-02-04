using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitch : MonoBehaviour
{
    public BoxMovementController origin;
    public BoxMovementController normallyOpen;
    public BoxMovementController normallyClosed;
    public bool mySwitch = false;

    void OnCollisionEnter(Collision collision)
    {
        GameObject box = collision.gameObject;
        BoxTracker tracker = box.GetComponent<BoxTracker>();
        if (tracker) {
            if (mySwitch) {
                tracker.SwitchTrack(normallyOpen);
            }
            else {
                tracker.SwitchTrack(normallyClosed);
            }
        }
    }
}
