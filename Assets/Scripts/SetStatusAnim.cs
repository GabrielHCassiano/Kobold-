using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStatusAnim : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetInHurt()
    {
        if (playerControl == null)
            enemyAI.ResetInHurt();
        else
            playerControl.ResetInHurt();
    }
}
