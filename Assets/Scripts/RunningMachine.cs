using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMachine : MonoBehaviour
{
    private Transform start;
    private Transform end;

    [SerializeField] private bool reverse;

    public float speed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.Find("Start");
        end = transform.Find("End");

        reverse = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (end.position - start.position).normalized;

        direction = (reverse == true) ? direction * 1 : direction * -1;

        Vector3 offset = new Vector3(0.0f, 0.0001f, 0.0f);
        RaycastHit[] hits = Physics.RaycastAll(start.position + offset, end.position + offset);

        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i].transform.GetComponent<Rigidbody>() != null)
                hits[i].transform.GetComponent<Rigidbody>().velocity = transform.right * speed * Time.deltaTime;
        }
    }
}