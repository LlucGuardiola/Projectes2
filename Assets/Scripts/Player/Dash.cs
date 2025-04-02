using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class 冲刺 : MonoBehaviour // "Dash" → "冲刺"
{
    [SerializeField] private float 冲刺冷却时间; // dashCooldown → 冲刺冷却时间
    [SerializeField] private float 冲刺速度; // dashSpeed → 冲刺速度
    private Rigidbody2D 刚体; // _rigidbody → 刚体
    private bool 正在冲刺; // isDashing → 正在冲刺
    private Vector2 当前目标; // currentTarget → 当前目标
    private Vector2 下一个目标; // nextTarget → 下一个目标
    private Vector2 方向; // direction → 方向
    private float 初始重力; // initialGravity → 初始重力

    private void 开始() // Start → 开始
    {
        刚体 = GetComponent<Rigidbody2D>();
        初始重力 = 刚体.gravityScale;
    }

    private void 启用() // OnEnable → 启用
    {
        SpecialAbility.OnDash += 执行冲刺;
    }

    private void 禁用() // OnDisable → 禁用
    {
        SpecialAbility.OnDash -= 执行冲刺;
    }

    private void 执行冲刺(Vector2 目标) // DashToTarget → 执行冲刺
    {
        if (!正在冲刺 && (冲刺冷却时间 == 0))
        {
            当前目标 = 目标;
            正在冲刺 = true;
            方向 = (目标 - (Vector2)transform.position).normalized;
        }
        else if (正在冲刺) 下一个目标 = 目标;
    }

    private bool 检查冲刺结束() // CheckDashEnd → 检查冲刺结束
    {
        if (((Vector2)transform.position - 当前目标).sqrMagnitude < 1f)
        {
            GetComponent<PlayerMovement>().CanMove = true;
            刚体.linearVelocity = Vector2.zero;

            if (下一个目标 != Vector2.positiveInfinity)
            {
                当前目标 = 下一个目标;
                下一个目标 = Vector2.positiveInfinity;

                //方向 = (当前目标 - (Vector2)transform.position).normalized;
                正在冲刺 = true;
            }

            刚体.gravityScale = 初始重力;
            transform.rotation = Quaternion.identity;
            GetComponent<BoxCollider2D>().enabled = true;
            return false;
        }

        return true;
    }

    private void 固定更新() // FixedUpdate → 固定更新
    {
        if (!正在冲刺) return;
        刚体.gravityScale = 0;

        if (GetComponent<BoxCollider2D>().enabled) GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Animator>().SetBool("IsOnAir?", 正在冲刺);

        float 角度 = Mathf.Atan2(方向.y, 方向.x) * Mathf.Rad2Deg;
        方向.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, 角度);

        刚体.linearVelocity = 方向 * 冲刺速度;

        正在冲刺 = 检查冲刺结束();
    }
}
