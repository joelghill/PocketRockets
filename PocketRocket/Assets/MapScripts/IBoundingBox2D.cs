using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PocketRockets
{
    public interface IBoundingBox2d
    {

        float GetLeftEdge();

        float GetRightEdge();

        float GetTopEdge();

        float GetBottomEdge();

        BoundingEdges GetBoundingEdges();
    }
}
