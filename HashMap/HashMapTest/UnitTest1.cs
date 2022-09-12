using HashMap;
using System.Collections.Generic;
using Xunit;

namespace HashMapTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestAdd()
        {
            var hashMap = new HashMap<int, int>(1)
            {
                new KeyValuePair<int, int>(-1, 1),
                { 0, 2 },
                { 1, 3 }
            };

            Assert.Equal(3, hashMap.Count);
            Assert.Contains(new KeyValuePair<int, int>(-1, 1), hashMap);
            Assert.Contains(new KeyValuePair<int, int>(0, 2), hashMap);
        }

        [Fact]
        public void TestRemove()
        {
            var hashMap = new HashMap<int, int>(1)
            {
                new KeyValuePair<int, int>(-1, 1),
                { 0, 2 },
                { 1, 3 }
            };

            hashMap.Remove(-1);
            hashMap.Remove(new KeyValuePair<int, int>(0, 2));

            Assert.Single(hashMap);
        }

        [Fact]
        public void TestContains()
        {
            var hashMap = new HashMap<int, int>(1)
            {
                new KeyValuePair<int, int>(-1, 1),
                { 0, 2 },
                { 1, 3 }
            };

            Assert.Contains(new KeyValuePair<int, int>(-1, 1), hashMap);
            Assert.Contains(new KeyValuePair<int, int>(0, 2), hashMap);
            Assert.DoesNotContain(new KeyValuePair<int, int>(0, 100), hashMap);
        }

        [Fact]
        public void TestIndexing()
        {
            var hashMap = new HashMap<int, int>(1)
            {
                new KeyValuePair<int, int>(-1, 1),
                { 0, 2 },
                { 1, 3 }
            };

            Assert.Equal(1, hashMap[-1]);
            Assert.Throws<KeyNotFoundException>(() => { var a = hashMap[2]; });
            Assert.DoesNotContain(new KeyValuePair<int, int>(2, 4), hashMap);
            hashMap[2] = 4;
            Assert.Contains(new KeyValuePair<int, int>(2, 4), hashMap);
            hashMap[-1] = 10;
            Assert.Equal(10, hashMap[-1]);
        }

        [Fact]
        public void TestEnumeration()
        {
            var hashMap = new HashMap<int, int>(1);
            var list = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(-1, 1),
                new KeyValuePair<int, int>(0, 2),
                new KeyValuePair<int, int>(1, 3)
            };

            foreach(var pair in list)
            {
                hashMap.Add(pair);
            }
            
            foreach(var pair in hashMap)
            {
                bool isSuccessfulRemove = list.Remove(pair);
                Assert.True(isSuccessfulRemove);
            }

            Assert.Empty(list);
        }
    }
}
