﻿using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Template.Scripts
{
    public class EconomyManager : Singleton<EconomyManager>
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Money moneyPrefab;
        [SerializeField] private RectTransform spawnPos;
        [SerializeField] private RectTransform targetPos;

        private int oldMoneyTarget, newMoneyTarget;
        
        private void OnEnable()
        {
            BusSystem.OnSetMoneys += SetMoneyText;
        }

        private void OnDisable()
        {
            BusSystem.OnSetMoneys -= SetMoneyText;
        }

        private void Start()
        {
            BusSystem.CallSetMoneys();
        }
        
        public void AddMoneys(int amount)
        {
            var oldAmount =  SaveManager.Instance.saveData.GetMoneys();
            var newAmount = oldAmount + amount;

            oldMoneyTarget = oldAmount;
            newMoneyTarget = newAmount;
            
            SaveManager.Instance.saveData.moneys = newAmount;
            SaveManager.Instance.Save();
            
            SetMoneyText();
        }

        public void ResetMoneys()
        {
            SaveManager.Instance.saveData.moneys = 0;
            SaveManager.Instance.Save();

            SetMoneyText();
        }

        private void SetMoneyText()
        {
            if (oldMoneyTarget == 0 || newMoneyTarget == 0)
            {
                oldMoneyTarget = 0;
                newMoneyTarget = SaveManager.Instance.saveData.GetMoneys();
            }
            
            if (InfrastructureManager.Instance.gameSettings.economyOptions.useEconomyAnim)
            {
                AnimateMoneyText(oldMoneyTarget, newMoneyTarget);
            }
            else
            {
                moneyText.text = MoneyCalculator.NumberToStringFormatter(newMoneyTarget);
            }
            
            // BusSystem.CallRefreshUpgradeValues();
        }
        
        public void SpawnMoneys()
        {
            for (int i = 0; i < InfrastructureManager.Instance.gameSettings.economyOptions.spawnIncomeAmount; i++)
            {
                var money = Instantiate(moneyPrefab, spawnPos);
                money.InitMoney(targetPos);
            }
        }
            
        private void AnimateMoneyText(int startAmount, int targetAmount)
        {
            DOTween.To(() => startAmount, x => startAmount = x, targetAmount, InfrastructureManager.Instance.gameSettings.economyOptions.economyAnimDuration)
                .OnUpdate(() => moneyText.text = MoneyCalculator.NumberToStringFormatter(startAmount))
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .SetSpeedBased(false);
        }
    }
}