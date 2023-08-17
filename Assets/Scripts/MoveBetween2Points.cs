using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween2Points : MonoBehaviour
{

    [SerializeField]
    private Transform _point1;
    [SerializeField]
    private Transform _point2;

    [SerializeField, Range(1f, 50f)]
    private float _speed = 3f;

    void Start()
    {
        StartCoroutine(MoveBetweenPoints());
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            yield return MoveToPoint(_point1);
            yield return MoveToPoint(_point2);
        }
    }

    private IEnumerator MoveToPoint(Transform point)
    {
        while (transform.position != point.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _speed * Time.deltaTime);
            yield return null;
        }

        yield break;
    }
}
