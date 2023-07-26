using UnityEngine;

public static class Consts
{
    public static float RBorder => RuBorder[0];

    public static float LBorder => LdBorder[0];

    public static float UBorder => RuBorder[1];

    public static float BBorder => LdBorder[1];
    private static Vector2 RuBorder => Camera.main!.ViewportToWorldPoint(Vector2.one);

    private static Vector2 LdBorder => Camera.main!.ViewportToWorldPoint(Vector2.zero);
}