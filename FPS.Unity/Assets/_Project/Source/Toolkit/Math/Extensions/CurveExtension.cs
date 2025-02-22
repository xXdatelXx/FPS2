﻿using UnityEngine;

namespace FPS.Toolkit
{
    public static class CurveExtension
    {
        public static AnimationCurve ThrowExceptionIfValuesSubZero(this AnimationCurve curve, string name)
        {
            for (float i = 0; i < curve[curve.length - 1].time; i += Time.fixedTime)
                curve.Evaluate(i).ThrowExceptionIfValueSubZero(name);

            return curve;
        }

        public static Curve ThrowExceptionIfValuesSubZero(this Curve curve, string name)
        {
            for (float i = 0; i <= curve.Time; i += Time.fixedDeltaTime)
                curve[i].ThrowExceptionIfValueSubZero(name);

            return curve;
        }
    }
}