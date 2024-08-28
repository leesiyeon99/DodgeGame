using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    public event Action OnDied;

    private void Update()
    {

        Move();
    }

    private void Move()
    {
        // 입력 값을 받아 저장
        // 입력매니저: Edit -> ProjectSetting 에서 설정한 이름의 입력 방식을 사용
        // GetAxis() : 축 입력 -1 ~ 1 float 값 => 조이스틱처럼 조금 입력도 가능
        // GetAxisRaw() : 축 입력 -1, 0, +1 float 값 소수점 없이 입력 여부 판단 => 키보드처럼 누르고 있는 경우에 적합
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // 콘솔: 게임의 정보를 텍스트 형태로 제작자에게 알려주는 창
        Debug.Log($"{x}, {z}");

        // 정규화: 크기가 1이 아닌 벡터의 크기를 1로 만들기
        Vector3 moveDir = new Vector3(x, 0, z);
        //moveDir.Normalize();
        //= new Vector3 (x,0,z).Normalized;
        if (moveDir.magnitude > 1) //magnitude: 백터의 크기
        {
            moveDir.Normalize();
        }

        // 리지드바디: 물리엔진담당의 컴포넌트
        // AddForce, AddTorque, velocity, angularVelocity
        rigid.velocity = moveDir * moveSpeed;
        if (moveDir == Vector3.zero) { return; }
        rigid.rotation = Quaternion.Lerp(rigid.rotation, Quaternion.LookRotation(rigid.velocity), rotateSpeed * Time.deltaTime);
    }

    public void TakeHit()
    {
        OnDied?.Invoke();
        Destroy(gameObject);
    }
}
