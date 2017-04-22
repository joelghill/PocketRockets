using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

    public GameObject target;

    public float MinDepth = 9.0f;

    public float Margin = 1.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            var newPosition = this.target.transform.position;
            newPosition.z = -10;

            this.transform.position = newPosition;

            return;

            float newX;
            float newY;

            Vector3 targetPos = target.transform.position;
            Vector3 cameraPos = this.transform.position;

            float dX = targetPos.x - cameraPos.x;
            float dY = targetPos.y - cameraPos.y;

            newY = targetPos.y;
            newX = targetPos.x;

            this.transform.position = new Vector3(newX, newY);

        }
        else
        {
            Debug.Log("CAMERA IS NULL");
        }
    }
}
