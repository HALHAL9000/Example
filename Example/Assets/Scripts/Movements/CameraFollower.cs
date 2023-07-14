using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform targetSimulation,targetRunner;
    [SerializeField] float movementTime = 0.3f;
    [SerializeField] float rotationSpeed = 4.2f;

    Vector3 refPos;

    private void FixedUpdate()
    {
        if (PlateLevelController.isRunnerStarted == false)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetSimulation.position, ref refPos, movementTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetSimulation.rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetRunner.position, ref refPos, movementTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRunner.rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
