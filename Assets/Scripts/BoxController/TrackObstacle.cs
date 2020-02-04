using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObstacle : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GameObject box = collision.collider.gameObject;
        BoxTracker tracker = box.GetComponent<BoxTracker>();
        if (tracker && tracker.boxCtrl) {
            //detectar se a velocidade do tracker está na direção desse objeto
            if (this.gameObject == tracker.aboutToHit) {
                tracker.LeaveTrack();
                //tracker.Stuck();
            }
        }
    }
}