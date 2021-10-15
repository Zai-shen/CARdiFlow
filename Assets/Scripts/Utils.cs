using UnityEngine;

public static class Utils
{
    public static int[] ResolutionStringToIntArray(string res)
    {
        int[] resI = new int[2];
        string[] splits = res.Split('x');

        int.TryParse(splits[0], out int xVal);
        int.TryParse(splits[1].Split('@')[0], out int yVal);
        resI[0] = xVal;
        resI[1] = yVal;

        return resI;
    }
}