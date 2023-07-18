using UnityEngine;

public class Consts
{
    public static float RBorder
    {
        get
        {
            return RuBorder[0];
        }
    }
    public static float LBorder
    {
        get
        {
            return LdBorder[0];
        }
    }
    public static float UBorder
    {
        get
        {
            return RuBorder[1];
        }
    }
    public static float BBorder
    {
        get
        {
            return LdBorder[1];
        }
    }
    public const float DATA_BONUS = 0.05f;
    private static Vector2 RuBorder
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(Vector2.one);
        }
    }
    private static Vector2 LdBorder
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(Vector2.zero);
        }
    }
}