using UnityEngine;
using System.Collections;
using System.Security.Principal;
using Assets.script;

public class Game : MonoBehaviour
{
    public static Game  Instance;
    public GameObject   White;
    public GameObject   Black;
    public GameObject   Tile;
    public bool         PlayerTurn = false;
    public int[][]      Board;

    private GameObject[][]  _board;
    private IA              _ia;

	void Start ()
	{
	    PlayerTurn = true;

	    Instance = this;

	    _board = new GameObject[15][];
        Board = new int[15][];
	    for (int i = 0; i< 15;i++ )
	    {
	        _board[i] = new GameObject[15];
            Board[i] =  new int[15];
	    }

        for (int i = 0; i < 15; i++)
	    {
	        for (int j = 0; j < 15; j++)
	        {
	            Board[i][j] = 0;
                _board[i][j] = (GameObject)GameObject.Instantiate(Tile, new Vector3( (i * 0.4f)-2.8f, (j * 0.4f)-2.8f, 0), Quaternion.identity);
	            _board[i][j].GetComponent<Tile>().i = i;
                _board[i][j].GetComponent<Tile>().j= j;
	        }
	    }

	    _ia = GetComponent<IA>();
	}

    public void EndPlayerTurn(int i, int j)
    {
        Board[i][j] = 2;
        PlayerTurn = false;

        if (CheckVictory(i, j))
        {
            Debug.Log("VICTORY");
            Application.LoadLevel(Application.loadedLevel);
        }


        _ia.MakePlay();
    }

    public void EndComputerTurn(int i, int j)
    {
      //  Debug.Log(i);
      //  Debug.Log(j);
        _board[i][j].GetComponent<Tile>().PutPiece(White);
        Board[i][j] = 1;
        PlayerTurn = true;

        if (CheckVictory(i, j))
        {
            Debug.Log("VICTORY");
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public bool CheckVictory(int i, int j)
    {
        return (CheckHorizontally(i, j) || CheckVertically(i, j) | CheckDiagonalAsc(i, j) || CheckDiagonalDesc(i,j));
    }

    private bool CheckHorizontally(int i, int j)
    {
        int v = Board[i][j];
     
        int firstCheckSum = 0;
        int secondCheckSum = 0;
        for (int m = 0; m <= 5; m++)
        {
          //  Debug.Log("VICTORYHori first : " + firstCheckSum);
            firstCheckSum = m;
            if (i + m >= 15)
                break;

            if (Board[i + m][j] != v)
                break;
        }
        
        for (int m = 0; m >= -5; m--)
        {
            secondCheckSum = -m;
            if (i + m < 0)
                break;

            if (Board[i + m][j] != v)
                break;
        }
//        if (firstCheckSum + secondCheckSum > 5)
//            Debug.Log("VICTORYHori first : "+firstCheckSum);
//            Debug.Log("VICTORYHori second : " + secondCheckSum);

        return (firstCheckSum+secondCheckSum > 5);
    }

    private bool CheckVertically(int i, int j)
    {
        int v = Board[i][j];
        
        int firstCheckSum = 0;
        int secondCheckSum = 0;
        for (int m = 0; m <= 5; m++)
        {
            firstCheckSum = m;
            if (j + m >= 15)
                break;

            if (Board[i][j+m] != v)
                break;
        }

        for (int m = 0; m >= -5; m--)
        {
            secondCheckSum = -m;
            if (j + m < 0)
                break;

            if (Board[i][j+m] != v)
                break;

        }

        if (firstCheckSum + secondCheckSum > 5)
            Debug.Log("VICTORYver");

        return (firstCheckSum + secondCheckSum > 5);
    }

    private bool CheckDiagonalAsc(int i, int j)
    {
        int v = Board[i][j];
        
        int firstCheckSum = 0;
        int secondCheckSum = 0;
        for (int m = 0; m <= 5; m++)
        {
            firstCheckSum = m;
            if (i + m >= 15 || j + m >= 15)
                break;

            if (Board[i + m][j+ m] != v)
                break;

        }


        for (int m = 0; m >= -5; m--)
        {
            secondCheckSum = -m;
            if (i + m < 0 || j + m < 0)
                break;

            if (Board[i + m][j + m] != v)
                break;

        }


        if (firstCheckSum + secondCheckSum > 5)
            Debug.Log("VICTORYasc");

        return (firstCheckSum + secondCheckSum > 5);
    }

    private bool CheckDiagonalDesc(int i, int j)
    {
        int v = Board[i][j];
        
        int firstCheckSum = 0;
        int secondCheckSum = 0;
        for (int m = 0; m <= 5; m++)
        {
            firstCheckSum = m;
            if (i + m >= 15 || j - m < 0)
                break;

            if (Board[i + m][j - m] != v)
                break;

        }


        for (int m = 0; m >= -5; m--)
        {
            secondCheckSum = -m;
            if (i + m < 0 || j - m >= 15)
                break;

            if (Board[i + m][j - m] != v)
                break;

        }

        if (firstCheckSum + secondCheckSum > 5)
            Debug.Log("VICTORYDESC");

        return (firstCheckSum + secondCheckSum > 5);
    }
	
}
