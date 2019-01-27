using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHintable
{
    bool CollectHint();
    void DisableHint();
    void EnableHint();
}
