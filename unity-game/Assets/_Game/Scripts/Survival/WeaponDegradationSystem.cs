using System;
using UnityEngine;

namespace OldManAndTheSea.Survival
{
    public enum WeaponType
    {
        Harpoon,            // 铁飞叉 (150伤，1次即丢)
        KnifeOnOar,         // 绑刀船桨 (85伤，耐久2次)
        Club,               // 金枪鱼短木棒 (45伤，耐久5次)
        Tiller,             // 船用木舵柄 (35伤)
        SplinteredTiller,   // 折断尖刺木桩 (20伤)
        BareHands           // 徒留双手与残骸
    }

    /// <summary>
    /// 武器与工具阶梯式损耗降级树
    /// 依据 docs/03_GAME_SYSTEMS_AND_MECHANICS.md 第 2 节实现
    /// </summary>
    public class WeaponDegradationSystem : MonoBehaviour
    {
        [Header("Current Weapon")]
        [SerializeField] private WeaponType currentWeapon = WeaponType.Harpoon;
        [SerializeField] private int currentDurability = 1;

        // Events
        public event Action<WeaponType> OnWeaponDegraded;

        public WeaponType CurrentWeapon => currentWeapon;
        public int CurrentDurability => currentDurability;

        public float GetWeaponDamage()
        {
            switch (currentWeapon)
            {
                case WeaponType.Harpoon: return 150f;
                case WeaponType.KnifeOnOar: return 85f;
                case WeaponType.Club: return 45f;
                case WeaponType.Tiller: return 35f;
                case WeaponType.SplinteredTiller: return 20f;
                default: return 5f;
            }
        }

        public void UseWeaponOnShark()
        {
            currentDurability--;
            if (currentDurability <= 0)
            {
                DegradeToNextWeapon();
            }
        }

        private void DegradeToNextWeapon()
        {
            switch (currentWeapon)
            {
                case WeaponType.Harpoon:
                    currentWeapon = WeaponType.KnifeOnOar;
                    currentDurability = 2; // 刺杀两只角鲨后折断
                    break;
                case WeaponType.KnifeOnOar:
                    currentWeapon = WeaponType.Club;
                    currentDurability = 5;
                    break;
                case WeaponType.Club:
                    currentWeapon = WeaponType.Tiller;
                    currentDurability = 4;
                    break;
                case WeaponType.Tiller:
                    currentWeapon = WeaponType.SplinteredTiller;
                    currentDurability = 8;
                    break;
                case WeaponType.SplinteredTiller:
                    currentWeapon = WeaponType.BareHands;
                    currentDurability = 999;
                    break;
            }

            OnWeaponDegraded?.Invoke(currentWeapon);
        }
    }
}
