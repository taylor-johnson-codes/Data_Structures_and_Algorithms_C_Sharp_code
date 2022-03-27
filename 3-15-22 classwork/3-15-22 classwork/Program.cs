﻿using System;
using System.Collections.Generic;  // for queue

namespace _3_15_22_classwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BST<int> myTree = new BST<int>();
            //myTree.Add(12);
            //myTree.Add(5);
            //myTree.Add(22);
            //myTree.Add(3);
            //myTree.Add(18);
            //myTree.Add(12);
            //// put break point here, run, in watch window put in myTree and see how tree is structured

            //Console.Write("PreOrder values: ");
            //myTree.PrintPreOrder();
            //Console.WriteLine();

            //Console.Write("InOrder values: ");
            //myTree.PrintInOrder();
            //Console.WriteLine();

            //Console.Write("PostOrder values: ");
            //myTree.PrintPostOrder();
            //Console.WriteLine();

            //Console.WriteLine($"Min value: {myTree.Min()}");
            //Console.WriteLine($"Max value: {myTree.Max()}");

            //Console.WriteLine($"Number of leaf nodes: {myTree.CountLeafNodes()}");

            BST<int> myTree = new BST<int>();
            myTree.Add(14);
            myTree.Add(9);
            myTree.Add(20);
            myTree.Add(3);
            myTree.Add(10);
            myTree.Add(19);
            myTree.Add(45);
            myTree.Add(3);
            myTree.Add(7);
            myTree.Add(15);
            myTree.Add(20);
            myTree.Add(1);
            myTree.Add(8);

            //Console.WriteLine(myTree.MyMainCount3());

            //Console.WriteLine(myTree.CountLeafNodes());

            myTree.BreadthFirstSearch();  // displays nodes level by level left to right 

            //Hanoi(1, 'A', 'B', 'C');  // 1 move
            //Hanoi(2, 'A', 'B', 'C');  // 2 moves
            // (3) would be 7 moves; (4) would be 15 moves; 2^n - 1
            //Hanoi(10, 'A', 'B', 'C');
        }

        // Tower of Hanoi (recursion example)
        static void Hanoi(int n, char start, char end, char temp)
        {
            if (n >= 1)  // so n-1 doesn't go forever in recursion
            {
                Hanoi(n - 1, start, temp, end);  // recursive call
                Console.WriteLine($"Move disk {n} from {start} onto {end}");
                Hanoi(n - 1, temp, end, start);  // recursive call
            }
            //else  // base case
            //{
            //    // do nothing
            //}
        }
    }

    class Node<T>
    {
        // DATA
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public bool WasVisited { get; set; }  // for DepthFirstSearch()


        // CONSTRUCTOR
        public Node(T newValue)
        {
            Value = newValue;
        }
    }

    class BST<T> where T : IComparable  // to compare values
    {
        // DATA
        // only stores the root and everything else is based off the root
        public Node<T> root { get; private set; }
        public int Count { get; private set; }  // number of nodes in the tree

        // METHODS
        public bool isEmpty()
        {
            // can check if the root is empty, or check if count is 0
            return root == null;  // returns true or false
        }

        // max: go the right as much as possible with while loop until leaf is hit (the end of the branch)
        public T Max()
        {
            if (isEmpty())  // if tree is empty, pointer.Right will crash the program
                throw new Exception("No max value in an empty tree.");
            Node<T> pointer = root;  // start at the root
            while (pointer.Right != null)  // move right as long as there is a next node
                pointer = pointer.Right;
            return pointer.Value;  // return the right-most value
        }

        // min: go the left as much as possible with while loop until leaf is hit (the end of the branch)
        public T Min()
        {
            if (isEmpty())  // if tree is empty, pointer.Left will crash the program
                throw new Exception("No min value in an empty tree.");
            Node<T> pointer = root;  // start at the root
            while (pointer.Left != null)  // move left as long as there is a next node
                pointer = pointer.Left;
            return pointer.Value;  // return the left-most value
        }

        // search: return null if not found, return node reference if found
        // running time worst case O(n) - unbalanced tree
        public Node<T> Search(T valueToFind)
        {
            if (isEmpty())
                return null;  // the value is not in an empty list
            Node<T> pointer = root;  // start at the root
            while (pointer != null)  // while there are still nodes to search
            {
                // compare the value of the pointer to valueToFind
                // CompareTo results: -1 if pointer.Value is smaller, 0 if there's a match, 1 if pointer.Value is larger
                if (pointer.Value.CompareTo(valueToFind) == 0)
                    return pointer;  // value was found
                else if (valueToFind.CompareTo(pointer.Value) < 0)
                    pointer = pointer.Left;  // move left
                else
                    pointer = pointer.Right;  // move right
            }
            // you get here when pointer == null
            return null;  // couldn't find value in tree
        }

        // add new value (can return the new node instead of void)
        // running time worst case O(n) - unbalanced tree
        public void Add(T newValue)
        {
            // create a new node
            Node<T> newNode = new Node<T>(newValue);
            Count++;

            // link in the new node to the tree
            if (isEmpty())
                root = newNode;
            else
            {
                // traverse to the point where to link the new node to the tree
                Node<T> pointer = root;  // start at the root
                // while (true) works also and is more efficient because the program doesn't have to compare each time
                // (just make sure there are breaks/exceptions throughout so it's not an infinite loop)
                while (pointer != null)  // while there are still nodes in the tree
                {
                    // CompareTo results: -1 if newValue is smaller, 0 if there's a match, 1 if newValue is larger
                    if (newValue.CompareTo(pointer.Value) <= 0)  // <= move left
                    {
                        // is there a left node?
                        if (pointer.Left != null)  // if there is a left node
                            pointer = pointer.Left;  // move left
                        else // there is no left node, add newNode to left
                        {
                            pointer.Left = newNode;  // link in newNode
                            break;  // newNode is added to tree so break out of the while loop
                        }
                    }
                    else // > move right
                    {
                        // is there a right node?
                        if (pointer.Right != null)  // if there is a right node
                            pointer = pointer.Right;  // move right
                        else // there is no right node, add newNode to right
                        {
                            pointer.Right = newNode;  // link in newNode
                            break;  // newNode is added to tree so break out of the while loop
                        }
                    }
                }
            }
        }

        // TRAVERSAL METHODS:

        // Breadth First Traversal for searching
        // traverse level by level (horizontally; from top level to bottom level)
        public void BreadthFirstSearch()
        {
            Queue<Node<T>> myQueue = new Queue<Node<T>>();  // a new empty queue

            // add root to the queue
            if (root != null)
                myQueue.Enqueue(root);

            while (myQueue.Count > 0)
            {
                // dequeue and display
                Node<T> pointer = myQueue.Dequeue();
                Console.Write($"{pointer.Value} ");
                // enqueue left and right
                if (pointer.Left != null)  // don't put in queue if null
                    myQueue.Enqueue(pointer.Left);
                if (pointer.Right != null) // don't put in queue if null
                    myQueue.Enqueue(pointer.Right);
            }
            Console.WriteLine();
        }

        // Depth First Traversal (vertical traversals) for searching
        // use stack for depth first search to allow us to go backwards in the tree
        public void DepthFirstSearch()
        {
            Stack<Node<T>> myStack = new Stack<Node<T>>();  // a new empty stack

            if (root != null)
            {
                myStack.Push(root);  // put root in stack
                Console.WriteLine(root.Value);
                root.WasVisited = true;  // to know that we don't need to visit it again
            }

            while (myStack.Count > 0)
            {
                Node<T> pointer = myStack.Peek();  // pointer is set to what's on top of the stack

                if (pointer.Left != null && pointer.Left.WasVisited == false)
                {
                    myStack.Push(pointer.Left);  // go deeper on left side
                    Console.WriteLine(pointer.Left.Value);
                    pointer.Left.WasVisited = true;
                }
                else if (pointer.Right != null && pointer.Right.WasVisited == false)
                {
                    myStack.Push(pointer.Right);  // go deeper on right side
                    Console.WriteLine(pointer.Right.Value);
                    pointer.Right.WasVisited = true;
                }
                else 
                    myStack.Pop();  // go back to the top
            }
            Console.WriteLine();
        }

        // Note: PreOrder, InOrder, and PostOrder traversals are considered Depth First Traversals

        // PREORDER TRAVERSAL (Node Left Right)
        // gets split into L and R method calls with recursion
        public void PreOrder(Node<T> currentNode)  
        {
            if (currentNode != null)
            {
                Console.Write($"{currentNode.Value} ");  // Node
                PreOrder(currentNode.Left);  // Left
                PreOrder(currentNode.Right);  // Right
            }
        }

        public void PrintPreOrder()
        {
            PreOrder(root);
        }

        // INORDER TRAVERSAL (Left Node Right) - prints the tree values in order from smallest to largest
        // gets split into L and R method calls with recursion
        public void InOrder(Node<T> currentNode)
        {
            if (currentNode != null)
            {
                InOrder(currentNode.Left);  // Left (recursively call the method on the left subtree)
                Console.Write($"{currentNode.Value} ");  // Node (visit the currentNode)
                InOrder(currentNode.Right);  // Right (recursively call the method on the right subtree)
            }
        }

        public void PrintInOrder()
        {
            InOrder(root);
        }

        public void InOrderCount()
        {
            int myCount = 0;  // will be passed by reference so this myCount will be updated
            InOrderCountHelper(root, ref myCount);
            Console.WriteLine(myCount);
        }

        public void InOrderCountHelper(Node<T> currentNode, ref int myCount)
        {
            if (currentNode != null)
            {
                InOrderCountHelper(currentNode.Left, ref myCount);  // Left (recursively call the method on the left subtree)
                myCount++;  // "visit" the current node
                InOrderCountHelper(currentNode.Right, ref myCount);  // Right (recursively call the method on the right subtree)
            }
        }

        // can't remember what this method is for; maybe it's part of the InOrder stuff
        public int MyCount3(Node<T> currentNode)
        {
            //MyCount3(currentNode.Left);  // propagate to the left subtree
            //MyCount3(currentNode.Right);  // propagate to the right subtree
            //return Left + Right + 1

            if (currentNode == null)
                return 0;
            return MyCount3(currentNode.Left) + MyCount3(currentNode.Right) + 1;
        }

        // goes with the method above; can't remember what that method is for
        public int MyMainCount3()
        {
            return MyCount3(root);
        }

        // POSTORDER TRAVERSAL (Left Right Node)
        // gets split into L and R method calls with recursion
        public void PostOrder(Node<T> currentNode)
        {
            if (currentNode != null)
            {
                PostOrder(currentNode.Left);  // Left
                PostOrder(currentNode.Right);  // Right
                Console.Write($"{currentNode.Value} ");  // Node
            }
        }

        public void PrintPostOrder()
        {
            PostOrder(root);
        }

        // find height of the tree; find the deepest leaf (lowest level leaf)
        // the height is the number of edges (lines between nodes) from the root down to the deepest leaf
        public int Height()
        {
            return HeightHelper(root);
        }

        public int HeightHelper(Node<T> currentNode)
        {
            if (currentNode == null)
                return -1;  // need to undo an extra 1 from the +1 below
            return Math.Max(HeightHelper(currentNode.Left), HeightHelper(currentNode.Right)) + 1;
        }

        // find how many leaf nodes the tree contains (nodes that have no right or left node children attached to them)
        public int CountLeafNodes()
        {
            return CountLeafNodesHelper(root);
        }

        public int CountLeafNodesHelper(Node<T> currentNode)
        {
            if (currentNode == null)
                return 0;  // there are no leaves in an empty tree
            else if (currentNode.Left == null && currentNode.Right == null)  // check if it is a leaf node; if it is it counts as 1
                return 1;
            else  // ask left side, ask right side, add them up; do recursively 
                return CountLeafNodesHelper(currentNode.Left) + CountLeafNodesHelper(currentNode.Right);
        }
    }
}
