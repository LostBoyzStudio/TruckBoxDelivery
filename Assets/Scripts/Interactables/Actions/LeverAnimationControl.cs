using UnityEngine;

public class LeverAnimationControl : MonoBehaviour
{
    [SerializeField] private Lever lever;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lever.active += OnActive;
    }

    void OnDestroy()
    {
        lever.active -= OnActive;
    }

    private void OnActive()
    {
        animator.SetTrigger("lever_active");
    }
}