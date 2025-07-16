using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables

    [Header("Camera Refs")]
    public Rigidbody MyRigidbodyref;
    public Rigidbody targetRigidBodyRef;    
    public Vector3 offset;    

    public float Speed;

    #endregion

    #region Unity Default Methods    

    //Update is called once per frame.
    void FixedUpdate()
    {              
        float interpolation = Speed * Time.deltaTime;

        Vector3 position = MyRigidbodyref.position;
        position.z = Mathf.Lerp(MyRigidbodyref.position.z, targetRigidBodyRef.position.z + offset.z, interpolation);
        position.y = Mathf.Lerp(MyRigidbodyref.position.y, targetRigidBodyRef.position.y + offset.y, interpolation);
        position.x = 0f;        
        MyRigidbodyref.position = position;
    } 

    #endregion
}
