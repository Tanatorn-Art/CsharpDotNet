using System;
using System.Collections.Generic;

public record Tree(char Value, Tree? Left, Tree? Right);

public static class Satellite
{
    public static Tree? TreeFromTraversals(char[] preOrder, char[] inOrder)
    {
        if (preOrder.Length == 0 || inOrder.Length == 0)
            return null;

        if (preOrder.Length != inOrder.Length)
            throw new ArgumentException("Pre-order and In-order traversals must have the same length.");

        // สร้าง dictionary สำหรับค้นหา index ของ inOrder
        var inOrderIndex = new Dictionary<char, int>();
        for (int i = 0; i < inOrder.Length; i++)
        {
            if (inOrderIndex.ContainsKey(inOrder[i]))
                throw new ArgumentException("Traversals must contain unique items.");
            inOrderIndex[inOrder[i]] = i;
        }

        int preIndex = 0;

        Tree BuildTree(int inStart, int inEnd)
        {
            if (inStart > inEnd) return null;

            // ดึง root จาก preOrder
            char rootVal = preOrder[preIndex++];

            // ✅ ตรวจสอบก่อนว่ามีค่า root นี้ใน inOrder ไหม
            if (!inOrderIndex.ContainsKey(rootVal))
                throw new ArgumentException("Pre-order and In-order traversals must contain the same elements.");

            int rootPos = inOrderIndex[rootVal];

            var root = new Tree(rootVal, null, null);

            root = root with
            {
                Left = BuildTree(inStart, rootPos - 1),
                Right = BuildTree(rootPos + 1, inEnd)
            };

            return root;
        }

        return BuildTree(0, inOrder.Length - 1);
    }
}
