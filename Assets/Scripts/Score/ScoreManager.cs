using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        [SerializeField] private int baseHitScore;
        [SerializeField] private TextMeshProUGUI[] scoreText;

        private Dictionary<int, int> _playerScores = new Dictionary<int, int>();

        private EventBinding<PlayerJoinedEvent> _playerJoinedEvent;
        private EventBinding<ProjectileHitEvent> _projectileHitEvent;

        private void OnEnable()
        {
            _playerJoinedEvent = new EventBinding<PlayerJoinedEvent>(OnPlayerJoined);
            EventBus<PlayerJoinedEvent>.Register(_playerJoinedEvent);

            _projectileHitEvent = new EventBinding<ProjectileHitEvent>(CalculateScore);
            _projectileHitEvent.Add(UpdateScoreText);
            EventBus<ProjectileHitEvent>.Register(_projectileHitEvent);
        }

        private void OnDisable()
        {
            EventBus<PlayerJoinedEvent>.Deregister(_playerJoinedEvent);
            EventBus<ProjectileHitEvent>.Deregister(_projectileHitEvent);
        }

        private void Awake()
        {
            if (!Instance && Instance != this)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void OnPlayerJoined(PlayerJoinedEvent @event)
        {
            _playerScores.Add(@event.PlayerIndex, 0);
            Debug.Log($"Player {@event.PlayerIndex} joined.");
        }

        private void CalculateScore(ProjectileHitEvent @event)
        {
            // If a player hit the enemy player
            if (@event.PlayerIndex != @event.ProjectileSpawnedByPlayerIndex)
            {
                _playerScores[@event.ProjectileSpawnedByPlayerIndex] += baseHitScore;
                return;
            }

            // If a player hit themselves, add score to the other/next player
            _playerScores[(@event.ProjectileSpawnedByPlayerIndex + 1) % _playerScores.Keys.Count] += baseHitScore;
        }

        private void UpdateScoreText()
        {
            foreach (var playerIndex in _playerScores.Keys)
            {
                scoreText[playerIndex].text = $"{_playerScores[playerIndex]}";
            }
        }
    }
}