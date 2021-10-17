using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variation
{
    int startIndex;
    int endIndex;
    AnimationCurve curve;
    int prototypeCount;
    int detailCount;
    float abundance;
    float startDegree;
    float degreeInterval;

    public Variation(int startIndex, int prototypeCount, int detailCount, float abundance, float startDegree, float degreeInterval)
    {
        this.startIndex = startIndex;
        this.endIndex = startIndex + prototypeCount - 1;
        curve = new AnimationCurve(new Keyframe(-0.2f, startIndex), new Keyframe(1.2f, endIndex));
        this.prototypeCount = prototypeCount;
        this.detailCount = detailCount;
        this.abundance = abundance;
        this.startDegree = startDegree;
        this.degreeInterval = degreeInterval;       
    }//

    public int GetPrototypeIndex(float slope, float value)
    {
        float minimum = 0f;
        int index = -1;
        int start = Mathf.RoundToInt(curve.Evaluate(value));  

        for (int i = 0; i < detailCount; i++)
        {
            minimum = startDegree + (i * degreeInterval);
            if (slope >= minimum && slope < minimum + (degreeInterval * abundance))
            {
                index = start;              
                break;
            }
            start -= startIndex;
            start++;
            start = start % prototypeCount;
            start += startIndex;
        }
        return index;
    }//


}//
