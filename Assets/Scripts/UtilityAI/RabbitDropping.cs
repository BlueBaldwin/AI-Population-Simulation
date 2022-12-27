using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDropping : MonoBehaviour
{
    public Vector3 RabbitTrail { get; set; }
    [SerializeField] private float lifeSpan;

    private void Start()
    {
        StartCoroutine(DroppingLifespan());
    }

    IEnumerator DroppingLifespan()
    {
	    yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
