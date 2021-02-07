using System;
using Blumind.Core;
using Blumind.Model;
using Blumind.Model.Documents;
using Blumind.Model.MindMaps;

namespace Blumind.Controls.MapViews
{
    class AddTopicCommand : Command
    {
        Topic ParentTopic;
        Topic[] SubTopics;
        int Index;

        public AddTopicCommand(Topic parentTopic, Topic subTopic, int index)
        {
            if (parentTopic == null || subTopic == null)
            {
                throw new ArgumentNullException();
            }

            ParentTopic = parentTopic;
            SubTopics = new Topic[] { subTopic };
            Index = index;
        }

        public AddTopicCommand(Topic parentTopic, Topic[] subTopics, int index)
        {
            if (parentTopic == null || subTopics.IsNullOrEmpty())
            {
                throw new ArgumentNullException();
            }

            ParentTopic = parentTopic;
            SubTopics = subTopics;
            Index = index;
        }

        public override string Name
        {
            get { return "Add"; }
        }

        public override bool Rollback()
        {
            foreach (var st in SubTopics)
            {
                if (ParentTopic.Children.Contains(st))
                {
                    ParentTopic.Children.Remove(st);
                }
            }

            return true;
        }

        /// <summary>
        ///  关于UndoRedo
        ///  1. 第一段代码是否是禁止成环？如果是那问题解决了吗？
        ///  2. Index是什么？似乎是某种子节点的计数
        ///  3. 左右两边以此插入的话，那算子是如何存储的呢？以什么顺序存储？
        ///     3.1 子节点以数组的形式存在，XList<Topic> Children
        ///     3.2 节点首次插入使用Add，后续的插入使用Insert
        /// </summary>
        /// <returns></returns>
        public override bool Execute()
        {
            foreach (var st in SubTopics)
            {
                if (ParentTopic == st || st.IsDescent(ParentTopic))
                    return false;
            }
            
            if (Index >= 0 && Index < ParentTopic.Children.Count)
            {
                var index = Index;
                foreach (var st in SubTopics)
                {
                    ParentTopic.Children.Insert(index, st);
                    index++;
                }
            }
            else
            {
                foreach (var st in SubTopics)
                {
                    ParentTopic.Children.Add(st);
                }
            }

            return true;
        }
    }
}
