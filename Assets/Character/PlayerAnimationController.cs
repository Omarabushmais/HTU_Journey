using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Mouse X");

        // Speed is based on vertical input
        float speed = Mathf.Abs(v) > 0.1f ? (Input.GetKey(KeyCode.LeftShift) ? 1f : 0.5f) : 0f;

        anim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        anim.SetFloat("StrafeX", h, 0.1f, Time.deltaTime);
        anim.SetFloat("TurnDir", turn, 0.1f, Time.deltaTime);
    }
}