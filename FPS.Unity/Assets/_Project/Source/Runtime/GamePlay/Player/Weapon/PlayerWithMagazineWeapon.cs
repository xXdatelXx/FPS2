﻿using FPS.Input;
using FPS.Toolkit;

namespace FPS.GamePlay
{
    public sealed class PlayerWithMagazineWeapon : IPlayerWithWeapon
    {
        private readonly IPlayerWeaponInput _input;
        private readonly IPlayerWithWeapon _playerWithWeapon;
        private readonly IWeaponWithMagazine _weapon;

        public PlayerWithMagazineWeapon(IPlayerWithWeapon playerWithWeapon, IWeaponWithMagazine weapon, IPlayerWeaponInput input)
        {
            _playerWithWeapon = playerWithWeapon.ThrowExceptionIfArgumentNull(nameof(playerWithWeapon));
            _weapon = weapon.ThrowExceptionIfArgumentNull(nameof(weapon));
            _input = input.ThrowExceptionIfArgumentNull(nameof(input));
        }

        public void Tick(float deltaTime)
        {
            if (_input.Reloading && _weapon.CanReload)
                _weapon.Reload();

            _playerWithWeapon.Tick(deltaTime);
        }

        public void Enable() => _playerWithWeapon.Enable();

        public void Disable() => _playerWithWeapon.Disable();
    }
}