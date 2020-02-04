using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTest : MonoBehaviour
{
    public TrackSwitch ts;
    
    void Update()
    {
        if (ts.mySwitch) {
            Debug.Log("direita");
        }
        else {
            Debug.Log("esquerda");
        }
        if (Input.GetKeyDown("space")) {
            if (ts.mySwitch) {
                ts.mySwitch = false;
            }
            else {
                ts.mySwitch = true;
            }
        }
    }
}
