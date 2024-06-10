using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

namespace DailyRewardSystem
{
    public enum RewardType
    {
        Coins,
        Gems
    }

    [Serializable]
    public struct Reward
    {
        public RewardType Type;
        public int Amount;
    }

    public class DailyRewards : MonoBehaviour
    {
        [Header("Main Menu UI")]
        [SerializeField] TextMeshProUGUI coinsText;
        [SerializeField] TextMeshProUGUI gemsText;

        [Space]
        [Header("Reward UI")]
        [SerializeField] GameObject rewardsCanvas;
        [SerializeField] Button closeButton;

        [Space]
        [Header("Claim Day 1")]
        [SerializeField] Button DayButton1;
        [SerializeField] GameObject clear1;
        [SerializeField] GameObject missed1;
        [SerializeField] GameObject Focus1;
        [Header("Day 2")]
        [SerializeField] Button DayButton2;
        [SerializeField] GameObject clear2;
        [SerializeField] GameObject missed2;
        [SerializeField] GameObject Focus2;
        [Header("Day 3")]
        [SerializeField] Button DayButton3;
        [SerializeField] GameObject clear3;
        [SerializeField] GameObject missed3;
        [SerializeField] GameObject Focus3;
        [Header("Day 4")]
        [SerializeField] Button DayButton4;
        [SerializeField] GameObject clear4;
        [SerializeField] GameObject missed4;
        [SerializeField] GameObject Focus4;
        [Header("Day 5")]
        [SerializeField] Button DayButton5;
        [SerializeField] GameObject clear5;
        [SerializeField] GameObject missed5;
        [SerializeField] GameObject Focus5;
        [Header("Day 6")]
        [SerializeField] Button DayButton6;
        [SerializeField] GameObject clear6;
        [SerializeField] GameObject missed6;
        [SerializeField] GameObject Focus6;
        [Header("Day 7")]
        [SerializeField] Button DayButton7;
        [SerializeField] GameObject clear7;
        [SerializeField] GameObject missed7;
        [SerializeField] GameObject Focus7;

        [Space]
        [Header("Rewards Database")]
        [SerializeField] RewardsDatabase rewardsDB;

        [Space]
        [Header("FX")]
        [SerializeField] ParticleSystem fxCoins;
        [SerializeField] ParticleSystem fxGems;

        [Space]
        [Header("Timing")]
        // wait 5 minutes to activate the next reward
        [SerializeField] double nextRewardDelay = 5f / 60f;
        // check if reward is available every 5 seconds
        [SerializeField] float checkForRewardDelay = 5f;

        private int nextRewardIndex;
        private bool isRewardReady = false;
        private bool canClaimReward = false;

        void Start()
        {
            Initialize();

            StopAllCoroutines();
            StartCoroutine(CheckForRewards());

            LoadState();
            CheckLastGameOpenTime();
        }

        void Initialize()
        {
            nextRewardIndex = PlayerPrefs.GetInt("Next_Reward_Index", 0);

            UpdateCoinsTextUI();
            UpdateGemsTextUI();

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClick);

            AddButtonListeners();
        }

        void AddButtonListeners()
        {
            DayButton1.onClick.RemoveAllListeners();
            DayButton1.onClick.AddListener(() => OnClaimButtonClick(1, clear1, missed1, Focus1, DayButton1));
            DayButton2.onClick.RemoveAllListeners();
            DayButton2.onClick.AddListener(() => OnClaimButtonClick(2, clear2, missed2, Focus2, DayButton2));
            DayButton3.onClick.RemoveAllListeners();
            DayButton3.onClick.AddListener(() => OnClaimButtonClick(3, clear3, missed3, Focus3, DayButton3));
            DayButton4.onClick.RemoveAllListeners();
            DayButton4.onClick.AddListener(() => OnClaimButtonClick(4, clear4, missed4, Focus4, DayButton4));
            DayButton5.onClick.RemoveAllListeners();
            DayButton5.onClick.AddListener(() => OnClaimButtonClick(5, clear5, missed5, Focus5, DayButton5));
            DayButton6.onClick.RemoveAllListeners();
            DayButton6.onClick.AddListener(() => OnClaimButtonClick(6, clear6, missed6, Focus6, DayButton6));
            DayButton7.onClick.RemoveAllListeners();
            DayButton7.onClick.AddListener(() => OnClaimButtonClick(7, clear7, missed7, Focus7, DayButton7));
        }

        IEnumerator CheckForRewards()
        {
            while (true)
            {
                if (!isRewardReady)
                {
                    DateTime currentDatetime = DateTime.Now;
                    DateTime rewardClaimDatetime = DateTime.Parse(PlayerPrefs.GetString("Reward_Claim_Datetime", currentDatetime.ToString()));

                    // get total Hours between these two dates
                    double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalHours;

                    if (elapsedHours >= nextRewardDelay)
                    {
                        ActivateReward();
                        AdvanceToNextDay();
                    }
                }

                yield return new WaitForSeconds(checkForRewardDelay);
            }
        }

        void ActivateReward()
        {
            isRewardReady = true;
            Reward reward = rewardsDB.GetReward(nextRewardIndex);
        }

        void AdvanceToNextDay()
        {
            nextRewardIndex++;
            if (nextRewardIndex >= rewardsDB.rewardsCount)
                nextRewardIndex = 0;

            PlayerPrefs.SetInt("Next_Reward_Index", nextRewardIndex);
            PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());

            // Set focus for the next day's button
            SetFocusForNextDay();
        }

        void SetFocusForNextDay()
        {
            switch (nextRewardIndex)
            {
                case 1:
                    Focus1.SetActive(true);
                    break;
                case 2:
                    Focus2.SetActive(true);
                    break;
                case 3:
                    Focus3.SetActive(true);
                    break;
                case 4:
                    Focus4.SetActive(true);
                    break;
                case 5:
                    Focus5.SetActive(true);
                    break;
                case 6:
                    Focus6.SetActive(true);
                    break;
                case 7:
                    Focus7.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        void OnClaimButtonClick(int day, GameObject clear, GameObject missed, GameObject focus, Button dayButton)
        {
            if (!canClaimReward)
            {
                Debug.Log("You cannot claim the reward yet. Please wait for 5 minutes.");
                return;
            }

            clear.SetActive(true);
            focus.SetActive(false);

            PlayerPrefs.SetInt("DoneState" + day, 1);  // Save the state of "clear" as active
            PlayerPrefs.SetInt("DayButton" + day + "Clicked", 1); // Save the state of DayButton as clicked
            dayButton.interactable = false; // Disable the day button

            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            fxCoins.Play();
            if (reward.Type == RewardType.Coins)
            {
                Debug.Log("<color=yellow>" + reward.Type.ToString() + " Claimed : </color>+" + reward.Amount);
                GameData.Coins += reward.Amount;
                fxCoins.Play();
                UpdateCoinsTextUI();
            }
            else
            {
                Debug.Log("<color=green>" + reward.Type.ToString() + " Claimed : </color>+" + reward.Amount);
                GameData.Gems += reward.Amount;
                fxGems.Play();
                UpdateGemsTextUI();
            }

            AdvanceToNextDay();
            PlayerPrefs.SetString("LastGameOpenTime", DateTime.Now.ToString());
        }

        void CheckLastGameOpenTime()
        {
            DateTime currentDatetime = DateTime.Now;
            DateTime lastOpenDatetime = DateTime.Parse(PlayerPrefs.GetString("LastGameOpenTime", currentDatetime.ToString()));
            double elapsedMinutes = (currentDatetime - lastOpenDatetime).TotalMinutes;

            if (elapsedMinutes >= 5)
            {
                canClaimReward = true;
            }
            else
            {
                canClaimReward = false;
            }
        }

        void LoadState()
        {
            // Load the state of the "clear" and "focus" GameObjects and day buttons
            for (int day = 1; day <= 7; day++)
            {
                bool isClearActive = PlayerPrefs.GetInt("DoneState" + day, 0) == 1;
                bool isDayButtonClicked = PlayerPrefs.GetInt("DayButton" + day + "Clicked", 0) == 1;

                switch (day)
                {
                    case 1:
                        clear1.SetActive(isClearActive);
                        DayButton1.interactable = !isDayButtonClicked;
                        break;
                    case 2:
                        clear2.SetActive(isClearActive);
                        DayButton2.interactable = !isDayButtonClicked;
                        break;
                    case 3:
                        clear3.SetActive(isClearActive);
                        DayButton3.interactable = !isDayButtonClicked;
                        break;
                    case 4:
                        clear4.SetActive(isClearActive);
                        DayButton4.interactable = !isDayButtonClicked;
                        break;
                    case 5:
                        clear5.SetActive(isClearActive);
                        DayButton5.interactable = !isDayButtonClicked;
                        break;
                    case 6:
                        clear6.SetActive(isClearActive);
                        DayButton6.interactable = !isDayButtonClicked;
                        break;
                    case 7:
                        clear7.SetActive(isClearActive);
                        DayButton7.interactable = !isDayButtonClicked;
                        break;
                    default:
                        break;
                }
            }
        }

        void UpdateCoinsTextUI()
        {
            coinsText.text = GameData.Coins.ToString();
        }

        void UpdateGemsTextUI()
        {
            gemsText.text = GameData.Gems.ToString();
        }

        void OnOpenButtonClick()
        {
            rewardsCanvas.SetActive(true);
        }

        void OnCloseButtonClick()
        {
            rewardsCanvas.SetActive(false);
        }

        void OnDestroy()
        {
            // Save the state of the "clear" GameObjects when the object is destroyed (e.g., on game exit)
            for (int day = 1; day <= 7; day++)
            {
                PlayerPrefs.SetInt("DoneState" + day, GetClearGameObject(day).activeSelf ? 1 : 0);
            }
            PlayerPrefs.Save();
        }

        GameObject GetClearGameObject(int day)
        {
            switch (day)
            {
                case 1:
                    return clear1;
                case 2:
                    return clear2;
                case 3:
                    return clear3;
                case 4:
                    return clear4;
                case 5:
                    return clear5;
                case 6:
                    return clear6;
                case 7:
                    return clear7;
                default:
                    return null;
            }
        }
    }
}
