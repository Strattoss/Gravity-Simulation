using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionLine : MonoBehaviour
{
    void FixedUpdate()
    {
        RemoveLastPointFromPredictionLine();
    }

    private void RemoveLastPointFromPredictionLine()
    {
        LineRenderer lineRenderer = this.GetComponent<LineRenderer>();

        if (lineRenderer.positionCount > 0)
        {
            lineRenderer.positionCount--;
        }
    }

    public void TrimPredictionLine()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        int shift = 0;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            if (!lineRenderer.GetPosition(i).Equals(Vector3.zero))
            {
                // Found the first non-zero position, break the loop
                break;
            }
            shift++;
        }

        for (int i = shift; i < lineRenderer.positionCount; i++)
        {
            // Shift non-zero positions to the front of the array
            lineRenderer.SetPosition(i - shift, lineRenderer.GetPosition(i));
        }

        // Set the new position count to remove the trailing zero positions
        lineRenderer.positionCount -= shift;
    }
}
