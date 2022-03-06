using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SumoDemo
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        [SerializeField] uint _remainPlayer = 1;
        [SerializeField] uint _remainEnemy = 7;

        [SerializeField] GameObject canvas;
        [SerializeField] Text _text;
        [SerializeField] Text _textPoint;

        private GameState _gameState;
        private bool _isGameOver;

        public static GameManager Instance { get => _instance; set => _instance = value; }
        public uint RemainEnemy { get => _remainEnemy; set => _remainEnemy = value; }
        public uint RemainPlayer { get => _remainPlayer; set => _remainPlayer = value; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StateMachine(GameState.GamePlay);
        }
        private void Update()
        {
            if (RemainPlayer <= 0 || RemainEnemy <= 0)
            {
                if (!_isGameOver)
                {
                    StateMachine(GameState.GameOver);
                }
            }
        }
        public void TextUpdate(string massage)
        {
            _text.text = massage;
        }
        private void StateMachine(GameState gameState)
        {
            _gameState = gameState;

            switch (_gameState)
            {
                case GameState.GamePlay:
                    canvas.SetActive(false);
                    Time.timeScale = 1;
                    break;

                case GameState.GameOver:

                    canvas.SetActive(true);
                    ScoreBoard();
                    _isGameOver = true;
                    Time.timeScale = 0;
                    break;
            }
        }

        private void ScoreBoard()
        {
            List<uint> scoreBoard = new List<uint>();

            CharacterCollect[] scores = FindObjectsOfType<CharacterCollect>();

            foreach (CharacterCollect item in scores)
            {
                scoreBoard.Add(item.Money);
            }

            scoreBoard.Sort();

            _textPoint.text = scoreBoard[scores.Length - 1].ToString();
        }
    }
    public enum GameState
    {
        GamePlay, GameOver
    }
}