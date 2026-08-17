using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator anim; 
    private CharacterController controller;
    private Vector3 velocity; 
    public float gravity = -20f; // Збільшив до -20 для більш "важкого" і реалістичного падіння

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 1. Отримуємо ввід
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        // 2. Логіка руху (Horizontal)
        bool isMoving = move.magnitude > 0.1f;

        if (isMoving)
        {
            controller.Move(move * speed * Time.deltaTime);
            transform.forward = move;
        }

        // 3. Анімація
        if (anim != null)
        {
            anim.SetBool("isWalking", isMoving);
        }

        // 4. ГРАВІТАЦІЯ (Ось тут було виправлено назву на controller)
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Притискаємо до землі
        }

        velocity.y += gravity * Time.deltaTime;

        // Рухаємо гравця вниз (застосовуємо гравітацію)
        controller.Move(velocity * Time.deltaTime);
    }
}