using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform target;

    //프리팹: 게임오브젝트 설계도 => 유니티에서 게임오브젝트를 생성할 때 만들 복사본
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletTime; //총알 생성 시간
    [SerializeField] float remainTime; //다음 총알 생성 할때까지 기다린 시간
    [SerializeField] bool isAttacking;

    private void Start()
    {
        //FindGameObjectWithTag: 게임에 있는 태그를 통해서 특정 게임오브젝트를 찾기
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        //GetComponent: 게임오브젝트에 있는 컴포넌트 가져오기
        target = playerObj.GetComponent<Transform>();

        //transform: 게임오브젝트의 위치, 회전, 크기를 관리하는 기능담당자
        //transform 컴포넌트는 모든 게임오브젝트에 반드시 있음 => transform컴포넌트만 특별하게 transform프로퍼티로 바로 사용 가능
        target = playerObj.transform;

        //위의 문장 한문장으로 : target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //공격 중이 아닐 때는 총알 생성을 하지 않도록 함
        if (!isAttacking) return;
        //다음 총알 생성까지 남은 시간을 계속 차감
        remainTime -= Time.deltaTime;    

        // 다음 총알을 생성할 때까지 남은 시간이 없는 경우 == 총알을 생성할 타이밍
        if (remainTime <= 0)
        {
            // BulletPrefab 설계도를 토대로 총알 생성
            // Instantiate : 프리팹을 토대로 게임 오브젝트 생성하기
            // Instantiate(생성할 게임 오브젝트, 생성할 위치, 생성할 방향)
            GameObject bulletGameObj = Instantiate(bulletPrefab, transform.position, transform.rotation); 
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();
            bullet.SetTarget(target);

            //다음 총알을 생성할때까지 남은 시간을 다시 설정
            remainTime = bulletTime;
        }
    }

    public void StartAttack()
    {
        isAttacking = true;
    }
    

    public void StopAttack()
    {
        isAttacking = false;
    }
}
