using UnityEngine;

public class ShootBall : MonoBehaviour
{
    #region Variables

    [Header("Ball Ref")]
    public Rigidbody rigidbodyBallRef;
    private float BallShootDelay = 1.5f;
    private float ForceValue = 50;

    #endregion    

    #region Collision Detection Methods    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(MoveBall), BallShootDelay);
        }
    }   

    public void MoveBall()
    {
        rigidbodyBallRef.AddForce(rigidbodyBallRef.transform.forward * ForceValue, ForceMode.Impulse);
    }

    #endregion
}
