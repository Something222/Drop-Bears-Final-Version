using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyManager instance = null;
    [SerializeField] private GameObject[] enemies;
    [SerializeField]   private GameManager code;
    [SerializeField] private EnemyAIBase test;
    [SerializeField]private int enemiesAlive;
    private GameObject actingEnemy;
    private bool onlyOnce = false;
    bool firstEnemyHasActed = false;
    
    public GameObject ActingEnemy { get => actingEnemy; set => actingEnemy = value; }
    public GameObject[] Enemies { get => enemies; set => enemies = value; }
    public bool FirstEnemyHasActed { get => firstEnemyHasActed; set => firstEnemyHasActed = value; }
    public int EnemiesAlive { get => enemiesAlive; set => enemiesAlive = value; }
    #region Singleton
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }       
    }
    public IEnumerator WaitForTurn()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
           EnemyAIBase enemyBrain = Enemies[i].GetComponent<EnemyAIBase>();
            if (enemyBrain.Acting)
            {
                actingEnemy = Enemies[i];
            }
            if (enemyBrain.TurnCompleted == true && Enemies.Length>i+1)
            {
                Enemies[i + 1].GetComponent<EnemyAIBase>().TakeTurn = true;
            }
        }
      
        yield return new WaitForSeconds(.1f);
    }
    //public void SwitchToPlayerTurns()
    //{
    //    code.EnemyPhase = false;
    //        code.MenuPhase = true;
    //        code.PlayerTurn = true;
    //        FirstEnemyHasActed = false;
        
    //}
    public void SwitchToPlayerTurnsEnum()
    {
        code.CurrPhase = GameManager.Phase.menuPhase;
        FirstEnemyHasActed = false;
        code.PlayerTurn = true;
        ResetEnemyTurns();
        BtnManager.instance.x = 0;
        BtnManager.instance.y = 1;
    }
    public void ResetEnemyTurns()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemyAIBase enemyBrain = Enemies[i].GetComponent<EnemyAIBase>();
            enemyBrain.TakeTurn = false;
            enemyBrain.OnlyOnce = false;
            enemyBrain.TurnCompleted = false;
        }
    }
    #endregion Singleton
    // Update is called once per frame
    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in Enemies)
        {
            EnemiesAlive++;
        }
    }
   
}
