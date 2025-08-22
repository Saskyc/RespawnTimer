using UnityEngine;

namespace RespawnTimer.API;

public class UnCld
{
    public float Cooldown = 3f;
    public float Timer = 0f;

    public void AppendTime()
    {
        Timer += Time.deltaTime;
    }
    
    public UnCld(){}

    public UnCld(float cooldown)
    {
        Cooldown = cooldown;
    }
    
    public bool IsReady => Timer >= Cooldown;

    public void Reset()
    {
        Timer = 0f;
    }

    public static implicit operator UnCld(float cooldown)
    {
        return new UnCld(cooldown);
    }
}