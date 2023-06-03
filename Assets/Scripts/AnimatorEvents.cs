using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationTrigger()
    {
        player.AttackOver();
    }
}
