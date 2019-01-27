using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Entity
{
    void Damage(float hit);
    float GetHealth();
}
