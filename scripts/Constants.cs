using Godot;
using System;
using System.Collections.Generic;

public static class Constants {
    public const string DRAW_CARD = "draw_card";
    public const string SKIP_CARD = "skip_card";

    private static readonly Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
    public static void DestroyChildren(this Node node) {
        for (int i = 0; i < node.GetChildren().Count; i++) {
            node.GetChild(i).QueueFree();
        }
    }
}