using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid; //플레이어에 닿거나 벽에 닿을 수 있게 리지드바디 적용
    [SerializeField] float speed; //총알의 속도
    [SerializeField] Transform target; //어떤 대상을 향에 총알이 날아갈지

    private void Start()
    {
        //총알이 타켓을 바라보게 한 뒤 속도를 줌
        //LookAt: 타켓 방향을 바라보는 회전
        transform.LookAt(target.position);

        rigid.velocity = transform.forward * speed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    //충돌(Collision): 물리적 충돌을 확인
    //트리거(Trigger): 겹침여부 확인
    //유니티 충돌 메시지 함수

    //충돌 진입 시점(플레이어와 부딛혔을 때)
    private void OnCollisionEnter(Collision collision)
    {
        //Collision 매개변수: 충돌한 상황에 대한 정보들을 가지고 있다. (ex. 충돌한 다른 충돌체, 받은 힘, 부딪힌 지점 등)
        if (collision.gameObject.tag == "Player")
        {
            //총알이 플레이어와 부딪혔을 때
            //총알이 플레이어와 부딪히는 순간 OnCollisionEnter가 호출되면서 플레이어컨트롤러를 가져올 수 있음
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }
        Destroy(gameObject);
    }

}
