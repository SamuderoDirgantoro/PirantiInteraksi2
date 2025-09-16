using UnityEngine;

public class EnemyIdle : BaseState
{
    private EnemySM _sm;
    private float _idleTime;
    private float _timer;

    public EnemyIdle(EnemySM stateMachine) : base("EnemyIdle", stateMachine)
    {
        _sm = (EnemySM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.gray;
        _timer = 0f;
        _idleTime = Random.Range(1f, 3f);
        _sm.rigidbody.velocity = Vector2.zero;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _timer += Time.deltaTime;

        if (_sm.player != null && Vector2.Distance(_sm.transform.position, _sm.player.position) < _sm.chaseRange)
        {
            stateMachine.ChangeState(_sm.chaseState);
            return;
        }

        if (_timer >= _idleTime)
        {
            stateMachine.ChangeState(_sm.patrolState);
        }
    }
}
