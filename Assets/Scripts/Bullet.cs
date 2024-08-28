using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid; //�÷��̾ ��ų� ���� ���� �� �ְ� ������ٵ� ����
    [SerializeField] float speed; //�Ѿ��� �ӵ�
    [SerializeField] Transform target; //� ����� �⿡ �Ѿ��� ���ư���

    private void Start()
    {
        //�Ѿ��� Ÿ���� �ٶ󺸰� �� �� �ӵ��� ��
        //LookAt: Ÿ�� ������ �ٶ󺸴� ȸ��
        transform.LookAt(target.position);

        rigid.velocity = transform.forward * speed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    //�浹(Collision): ������ �浹�� Ȯ��
    //Ʈ����(Trigger): ��ħ���� Ȯ��
    //����Ƽ �浹 �޽��� �Լ�

    //�浹 ���� ����(�÷��̾�� �ε����� ��)
    private void OnCollisionEnter(Collision collision)
    {
        //Collision �Ű�����: �浹�� ��Ȳ�� ���� �������� ������ �ִ�. (ex. �浹�� �ٸ� �浹ü, ���� ��, �ε��� ���� ��)
        if (collision.gameObject.tag == "Player")
        {
            //�Ѿ��� �÷��̾�� �ε����� ��
            //�Ѿ��� �÷��̾�� �ε����� ���� OnCollisionEnter�� ȣ��Ǹ鼭 �÷��̾���Ʈ�ѷ��� ������ �� ����
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }
        Destroy(gameObject);
    }

}
