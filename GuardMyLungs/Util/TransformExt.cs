using UnityEngine;

namespace Util
{
    public static class TransformExt
    {

        public static Transform GetChild(this Transform root, string name)
        {
            Transform result;
            if (root.name == name)
            {
                return root;
            }
            for (int i = 0; i < root.childCount; i++)
            {
                result = root.GetChild(i).GetChild(name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public static Transform[] GetChildren(this Transform root)
        {
            var result = new Transform[root.childCount];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = root.GetChild(i);
            }
            return result;
        }

        public static Vector3[] GetChildrenPositions(this Transform root)
        {
            var children = root.GetChildren();
            var result = new Vector3[children.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = children[i].position;
            }
            return result;
        }

    }
}