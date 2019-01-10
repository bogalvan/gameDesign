using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player; //refrence to the player beacuse that is what we are trying to look at
    private Vector3 offset; //current position of the camera - transform position of the camera 

    void Start()
    {
        offset = transform.position - player.transform.position; //original dstance from camera to player 
    }

    //Follow cameras use late update because it runs every frame but is waits untill all other objects have been updated
    void LateUpdate()
    {
        transform.position = player.transform.position + offset; //cameras new position is curretn player position + the original offset 
    }
}