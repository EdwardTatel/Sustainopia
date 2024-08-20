using UnityEngine;

public class LinearCongruentialGenerator : MonoBehaviour
{
    private long seed;
    private const long a = 1664525;
    private const long c = 1013904223;
    private const long m = 4294967296; // 2^32

    public LinearCongruentialGenerator(long seed)
    {
        this.seed = seed;
    }

    public float NextFloat(float min, float max)
    {
        seed = (a * seed + c) % m;
        return min + (float)seed / m * (max - min);
    }

    public int NextInt(int min, int max)
    {
        seed = (a * seed + c) % m;
        return min + (int)(seed % (max - min));
    }
}