using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DerivedStats
{
    const float min = 0.01f;
    const float max = 3f;

    //health related
    [Range(min, max)]
    public float healthPoints = 100;

    //strength related
    [Range(min, max)]
    public float strength = 1;
    [Range(min, max)]
    public float defense = 1;
    [Range(min, max)]
    public float jumpHeight = 1;

    //stamina related
    [Range(min, max)]
    public float staminaPoints = 1;
    [Range(min, max)]
    public float energy = 1;
    [Range(min, max)]
    public float aura = 1;

    //speed related
    [Range(min, max)]
    public float walkSpeed = 1;
    [Range(min, max)]
    public float sprintSpeed = 1;
    [Range(min, max)]
    public float projectileCheck = 1;
    [Range(min, max)]
    public float projectileSpeed = 1;

    public const float reductionSensitivity = 100;

    public static float GetReductionDamage(float offenseValue, float reducerValue)
    {
        return offenseValue * (reductionSensitivity / (reductionSensitivity + reducerValue));
    }
}
