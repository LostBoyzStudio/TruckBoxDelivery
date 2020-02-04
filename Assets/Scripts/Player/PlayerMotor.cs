using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void MoveToPoint(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void FollowTarget(Interactable newTarget)
    {
        // stop right after the target interaction radius
        agent.stoppingDistance = newTarget.radius;
        // set the target
        target = newTarget.transform;
    }

    public void StopFollowingTarget()
    {
        // stop at 0 distance from any point
        agent.stoppingDistance = 0f;
        // remove the target
        target = null;
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRtotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRtotation, Time.deltaTime * 5f);
    }
}