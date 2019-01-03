using System.Collections;

namespace Control.Web
{
    /// <summary>
    /// 自定义扩展HashTable类，实现“按什么顺序加进去就按什么顺序输出”
    /// </summary>
    public class NoSortHashTable : Hashtable
    {
        private ArrayList list = new ArrayList();

        public override void Add(object key, object value)
        {
            base.Add(key, value);
            list.Add(key);
        }

        public override void Clear()
        {
            base.Clear();
            list.Clear();
        }

        public override void Remove(object key)
        {
            base.Remove(key);
            list.Remove(key);
        }

        public override ICollection Keys
        {
            get
            {
                return list;
            }
        }
    }
}
