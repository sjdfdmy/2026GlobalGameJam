using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class SkeletonKnight : Monster
{
    private bool isEnraged = false; // 是否狂暴状态（血量低于50%）
    private float enrageMultiplier = 1.5f; // 狂暴时速度加成

    void Start()
    {
        Reset();
    }

    void Update()
    {
        LoadState();

        // 检查是否需要进入狂暴状态
        if (!isEnraged && health <= monsterdata.health * 0.5f)
        {
            Enrage();
        }
    }

    public override void Attack(Collider2D other, int id)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BasicControl>()?.TakeDamage(damage);
        }
    }

    public override void Die()
    {
        _isDead = true;
        // 可以添加死亡动画、特效等
        Destroy(gameObject, 0.5f); // 延迟销毁以播放动画
    }

    // 进入狂暴状态（血量低于50%时触发）
    void Enrage()
    {
        isEnraged = true;
        speed *= enrageMultiplier; // 速度变快
        anim.SetTrigger("Enrage"); // 如果有动画的话
        Debug.Log("骷髅骑士进入狂暴状态！速度提升！");
    }

    #region 行为状态实现
    protected override void IdleState(float dist)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (dist < monsterdata.detectRange)
        {
            currentState = State.Chase;
            return;
        }

        idleTimer += Time.deltaTime;
        if (idleTimer >= monsterdata.idleTime)
        {
            idleTimer = 0;
            currentState = State.Patrol;
            facingRight = Random.value > 0.5f;
            float moveDir = facingRight ? 1 : -1;
            transform.localScale = new Vector3(moveDir * baseScaleX, transform.localScale.y, 1);
        }
    }

    protected override void PatrolState(float dist)
    {
        if (dist < monsterdata.detectRange)
        {
            currentState = State.Chase;
            return;
        }

        patrolTimer += Time.deltaTime;
        float moveDir = facingRight ? 1 : -1;

        // 如果狂暴状态，巡逻速度也加快
        float currentSpeed = isEnraged ? speed * 1.2f : speed;
        rb.velocity = new Vector2(moveDir * currentSpeed, rb.velocity.y);

        if (ShouldTurn(moveDir))
        {
            patrolTimer = 0;
            FaceTo(-moveDir);
            currentState = State.Idle;
            return;
        }

        if (patrolTimer >= monsterdata.patrolDuration)
        {
            patrolTimer = 0;
            FaceTo(-moveDir);
            currentState = State.Idle;
            return;
        }

        if (Random.value < 0.0001f) currentState = State.Idle;
    }

    protected override void ChaseState(float dist)
    {
        if (dist <= monsterdata.attackRange)
        {
            rb.velocity = Vector2.zero;
            currentState = State.Attack;
            return;
        }

        if (dist > monsterdata.detectRange * 1.5f)
        {
            currentState = State.Idle;
            return;
        }

        float dirX = player.position.x > transform.position.x ? 1 : -1;

        if (ShouldTurn(dirX))
        {
            rb.velocity = Vector2.zero;
            FaceTo(-dirX);
            return;
        }

        // 狂暴状态追逐更快
        float chaseSpeed = isEnraged ? speed * 1.8f : speed * 1.5f;
        rb.velocity = new Vector2(dirX * chaseSpeed, rb.velocity.y);
        FaceTo(dirX);
    }

    protected override void AttackState(float dist)
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= monsterdata.attackCooldown)
        {
            attackTimer = 0;

            // 面对玩家方向
            float dirX = player.position.x > transform.position.x ? 1 : -1;
            FaceTo(dirX);

            // 播放攻击动画
            anim.SetTrigger("Attack");

            // 根据距离决定下一步状态
            if (dist <= monsterdata.attackRange)
            {
                // 在原地继续攻击
                rb.velocity = Vector2.zero;
                currentState = State.Attack;
            }
            else
            {
                // 追逐玩家
                float chaseSpeed = isEnraged ? speed * 1.8f : speed * 1.5f;
                rb.velocity = new Vector2(dirX * chaseSpeed, rb.velocity.y);
                currentState = State.Chase;
            }
        }
        else
        {
            // 攻击过程中面向玩家但不移动
            float dirX = player.position.x > transform.position.x ? 1 : -1;
            FaceTo(dirX);
        }
    }
    #endregion
}