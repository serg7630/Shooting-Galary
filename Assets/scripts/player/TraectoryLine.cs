using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraectoryLine : MonoBehaviour
{

   public Slinshot cannonController;
   public LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 50;

    // distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;
    void Start()
    {
        //cannonController = GetComponent<Slinshot>();
        //lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        lineRenderer.positionCount = numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = cannonController.ProjectPosition.position;
        Vector3 startingVelocity = cannonController.ProjectPosition.forward * cannonController.velosityMult/60;
        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }

        lineRenderer.SetPositions(points.ToArray());
    }
    //[SerializeField]
    //LineRenderer lineRenderer;

    //[SerializeField, Min(3)]
    //private int _lineSegment;

    //[SerializeField,Min(1)]
    //private int _timeOfTheLight;



    //public void ShowTraectoryLine(Vector3 startPoint, Vector3 startVelosity)
    //{
    //    float TimeStep = _timeOfTheLight / _lineSegment;

    //    Vector3[] LineRenderPoints = CalculatorTraecoryLine(startPoint, startVelosity, TimeStep);
    //    lineRenderer.positionCount = _lineSegment;
    //    lineRenderer.SetPositions(LineRenderPoints);
    //}

    //public Vector3[] CalculatorTraecoryLine(Vector3 startPoint, Vector3 startVelosity, float timeStep)
    //{
    //    Vector3[] LineRendersPoints = new Vector3[_lineSegment];

    //    LineRendersPoints[0] = startPoint;

    //    for (int i = 1; i < _lineSegment; i++)
    //    {
    //        //Debug.LogError(i);
    //        float TimeOffset = timeStep * 1;
    //        Vector3 progresBeforeGravity = startVelosity * TimeOffset;
    //        Vector3 gravityOffset = Vector3.up * -0.5f * Physics.gravity.y * TimeOffset * TimeOffset;
    //        Vector3 NewPosition = startPoint + progresBeforeGravity - gravityOffset;
    //        LineRendersPoints[i] = NewPosition;
    //    }
    //    return LineRendersPoints;
    //}
}
