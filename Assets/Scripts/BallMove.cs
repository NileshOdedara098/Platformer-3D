using UnityEngine;

public class BallMove : MonoBehaviour
{

    #region Variables

    [Header("Ball Ref")]
    public Rigidbody rigidbodyBallRef;
    private float ForceValue = 25f;

    #endregion

    #region Collision Detection Methods    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            MoveBall();
        }
    }

    public void MoveBall()
    {
        rigidbodyBallRef.AddRelativeForce(new Vector3(0f, 2f * ForceValue, 1.25f * ForceValue), ForceMode.Impulse);
    }

    #endregion
    
}
