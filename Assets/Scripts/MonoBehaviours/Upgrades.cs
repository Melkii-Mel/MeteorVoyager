using Assets.Scripts.GameStatsNameSpace;
using MeteorVoyager.Assets.Scripts.GameStatsNameSpace;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace MeteorVoyager.Assets.Scripts.MonoBehaviours
{
    public class Upgrades : MonoBehaviour
    {
        [SerializeField] bool _categoryChanger;
        [SerializeField] GameObject _player;
        Func<int, InfiniteInteger> _formula;
        public int Value { get; set; }
        public InfiniteInteger Cost { get; set; }

        private void OnEnable()
        {
            GameStats.onRelocation += Start;
        }

        private void OnDisable()
        {
            GameStats.onRelocation -= Start;
        }
        public void Start()
        {
            Init();
        }

        public void Init()
        {
            if (!_categoryChanger)
            {
                try
                {
                    _formula = TurretUpgrades.Functions[(int)Enum.Parse(typeof(TurretUpgrades.Upgrades), name)];
                }
                catch
                {
                    _formula = MeteorUpgrades.Functions[(int)Enum.Parse(typeof(MeteorUpgrades.Upgrades), name)];
                }
                Value = PlayerPrefs.GetInt(name, 1);
                Cost = _formula(Value);
                UpdateText(Cost);
                GetComponent<Button>().interactable = Cost != -1;
            }
            if (name == "Damage")
            {
                StartCoroutine(DamageUpgradeStateController());
            }
        }

        IEnumerator DamageUpgradeStateController()
        {
            GameStats.IsDamageUpgradeEnabled = false;
            GetComponent<Button>().interactable = false;
            PlayerPrefs.SetInt("isDamageUpgradeEnabled", 0);
            transform.GetChild(0).gameObject.GetComponent<Text>().text = "LOCKED";
            while (TurretUpgrades.Instance.SpawnCooldown < 50)
            {
                yield return null;
            }
            GameStats.IsDamageUpgradeEnabled = true;
            GetComponent<Button>().interactable = true;
            UpdateText(Cost);
            PlayerPrefs.SetInt("isDamageUpgradeEnabled", 1);
        }

        public void Buy()
        {
            void Upgrade()
            {
                Value++;
                Currency.Instance.Balance -= Cost;
            }
            if (BuyMultiplier.multiplier != -1)
            {
                for (int i = 0; i < BuyMultiplier.multiplier; i++)
                {
                    Cost = _formula(Value);
                    if (Currency.Instance.Balance >= Cost && Cost != -1)
                    {
                        Upgrade();
                    }
                    else break;
                }
            }
            else
            {
                for (; ; )
                {
                    Cost = _formula(Value);
                    if (Currency.Instance.Balance >= Cost && Cost != -1)
                    {
                        Upgrade();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            UpdateText(_formula(Value));
            GetComponent<Button>().interactable = _formula(Value) != -1;
        }

        public void UpdateText(InfiniteInteger cost)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            var text = transform.GetChild(0).GetComponent<Text>();

            if (_formula(Value) != -1)
            {
                text.text =
                    $"{r.Replace(name, " ")}\n" +
                    $"Lvl: {Value}\n" +
                    $"Cost: {(cost == 0 ? "FREE" : cost)}";
            }
            else
            {
                text.text = $"MAX LVL ({Value})";
                GetComponent<Button>().interactable = false;
            }
        }
    }
}