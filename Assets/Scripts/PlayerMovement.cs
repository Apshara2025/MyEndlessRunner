
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 200f;
    [SerializeField] Animator animator;
    [SerializeField] float speedChange = -0.1f;
    Rigidbody myRigidBody;
    Vector2 input;
    ChunkGeneration chunkGeneration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        Vector3 initialPosition = transform.position;
        chunkGeneration = FindFirstObjectByType<ChunkGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Cube") return;
        animator.SetTrigger("Hit");
        chunkGeneration.SpeedUpLevel(speedChange);

    }


    public void Move(InputAction.CallbackContext context)
    {

        input = context.ReadValue<Vector2>();

    }

    void FixedUpdate()
    {
        Vector2 origin = new Vector2(0f, 0f);
        Vector3 currentPosition = transform.position;
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        
        Vector3 resultingPosition = currentPosition + (moveDirection * moveSpeed * Time.fixedDeltaTime);
        float clampedX = Mathf.Clamp(resultingPosition.x, -2.7f, 2.7f);
        float clampedZ = Mathf.Clamp(resultingPosition.z, -2.5f, 2.5f);
        Vector3 finalPosition = new Vector3(clampedX, transform.position.y, clampedZ);
        if (input != origin)
        {
            myRigidBody.MovePosition(finalPosition);
            
        }
    }

    }


