﻿using System;
using FPS.Toolkit;
using UnityEngine;

namespace FPS.GamePlay
{
    public sealed class CharacterJump : ICharacterJump
    {
        private readonly IGroundMovement _controller;
        private readonly Curve _motion;
        private float _evaluatedTime;

        public CharacterJump(IGroundMovement controller, Curve motion)
        {
            _controller = controller.ThrowExceptionIfArgumentNull(nameof(controller));
            _motion = motion.ThrowExceptionIfValuesSubZero(nameof(motion));
        }

        public bool Jumping { get; private set; }
        public bool CanJump => _controller.Grounded;

        public void Jump()
        {
            if (!CanJump)
                throw new InvalidOperationException(nameof(Jump));

            Jumping = true;
        }

        public void Tick(float deltaTime)
        {
            if (!Jumping)
                return;

            _evaluatedTime += deltaTime;
            Move(deltaTime);

            if (_evaluatedTime >= _motion.Time)
                Ground();
        }

        private void Move(float deltaTime)
        {
            var motion = _motion[_evaluatedTime / _motion.Time];
            _controller.Move(new Vector3(0, motion * deltaTime));
        }

        private void Ground()
        {
            _evaluatedTime = 0;
            Jumping = false;
        }
    }
}