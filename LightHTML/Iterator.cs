namespace LightHTML.Core;

public static class DomIterator
{
    public static IEnumerable<LightNode> DepthFirst(LightElementNode root)
    {
        foreach (var child in root.Children)
        {
            yield return child;
            if (child is LightElementNode el)
                foreach (var desc in DepthFirst(el))
                    yield return desc;
        }
    }

    public static IEnumerable<LightNode> BreadthFirst(LightElementNode root)
    {
        var queue = new Queue<LightNode>(root.Children);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            yield return node;
            if (node is LightElementNode el)
                foreach (var child in el.Children)
                    queue.Enqueue(child);
        }
    }
}
