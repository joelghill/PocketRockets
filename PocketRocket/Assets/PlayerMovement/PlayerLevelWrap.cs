using PocketRockets;
using PocketRockets.ModularRocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelWrap : MonoBehaviour {

    public GameObject Level;
 
    private LevelBoundingBox2D levelBoundingBox;
    private Rigidbody2D VesselRigidBody;

    private ModularRocket2DBounds rocket2DBounds;

    // Use this for initialization
    void Start ()
    {
        this.VesselRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        this.levelBoundingBox = Level.GetComponent<LevelBoundingBox2D>();
        this.rocket2DBounds = this.gameObject.GetComponent<ModularRocket2DBounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.rocket2DBounds != null)
        {
            var vesselBoundingSize = this.rocket2DBounds.RocketBounds.size;
            var targetBounds = this.rocket2DBounds.GetBoundingEdges();

            if (targetBounds.Left > this.levelBoundingBox.GetRightEdge() && VesselRigidBody.velocity.x > 0)
            {
                var oldPos = this.transform.position;
                this.transform.position = new Vector3(this.levelBoundingBox.GetLeftEdge() + (vesselBoundingSize.x / 2), oldPos.y, oldPos.z);
            }

            if (targetBounds.Right < this.levelBoundingBox.GetLeftEdge() && VesselRigidBody.velocity.x < 0)
            {
                var oldPos = this.transform.position;
                this.transform.position = new Vector3(this.levelBoundingBox.GetRightEdge() - (vesselBoundingSize.x / 2), oldPos.y, oldPos.z);
            }
        }
    }

    private BoundingEdges GetTargetBoundingEdges(SpriteRenderer sprite)
    {
        var bounds = new BoundingEdges();

        if (sprite != null)
        {
            var spriteBoundingSize = sprite.bounds.size;

            bounds.Right = sprite.transform.position.x + (spriteBoundingSize.x / 2);
            bounds.Left = sprite.transform.position.x - (spriteBoundingSize.x / 2);

            bounds.Top = sprite.transform.position.y + (spriteBoundingSize.y / 2);
            bounds.Bottom = sprite.transform.position.y - (spriteBoundingSize.y / 2);
        }

        return bounds;
    }
}
