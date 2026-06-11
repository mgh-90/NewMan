using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    private CharacterController controller;
    private Vector3 move;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        controller.Move(move * walkSpeed * Time.deltaTime);
    }
}