public class Math
{
	public static int abs(int i)
	{
		if (i > 0)
		{
			return i;
		}
		return -i;
	}

	public static float abs(float i)
	{
		if (i > 0f)
		{
			return i;
		}
		return 0f - i;
	}

	public static int min(int x, int y)
	{
		if (x < y)
		{
			return x;
		}
		return y;
	}

	public static int max(int x, int y)
	{
		if (x > y)
		{
			return x;
		}
		return y;
	}

	public static int pow(int data, int x)
	{
		int num = 1;
		for (int i = 0; i < x; i++)
		{
			num *= data;
		}
		return num;
	}
}
