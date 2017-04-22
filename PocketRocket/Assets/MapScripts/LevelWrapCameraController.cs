using PocketRockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWrapCameraController : MonoBehaviour {

    public GameObject Vessel;
    public GameObject Level;

    private LevelBoundingBox2D levelBoundingBox;

    private Camera VesselCamera;
    public Camera SecondaryLevelCamera;

    // Use this for initialization
    void Start () {

        if (Vessel != null && Level != null)
        {
            this.levelBoundingBox = Level.GetComponent<LevelBoundingBox2D>();
        }

        this.VesselCamera = this.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(this.levelBoundingBox != null)
        {
            var levelBounds = this.levelBoundingBox.GetBoundingEdges();

            this.UpdateCameraToFollowTarget(this.VesselCamera, this.Vessel.gameObject, levelBounds);
            this.UpdateSecondaryCamera(this.VesselCamera, levelBounds);
        }

    }

    private void UpdateSecondaryCamera(Camera cam, BoundingEdges levelBounds)
    {
        if (cam != null)
        {
            var oldPosition = cam.transform.position;
            var cameraBounds = this.GetCameraEdges(cam);

            if (cameraBounds.Right > levelBounds.Right)
            {
                var xOffset = cameraBounds.Right - levelBounds.Right;
                var newPos = new Vector3(levelBounds.Left - this.GetCameraWidthExtent(cam) + xOffset, oldPosition.y, oldPosition.z);
                this.SecondaryLevelCamera.transform.position = newPos;
            }
            else if(cameraBounds.Left < levelBounds.Left)
            {
                var xOffset =  Math.Abs(levelBounds.Left - cameraBounds.Left);
                var newPos = new Vector3(levelBounds.Right + this.GetCameraWidthExtent(cam) - xOffset, oldPosition.y, oldPosition.z);
                this.SecondaryLevelCamera.transform.position = newPos;
            }
            else
            {
                this.SecondaryLevelCamera.transform.position = cam.transform.position;
            }

            return;
        }
    }

    private BoundingEdges GetCameraEdges(Camera cam)
    {
        var bounds = new BoundingEdges();
        
        if(cam != null)
        {
            var widthExtent = this.GetCameraWidthExtent(cam);

            bounds.Right = cam.transform.position.x + widthExtent;
            bounds.Left = cam.transform.position.x - widthExtent;

            bounds.Top = cam.transform.position.y + cam.orthographicSize;
            bounds.Bottom = cam.transform.position.y - cam.orthographicSize;
        }

        return bounds;
    }

    private float GetCameraWidthExtent(Camera cam)
    {
        return cam.orthographicSize* Screen.width / Screen.height;
    }

    private void UpdateCameraToFollowTarget(Camera cam, GameObject target, BoundingEdges levelBounds)
    {
        if (target != null && cam != null)
        {
            var oldPosition = cam.transform.position;
            float oldZ = oldPosition.z;

            var newPosition = target.transform.position;
            newPosition.z = oldZ;
  
            cam.transform.position = newPosition;

            var cameraBounds = this.GetCameraEdges(cam);

            if (cameraBounds.Bottom < levelBounds.Bottom)
            {
                var oldCamPos = this.VesselCamera.transform.position;
                var newY = levelBounds.Bottom + cam.orthographicSize;
                cam.transform.position = new Vector3(oldCamPos.x, newY, oldCamPos.z);
            }
            return;
        }
    }
}
