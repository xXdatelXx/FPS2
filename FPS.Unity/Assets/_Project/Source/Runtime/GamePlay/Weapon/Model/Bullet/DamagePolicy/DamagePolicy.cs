﻿using FPS.Toolkit;

namespace FPS.GamePlay
{
    public sealed class DamagePolicy : IDamagePolicy
    {
        private readonly IDamageCoefficient _coefficient;

        public DamagePolicy(IDamageCoefficient coefficient) =>
            _coefficient = coefficient.ThrowExceptionIfArgumentNull(nameof(coefficient));

        public float Affect(float damage, float distance)
        {
            distance.ThrowExceptionIfValueSubZero(nameof(distance));

            if (distance == 0)
                distance = 1;

            return damage / _coefficient.Next(distance);
        }
    }
}