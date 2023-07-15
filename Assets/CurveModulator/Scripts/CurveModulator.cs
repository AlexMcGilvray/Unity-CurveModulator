using System.Linq;
using UnityEngine;

internal enum CurveModulatorMode
{
    None,
    Add,
    Multiply
}

/// <summary>
/// Current expectes all curves to be in 0-1 space. Will clip if out of range.
/// </summary>
public class CurveModulator : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve animationCurve;

    [SerializeField]
    private AnimationCurve modulationCurve;

    [SerializeField]
    private CurveModulatorMode mode = CurveModulatorMode.Multiply;

    [SerializeField]
    private bool clipToNormalizedRange = true;

    public float AnimationCurveLength => animationCurve.keys.Max(x => x.time);
    public float ModulationCurveLength => modulationCurve.keys.Max(x => x.time);

    private float GetModuloTime(float time, float period)
    {
        while (time > period)
        {
            time -= period;
        }
        return time;
    }

    public float Evaluate(float time)
    {
        var animCurveValue = animationCurve.Evaluate(time);
        var modulationCurveValue = modulationCurve.Evaluate(time);

        float finalValue;
        switch (mode)
        {
            case CurveModulatorMode.None:
                finalValue = animCurveValue;
                break;
            case CurveModulatorMode.Add:
                finalValue = animCurveValue + modulationCurveValue;
                break;
            case CurveModulatorMode.Multiply:
                finalValue = animCurveValue * modulationCurveValue;
                break;
            default:
                finalValue = animCurveValue;
                break;
        }

        return clipToNormalizedRange ? Mathf.Clamp01(finalValue) : finalValue;
    }
}
