using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{

    public GameObject Black;
    public GameObject White;
    public int i;
    public int j;
    public bool IsWhite;

    private GameObject _placeHolder;
    private bool _isFree = true;
    
	void Start ()
	{
	    _placeHolder = transform.GetChild(0).gameObject;
	}

    public void PutPiece(GameObject piece)
    {
       
        if (_isFree)
        {
            _isFree = false;
            GameObject.Instantiate(piece, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseDown()
    {
        if (Game.Instance.PlayerTurn && _isFree)
        {
            PutPiece(Black);
            Game.Instance.EndPlayerTurn(i, j);
        }
       
    }

    void OnMouseExit()
    {
        _placeHolder.SetActive(false);
    }

    void OnMouseOver()
    {
       

    }

    void OnMouseEnter()
    {
        if(Game.Instance.PlayerTurn)
            _placeHolder.SetActive(true);
    }
}
