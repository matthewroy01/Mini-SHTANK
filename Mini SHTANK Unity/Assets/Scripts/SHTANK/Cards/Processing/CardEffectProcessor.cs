using System.Collections;
using System.Collections.Generic;
using SHTANK.Data.Cards;
using Utility;

namespace SHTANK.Cards.Processing
{
    public class CardEffectProcessor : Singleton<CardEffectProcessor>
    {
        private QueuedCardInfo _tmpQueuedCardInfo;
        private CardDefinition _tmpCardDefinition;
        private EffectProcessor _tmpEffectProcessor;
        
        public IEnumerator ProcessCards(Stack<QueuedCardInfo> queuedCardInfoStack)
        {
            if (queuedCardInfoStack.Count == 0)
                yield break;
            
            yield return ProcessCardEffects(queuedCardInfoStack);
        }

        private IEnumerator ProcessCardEffects(Stack<QueuedCardInfo> queuedCardObjectStack)
        {
            while (queuedCardObjectStack.Count > 0)
            {
                _tmpQueuedCardInfo = queuedCardObjectStack.Pop();
                _tmpCardDefinition = _tmpQueuedCardInfo.CardObject.CardDefinition;
                
                // TODO: process card effect
                foreach (CardEffectInfo cardEffect in _tmpCardDefinition.CardEffects)
                {
                    switch (cardEffect.CardEffectType)
                    {
                        case CardEffectType.Damage:
                            _tmpEffectProcessor = new EffectProcessor_Damage(cardEffect.DamageInfo, _tmpQueuedCardInfo.SourceCombatEntity, _tmpQueuedCardInfo.TargetCombatEntityArray);
                            break;
                        case CardEffectType.ChangeArea:
                            break;
                        case CardEffectType.Item:
                            break;
                        case CardEffectType.FollowUp:
                            break;
                        case CardEffectType.Aggro:
                            break;
                        case CardEffectType.Defend:
                            break;
                        case CardEffectType.Status:
                            break;
                        case CardEffectType.Healing:
                            break;
                        case CardEffectType.ForceMove:
                            break;
                    }

                    if (_tmpEffectProcessor != null)
                        yield return _tmpEffectProcessor.Process();
                }

                // TODO: do animations?
                
                yield return null;
            }
        }
    }
}