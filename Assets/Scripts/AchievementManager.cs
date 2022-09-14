using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    private int points = 0;
    private int jumps = 3;

    [SerializeField]
    [Range(0, 2)]
    float delay = 1;

    [Header("Set UI")]
        [SerializeField]
        Text pointsUI;

        [SerializeField]
        Text jumpsUI;
   
    void Start()
    {
        SetPoints(points);
        SetJumps(jumps);
        
       
    }
    void DestroyPlayer()
    {
        Destroy(gameObject);
    }
    public void AddPoints(int pointCount)
    {
        points += pointCount;
        SetPoints(points);
    }
    public void TakeJump()
    {
        jumps -= 1;
        SetJumps(jumps);
        if (jumps == -1)
        {
            DestroyPlayer();
            StartCoroutine(ReloadLevel());
            
            
        }

    }
    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        
    }
    void SetPoints(int points)
    {
        pointsUI.text = "Points: " + points;
    }
    void SetJumps(int jumps)
    {
        jumpsUI.text = "Jumps: " + jumps;
    }
   
    
   
}
