namespace Algorithms.Algorithms
{
    public class QuickFind
    {
        public int[] array;

        public QuickFind(int elementsCount)
        {
            array = new int[elementsCount];
            for (int i = 0; i < elementsCount; i++)
            {
                array[i] = i;
            }
        }

        public bool Connected(int a, int b)
        {
            return array[a] == array[b];
        }

        public void Union(int a, int b)
        {
            int aValue = array[a];
            int bValue = array[b];

            if (aValue != bValue)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == aValue) array[i] = bValue;
                }
            }
        }
    }
}
