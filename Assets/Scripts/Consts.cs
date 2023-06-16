using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeteorVoyager.Assets.Scripts
{
    public class Consts
    {
        public static float RBorder
        {
            get
            {
                return RUBorder[0];
            }
        }
        public static float LBorder
        {
            get
            {
                return LDBorder[0];
            }
        }
        public static float UBorder
        {
            get
            {
                return RUBorder[1];
            }
        }
        public static float BBorder
        {
            get
            {
                return LDBorder[1];
            }
        }
        public const float DATA_BONUS = 0.05f;
        private static Vector2 RUBorder
        {
            get
            {
                return Camera.main.ViewportToWorldPoint(Vector2.one);
            }
        }
        private static Vector2 LDBorder
        {
            get
            {
                return Camera.main.ViewportToWorldPoint(Vector2.zero);
            }
        }
    }
}