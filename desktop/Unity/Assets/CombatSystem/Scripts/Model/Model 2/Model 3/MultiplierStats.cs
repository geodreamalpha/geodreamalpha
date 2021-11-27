using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiplierStats
{
    //health related
    [SerializeField]
    public LevelCurve healthPoints = new LevelCurve();

    //strength related
    [SerializeField]
    public LevelCurve strength = new LevelCurve();
    [SerializeField]
    public LevelCurve defense = new LevelCurve();
    [SerializeField]
    public LevelCurve jumpHeight = new LevelCurve();

    //stamina related
    [SerializeField]
    public LevelCurve staminaPoints = new LevelCurve();
    [SerializeField]
    public LevelCurve energy = new LevelCurve();
    [SerializeField]
    public LevelCurve aura = new LevelCurve();

    //speed related
    [SerializeField]
    public LevelCurve speed = new LevelCurve(); //animator speed and walk/ sprint travelling speed

    [System.Serializable]
    public class LevelCurve
    {
        [SerializeField]
        public AnimationCurve curve = new AnimationCurve(new Keyframe(1, 10), new Keyframe(100, 500));

        public LevelCurve()
        {
            curve = new AnimationCurve(new Keyframe(1, 10), new Keyframe(100, 250));
            curve.preWrapMode = WrapMode.ClampForever;
            curve.postWrapMode = WrapMode.ClampForever;
        }
    }
}
