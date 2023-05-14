using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemies { none, basic };

[System.Serializable]
public class Level
{

    Level()
    {
        board = new Enemies[rows, columns];
    }
    Level(int rows, int columns)
    {
        board = new Enemies[rows, columns];
    }
#if UNITY_EDITOR
    public bool showBoard;
#endif
    public int rows = 8;
    public int columns = 8;
    public Enemies[,] board;
    public float delayAfterEnemiesDeath = 0.5f;
}


public class Levels : MonoBehaviour
{

    public Level[] allLevels;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}