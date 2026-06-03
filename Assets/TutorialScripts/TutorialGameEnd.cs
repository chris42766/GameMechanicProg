using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TutorialGameEnd : MonoBehaviour
{
    public int GEndscore = 0; // keeping count of targets hit, when sprite changes
    int currentlvlIndex;
    public GameObject phase1;
    public GameObject phase2;
    public GameObject phase3;
    public GameObject phase4;
    public GameObject phase4txt;
    public GameObject combotxt;
    public GameObject phase5;
    public GameObject phase6;
    public float cooldown =3f;
    public float cooldown2=3f;
    public float cooldown3 = 3f;
    public bool explode = false;




    void Start()
    {
    phase2.SetActive(false);
    phase3.SetActive(false);
    phase4.SetActive(false);
        phase4txt.SetActive(false); 
    combotxt.SetActive(false);
        phase5.SetActive(false);
        phase6.SetActive(false);
    }
    void Update()
    {
    
    if (GEndscore == 2)
     {
       //StartCoroutine(cooldown());
       phase1.SetActive(false);
            phase6.SetActive(true);
                }
   
    
    }


   
}