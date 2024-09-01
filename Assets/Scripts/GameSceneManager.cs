using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver }

    [SerializeField] GameState curState;
    [SerializeField] PlayerController player;
    [SerializeField] TowerController[] towers;
    [SerializeField] GameObject clearZone;

    [SerializeField] GameObject ready;
    [SerializeField] GameObject over;
    Coroutine clearZoneActive = null;
    Coroutine scoreroutine = null;

    [SerializeField] Text bestScoreText;
    [SerializeField] Text curScoreText;
    int score = 0;


    private void Start()
    {
        curState = GameState.Ready;
        //���� �ִ� ��� Ÿ����Ʈ�ѷ� ã���ֱ�
        towers = FindObjectsOfType<TowerController>();

        //GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
        //PlayerController playerController = playerGameObj.GetComponent<PlayerController>();

        player = FindObjectOfType<PlayerController>();
        player.OnDied += GameOver;
        ready.SetActive(true);
        over.SetActive(false);
        clearZone.SetActive(false);

        curScoreText.text = $"���� ����: {score}";
        bestScoreText.text = $"�ְ� ����: {GameManager.Instance.bestScore}";

    }


    private void Update()
    {
        if (curState == GameState.Ready && Input.anyKeyDown)
        {
            GameStart();
        }
        else if (curState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("DodgeScene");
        }
    }

    public void GameStart()
    {
        curState = GameState.Running;
        //Ÿ���� ���ݰ���
        foreach (var t in towers)
        {
            t.StartAttack();
        }
        ready.SetActive(false);
        over.SetActive(false);
        clearZone.SetActive(false);
        clearZoneActive =  StartCoroutine(ClearZoneActive());
        scoreroutine = StartCoroutine(Score());
    }

    public void GameOver()
    {
        curState = GameState.GameOver;
        foreach(var t in towers)
        {
            t.StopAttack();
        }
        ready.SetActive(false);
        over.SetActive(true);
        StopCoroutine(scoreroutine);
       
    }


    IEnumerator ClearZoneActive()
    {
        yield return new WaitForSeconds(20);
        clearZone.SetActive(true);
    }

    IEnumerator Score()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score++;
            curScoreText.text = $"���� ����: {score}";

            GameManager.Instance.SetBestScore(score);
            bestScoreText.text = $"�ְ� ����: {GameManager.Instance.bestScore}";

        }
    }
}
