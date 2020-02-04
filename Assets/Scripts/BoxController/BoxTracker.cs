using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumTracker 
{
    bound,
    unbound,
    stuck
}

public class BoxTracker : MonoBehaviour
{
    // tem suporte para múltiplas esteiras, basta substituir-se o boxCtrl
    public BoxMovementController boxCtrl;
    public EnumTracker state = EnumTracker.bound;
    public Vector3 velocity;
    public int previousId = 0;
    public Rigidbody rigidbody;
    public GameObject aboutToHit;

    void Awake() 
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        //verificar se ainda colide, caso contrário tentar BackTrack
        if (rigidbody.SweepTest(velocity, out hit, 1f)) {
            aboutToHit = hit.collider.gameObject;
            //certificar que o objeto mais próximo tem Rigidbody (não é estático)
            if (hit.distance > 0.1f) {
                //como ainda não houve a colisão, vamos voltar para esteira
                BackTrack();
            }
            else {
                //imediatamente antes à colisão, vamos sair da esteira
                LeaveTrack();
            }
        }
        else {
            //caso o próximo objeto está muito distante, vamos voltar para a esteira
            //Debug.Log("1");
            //BackTrack();
        }
    }

    public void SwitchTrack(BoxMovementController newBoxCtrl)
    {
        // por enquanto vamos começar do começo, no futuro podemos calcular uma transição
        previousId = 0;
        boxCtrl = newBoxCtrl;
        boxCtrl.boxes.Add(this.gameObject);
        rigidbody.isKinematic = true;
        state = EnumTracker.bound;
    }

    public void LeaveTrack()
    {
        if (boxCtrl && state != EnumTracker.unbound) {
            //Debug.Log("Leave");
            //Debug.Log(this + " saiu!");
            boxCtrl.removeBoxes.Add(this.gameObject);
            rigidbody.isKinematic = false;
            rigidbody.velocity = velocity;
            state = EnumTracker.unbound;
        }
    }

    public void BackTrack()
    {
        if (boxCtrl && state != EnumTracker.bound) {
            //Debug.Log("Back");
            //Debug.Log(this + " voltou!");
            boxCtrl.boxes.Add(this.gameObject);
            rigidbody.isKinematic = true;
            state = EnumTracker.bound;
        }
    }

    public void Stuck() 
    {
        state = EnumTracker.stuck;
        rigidbody.isKinematic = true;
    }
}