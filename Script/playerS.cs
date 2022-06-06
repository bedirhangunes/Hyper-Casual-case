using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Linq;


public class playerS : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [HideInInspector] public bool moveByTouch, gameStart;
    private Vector3 mouseStartPosition, boyPosition;
    [SerializeField] private float swipeSpeed, Distance;
    private Camera maincam;
    int step = 0;
    bool walked = false,dest;
    bool[] target = new bool[3];
    GameObject painter;
    GameObject lefthandTarget, rightHandTarget, spineTarget, targetPoints, agentTargetPoints;
 //   RigBuilder rigBuilder;
    float speed = 300, speed1 = 3;
    Vector3 targetAgent;
    Vector3 wallPosition = new Vector3(0f, 0f, 155);
    Vector3 afterHitPosition = new Vector3(0f, 0f, -130);

    public Text scoreText;
    int score;
    void Start()
    {
       
        step = 0;
        scoreText.text = "Tamamlanýyor % " + score;
        walked = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        maincam = Camera.main;
      //  rigBuilder = GetComponent<RigBuilder>();
        transform.position = afterHitPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
     //   GetComponent<RigBuilder>().enabled = true;
        agentTargetPoints = GameObject.Find("controlPoint").gameObject;
        rightHandTarget = transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject;
        spineTarget = transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;
        transform.rotation = Quaternion.Euler(0, 0, 0);targetAgent = agentTargetPoints.transform.GetChild(0).transform.position;
    }

   
    void Update()
    {
            agent.SetDestination(new Vector3(0f, 0f, 156.5f));
        animator.SetBool("run", true);
        if (Input.GetMouseButtonDown(0))
        {
            gameStart = moveByTouch = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;
        }
        if (moveByTouch)
        {
            float distance;
            var plane = new Plane(Vector3.up, 0f);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(plane.Raycast(ray,out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                Vector3 desirePos = mousePos - mouseStartPosition;
                Vector3 move = boyPosition + desirePos;
                move.x = Mathf.Clamp(move.x, -6f, 6f);//platformda bu aralýktaki x eksenlerine parmakla kaydýrýlýr
                move.z = -7f;
                var player = transform.position;
                player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * (swipeSpeed + 10F)), player.y, player.z);
                transform.position = player;
            }
        }
    }
    //private void LateUpdate()
    //{
    //    if (gameStart)
    //    {
    //        maincam.transform.position = new Vector3(Mathf.Lerp(maincam.transform.position.x, transform.position.x, (swipeSpeed - 5f) * Time.deltaTime
    //            ), maincam.transform.position.y, maincam.transform.position.z);
    //    }
    //}
     void OnTriggerEnter(Collider col)
    {
        if (col.tag == "wall")
        {
           StartCoroutine(wallPaint());
        }
        if (col.tag == "ban")
        {
            transform.position = afterHitPosition;
        }
        if (col.tag == "wallred")
        {
            score += 20;
            scoreText.text = "TAMAMLANMIÞ % " + score;
            if (score == 920)
            {
                SceneManager.LoadScene("level2");
            }
        }
    }
    IEnumerator wallPaint()
    {
        yield return new WaitForSeconds(0.50f);
        animator.SetBool("run", false);

        animator.SetBool("Jab", true);
      
    }
}

