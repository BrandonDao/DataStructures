using System;
using System.Collections.Generic;
using System.Text;

namespace UnionFind
{
    public class QuickFind<T>
    {
        private readonly int[] setIDs;
        private readonly Dictionary<T, int> itemIDMap;

        public QuickFind(IEnumerable<T> items)
        {
            int itemCount = 0;
            foreach(var item in items)
            {
                itemCount++;
            }

            setIDs = new int[itemCount];
            itemIDMap = new Dictionary<T, int>();

            int setID = 0;
            int itemID = 0;
            foreach(var item in items)
            {
                itemIDMap.Add(item, itemID);
                setIDs[itemID] = setID;
                
                setID++;
                itemID++;
            }
        }

        public int Find(T p) => setIDs[itemIDMap[p]];
        public bool Union(T p, T q)
        {
            if (AreConnected(p, q)) return false;

            int pSetID = Find(p);
            int qSetID = Find(q);

            for(int itemID = 0; itemID < setIDs.Length; itemID++)
            {
                if(setIDs[itemID] == qSetID)
                {
                    setIDs[itemID] = pSetID;
                }
            }

            return true;
        }
        public bool AreConnected(T p, T q)
        {
            return Find(p) == Find(q);
        }
    }
    public class QuickUnion<T>
    {
        private int[] parents;
        private Dictionary<T, int> map;

        public QuickUnion(IEnumerable<T> items)
        {

        }

        public int Find(T p)
        {

        }
        public bool Union(T p, T q)
        {

        }
        public bool AreConnected(T p, T q)
        {

        }
    }
}
