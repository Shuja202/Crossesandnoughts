using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gamecontroller : MonoBehaviour
{

    public GameObject circleprefab;
    public GameObject crossprefab;

    public int[] placements = new int[9];
    // crosses - 1
    // circles - 2
    // no placements - 0

    int currentplayer;

    public TextMeshProUGUI currentplayertext;
    public TextMeshProUGUI wintext;
    public GameObject winpanel;
    GameObject currentPlayersTurnPrefab = null;
    // Update is called once per frame


    private void Start()
    {
        winpanel.SetActive(false);

        currentplayer = 1;
        string playername = "Crosses";
        if(currentplayer == 2) { playername = "Noughts"; }
        currentplayertext.text = "Current Turn:\n<b>" + playername + "</b>";
        if(currentplayer == 1)
        {
            currentPlayersTurnPrefab = crossprefab;
        }else if(currentplayer == 2)
        {
            currentPlayersTurnPrefab = circleprefab;
        }

        
        for (int i = 0; i < placements.Length; i++)
        {
            placements[i] = 0;
        }
    }



    void Update()
    {
        
        bool clickedtile = false;
        GameObject selectedtile = null;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);
            if (hit.collider != null)
            {
                clickedtile = true;
                selectedtile = hit.collider.gameObject;
            }
        }

        if (clickedtile)
        {
            GameObject newprefab = GameObject.Instantiate(currentPlayersTurnPrefab);
            newprefab.transform.position = selectedtile.transform.position;

            string name = selectedtile.name;
            name = name.Replace("tile", "");
            Debug.Log(name);
            int num = int.Parse(name);
            placements[num-1] = currentplayer;
            Destroy(selectedtile);
            changeturn();

            if (checkwin(1))
            {
                winpanel.SetActive(true);
                wintext.text = "Crosses win!";
            }else if (checkwin(2))
            {
                Debug.Log("Circles Win");
                winpanel.SetActive(true);
                wintext.text = "Noughts win!";

            }
        }
        

    }

    bool checkwin(int playertocheck)
    {
        if(placements[0] == playertocheck && placements[1] == playertocheck && placements[2] == playertocheck)
        {
            return true;
        }
        if (placements[3] == playertocheck && placements[4] == playertocheck && placements[5] == playertocheck)
        {
            return true;
        }
        if (placements[6] == playertocheck && placements[7] == playertocheck && placements[8] == playertocheck)
        {
            return true;
        }
        if (placements[0] == playertocheck && placements[4] == playertocheck && placements[8] == playertocheck)
        {
            return true;
        }
        if (placements[2] == playertocheck && placements[4] == playertocheck && placements[6] == playertocheck)
        {
            return true;
        }
        return false;

    }

    void changeturn()
    {
        if(currentPlayersTurnPrefab == circleprefab)
        {
            currentPlayersTurnPrefab = crossprefab;
            currentplayer = 1;
        }
        else
        {
            currentPlayersTurnPrefab = circleprefab;
            currentplayer = 2;
        }
        string playername = "Crosses";
        if (currentplayer == 2) { playername = "Noughts"; }
        currentplayertext.text = "Current Turn:\n<b>" + playername + "</b>";
    }
    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
