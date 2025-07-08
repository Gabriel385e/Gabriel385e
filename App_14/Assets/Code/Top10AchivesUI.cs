using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class Top10AchivesUI : MonoBehaviour
    {
        [SerializeField] private TopMatchCard prefab;
        [SerializeField] private Transform content;

        private List<TopMatchCard> _created = new();
        
        private void OnEnable()
        {
            Reload();
        }

        private void Reload()
        {
            foreach (TopMatchCard card in _created)
            {
                Destroy(card.gameObject);
            }

            int index = 0;
            
            foreach (AllGameStatistics.MatchResult match in AllGameStatistics.Instance.GetTop10Matches())
            {
                TopMatchCard card = Instantiate(prefab, content);
                card.Init(match.time, match.score, index);
                _created.Add(card);
                index++;
            }
        }
    }
}