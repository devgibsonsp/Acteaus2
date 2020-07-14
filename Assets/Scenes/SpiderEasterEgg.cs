using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEasterEgg : MonoBehaviour
{
    private Animator Anim { get; set; }
    private float Timer { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Timer = 1f;
        Anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if(Anim.GetBool("Attack") && Timer <= 0)
        {
            Timer = 1f;
            Anim.SetBool("Attack", false);
        }
    }

    public void OnSpiderClick()
    {
        Anim.SetBool("Attack", true);
    }
}
