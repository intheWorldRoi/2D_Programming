using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDirector : MonoBehaviour
{
    PlayerInterface player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInterface>(); //업캐스팅
    }

    // Update is called once per frame
    void Update()
    {
        player.Fire(); // 이건 그냥 playerController 스크립트에서 해도 되지만 다향성을 구현해보고싶어서 플레이어가 취하는 행동 관련 메서드 호출하도록 함
        player.Jump();
        player.facing();
        player.bowingDown();
    }

    private void FixedUpdate()
    {
        player.Move();
        
    }
}
