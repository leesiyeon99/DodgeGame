using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { Ready, Running, GameOver }

    [SerializeField] GameState curState;
    [SerializeField] PlayerController player;
    [SerializeField] TowerController[] towers;

    [SerializeField] GameObject ready;
    [SerializeField] GameObject over;

    private void Start()
    {
        curState = GameState.Ready;
        //씬에 있는 모든 타워컨트롤러 찾아주기
        towers = FindObjectsOfType<TowerController>();

        //GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
        //PlayerController playerController = playerGameObj.GetComponent<PlayerController>();

        player = FindObjectOfType<PlayerController>();
        player.OnDied += GameOver;
        ready.SetActive(true);
        over.SetActive(false);

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
        //타워들 공격개시
        foreach (var t in towers)
        {
            t.StartAttack();
        }
        ready.SetActive(false);
        over.SetActive(false);
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
    }
}
