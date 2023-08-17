using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    [SerializeField]
    private bool _canInflictDamage;
    [SerializeField]
    private int _damageValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (_canInflictDamage)
        {
            GameManager.Instance.SetDamage(_damageValue);
            GameManager.Instance.SlowPlayer();
            Debug.LogWarning("TriggerDamage");
        }
        else
        {
            GameManager.Instance.UpdateLevel();
            Debug.LogWarning("Level Updated");
        }
    }

}
