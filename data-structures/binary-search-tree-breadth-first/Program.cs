using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Program
    {
        static void Main(String[] args) {
            Node root = null;
            int T = Int32.Parse(Console.ReadLine());
            while(T-- > 0) {
                int data = Int32.Parse(Console.ReadLine());
                root = insert(root,data);            
            }
            levelOrder(root);
        }

        static void levelOrder(Node root) {
  		
            var q = new Queue<Node>();
            if(root != null) {
                q.Enqueue(root);
            }
            while(q.Count != 0) {
                var current = q.Dequeue();
                Console.Write($"{current.data} ");
                
                var left = current.left;
                if(left != null) {
                    q.Enqueue(left);
                }

                var right = current.right;
                if(right != null) {
                    q.Enqueue(right);
                }
            }
        }

        static Node insert(Node root, int data) {
            if(root == null) {
                return new Node(data);
            }
            else {
                Node cur;
                if(data <= root.data) {
                    cur =  insert(root.left,data);
                    root.left = cur;
                }
                else{
                    cur = insert(root.right,data);
                    root.right = cur;
                }
                return root;
            }
        }
        
    }

    class Node {
        public Node left, right;
        public int data;
        public Node(int data) {
            this.data = data;
            left = right = null;
        }
    }
}
/* input
6
3
5
4
7
2
1
*/


/* output
3 2 5 1 4 7
*/