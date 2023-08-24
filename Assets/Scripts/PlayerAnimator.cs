using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    private Player player;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GameObject.Find("PlayerVisual").GetComponent<Animator>();
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
