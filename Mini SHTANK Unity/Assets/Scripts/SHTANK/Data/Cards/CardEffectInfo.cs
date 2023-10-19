using System;
using NaughtyAttributes;
using UnityEngine;

namespace SHTANK.Data.Cards
{
    [Serializable]
    public class CardEffectInfo
    {
        [SerializeField] private CardEffectType _cardEffectType;
        [ShowIf("_cardEffectType", CardEffectType.Damage), AllowNesting]
        [SerializeField] private CardEffect_Damage _damageInfo;
        [ShowIf("_cardEffectType", CardEffectType.ChangeArea), AllowNesting]
        [SerializeField] private CardEffect_ChangeArea _changeAreaInfo;
        [ShowIf("_cardEffectType", CardEffectType.Item), AllowNesting]
        [SerializeField] private CardEffect_ChangeArea _itemInfo;
        [ShowIf("_cardEffectType", CardEffectType.FollowUp), AllowNesting]
        [SerializeField] private CardEffect_FollowUp _followUpInfo;
        [ShowIf("_cardEffectType", CardEffectType.Aggro), AllowNesting]
        [SerializeField] private CardEffect_Aggro _aggroInfo;
        [ShowIf("_cardEffectType", CardEffectType.Defend), AllowNesting]
        [SerializeField] private CardEffect_Defend _defendInfo;
        [ShowIf("_cardEffectType", CardEffectType.Status), AllowNesting]
        [SerializeField] private CardEffect_Status _statusInfo;
        [ShowIf("_cardEffectType", CardEffectType.Healing), AllowNesting]
        [SerializeField] private CardEffect_Healing _healingInfo;
        [ShowIf("_cardEffectType", CardEffectType.ForceMove), AllowNesting]
        [SerializeField] private CardEffect_ForceMove _forceMoveInfo;
    }
}