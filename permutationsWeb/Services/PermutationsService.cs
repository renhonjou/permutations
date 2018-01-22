using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace permutationsWeb.Services
{
    public static class PermutationsService
    {
        public static String[] GetPermutations(String request)
        {
            Char[] array = request.OrderBy(c => c).ToArray();

            var result = new List<String> {
                String.Concat(array)
            };
            while (GetNext(ref array))
            {
                result.Add(String.Concat(array));
            }

            return result.ToArray();
        }

        private static Boolean GetNext(ref Char[] array)
        {
            Int32 indexFrom = array.Length, indexTo = array.Length - 1;
            for (var i = array.Length - 1; i != 0; i--)
            {
                if (array[i - 1] < array[i])
                {
                    indexFrom = i - 1;
                    break;
                }
            }
            if (indexFrom == array.Length)
                return false;

            for (var i = array.Length - 1; i != 0; i--)
            {
                if (array[indexFrom] < array[i])
                {
                    indexTo = i;
                    break;
                }
            }
            Swap(ref array, indexFrom, indexTo);
            for (Int32 i = indexFrom + 1, j = array.Length - 1; i < j; i++, j--)
            {
                Swap(ref array, i, j);
            }
            return true;
        }

        private static void Swap(ref Char[] array, Int32 indexFrom, Int32 indexTo)
        {
            var tmp = array[indexFrom];
            array[indexFrom] = array[indexTo];
            array[indexTo] = tmp;
        }
    }
}
