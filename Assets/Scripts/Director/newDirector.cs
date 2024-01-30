using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newDirector : MonoBehaviour
{
    PlayerInterface player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInterface>(); //��ĳ����
    }

    // Update is called once per frame
    void Update()
    {
        player.Fire(); // �̰� �׳� playerController ��ũ��Ʈ���� �ص� ������ ���⼺�� �����غ���; �÷��̾ ���ϴ� �ൿ ���� �޼��� ȣ���ϵ��� ��
        player.Jump();
        player.facing();
        player.bowingDown();
    }

    private void FixedUpdate()
    {
        player.Move();
        
    }
}
