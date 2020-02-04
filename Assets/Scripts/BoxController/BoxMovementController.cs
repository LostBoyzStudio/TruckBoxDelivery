using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovementController : MonoBehaviour
{
    [SerializeField] private Lever lever;

    public Transform[] waypoints;
    public List<GameObject> boxes;
    public List<GameObject> removeBoxes;
    public float speed;

    void Start() {
        lever.active += ChangeState;
    }

    void OnDestroy()
    {
        lever.active -= ChangeState;
    }

    public void ChangeState() {
        if (speed > 0) {
            speed = 0;
        }
        else {
            speed = 3;
        }
    }

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        // utilizando pattern similar à Entity - System - Component
        for (int i = 0; i < boxes.Count; i++) {
            Transform previous, next;
            Vector3 position;
            GameObject box = boxes[i];
            Rigidbody rbox = box.GetComponent<Rigidbody>();
            BoxTracker tracker = box.GetComponent<BoxTracker>();
            
            // se o tracker.previousId for o último dos waypoints, destruir box
            if (tracker.previousId >= waypoints.Length - 1) {
                tracker.LeaveTrack();
                tracker.boxCtrl = null;
                continue;
            }

            // se ainda tiver algum waypoint seguinte, mover box na sua direção
            position = box.transform.position;
            previous = waypoints[tracker.previousId];
            next = waypoints[tracker.previousId + 1];

            box.transform.position = Vector3.MoveTowards(position, next.position, step);
            tracker.velocity = position != next.position ? speed * (next.position - position).normalized : tracker.velocity;
            // se a distância do box e do próximo waypoint for aproximadamente igual,
            if (Vector3.Distance(position, next.position) < 0.1f) {
                // incrementa o tracker.previousId, e move para o waypoint
                tracker.previousId++;
            }
        }
        // remover do BoxMovementController as caixas que saíram da esteira
        for (int i = 0; i < removeBoxes.Count; i++) {
            GameObject box = removeBoxes[i];
            boxes.Remove(box);
        }
        removeBoxes = new List<GameObject>();
    }
}