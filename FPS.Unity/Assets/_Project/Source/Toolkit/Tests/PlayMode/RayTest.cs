﻿using NUnit.Framework;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace FPS.Toolkit.Test
{
    internal sealed class RayTest
    {
        [Test]
        public void HitCorrectly()
        {
            var spawnPoint = Object.Instantiate(new UnityGameObject()).AddComponent<RaySpawnPoint>();
            var ray = new UnityRay<UnityGameObject>(spawnPoint);
            var border = Object.Instantiate(new UnityGameObject());
            border.transform.position = spawnPoint.Forward;
            border.AddComponent<BoxCollider>().size = Vector3.one;

            ray.Cast(out var hit);

            Assert.AreEqual(hit.Target.name, border.name);
        }
    }
}