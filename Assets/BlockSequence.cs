using System;
using System.Collections.Generic;
using System.Linq;

public class BlockSequence
{

    public IEnumerable<BlockType> CreateSequence()
    {
        Random random = new Random();
        Queue<BlockType> queue = new Queue<BlockType>();
        foreach (var block in randomSequence(random))
        {
            queue.Enqueue(block);
        }

        while (true)
        {
            if (queue.Count <= 7)
            {
                foreach (var block in randomSequence(random))
                {
                    queue.Enqueue(block);
                }
            }
            yield return queue.Dequeue();
        }
    }

    private BlockType[] randomSequence(Random random)
    {
        var linkedList = new LinkedList<BlockType>(Enum.GetValues(typeof(BlockType)).Cast<BlockType>());
        var resultList = new List<BlockType>();
        while (linkedList.Count > 0)
        {
            int index = random.Next(linkedList.Count);
            var blockType = linkedList.ElementAt(index);
            resultList.Add(blockType);
            linkedList.Remove(blockType);
        }
        return resultList.ToArray();
    }
}
