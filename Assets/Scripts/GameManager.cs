using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string SCORE_UI_PREFIX_TEXT = "Счёт: ";
    private const float BLOCK_Z_SIZE = 30f;
    public static GameManager Instance;

    [SerializeField, Range(1f, 1000f)]
    private int _health = 3;
    [SerializeField]
    private BasePlayerController _player;
    [SerializeField]
    private Text _scoreUI;
    [SerializeField]
    private Transform[] _blocks = null;

    private int _scoreValue = 0;
    private int _blockIndex = 0;
    private float _lastBlockZPosition;
    private float _blockZSize;

    private void Start()
    {
        Instance = this;
        _lastBlockZPosition = GetLastBlock(_blocks).position.z;
        _blockZSize = GetBlockZSize(_blocks);
    }

    private void Update()
    {
        if (_player.transform.position.y <= -1f)
        {
            SetDamage(1);
        }
    }
    public void SetDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Debug.Log("Player is died");
            Debug.Log("Game over");
            Debug.Log("Счёт: " + _scoreValue);
            UnityEditor.EditorApplication.isPaused = true;
        }
    }

    public void UpdateLevel()
    {
        IncreaseScore(1);
        UpdateScoreUI();

        _lastBlockZPosition += _blockZSize;

        Vector3 position = _blocks[_blockIndex].position;
        position.z = _lastBlockZPosition;
        _blocks[_blockIndex].position = position;

        _blockIndex++;

        if (_blockIndex >= _blocks.Length)
        {
            _blockIndex = 0;
        }
    }

    public void SlowPlayer()
    {
        _player.SlowPlayer();
    }

    private void IncreaseScore(int score)
    {
        _scoreValue += score;
    }

    private void UpdateScoreUI()
    {
        _scoreUI.text = SCORE_UI_PREFIX_TEXT + _scoreValue.ToString();
    }

    private Transform GetLastBlock(Transform[] blocks)
    {
        if (blocks == null || blocks.Length == 0)
        {
            throw new MissingReferenceException("В поле blocks не передан список блоков на уровне (Blocks)!");
        }

        int lastBlockIndex = blocks.Length - 1;
        return blocks[lastBlockIndex];
    }

    private float GetBlockZSize(Transform[] blocks)
    {

        return BLOCK_Z_SIZE;
    }
}
