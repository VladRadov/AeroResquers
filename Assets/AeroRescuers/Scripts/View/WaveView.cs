using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveView : EnemyView
{
    public void SetActive(bool value)
        => gameObject.SetActive(value);
}
