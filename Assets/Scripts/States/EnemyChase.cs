using UnityEngine;

public class EnemyChase : BaseState
{
    private EnemySM _sm;

    public EnemyChase(EnemySM stateMachine) : base("EnemyChase", stateMachine)
    {
        _sm = (EnemySM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.spriteRenderer.color = Color.red;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_sm.player == null) return;

        float distance = Vector2.Distance(_sm.transform.position, _sm.player.position);

        if (distance > _sm.chaseRange)
        {
            stateMachine.ChangeState(_sm.patrolState);
            return;
        }

        if (_sm.player.position.y > _sm.transform.position.y + 1f)
        {
            stateMachine.ChangeState(_sm.jumpingState);
            return;
        }

        if (distance <= _sm.attackRange)
        {
            stateMachine.ChangeState(_sm.idleState);
            return;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (_sm.player == null) return;

        Vector2 direction = (_sm.player.position - _sm.transform.position).normalized;
        Vector2 vel = _sm.rigidbody.velocity;
        vel.x = direction.x * _sm.chaseSpeed;
        _sm.rigidbody.velocity = vel;
    }
}
