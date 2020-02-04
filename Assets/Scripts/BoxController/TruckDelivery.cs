using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDelivery : MonoBehaviour
{
    public int box1Count = 6;
    public int box2Count = 10;
    public int box3Count = 4;
    public GameObject box1Fab;
    public GameObject box2Fab;
    public GameObject box3Fab;
    public BoxMovementController deliveryTrack;
    public Vector3 position;

    void Start()
    {
        position = deliveryTrack.waypoints[0].position;
        StartCoroutine("Deliver");
    }

    IEnumerator Deliver() {
        GameObject box1, box2, box3;
        for(;;) {
            // escolhe uma das caixas disponíveis
            int total = box1Count + box2Count + box3Count;
            int choose = (int) (Random.Range(0f, 1f) * total);
            if (choose >= 0 && choose < box1Count) {
                box1 = Instantiate(box1Fab);
                box1.transform.position = position;
                box1.GetComponent<BoxTracker>().SwitchTrack(deliveryTrack);
                box1Count--;
            }
            else if (choose >= box1Count && choose < box1Count + box2Count) {
                box2 = Instantiate(box2Fab);
                box2.transform.position = position;
                box2.GetComponent<BoxTracker>().SwitchTrack(deliveryTrack);
                box2Count--;
            }
            else if (choose >= box1Count + box2Count && choose < total) {
                box3 = Instantiate(box3Fab);
                box3.transform.position = position;
                box3.GetComponent<BoxTracker>().SwitchTrack(deliveryTrack);
                box3Count--;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}