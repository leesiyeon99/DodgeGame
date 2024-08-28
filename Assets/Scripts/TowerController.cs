using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform target;

    //������: ���ӿ�����Ʈ ���赵 => ����Ƽ���� ���ӿ�����Ʈ�� ������ �� ���� ���纻
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletTime; //�Ѿ� ���� �ð�
    [SerializeField] float remainTime; //���� �Ѿ� ���� �Ҷ����� ��ٸ� �ð�
    [SerializeField] bool isAttacking;

    private void Start()
    {
        //FindGameObjectWithTag: ���ӿ� �ִ� �±׸� ���ؼ� Ư�� ���ӿ�����Ʈ�� ã��
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        //GetComponent: ���ӿ�����Ʈ�� �ִ� ������Ʈ ��������
        target = playerObj.GetComponent<Transform>();

        //transform: ���ӿ�����Ʈ�� ��ġ, ȸ��, ũ�⸦ �����ϴ� ��ɴ����
        //transform ������Ʈ�� ��� ���ӿ�����Ʈ�� �ݵ�� ���� => transform������Ʈ�� Ư���ϰ� transform������Ƽ�� �ٷ� ��� ����
        target = playerObj.transform;

        //���� ���� �ѹ������� : target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //���� ���� �ƴ� ���� �Ѿ� ������ ���� �ʵ��� ��
        if (!isAttacking) return;
        //���� �Ѿ� �������� ���� �ð��� ��� ����
        remainTime -= Time.deltaTime;    

        // ���� �Ѿ��� ������ ������ ���� �ð��� ���� ��� == �Ѿ��� ������ Ÿ�̹�
        if (remainTime <= 0)
        {
            // BulletPrefab ���赵�� ���� �Ѿ� ����
            // Instantiate : �������� ���� ���� ������Ʈ �����ϱ�
            // Instantiate(������ ���� ������Ʈ, ������ ��ġ, ������ ����)
            GameObject bulletGameObj = Instantiate(bulletPrefab, transform.position, transform.rotation); 
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();
            bullet.SetTarget(target);

            //���� �Ѿ��� �����Ҷ����� ���� �ð��� �ٽ� ����
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
