using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] Transform followingtarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;
    void Start()
    {
        if (!followingtarget)
            followingtarget = Camera.main.transform;

        targetPreviousPosition = followingtarget.position;
    }
    void Update()
    {
        var delta = followingtarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
            delta.y = 0;

        targetPreviousPosition = followingtarget.position;

        transform.position += delta * parallaxStrength;
    }
}
