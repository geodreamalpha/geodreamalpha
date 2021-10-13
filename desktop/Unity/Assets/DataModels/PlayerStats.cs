using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    protected int MaxHealth { get; set; }
    protected int CurrentHealth { get; set; }
    protected int MaxStamina { get; set; }
    protected int CurrentStamina { get; set; }
}
