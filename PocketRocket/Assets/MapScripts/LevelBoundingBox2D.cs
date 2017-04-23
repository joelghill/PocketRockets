using PocketRockets;
using System;
using System.Collections;
using System.Collections.Generic;
using Tiled2Unity;
using UnityEngine;

public class LevelBoundingBox2D : MonoBehaviour, IBoundingBox2d {

    private BoxCollider2D MapBoundsCollider;

    private TiledMap tiledMap;

    private float mapWidth;
    private float mapHeight;

    // Use this for initialization
    void Start ()
    {
        this.MapBoundsCollider = this.gameObject.GetComponent<BoxCollider2D>();
        this.tiledMap = this.gameObject.GetComponent<TiledMap>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public float GetBottomEdge()
    {
        var yPos = this.transform.position.y;
        var bottom = yPos - tiledMap.MapHeightInPixels * tiledMap.ExportScale;
        return bottom;
    }

    public float GetLeftEdge()
    {
        return this.transform.position.x;
    }

    public float GetRightEdge()
    {
        var xPos = this.transform.position.x;
        var right = xPos + tiledMap.MapWidthInPixels * tiledMap.ExportScale;
        return right;
    }

    public float GetTopEdge()
    {
        return this.transform.position.y;
    }

    public BoundingEdges GetBoundingEdges()
    {
        var bounds = new BoundingEdges()
        {
            Right = this.GetRightEdge(),
            Left = this.GetLeftEdge(),

            Top = this.GetTopEdge(),
            Bottom = this.GetBottomEdge()
        };
        return bounds;
    }
}
