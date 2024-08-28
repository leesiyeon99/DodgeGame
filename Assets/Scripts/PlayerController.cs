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
        // �Է� ���� �޾� ����
        // �Է¸Ŵ���: Edit -> ProjectSetting ���� ������ �̸��� �Է� ����� ���
        // GetAxis() : �� �Է� -1 ~ 1 float �� => ���̽�ƽó�� ���� �Էµ� ����
        // GetAxisRaw() : �� �Է� -1, 0, +1 float �� �Ҽ��� ���� �Է� ���� �Ǵ� => Ű����ó�� ������ �ִ� ��쿡 ����
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // �ܼ�: ������ ������ �ؽ�Ʈ ���·� �����ڿ��� �˷��ִ� â
        Debug.Log($"{x}, {z}");

        // ����ȭ: ũ�Ⱑ 1�� �ƴ� ������ ũ�⸦ 1�� �����
        Vector3 moveDir = new Vector3(x, 0, z);
        //moveDir.Normalize();
        //= new Vector3 (x,0,z).Normalized;
        if (moveDir.magnitude > 1) //magnitude: ������ ũ��
        {
            moveDir.Normalize();
        }

        // ������ٵ�: ������������� ������Ʈ
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
