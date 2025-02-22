﻿using FPS.Toolkit;

namespace FPS.GamePlay
{
    public sealed class RayBullet : IBullet
    {
        private readonly float _damage;
        private readonly IDamagePolicy _damagePolicy;
        private readonly IRay<IHealth> _ray;
        private readonly IBulletView _view;

        public RayBullet(float damage, IDamagePolicy damagePolicy, IRay<IHealth> ray) :
            this(damage, damagePolicy, ray, new NullBulletView())
        { }

        public RayBullet(float damage, IDamagePolicy damagePolicy, IRay<IHealth> ray, IBulletView view)
        {
            _damage = damage.ThrowExceptionIfValueSubZero(nameof(damage));
            _damagePolicy = damagePolicy.ThrowExceptionIfArgumentNull(nameof(damagePolicy));
            _ray = ray.ThrowExceptionIfArgumentNull(nameof(ray));
            _view = view.ThrowExceptionIfArgumentNull(nameof(view));
        }

        public void Fire()
        {
            if (_ray.Cast(out var hit))
            {
                _view.Hit(hit.Points.End);

                if (CanDamage(hit, out var target))
                    InflictDamage(target, hit.Distance());
            }
            else _view.Miss();
        }

        private bool CanDamage(RayHit<IHealth> hit, out IHealth health)
        {
            health = hit.Occurred ? hit.Target : default;
            return health != null && health.Alive();
        }

        private void InflictDamage(IHealth target, float distance)
        {
            var damage = _damagePolicy.Affect(_damage, distance);

            target.TakeDamage(damage);
            _view.Damage();

            if (target.Died)
                _view.Kill();
        }
    }
}