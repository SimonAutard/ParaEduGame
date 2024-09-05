using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GateOpener : MonoBehaviour
{
    public int currentLevel;

    [SerializeField] private DisplayManager displayManager;

    //Gestion des barrières
    [SerializeField] private GameObject[] levelBarriers;
    [SerializeField] float moveSyncWithNPCMargin = 0.2f; // marge d'erreur sur la vélocité quand le joueur se synchro avec les npc
    private float yThinkOutBox; // hauteur de la barriere pour l'étage où il faut contourner

    //Gestion des NPC
    [SerializeField] float NPCSpeed;
    [SerializeField] float NPCAmplitude;
    private float NPCvelocity;
    [SerializeField] private GameObject level1NPC;
    [SerializeField] private GameObject level2NPC;
    [SerializeField] private GameObject level3NPC;
    private GameObject[] levelNPC;

    //Gestion du player
    [SerializeField] private GameObject controlledBall;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;

        rb = controlledBall.GetComponent<Rigidbody>();

        yThinkOutBox = levelBarriers[1].transform.position.y;

        level1NPC.SetActive(true);
        level2NPC.SetActive(false);
        level3NPC.SetActive(false);
        levelNPC = new GameObject[] { level1NPC, level2NPC, level3NPC };
    }

    private void FixedUpdate()
    {
        //Gestion des NPC
        // Calcule la vitesse verticale en fonction du temps, de l'amplitude et de la vitesse
        NPCvelocity = Mathf.Cos(Time.time * NPCSpeed) * NPCAmplitude;
        foreach (GameObject npcGroup in levelNPC)
        {
            if (npcGroup.activeSelf == true & currentLevel == 1)
            {
                foreach (Transform child in npcGroup.transform)
                {

                    // Applique la vélocité au Rigidbody
                    child.GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x, NPCvelocity, rb.velocity.z);
                }
            }
        }

    }


    public void OpenGateAttempt(int protectedLevel)
    {
        //Condition de passage au niveau 2
        if (currentLevel == 1 & CheckForSynchronicity())
        {
            GoToNextLevel();
            level2NPC.SetActive(true);

        }
        //Condition de passage au niveau 3
        if (currentLevel == 2 & CheckForOutsideBox())
        {
            GoToNextLevel();
            level3NPC.SetActive(true);
        }
        //Condition de passage au niveau 4
        if (currentLevel == 3 & CheckForSpeed())
        {
            GoToNextLevel();
        }

        if (currentLevel == 4 )
        {
            Debug.Log("wahou");
        }
    }

    //Renvoie true si la boule est assez rapide, false sinon
    private bool CheckForSpeed()
    {
        if (rb.velocity.magnitude >= 80)        {            return true;        }
        else {            return false;        }

    }

    //Renvoie true si la boule est synchro zvec les NPC, false sinon
    private bool CheckForSynchronicity()
    {
        float bornUp = Math.Abs(NPCvelocity * (1 + moveSyncWithNPCMargin));
        float bornLow = Math.Abs(NPCvelocity * (1 - moveSyncWithNPCMargin));
        float vel = Math.Abs(rb.velocity.y);
        if (vel < bornUp & vel > bornLow) { StopAllNPC(); return true; }
        else { return false; }  
    }
    
    //Renvoie true si la balle est assez haute, false sinon. Sert à vérifier que le joeuur a pensé hors de la boite et donc s'est échappé de l'étage 2
    private bool CheckForOutsideBox()
    {
        if(controlledBall.transform.position.y > yThinkOutBox) {return true;}
        else { return false; }
    }

    //Renvoie true si la boule a pris assez d'élan, false sinon
    private bool CheckForMomentum()
    {
        return true;
    }

    //Fonction qui déclenche la procédure complète de changement de niveau
    private void GoToNextLevel()
    {
        currentLevel++;
        Destroy(levelBarriers[currentLevel-2]);
        displayManager.DisplayProcess(currentLevel);

    }
    private void StopAllNPC()
    {
        foreach (GameObject npcGroup in levelNPC)
        {
            if (npcGroup.activeSelf == true)
            {
                foreach (Transform child in npcGroup.transform)
                {

                    // Applique la vélocité au Rigidbody
                    child.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }
}
