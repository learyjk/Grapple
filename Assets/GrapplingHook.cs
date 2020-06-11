using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer line;
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 10f;
    public float step = 0.2f;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Grapple();
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("moused!");
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos-transform.position, distance, mask);
            Debug.Log(hit.collider.gameObject.GetComponent<Rigidbody2D>());

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
        }
    }

    //Pulls Player toward Grapple point and releases the grapple when close.
    void Grapple()
    {
        if (joint.distance > 1f)
        {
            joint.distance -= step;
        }
        else
        {
            line.enabled = false;
            joint.enabled = false;
        }
    }
}
