using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using TMPro;

public class halfScript : MonoBehaviour
{
    Animator animator;
    float menzil = 10f, mesafe, pushPower = 30000f;
  
    NavMeshAgent agent;
   public Vector3 random;

    private bool dirRight = true;
    public float speed = 10f;

    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 12.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= -12.0f)
        {
            dirRight = true;
            Quaternion.Euler(0, 180, 0);
        }
    }
    IEnumerator halfDondurme(float speed,int carpim,float frame)
    {
        for(int i=0; i < carpim; i++)
        {
            transform.Rotate(0, speed, 0);
            yield return new WaitForSeconds(frame);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
