using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    private int currentLevel;

    [SerializeField] private DisplayManager displayManager;

    [SerializeField] private GameObject controlledBall;
    private Rigidbody rb;

    [SerializeField] private GameObject level2Barrier;
    [SerializeField] private GameObject level3Barrier;
    [SerializeField] private GameObject level4Barrier;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        rb = controlledBall.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        //Condition de passage au niveau 2

        //Condition de passage au niveau 3

        //Condition de passage au niveau 4
        if (rb.velocity.magnitude >= 60 && currentLevel == 3)
        {
            currentLevel++;
            Destroy(level3Barrier);
            displayManager.DisplayProcess(currentLevel);
        }
    }
}
