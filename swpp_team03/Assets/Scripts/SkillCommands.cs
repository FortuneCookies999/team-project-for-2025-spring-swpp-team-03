using UnityEngine;

// 청룡 스킬 - 근처 장애물 파괴
public class DragonSkillCommand : ISkillCommand
{
    private GameObject player;
    
    public DragonSkillCommand()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void Execute()
    {
        Debug.Log("근처 장애물 파괴!");
        if (player != null)
        {
            AreaDestroy areaDestroy = player.GetComponent<AreaDestroy>();
            if (areaDestroy != null)
            {
                areaDestroy.ManualTrigger();
            }
            else
            {
                Debug.LogWarning("🚫 AreaDestroy 스크립트가 없음!");
            }
        }
    }
    
    public bool CanExecute()
    {
        Debug.Log(player);
        Debug.Log(player.GetComponent<AreaDestroy>());
        return player != null && player.GetComponent<AreaDestroy>() != null;
    }
    
    public string GetSkillName()
    {
        return "청룡 스킬";
    }
}

// 백호 스킬 - 돌진
public class TigerSkillCommand : ISkillCommand
{
    private GameObject player;
    
    public TigerSkillCommand()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void Execute()
    {
        Debug.Log("🐯 백호 스킬 발동: 돌진!");
        if (player != null)
        {
            DashForward dash = player.GetComponent<DashForward>();
            if (dash != null)
            {
                dash.StartDash();
            }
            else
            {
                Debug.LogWarning("🚫 DashForward 컴포넌트가 없음!");
            }
        }
    }
    
    public bool CanExecute()
    {
        Debug.Log(player);
        Debug.Log(player.GetComponent<DashForward>());
        return player != null && player.GetComponent<DashForward>() != null;
    }
    
    public string GetSkillName()
    {
        return "백호 스킬";
    }
}

// 주작 스킬 - 점프
public class PhoenixSkillCommand : ISkillCommand
{
    private GameObject player;
    
    public PhoenixSkillCommand()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void Execute()
    {
        Debug.Log("점프!");
        if (player != null)
        {
            HighJump jumpSkill = player.GetComponent<HighJump>();
            if (jumpSkill != null)
            {
                jumpSkill.ManualTrigger();
            }
            else
            {
                Debug.LogWarning("🚫 HighJump 스크립트가 없음!");
            }
        }
    }
    
    public bool CanExecute()
    {
        Debug.Log(player);
        Debug.Log(player.GetComponent<HighJump>());
        return player != null && player.GetComponent<HighJump>() != null;
    }
    
    public string GetSkillName()
    {
        return "주작 스킬";
    }
}

// 현무 스킬 - 불덩이 발사
public class TurtleSkillCommand : ISkillCommand
{
    private GameObject player;
    
    public TurtleSkillCommand()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    public void Execute()
    {
        Debug.Log("🔥 불덩이 발사!");
        if (player != null)
        {
            HyunmuMode hyunmu = player.GetComponent<HyunmuMode>();
            if (hyunmu != null)
            {
                hyunmu.ManualTrigger();
            }
            else
            {
                Debug.LogWarning("🚫 HyunmuMode 스크립트가 없음!");
            }
        }
    }
    
    public bool CanExecute()
    {
        Debug.Log(player);
        Debug.Log(player.GetComponent<HyunmuMode>());
        return player != null && player.GetComponent<HyunmuMode>() != null;
    }
    
    public string GetSkillName()
    {
        return "현무 스킬";
    }
} 