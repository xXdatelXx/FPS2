using FPS.Toolkit;
using UnityEngine;

namespace FPS.GamePlay
{
    public sealed class BodyRotation : IBodyRotation
    {
        private readonly IRotation _body;

        public BodyRotation(IRotation body) =>
            _body = body.ThrowExceptionIfArgumentNull(nameof(body));

        public void Rotate(float euler) =>
            _body.Rotate(new Vector3(0, euler));
    }
}