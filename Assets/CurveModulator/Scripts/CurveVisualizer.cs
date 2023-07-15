using System.Collections.Generic;
using UnityEngine;

public class CurveVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject renderObjectTemplate;

    [SerializeField]
    private float width = 10;

    [SerializeField]
    private float height = 5;

    [SerializeField]
    private int resolution = 100;

    private CurveModulator curveModulator;

    private List<GameObject> _renderObjects;

    void Start()
    {
        curveModulator = GetComponent<CurveModulator>();
        _renderObjects = new List<GameObject>(resolution);
        for (int i = 0; i < resolution; ++i)
        {
            var rendObj = Instantiate(renderObjectTemplate);
            _renderObjects.Add(rendObj);

            var posOffset = new Vector3();

            var normalizedTimePosition = i / (float)resolution;
            var timePosition = normalizedTimePosition * curveModulator.AnimationCurveLength;

            posOffset.x = normalizedTimePosition * width;
            posOffset.y = curveModulator.Evaluate(timePosition ) * height;
            posOffset.z = 0;

            rendObj.transform.position = gameObject.transform.position + posOffset;
            rendObj.transform.parent = gameObject.transform;
        }
    }

    void Update()
    {
        for (int i = 0; i < resolution; ++i)
        {
            var rendObj = _renderObjects[i];

            var posOffset = new Vector3();

            var normalizedTimePosition = i / (float)resolution;
            var timePosition = normalizedTimePosition * curveModulator.AnimationCurveLength;

            posOffset.x = normalizedTimePosition * width;
            posOffset.y = curveModulator.Evaluate(timePosition) * height;
            posOffset.z = 0;

            rendObj.transform.position = gameObject.transform.position + posOffset;
            rendObj.transform.parent = gameObject.transform;
        }
    }
}
