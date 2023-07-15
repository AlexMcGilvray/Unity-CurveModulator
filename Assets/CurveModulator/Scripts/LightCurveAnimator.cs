using UnityEngine;

public class LightCurveAnimator : MonoBehaviour
{
    private Light _light;

    [SerializeField]
    private CurveModulator curveModulator;

    private float _originalLightIntensity;

    private float _currentTime;

    void Start()
    {
        _light = GetComponent<Light>();
        _originalLightIntensity = _light.intensity;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;

        var curveValue = curveModulator.Evaluate(_currentTime);

        _light.intensity = _originalLightIntensity * curveValue;
    }
}
