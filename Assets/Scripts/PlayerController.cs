using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    private Rigidbody _rigidbody;   // Used for movements.
    private Vector3 movement_vector;    // Used for movements. Idle: (0,0,0).
    public float movement_speed = 5.0f; // Used for movements. Affects whole character speed.
    public float jump_factor = 20.0f;
    private float timestamp_last_movement;

    [Header("Player position Clamper")]
    public float PlayerXPosClampVector;

    public bool isGrounded = false;
    public bool DoubleJump = false;

    public bool StopInput;

    #endregion    

    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    void Start()
    {                
        _rigidbody = this.GetComponent<Rigidbody>();

        // Sets coroutines execution to false in order to enable first calls.
        isExecuteMovementCoroutineRunning = false;        

        // At beginning you should not run idle animation, so we consider player as if he already did a movement.
        timestamp_last_movement = Time.time;
        
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    void Update()
    {
        if (!StopInput)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                movement_vector.z = movement_speed * 1.0f;    // Go up.
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {                
                movement_vector.x = movement_speed * 1.0f;    // Go right.
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                movement_vector.x = movement_speed * -1.0f;    // Go left.
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    //Debug.Log("On Ground Jump");
                    _rigidbody.AddForce(Vector3.up * jump_factor, ForceMode.Impulse);                    
                }
                else
                {
                    if (DoubleJump == false)
                    {
                        DoubleJump = true;
                        //Debug.Log("On Double Jump");
                        _rigidbody.AddForce(Vector3.up * jump_factor, ForceMode.Impulse);                        
                    }
                }

                timestamp_last_movement = Time.time;
            }
        }               
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {        
        if (movement_vector != Vector3.zero)
        { 
            // If movement vector isn't null...            
            if (!isExecuteMovementCoroutineRunning)
            {
                StartCoroutine(executeMovement()); // Delegate movement to 'async' coroutine function to save performance.
            }
            timestamp_last_movement = Time.time;            
        }
    }

    /*
        executeMovement is a coroutine called from FixedUpdate method to manage efficiently character movement.
        It relies on movement_vector value (set from Update() function).
    */
    private bool isExecuteMovementCoroutineRunning; // This value estabilishes if coroutine is already called.
    IEnumerator executeMovement()
    {
        // Coroutine started running, so the movement vector is != (0,0,0). Let's warn that this coroutine can't be runned twice.
        isExecuteMovementCoroutineRunning = true;        

        // Until the movement vector is not (0,0,0), execute the movement each FixedUpdate frame.
        while (movement_vector != Vector3.zero)
        {
            transform.position += ((movement_vector = movement_vector * Time.deltaTime));  // Make movement and reduce movement_vector on deltaTime.
            transform.position = new Vector3(Mathf.Clamp(transform.position.x,-PlayerXPosClampVector, PlayerXPosClampVector), transform.position.y, transform.position.z);
            if (transform.position.y < 0.75f)
            {
                StopInput = true;
            }

            yield return CoroutineHelper.getInstance().m_WaitForFixedUpdate;   // Wait for next frame elaboration.
        }        

        // Character speed is zero. Coroutine is already going to die. Let's warn that this coroutine can be runned again.
        isExecuteMovementCoroutineRunning = false;
    }

    /*
        This method should check for collisions.
     */
    void OnCollisionEnter(Collision collision)
    {
        // Check if we Found Ground
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            DoubleJump = false;
        }
        else if (collision.collider.CompareTag("CubeFall") || collision.collider.CompareTag("Ball"))
        {
            StopInput = true;
            SceneManager.LoadScene(0);
        }
        // Name of the object who collided with: collision.transform.name.ToString()
        // Name of the layer of the object who collided with: collision.collider.gameObject.layer
        //handleCollision(collision);
    }    

    private void OnCollisionExit(Collision collision)
    {
        // Check if we Found Ground
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
    }    

    public void handleCollision(Collision collision)
    {
        if (collision.contacts[0].thisCollider.name.Equals("GroundCheck"))
        {
            Debug.Log("GroundCheck");
            _rigidbody.AddForce(Vector3.up * jump_factor, ForceMode.Impulse);
        }
        else
        {
            //Debug.Log("PlayerBodyParts");
        }
    }

}