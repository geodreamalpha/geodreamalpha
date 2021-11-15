using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DerivedStats
{
    //health related
    public float healthPoints = 1;

    //strength related
    public float strength = 1;
    public float defense = 1;
    public float jumpHeight = 1;

    //stamina related
    public float staminaPoints = 1;
    public float energy = 1;
    public float aura = 1;

    //speed related
    public float walkSpeed = 1;
    public float sprintSpeed = 1;
    public float projectileCheck = 1;
    public float projectileSpeed = 1;

    public const float reductionSensitivity = 100;

    public static float GetReductionDamage(float offenseValue, float reducerValue)
    {
        return offenseValue * (reductionSensitivity / (reductionSensitivity + reducerValue));
    }
}
