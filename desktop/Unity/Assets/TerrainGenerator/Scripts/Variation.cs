using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variation
{
    const float maxDegree = 90f;

    AnimationCurve curve;
    
    int detail;
    float scale;
    float abundance;
    float sensitivity;
    float startDegree;
    float degreeInterval;

    public Variation(int startIndex, int count, int detail, float abundance, float sensitivity, float startDegree, float degreeInterval)
    {
        curve = new AnimationCurve(new Keyframe(-0.2f, startIndex), new Keyframe(1.2f, startIndex + count - detail));
        this.detail = detail;
        this.abundance = abundance;
        this.sensitivity = sensitivity;
        this.startDegree = startDegree;
        this.degreeInterval = degreeInterval;       
    }//

    public int GetPrototypeIndex(float slope, float value)
    {
        float minimum = 0f;
        int index = -1;
        int start = (int)curve.Evaluate(value);

        for (int i = 0; i < detail; i++)
        {
            minimum = startDegree + (i * degreeInterval);
            if (slope >= minimum && slope < minimum + (degreeInterval * abundance))
            {
                index = start;              
                break;
            }
            start++;
        }
        return index;
    }//


}//
