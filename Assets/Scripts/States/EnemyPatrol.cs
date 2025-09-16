using UnityEngine;

public class EnemyPatrol : BaseState
{
    private EnemySM _sm;
    private int _direction = 1;
    private float _patrolTime;
    private float _timer;

    public EnemyPatrol(EnemySM stateMachine) : base("EnemyPatrol", stateMachine)
    {
        _sm = (EnemySM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.yellow;

        _direction = Random.value > 0.5f ? 1 : -1;
        _patrolTime = Random.Range(2f, 4f);
        _timer = 0f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _timer += Time.deltaTime;

        if (_timer >= _patrolTime)
        {
            stateMachine.ChangeState(_sm.idleState);
            return;
        }

        if (Random.value < 0.002f)
        {
            _direction *= -1;
        }

        if (_sm.player != null && Vector2.Distance(_sm.transform.position, _sm.player.position) < _sm.chaseRange)
        {
            stateMachine.ChangeState(_sm.chaseState);
            return;
        }

        if (Random.value < 0.001f)
        {
            stateMachine.ChangeState(_sm.jumpingState);
            return;
        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        Vector2 vel = _sm.rigidbody.velocity;
        vel.x = _direction * _sm.patrolSpeed;
        _sm.rigidbody.velocity = vel;
    }
}
