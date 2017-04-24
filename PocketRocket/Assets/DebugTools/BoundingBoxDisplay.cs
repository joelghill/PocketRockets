using PocketRockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets.DebugTools
{
    public class BoundingBoxDisplay : MonoBehaviour
    {

        public bool Enabled = true;
        public IBoundingBox2d BoundingBox;

        public float margin = 0;

        private Vector2[] points = new Vector2[4];

        public void Start()
        {
            this.BoundingBox = this.gameObject.GetComponent<IBoundingBox2d>();
        }

        public void OnGUI()
        {
            if (this.Enabled == false || BoundingBox == null)
            {
                return;
            }

            Camera cam = Camera.main;

            //All 4 vertices of the bounds
            points[0] = cam.WorldToScreenPoint(new Vector2(this.BoundingBox.GetLeftEdge(), this.BoundingBox.GetTopEdge()));
            points[1] = cam.WorldToScreenPoint(new Vector2(this.BoundingBox.GetRightEdge(), this.BoundingBox.GetTopEdge()));
            points[2] = cam.WorldToScreenPoint(new Vector2(this.BoundingBox.GetLeftEdge(), this.BoundingBox.GetBottomEdge()));
            points[3] = cam.WorldToScreenPoint(new Vector2(this.BoundingBox.GetRightEdge(), this.BoundingBox.GetBottomEdge()));

            //Get them in GUI space
            for (int i = 0; i < points.Length; i++) points[i].y = Screen.height - points[i].y;

            //Calculate the min and max positions
            Vector2 min = points[0];
            Vector2 max = points[0];
            for (int i = 1; i < points.Length; i++)
            {
                min = Vector2.Min(min, points[i]);
                max = Vector2.Max(max, points[i]);
            }

            //Construct a rect of the min and max positions and apply some margin
            Rect r = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
            r.xMin -= margin;
            r.xMax += margin;
            r.yMin -= margin;
            r.yMax += margin;

            //Render the box
            GUI.Box(r, "This is a box covering the player");
        }
    }
}
