using System;
using FPS.Input;
using FPS.Toolkit;
using UnityEngine;

namespace FPS.GamePlay
{
    public sealed class CharacterRotation : ICharacterRotation
    {
        private readonly IBodyRotation _body;
        private readonly IHeadRotation _head;
        private readonly float _sensitivity;

        public CharacterRotation(IBodyRotation body, IHeadRotation head, IReadOnlyMouseSensitivity mouseSensitivity)
        {
            _body = body.ThrowExceptionIfArgumentNull(nameof(body));
            _head = head.ThrowExceptionIfArgumentNull(nameof(head));
            _sensitivity = mouseSensitivity.Value;
        }

        public void Rotate(Vector3 direction)
        {
            if (direction == Vector3.zero)
                throw new InvalidOperationException($"{nameof(direction)} is empty");

            _head.Rotate(direction.x * _sensitivity);
            _body.Rotate(direction.y * _sensitivity);
        }
    }
}